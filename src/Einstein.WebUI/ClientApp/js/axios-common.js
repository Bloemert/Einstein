import axios from 'axios';
import store from './../store.js'; // path to your Vuex store

let appSettings = store.getters['core/getAppSettings'];

const HTTP = axios.create({
	baseURL: appSettings.baseUrl,
	withCredentials: true,
	crossDomain: true
})

HTTP.interceptors.request.use(function (config) {
	// Do something before request is sent
	let user = store.getters['core/getCurrentUser'];

	config.auth = {
		username: user.name,
		password: user.password
	}

	return config;
}, function (error) {
	// Do something with request error
	alert('Error in HTTP request interceptor! : ' + error);
	return Promise.reject(error);
	});


export default HTTP;