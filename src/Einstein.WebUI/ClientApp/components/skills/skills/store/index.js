import HTTP from '../../../../js/axios-common';

import { Skill } from '../models/Skill';

import { createNamespacedHelpers } from 'vuex';

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new Skill()],
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

	getSelectedSkill: state => {
		return state.rows[state.selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetSkills(state, payload) {
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

	AddSkill(state, newSkill) {
		return new Promise(resolve => {
			newSkill.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newSkill.seqno;
			newSkill.status.added = true;
			newSkill.id = 0;
			state.rows.push(newSkill);
		});
	},

	RemoveSkill(state, oldSkill) {
		var rowRefObj = state.rows[oldSkill.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	addSkill(context) {
		var newSkill = new Skill();
		return context.commit('AddSkill', newSkill);
	},

	removeSkill(context, oldSkill) {
		context.commit('RemoveSkill', oldSkill);
	},

	loadFromDB(context) {
		return HTTP.get('/skills/skills/list.json')
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
				context.commit('SetSkills', {
					rows: data,
					selectedSeqno: 0,
					currentPage: 1
				});
			})
			.catch(e => {
				alert('Error: ' + e)
			});
	},

	saveSkillChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/skills/skills/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/skills/skills/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/skills/skills/' + item.id + '/save.json', { data: item })
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


const modSkills = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
};

export default modSkills;

export const { mapActions: mapSkillsActions } = createNamespacedHelpers('skills');


export const { mapFields: mapSkillsFields } = createHelpers({
	getterType: 'skills/getField',
	mutationType: 'skills/UpdateField'
});

export const { mapMultiRowFields: mapSkillsMultiRowFields } = createHelpers({
	getterType: 'skills/GetField',
	mutationType: 'skills/UpdateField'
});

