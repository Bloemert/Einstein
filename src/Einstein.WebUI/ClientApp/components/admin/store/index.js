import HTTP from './../../../js/axios-common';

import modUsers from './users';
import modLogs from './logs';

import { createNamespacedHelpers } from 'vuex';

import { createHelpers, getField, updateField } from 'vuex-map-fields';


const state = () => ({

	apiError: null
});

const getters = {
	getField
};


const mutations = {
	updateField
};


const actions = {

};

const modules = {
	users: modUsers,
	logs: modLogs
};

const modAdmin = {
	namespaced: true,

	state: state,

	getters: getters,
	mutations: mutations,
	actions: actions,

	modules: modules
};

export default modAdmin;

//export const { mapGetters: mapAdminGetters, mapState: mapAdminState, mapActions: mapAdminActions } = createNamespacedHelpers('admin');


export const { mapFields: mapAdminFields } = createHelpers({
	getterType: 'admin/getField',
	mutationType: 'admin/updateField'
});

export const { mapMultiRowFields: mapAdminMultiRowFields } = createHelpers({
	getterType: 'admin/getField',
	mutationType: 'admin/updateField'
});

//export const { mapFields: mapUserFields } = createHelpers({
//	getterType: 'admin/userRows/getField',
//	mutationType: 'admin/userRows/updateField'
//});