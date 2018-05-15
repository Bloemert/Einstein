import Vue from 'vue';
import Vuex from 'vuex';
import 'es6-promise/auto';

Vue.use(Vuex);


const modCore = {
	namespaced: true,

	state: {
		currentUser: {
			id: -1,
			name: 'UNKNOWN',
			password: 'secret!',
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
		setCurrentUser(state, newData) {
			state.currentUser = newData;
		}
	},
	actions: {
		setCurrentUser(context, newData) {
			context.commit('setCurrentUser', newData);
		}
	}
}


const store = new Vuex.Store({
	modules: {
		core: modCore
	}
})

export default store;
