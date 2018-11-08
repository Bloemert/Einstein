import HTTP from './../../../js/axios-common';

import { Log } from '../models/log';

import { createNamespacedHelpers } from 'vuex'

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({
	rows: [new Log()],
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

	getSelectedLog: state => {
		return state.rows[state.selectedSeqno];
	},

	getSelectedSeqno: state => {
		return state.selectedSeqno;
	}
};


const mutations = {

	SetLogs(state, payload) {
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

	AddLog(state, newLog) {
		return new Promise(resolve => {
			newLog.seqno = _last(state.rows).seqno + 1;
			state.selectedSeqno = newLog.seqno;
			newLog.status.added = true;
			newLog.id = 0;
			state.rows.push(newLog);
		});
	},

	RemoveLog(state, oldLog) {
		var rowRefObj = state.rows[oldLog.seqno];
		rowRefObj.status.removed = true;
	}
};


const actions = {

	//updateLog(context, payload) {
	//	context.commit('updateLog', payload);
	//},

	addLog(context) {
		var newLog = new Log();
		return context.commit('AddLog', newLog);
	},

	removeLog(context, oldLog) {
		context.commit('RemoveLog', oldLog);
	},

	loadFromDB(context) {
		return HTTP.get('/logs/list.json')
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
				context.commit('SetLogs', {
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

		let otherLog = _find(context.getters.getRows, function (o) { return o.seqno != context.getters.getSelectedSeqno && o.login == fieldValue });

		return otherLog == undefined;
	},

	saveLogChangesToDB(context) {

		_forEach(context.getters.getRows, function (item) {
			if (item.status.removed && !item.status.added) {
				// REMOVE
				HTTP.delete('/logs/' + item.id + '/delete.json')
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.added && !item.status.removed) {
				// ADD
				HTTP.put('/logs/' + item.id + '/save.json', { data: item })
					.catch(e => {
						alert(e);
					})
					.then(response => {
						context.dispatch("loadFromDB");
					});
			}
			else if (item.status.modified && !item.status.removed && !item.status.added) {
				// UPDATE
				HTTP.put('/logs/' + item.id + '/save.json', { data: item })
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


const modLogs = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions
};

export default modLogs;

export const { mapActions: mapLogsActions } = createNamespacedHelpers('admin/logs');


export const { mapFields: mapLogsFields } = createHelpers({
	getterType: 'admin/logs/getField',
	mutationType: 'admin/logs/UpdateField'
});

export const { mapMultiRowFields: mapLogsMultiRowFields } = createHelpers({
	getterType: 'admin/logs/GetField',
	mutationType: 'admin/logs/UpdateField'
});

