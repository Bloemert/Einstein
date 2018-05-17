import Vue from 'vue';
import Vuex from 'vuex';
import 'es6-promise/auto';
import { HTTP } from './js/axios-common';
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
		getCurrentUser: state => {
			return state.currentUser;
		}
	},
	mutations: {
		setCurrentUser(state, newData) {
			state.currentUser = newData;
		}
	},
	actions: {
		setCurrentUser(context, newData) {
			context.commit('setCurrentUser', newData);
		},

		logout(context, payload) {
			return new Promise((resolve) => {

				let clonedUser = _.cloneDeep(this.state.core.currentUser);
				clonedUser.id = -1;
				clonedUser.name = 'Guest';
				clonedUser.password = 'Welcome!'
				clonedUser.admin = false;

				this.dispatch('core/setCurrentUser', clonedUser);

				if (typeof window !== 'undefined') {
					window.sessionStorage.setItem('sessionUser', null);
				}

				payload.router.push({ path: '/' })
			})
		},
		login(context, payload) {
			return new Promise((resolve) => {

				HTTP.get(this.state.core.appSettings.baseUrl + '/users/login.json', {
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

						payload.router.push({ path: '/' })
					})
					.catch(e => {
						alert('Error: ' + e)
					});
			})
		}
	}
}


const store = new Vuex.Store({
	modules: {
		core: modCore
	}
})

export default store;
