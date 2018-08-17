import HTTP from './../../../js/axios-common';

import { Spot } from '../models/spot';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new Spot()],
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

	getSelectedSpot: state => {
		return state.rows[selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetSpots(state, payload) {
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

	AddSpot(state, newSpot) {
		return new Promise(resolve => {
			newSpot.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newSpot.seqno;
			newSpot.status.added = true;
			newSpot.id = 0;
			state.rows.push(newSpot);
		});
	},

	RemoveSpot(state, oldSpot) {
		var rowRefObj = state.rows[oldSpot.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateSpot(context, payload) {
	//	context.commit('updateSpot', payload);
	//},

	addSpot(context) {
		var newSpot = new Spot();
		return context.commit('AddSpot', newSpot);
	},

	removeSpot(context, oldSpot) {
		context.commit('RemoveSpot', oldSpot);
	},

	loadFromDB(context) {
		return HTTP.get('/spots/list.json')
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
				context.commit('SetSpots', {
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

		let otherSpot = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherSpot == undefined;
	},

	saveSpotChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/spots/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/spots/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/spots/' + item.id + '/save.json', { data: item })
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


const modSpots = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
}

export default modSpots;

export const { mapActions: mapSpotsActions } = createNamespacedHelpers('spots');


export const { mapFields: mapSpotsFields } = createHelpers({
	getterType: 'spots/getField',
	mutationType: 'spots/UpdateField'
});

export const { mapMultiRowFields: mapSpotsMultiRowFields } = createHelpers({
	getterType: 'spots/GetField',
	mutationType: 'spots/UpdateField'
});

