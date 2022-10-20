export class User {
	id:              number;
	name:            string;
	surname:         string;
	age:             null |number;
	email:           string;
	registationDate: null | string;
	constructor() {
		this.id = 0;
		this.name = "test";
		this.surname = "test";
		this.age = 0;
		this.email = "test";
		this.registationDate = null;
    }
}