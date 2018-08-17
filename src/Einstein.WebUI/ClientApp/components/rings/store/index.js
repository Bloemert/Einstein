import HTTP from './../../../js/axios-common';

import { Ring } from '../models/ring';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new Ring()],
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

	getSelectedRing: state => {
		return state.rows[selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetRings(state, payload) {
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

	AddRing(state, newRing) {
		return new Promise(resolve => {
			newRing.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newRing.seqno;
			newRing.status.added = true;
			newRing.id = 0;
			state.rows.push(newRing);
		});
	},

	RemoveRing(state, oldRing) {
		var rowRefObj = state.rows[oldRing.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateRing(context, payload) {
	//	context.commit('updateRing', payload);
	//},

	addRing(context) {
		var newRing = new Ring();
		return context.commit('AddRing', newRing);
	},

	removeRing(context, oldRing) {
		context.commit('RemoveRing', oldRing);
	},

	loadFromDB(context) {
		return HTTP.get('/rings/list.json')
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
				context.commit('SetRings', {
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

		let otherRing = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherRing == undefined;
	},

	saveRingChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/rings/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/rings/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/rings/' + item.id + '/save.json', { data: item })
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


const modRings = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
}

export default modRings;

export const { mapActions: mapRingsActions } = createNamespacedHelpers('rings');


export const { mapFields: mapRingsFields } = createHelpers({
	getterType: 'rings/getField',
	mutationType: 'rings/UpdateField'
});

export const { mapMultiRowFields: mapRingsMultiRowFields } = createHelpers({
	getterType: 'rings/GetField',
	mutationType: 'rings/UpdateField'
});

