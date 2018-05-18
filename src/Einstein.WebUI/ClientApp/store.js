import Vue from 'vue';
import Vuex from 'vuex';

import modAdmin from './components/admin/store.js';

import undoRedoHistory from './UndoRedoHistory.js';

import 'es6-promise/auto';
import axios from 'axios';
var _ = require('lodash');

Vue.use(Vuex);

const modCore = {
	namespaced: true,

	state: {
		appSettings: {
			baseUrl: 'http://localhost:8088/api'
		},
		currentUser: {
			id: -1,
			name: 'Guest',
			password: 'Welcome!',
			admin: false
		}
	},
	getters: {
		getAppSettings: state => {
			return state.appSettings;
		},
		getCurrentUser: state => {
			return state.currentUser;
		}
	},
	mutations: {
		updateCurrentUser(state, newData) {
			state.currentUser = newData;
		}
	},
	actions: {
		updateCurrentUser(context, newData) {
			context.commit('updateCurrentUser', newData);
		},

		logout(context, payload) {
			return new Promise((resolve) => {

				let clonedUser = _.cloneDeep(this.state.core.currentUser);
				clonedUser.id = -1;
				clonedUser.name = 'Guest';
				clonedUser.password = 'Welcome!'
				clonedUser.admin = false;

				this.dispatch('core/updateCurrentUser', clonedUser);

				if (typeof window !== 'undefined') {
					window.sessionStorage.setItem('sessionUser', null);
				}

				payload.router.push({ path: '/' })
			})
		},

		login(context, payload) {
			return new Promise((resolve) => {

				// Don't use the generic HTTP instance heir because you get old credentials during login...!
				axios.get(this.state.core.appSettings.baseUrl + '/users/login.json', {
					withCredentials: true,
					auth: {
						username: payload.userName,
						password: payload.userPassword
					},
					crossDomain: true
				})
					.then(response => {
						// JSON responses are automatically parsed.
						var validatedUser = response.data.data;

						this.state.core.currentUser.id = validatedUser.id;
						this.state.core.currentUser.name = payload.userName;
						this.state.core.currentUser.password = payload.userPassword;
						this.state.core.currentUser.admin = validatedUser.admin;

						window.sessionStorage.setItem('sessionUser', JSON.stringify(this.state.core.currentUser) );

						// Clear Undo / Redo history cache because we don't want to do this for login/logout!
						undoRedoHistory.clear();

						payload.router.push({ path: '/' })
					})
					.catch(e => {
						alert('Error: ' + e)
					});
			})
		}
	}
}


const undoRedoPlugin = (store) => {
	// initialize and save the starting stage
	undoRedoHistory.init(store);
	let firstState = _.cloneDeep(store.state);
	undoRedoHistory.addState(firstState);

	store.subscribe((mutation, state) => {
		// is called AFTER every mutation
		undoRedoHistory.addState(_.cloneDeep(state));
	});
}


const store = new Vuex.Store({
	modules: {
		core: modCore,
		admin: modAdmin
	},

	plugins: [undoRedoPlugin]
})

export default store;
