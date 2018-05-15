import Vue from 'vue';
import Vuex from 'vuex';
import 'es6-promise/auto';

Vue.use(Vuex);


const modCore = {
	state: {
		currentUser: {
			id: -1,
			name: 'UNKNOWN',
			token: null,
			admin: false
		} 
	},
	getters: {
		getCurrentUser: state => {
			return state.currentUser;
		}
	},
	mutations: {
		setCurrentUserID(state, id) {
			state.currentUser.id = id;
		},
		setCurrentUserName(state, name) {
			state.currentUser.name = name;
		},
		setCurrentUserToken(state, token) {
			state.currentUser.token = token;
		},
		setCurrentUserIsAdmin(state, admin) {
			state.currentUser.admin = admin;
		}
	},
	actions: {
		setCurrentUserID(context, id) {
			context.commit('setCurrentUserID', { id: id });
		},
		setCurrentUserName(context, name) {
			context.commit('setCurrentUserName', { name: name });
		},
		setCurrentUserToken(context, token) {
			context.commit('setCurrentUserToken', { token: token });
		},
		setCurrentUserIsAdmin(context, admin) {
			context.commit('setCurrentUserIsAdmin', { admin: admin });
		}
	}
}


const store = new Vuex.Store({
	modules: {
		core: modCore
	}
})

export default store;
