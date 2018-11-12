
export class Log {
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

		message = '',
		level = '',
		timeStamp = Date.now(),
		exception = '',
		properties = '',
		logEvent = ''
		} = {}) {

		this.seqno = seqno;
		this.status = status;

		this.id = id;

		this.message = message;
		this.level = level;

		this.timeStamp = timeStamp;
		this.exception = exception;
		this.properties = properties;

		this.logEvent = logEvent;
	}
}

export function createLog(data) {
	return Object.freeze(new Log(data));
}