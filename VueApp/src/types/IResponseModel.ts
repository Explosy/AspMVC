import { IUserTypes } from "./IUserTypes";

export interface IResponseModel {
	error:      null;
	stackTrace: null;
	isSuccess:  boolean;
	data:       IUserTypes[];
}
