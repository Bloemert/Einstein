import HTTP from '../../../../js/axios-common';

import { EmployeeSkill } from '../models/EmployeeSkill';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new EmployeeSkill()],
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

	getSelectedEmployeeSkill: state => {
		return state.rows[state.selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetEmployeeSkills(state, payload) {
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

	AddEmployeeSkill(state, newEmployeeSkill) {
		return new Promise(resolve => {
			newEmployeeSkill.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newEmployeeSkill.seqno;
			newEmployeeSkill.status.added = true;
			newEmployeeSkill.id = 0;
			state.rows.push(newEmployeeSkill);
		});
	},

	RemoveEmployeeSkill(state, oldEmployeeSkill) {
		var rowRefObj = state.rows[oldEmployeeSkill.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateEmployeeSkill(context, payload) {
	//	context.commit('updateEmployeeSkill', payload);
	//},

	addEmployeeSkill(context) {
		var newEmployeeSkill = new EmployeeSkill();
		return context.commit('AddEmployeeSkill', newEmployeeSkill);
	},

	removeEmployeeSkill(context, oldEmployeeSkill) {
		context.commit('RemoveEmployeeSkill', oldEmployeeSkill);
	},

	loadFromDB(context) {
		return HTTP.get('/employeeSkills/list.json')
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
				context.commit('SetEmployeeSkills', {
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

		let otherEmployeeSkill = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherEmployeeSkill == undefined;
	},

	saveEmployeeSkillChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/employeeSkills/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/employeeSkills/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/employeeSkills/' + item.id + '/save.json', { data: item })
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


const modEmployeeSkills = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
}

export default modEmployeeSkills;

export const { mapActions: mapEmployeeSkillsActions } = createNamespacedHelpers('employeeSkills');


export const { mapFields: mapEmployeeSkillsFields } = createHelpers({
	getterType: 'employeeSkills/getField',
	mutationType: 'employeeSkills/UpdateField'
});

export const { mapMultiRowFields: mapEmployeeSkillsMultiRowFields } = createHelpers({
	getterType: 'employeeSkills/GetField',
	mutationType: 'employeeSkills/UpdateField'
});

