
export class Ring {
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

		name = '',
		description = '',

		} = {}) {

		this.seqno = seqno;
		this.status = status;

		this.id = id;
		this.deleted = deleted;

		this.name = name;
		this.description = description;

	}
}

export function createRing(data) {
	return Object.freeze(new Ring(data));
}