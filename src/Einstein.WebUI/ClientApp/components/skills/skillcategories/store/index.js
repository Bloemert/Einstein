import HTTP from '../../../../js/axios-common';

import { SkillCategory } from '../models/SkillCategory';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new SkillCategory()],
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

	getSelectedSkillCategory: state => {
		return state.rows[state.selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetSkillCategories(state, payload) {
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

	AddSkillCategory(state, newSkillCategory) {
		return new Promise(resolve => {
			newSkillCategory.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newSkillCategory.seqno;
			newSkillCategory.status.added = true;
			newSkillCategory.id = 0;
			state.rows.push(newSkillCategory);
		});
	},

	RemoveSkillCategory(state, oldSkillCategory) {
		var rowRefObj = state.rows[oldSkillCategory.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	addSkillCategory(context) {
		var newSkillCategory = new SkillCategory();
		return context.commit('AddSkillCategory', newSkillCategory);
	},

	removeSkillCategory(context, oldSkillCategory) {
		context.commit('RemoveSkillCategory', oldSkillCategory);
	},

	loadFromDB(context) {
		return HTTP.get('/skills/categories/list.json')
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
				context.commit('SetSkillCategories', {
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

		let otherSkillCategory = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherSkillCategory == undefined;
	},

	saveSkillCategoryChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/skills/categories/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/skills/categories/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/skills/categories/' + item.id + '/save.json', { data: item })
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


const modSkillCategories = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
};

export default modSkillCategories;

export const { mapActions: mapSkillCategoryActions } = createNamespacedHelpers('skillcategories');


export const { mapFields: mapSkillCategoryFields } = createHelpers({
	getterType: 'skillcategories/getField',
	mutationType: 'skillcategories/UpdateField'
});

export const { mapMultiRowFields: mapSkillCategoryMultiRowFields } = createHelpers({
	getterType: 'skillcategories/GetField',
	mutationType: 'skillcategories/UpdateField'
});

