import axios from 'axios';

export const HTTP = axios.create({
	baseURL: 'http://localhost:8088',
	withCredentials: true,
	auth: {
		username: 'henry',
		password: 'HOI'
	},
	crossDomain: true
})