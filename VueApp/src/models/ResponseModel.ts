import { User } from "./User";

export type ResponseModel = {
	error:      null | string;
	stackTrace: null | string;
	isSuccess:  boolean;
	data:       User | User[];
}
