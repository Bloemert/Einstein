﻿import Vue from 'vue';
import VueRouter from 'vue-router';

import Login from './components/auth/Login.vue'
import Logout from './components/auth/Logout.vue'
import Home from './components/Home.vue'

import Admin from './components/Admin.vue'
import User from './components/admin/UsersTable.vue'
import Logs from './components/admin/Logs.vue'

import PNF from './components/PageNotFound.vue'

Vue.use(VueRouter);

const router = new VueRouter({
	mode: 'history',
	linkActiveClass: 'is-active',
	scrollBehavior: (to, from, savePosition) => ({ y: 0 }),
	routes: [
		{ path: '/login', component: Login },
		{ path: '/logout', component: Logout },
		{ path: '/', component: Home },

		{
			path: '/admin', component: Admin,
			children: [
				{ path: 'users', components: { adminView: User } },
				{ path: 'logs', components: { adminView: Logs } }
			]
		},

		{ path: '*', component: PNF }
	]
})

export default router;
