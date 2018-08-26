
export class SkillType {
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

		this.id = id;
		this.deleted = deleted;

		this.seqno = seqno;
		this.status = status;

		this.name = name;
		this.description = description;
	}
}

export function createSkillType(data) {
	return Object.freeze(new SkillType(data));
}