import HTTP from './../../../js/axios-common';

import { Sector } from '../models/sector';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new Sector()],
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

	getSelectedSector: state => {
		return state.rows[selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetSectors(state, payload) {
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

	AddSector(state, newSector) {
		return new Promise(resolve => {
			newSector.seqno = (state.rows.length ? _last(state.rows).seqno : 0) + 1;
			state.selectedSeqno = newSector.seqno;
			newSector.status.added = true;
			newSector.id = 0;
			state.rows.push(newSector);
		});
	},

	RemoveSector(state, oldSector) {
		var rowRefObj = state.rows[oldSector.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateSector(context, payload) {
	//	context.commit('updateSector', payload);
	//},

	addSector(context) {
		var newSector = new Sector();
		return context.commit('AddSector', newSector);
	},

	removeSector(context, oldSector) {
		context.commit('RemoveSector', oldSector);
	},

	loadFromDB(context) {
		return HTTP.get('/sectors/list.json')
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
				context.commit('SetSectors', {
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

		let otherSector = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherSector == undefined;
	},

	saveSectorChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/sectors/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/sectors/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
			.then(response => {
				context.dispatch("loadFromDB");
			});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/sectors/' + item.id + '/save.json', { data: item })
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


const modSectors = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
}

export default modSectors;

export const { mapActions: mapSectorsActions } = createNamespacedHelpers('sectors');


export const { mapFields: mapSectorsFields } = createHelpers({
	getterType: 'sectors/getField',
	mutationType: 'sectors/UpdateField'
});

export const { mapMultiRowFields: mapSectorsMultiRowFields } = createHelpers({
	getterType: 'sectors/GetField',
	mutationType: 'sectors/UpdateField'
});

