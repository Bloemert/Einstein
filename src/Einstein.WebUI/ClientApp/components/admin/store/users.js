﻿import HTTP from './../../../js/axios-common';

import { User } from '../models/user';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new User()],
	selectedSeqno: 0,
	currentPage: 1,

	apiError: null
});

const getters = {
	getField,
	GetField: state => {
		return getField(state);
	},

	getRows: state => {
		return state.rows;
	},

	getSelectedUser: state => {
		return state.rows[selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetUsers(state, payload) {
		state.rows = payload.rows;
		state.selectedSeqno = payload.selectedSeqno;
		state.currentPage = payload.currentPage;
	},

	UpdateField(state, payload) {

		// Track changes for transaction(s)...
		if (payload.path && payload.path.startsWith('rows[')) {

			var rowRefStr = payload.path.substring(0, payload.path.lastIndexOf('.'));
			var rowRefObj = _get(state, rowRefStr);
			if (rowRefObj.status.valid) {
				rowRefObj.status.modified = true;
			}
		}

		return updateField(state, payload);
	},

	AddUser(state, newUser) {
		return new Promise(resolve => {
			newUser.seqno = _last(state.rows).seqno + 1;
			state.selectedSeqno = newUser.seqno;
			newUser.status.added = true;
			newUser.id = 0;
			state.rows.push(newUser);
		});
	},

	RemoveUser(state, oldUser) {
		var rowRefObj = state.rows[oldUser.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateUser(context, payload) {
	//	context.commit('updateUser', payload);
	//},

	addUser(context) {
		var newUser = new User();
		return context.commit('AddUser', newUser);
	},

	removeUser(context, oldUser) {
		//return HTTP.delete('/users/' + oldUser.id + '/delete.json')
		//	.then(response => {
		//		// JSON responses are automatically parsed.
		//		context.commit('RemoveUser', oldUser);
		//	})
		//	.catch(e => {
		//		alert(e);
		//	});

		context.commit('RemoveUser', oldUser);
	},

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
						['status']: { valid: true, added: false, modified: false, removed: false, loaded: true }
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
	getterType: 'admin/users/GetField',
	mutationType: 'admin/users/UpdateField'
});

