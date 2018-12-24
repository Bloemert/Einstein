

import { RouterOptions, RouteConfig } from 'vue-router';

import Layout from 'layouts/MainLayout.vue';
import IndexPage from 'pages/Index.vue';
import LoginPage from 'pages/user/Login.vue';
import LogoutPage from 'pages/user/Logout.vue';
import ErrorPage from 'pages/Error404.vue';


export const routes = <Array<RouteConfig>>[
  {
    name: 'Layout', path: '/', component: Layout, props: true,
    children: [
      { name: 'IndexPage', path: '', component: IndexPage, props: true },
      { name: 'LoginPage', path: 'user/login', component: LoginPage, props: true },
      { name: 'LogoutPage', path: 'user/logout', component: LogoutPage, props: true }
    ],
  },
  {
    name: 'ErrorPage', path: '*', component: ErrorPage, props: true
  }
];

