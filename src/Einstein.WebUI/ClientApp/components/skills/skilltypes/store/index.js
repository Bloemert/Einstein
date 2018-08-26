import HTTP from '../../../../js/axios-common';

import { SkillType } from '../models/SkillType';

import { createNamespacedHelpers } from 'vuex';

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new SkillType()],
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

	getSelectedSkillType: state => {
		return state.rows[state.selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetSkillTypes(state, payload) {
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

	AddSkillType(state, newSkillType) {
		return new Promise(resolve => {
			newSkillType.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newSkillType.seqno;
			newSkillType.status.added = true;
			newSkillType.id = 0;
			state.rows.push(newSkillType);
		});
	},

	RemoveSkillType(state, oldSkillType) {
		var rowRefObj = state.rows[oldSkillType.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateSkillType(context, payload) {
	//	context.commit('updateSkillType', payload);
	//},

	addSkillType(context) {
		var newSkillType = new SkillType();
		return context.commit('AddSkillType', newSkillType);
	},

	removeSkillType(context, oldSkillType) {
		context.commit('RemoveSkillType', oldSkillType);
	},

	loadFromDB(context) {
		return HTTP.get('/skills/types/list.json')
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
				context.commit('SetSkillTypes', {
					rows: data,
					selectedSeqno: 0,
					currentPage: 1
				});
			})
			.catch(e => {
				alert('Error: ' + e);
			});
	},

	/*
	 * @Todo This should/must be done server(REST) side but for now...
	 */
	isUniqueByField(context, { fieldName, fieldValue }) {

		let otherSkillType = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue; });

		return otherSkillType == undefined;
	},

	saveSkillTypeChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/skills/types/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/skills/types/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/skills/types/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
		});
	}
};


const modSkillTypes = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
};

export default modSkillTypes;

export const { mapActions: mapSkillTypeActions } = createNamespacedHelpers('skilltypes');


export const { mapFields: mapSkillTypeFields } = createHelpers({
	getterType: 'skilltypes/getField',
	mutationType: 'skilltypes/UpdateField'
});

export const { mapMultiRowFields: mapSkillTypeMultiRowFields } = createHelpers({
	getterType: 'skilltypes/GetField',
	mutationType: 'skilltypes/UpdateField'
});

