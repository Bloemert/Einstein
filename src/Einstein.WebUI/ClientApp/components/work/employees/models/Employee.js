
export class Employee {
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

		employeeNumber = -1,

		firstname = '',
		lastname = '',
		email = '',

		employedSince = null,


		availabilityPerWeek = 100,

		functionLevel = 'junior',

		functionTitle = 'Software Developer',

		managerId = -1

	} = {}) {

		this.id = id;
		this.deleted = deleted;

		this.seqno = seqno;
		this.status = status;

		this.employeeNumber = employeeNumber,

		this.firstname = firstname,
		this.lastname = lastname,
		this.email = email,

		this.employedSince = employedSince,

		this.availabilityPerWeek = availabilityPerWeek,

		this.functionLevel = functionLevel,

		this.functionTitle = functionTitle,

		this.managerId = managerId
	}
}

export function createEmployee(data) {
	return Object.freeze(new Employee(data));
}