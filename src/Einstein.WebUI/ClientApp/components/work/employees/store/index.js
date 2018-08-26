import HTTP from '../../../../js/axios-common';

import { Employee } from '../models/Employee';

import { createNamespacedHelpers } from 'vuex';

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new Employee()],
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

	getSelectedEmployee: state => {
		return state.rows[state.selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetEmployees(state, payload) {
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

	AddEmployee(state, newEmployee) {
		return new Promise(resolve => {
			newEmployee.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newEmployee.seqno;
			newEmployee.status.added = true;
			newEmployee.id = 0;
			state.rows.push(newEmployee);
		});
	},

	RemoveEmployee(state, oldEmployee) {
		var rowRefObj = state.rows[oldEmployee.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	addEmployee(context) {
		var newEmployee = new Employee();
		return context.commit('AddEmployee', newEmployee);
	},

	removeEmployee(context, oldEmployee) {
		context.commit('RemoveEmployee', oldEmployee);
	},

	loadFromDB(context) {
		return HTTP.get('/work/employees/list.json')
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
				context.commit('SetEmployees', {
					rows: data,
					selectedSeqno: 0,
					currentPage: 1
				});
			})
			.catch(e => {
				alert(e);
			});
	},

	saveEmployeeChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/work/employees/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/work/employees/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/work/employees/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
		})
	}
};


const modEmployees = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
};

export default modEmployees;

export const { mapActions: mapEmployeesActions } = createNamespacedHelpers('employees');


export const { mapFields: mapEmployeesFields } = createHelpers({
	getterType: 'employees/getField',
	mutationType: 'employees/UpdateField'
});

export const { mapMultiRowFields: mapEmployeesMultiRowFields } = createHelpers({
	getterType: 'employees/GetField',
	mutationType: 'employees/UpdateField'
});

