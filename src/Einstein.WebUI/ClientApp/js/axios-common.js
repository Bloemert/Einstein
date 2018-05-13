import axios from 'axios';

export const HTTP = axios.create({
	baseURL: 'http://192.168.1.88:8088',
	withCredentials: true,
	auth: {
		username: 'henry',
		password: 'HOI'
	},
	crossDomain: true
})