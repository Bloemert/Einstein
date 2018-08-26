
export class SkillScore {
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
		value = 1 * seqno,

		skillid = -1

		} = {}) {

		this.id = id;
		this.deleted = deleted;

		this.seqno = seqno;
		this.status = status;

		this.name = name;
		this.description = description;
		this.value = value;

		this.skillid = skillid;
	}
}

export function createSkillScore(data) {
	return Object.freeze(new SkillScore(data));
}