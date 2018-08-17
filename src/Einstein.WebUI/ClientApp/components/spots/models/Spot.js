
export class Spot {
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

		isNew = true,

		name = '',
		description = '',

		ring = '',
		quadrant = '',

		spotterId = -1,
		spottedOn = new Date(),

		} = {}) {

		this.seqno = seqno;
		this.status = status;

		this.id = id;
		this.deleted = deleted;

		this.isNew = isNew;

		this.name = name;
		this.description = description;

		this.ring = ring;
		this.quadrant = quadrant;

		this.spotterId = spotterId;
		this.spottedOn = spottedOn;

	}
}

export function createSpot(data) {
	return Object.freeze(new Spot(data));
}