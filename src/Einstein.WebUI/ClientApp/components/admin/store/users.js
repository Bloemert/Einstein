import HTTP from './../../../js/axios-common';

import { User } from '../models/user';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new User()],
	selectedSeqno: 0,
	selectedUser: {},
	currentPage: 1,

	apiError: null
});

const getters = {
	getField,

	getRows: state => {
		return state.rows;
	},

	getSelectedUser: state => {
		return state.selectedUser;
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {
	//SetUserRows(state, data) {
	//	state.userRows = data;
	//},
	//SetUserCurrentSeqno(state, data) {
	//	state.userCurrentSeqno = data;
	//	state.selectedUser = state.userRows[state.userCurrentSeqno];
	//},
	//SetUserCurrentSeqno(state, data) {
	//	state.userCurrentSeqno = data;
	//},
	SetUsers(state, payload) {
		state.rows = payload.rows;
		state.selectedSeqno = payload.selectedSeqno;
		state.selectedUser = state.rows[state.selectedSeqno];
		state.userCurrentPage = payload.currentPage;
	},
	//updateField,
	UpdateField(state, payload) {

		if (payload.path == 'selectedSeqno') {
			state.selectedUser = state.rows[payload.value];
		}

		// Track changes for transaction(s)...

		return updateField(state, payload);
	}
	/*
	let idx = state.users.indexOf(payload.updatedUser);
	if (idx > -1 && idx < state.users.length) {
		_assign(state.users[idx], {
			[field]: value
		});
	}
	*/


	//AddUser(state, newUser) {
	//	state.users.push(newUser);
	//},

	//removeUser(state, oldUser) {
	//	//state.usersData.data.splice(state.usersData.data.indexOf(oldUser), 1)
	//	let idx = state.usersData.data.indexOf(oldUser);
	//	if (idx > -1 && idx < state.usersData.data.length) {
	//		state.usersData.data[idx] = _assign({ dirty: 'R' }, oldUser);
	//	}
	//}

};


const actions = {

	//updateUser(context, payload) {
	//	context.commit('updateUser', payload);
	//},

	//addUser(context) {
	//	return HTTP.get('/users/new.json')
	//			.then(response => {
	//				// JSON responses are automatically parsed.
	//				context.commit('addUser', response.data.data);
	//			})
	//			.catch(e => {
	//				this.errors.push(e)
	//			});
	//},

	//removeUser(context, oldUser) {
	//	context.commit('removeUser', oldUser);
	//},

	loadFromDB(context) {
		return HTTP.get('/users/list.json')
			.then(response => {

				let objs = {};
				let data = response.data.data;
				var seqno = 0;

				_forEach(data, function (row) {
					_assign(row, {
						// Add unique seqno to array for UI side uniqueness!
						['seqno']: seqno++,
						// Add row status to array for tracking changes and possiblity to save deltas!
						['status']: { action: 'Loaded', result: 'OK' }
					});
				});
				context.commit('SetUsers', {
					rows: data,
					selectedSeqno: 0,
					currentPage: 1
				});
			})
			.catch(e => {
				alert('Error: ' + e)
			});
	},

	/*
	 * @Todo This should/must be done server(REST) side but for now...
	 */
	isUniqueByField(context, { fieldName, fieldValue }) {

		let otherUser = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherUser == undefined;
	}

	//saveUserChangesToDB(context) {

	//	// @Todo 1: Create batch list to send 1 : 1 to REST API!

	//	_forEach(context.getters.usersData.data, function (item) {

	//		// Note: empty addUsers ( action 'A' ) not send!
	//		if (item.dirty) {
	//			if (item.dirty == 'U') {
	//				HTTP.put('/users/' + item.id + '/save.json', { data: item })
	//					.then(() => {
	//						context.dispatch('loadUsersFromDB');
	//					})
	//					.catch(e => {
	//						alert('Error: ' + e)
	//					});
	//			}
	//			else if (item.dirty == 'R') {
	//				HTTP.delete('/users/' + item.id + '/delete.json')
	//					.then(() => {
	//						context.dispatch('loadUsersFromDB');
	//					});
	//			}
	//		}
	//	});
	//}
};


const modUsers = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
}

export default modUsers;

export const { mapActions: mapUsersActions } = createNamespacedHelpers('admin/users');


export const { mapFields: mapUsersFields } = createHelpers({
	getterType: 'admin/users/getField',
	mutationType: 'admin/users/UpdateField'
});

export const { mapMultiRowFields: mapUsersMultiRowFields } = createHelpers({
	getterType: 'admin/users/getField',
	mutationType: 'admin/users/UpdateField'
});

