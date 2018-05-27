
export class User {
	constructor({
		seqno = 0,

		status = {
			action: '',
			result: ''
		},

		id = -1,
		deleted = false,

		active = false,
		login = '',

		newPassword = '',

		expireDate = new Date(),
		lastLogin = new Date(),

		failedAttempts = -1,
		goodLogins = -1 } = {}) {

		this.seqno = seqno;
		this.status = status;

		this.id = id;
		this.deleted = deleted;

		this.active = active;
		this.login = login;

		this.newPassword = newPassword;

		this.expireDate = expireDate;
		this.lastLogin = lastLogin;

		this.failedAttempts = failedAttempts;
		this.goodLogins = goodLogins;
	}
}

export function createUser(data) {
	return Object.freeze(new User(data));
}