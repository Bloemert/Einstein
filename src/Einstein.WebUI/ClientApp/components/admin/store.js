import HTTP from './../../js/axios-common';

const modAdmin = {
	namespaced: true,

	state: {
		usersData: {
			data: []
		}
	},

	getters: {
		usersData: state => {
			return state.usersData;
		}
	},

	mutations: {
		usersData(state, newData) {
			state.usersData = newData;
		},

		updateUser(state, payload) {
			let idx = state.usersData.data.indexOf(payload.updatedUser);
			if (idx > -1 && idx < state.usersData.data.length) {
				state.usersData.data[idx] = _.assign({dirty: 'U'}, payload.updatedUser);
			}
		},

		addUser(state, newUser) {
			state.usersData.data.push(newUser);
		},

		removeUser(state, oldUser) {
			//state.usersData.data.splice(state.usersData.data.indexOf(oldUser), 1)
			let idx = state.usersData.data.indexOf(oldUser);
			if (idx > -1 && idx < state.usersData.data.length) {
				state.usersData.data[idx] = _.assign({ dirty: 'R' }, oldUser);
			}
		}

	},
	actions: {
		usersData(context, newData) {
			context.commit('usersData', newData);
		},

		updateUser(context, payload) {
			context.commit('updateUser', payload);
		},

		addUser(context) {
			return HTTP.get('/users/new.json')
					.then(response => {
						// JSON responses are automatically parsed.
						context.commit('addUser', response.data.data);
					})
					.catch(e => {
						this.errors.push(e)
					});
		},

		removeUser(context, oldUser) {
			context.commit('removeUser', oldUser);
		},

		loadUsersFromDB(context) {
			return HTTP.get('/users/list.json')
					.then(response => {
						// JSON responses are automatically parsed.
						context.commit('usersData', response.data);
					})
					.catch(e => {
						alert('Error: ' + e)
					});
		},

		saveUserChangesToDB(context) {

			// @Todo 1: Create batch list to send 1 : 1 to REST API!

			_.forEach(context.getters.usersData.data, function (item) {

				// Note: empty addUsers ( action 'A' ) not send!
				if (item.dirty) {
					if (item.dirty == 'U') {
						HTTP.put('/users/' + item.id + '/save.json', { data: item })
							.then(() => {
								context.dispatch('loadUsersFromDB');
							})
							.catch(e => {
								alert('Error: ' + e)
							});
					}
					else if (item.dirty == 'R') {
						HTTP.delete('/users/' + item.id + '/delete.json')
							.then(() => {
								context.dispatch('loadUsersFromDB');
							});
					}
				}
			});
		}
	}
}

export default modAdmin;
