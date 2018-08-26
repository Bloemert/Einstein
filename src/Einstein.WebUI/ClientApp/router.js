import VueRouter from 'vue-router';

import Home from './components/Home.vue';
import Login from './components/auth/Login.vue';
import Logout from './components/auth/Logout.vue';

import SkillsMenu from './components/skills/Index.vue';
import Skills from './components/skills/skills/Index.vue';
import SkillTypes from './components/skills/skilltypes/Index.vue';
import SkillCategories from './components/skills/skillcategories/Index.vue';
import Spots from './components/skills/spots/Index.vue';
import Sectors from './components/skills/sectors/Index.vue';
import Rings from './components/skills/rings/Index.vue';

import WorkMenu from './components/work/Index.vue';
import Employees from './components/work/employees/Index.vue';
import EmployeeSkills from './components/work/employeeskills/Index.vue';

import AdminMenu from './components/admin/Index.vue';
import User from './components/admin/UsersTable.vue';
import Logs from './components/admin/Logs.vue';

import About from './components/About.vue';
import PNF from './components/PageNotFound.vue';

Vue.use(VueRouter);

const router = new VueRouter({
	mode: 'history',
	linkActiveClass: 'is-active',
	scrollBehavior: (to, from, savePosition) => ({ y: 0 }),

	routes: [
		{ path: '/', component: Home },
		{ path: '/login', component: Login },
		{ path: '/logout', component: Logout },

		{
			path: '/skills', name: 'SkillsMenu', component: SkillsMenu,
			redirect: {
				name: 'Skills'
			},
			children: [
				{ path: 'skills', name: 'Skills', components: { skillsView: Skills } },
				{ path: 'types', name: 'Types', components: { skillsView: SkillTypes } },
				{ path: 'categories', name: 'Categories', components: { skillsView: SkillCategories } },
				{ path: 'rings', name: 'Rings', components: { skillsView: Rings } },
				{ path: 'sectors', name: 'Sectors', components: { skillsView: Sectors } },
				{ path: 'spots', name: 'Spots', components: { skillsView: Spots } },
			]
		},

		{
			path: '/work', component: WorkMenu,
			redirect: {
				name: 'Employees'
			},
			children: [
				{ path: 'employees', name: 'Employees', components: { workView: Employees } },
				{ path: 'skills', name: 'EmployeeSkills', components: { workView: EmployeeSkills } }
			]
		},

		{
			path: '/admin', component: AdminMenu,
			redirect: {
				name: 'Users'
			},
			children: [
				{ path: 'users', name: 'Users', components: { adminView: User } },
				{ path: 'logs', name: 'Logs', components: { adminView: Logs } }
			]
		},

		{ path: '/about', component: About },

		{ path: '*', component: PNF }
	]
})

export default router;
