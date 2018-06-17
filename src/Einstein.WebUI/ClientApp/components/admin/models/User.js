
export class User {
	constructor({
		seqno = 0,

		status = {
			valid: true,
			added: false,
			modified: false,
			removed: false,
			loaded: false
		},

		id = -1,
		deleted = false,

		active = false,
		login = '',

		firstname = '',
		lastname = '',
		email = '',

		newPassword = '',
		confirmPassword = '',

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

		this.firstname = firstname;
		this.lastname = lastname;
		this.email = email;

		this.newPassword = newPassword;
		this.confirmPassword = confirmPassword;

		this.expireDate = expireDate;
		this.lastLogin = lastLogin;

		this.failedAttempts = failedAttempts;
		this.goodLogins = goodLogins;
	}
}

export function createUser(data) {
	return Object.freeze(new User(data));
}