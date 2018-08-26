
export class Skill {
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
		version = '',

		skillCategoryId = -1,
		skillTypeId = -1,

		possibleScores = []

		} = {}) {

		this.id = id;
		this.deleted = deleted;

		this.seqno = seqno;
		this.status = status;

		this.name = name;
		this.description = description;
		this.version = version;

		this.skillTypeId = skillTypeId;
		this.skillCategoryId = skillCategoryId;

		this.possibleScores = possibleScores;
	}
}

export function createSkill(data) {
	return Object.freeze(new Skill(data));
}