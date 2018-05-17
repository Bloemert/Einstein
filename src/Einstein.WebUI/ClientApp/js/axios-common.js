import axios from 'axios';
import store from './../store.js'; // path to your Vuex store
//import Vue from 'vue'

let user = store.getters['core/getCurrentUser'];

export const HTTP = axios.create({
	baseURL: 'http://localhost:8088',
	withCredentials: true,
	auth: {
		username: user.name,
		password: user.password
	},
	crossDomain: true
})