
export class EmployeeSkill {
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

		employeeId = -1,

		skillId = -1,

		scoreId = -1

		} = {}) {

		this.id = id;
		this.deleted = deleted;

		this.seqno = seqno;
		this.status = status;

		this.employeeId = employeeId;
		this.skillId = skillId;
		this.scoreId = scoreId;
	}
}

export function createEmployeeSkill(data) {
	return Object.freeze(new EmployeeSkill(data));
}