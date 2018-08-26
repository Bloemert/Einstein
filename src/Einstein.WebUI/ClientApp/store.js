import Vuex from 'vuex';

import modAdmin from './components/admin/store';

import modSectors from './components/skills/sectors/store';
import modRings from './components/skills/rings/store';
import modSpots from './components/skills/spots/store';

import modSkills from './components/skills/skills/store';
import modSkillTypes from './components/skills/skilltypes/store';
import modSkillCategories from './components/skills/skillcategories/store';

import modEmployees from './components/work/employees/store';
import modEmployeeSkills from './components/work/employeeskills/store';



import axios from 'axios';

Vue.use(Vuex);

const modCore = {
	namespaced: true,

	state: {
		appSettings: {
			baseUrl: 'http://localhost:8088/api',
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

				let clonedUser = _cloneDeep(context.getters.getCurrentUser);
				clonedUser.id = -1;
				clonedUser.name = 'Guest';
				clonedUser.password = 'Welcome!'
				clonedUser.admin = false;

				context.dispatch('updateCurrentUser', clonedUser);

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
						var authenticatedUser = response.data.data;
						let clonedUser = _cloneDeep(context.getters.getCurrentUser);

						clonedUser.id = authenticatedUser.id;
						clonedUser.name = payload.userName;
						clonedUser.password = payload.userPassword;
						clonedUser.admin = authenticatedUser.admin;

						context.dispatch('updateCurrentUser', clonedUser);

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
	strict: process.env.NODE_ENV !== 'production',

	modules: {
		core: modCore,

		admin: modAdmin,

		spots: modSpots,
		sectors: modSectors,
		rings: modRings,
		skills: modSkills,
		skilltypes: modSkillTypes,
		skillcategories: modSkillCategories,

		employees: modEmployees,
		employeeskills: modEmployeeSkills
	}
})

export default store;
