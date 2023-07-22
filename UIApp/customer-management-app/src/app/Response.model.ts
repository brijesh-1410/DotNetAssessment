import { Customer } from "./customer/customer.model";

enum StatusCode {
    Success = 200,
    Created = 201,
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    InternalServerError = 500,
    NoContent = 204,
    Updated = 202
  }
  
  
export interface Repsonse {
    statusCode: StatusCode;
    data: Customer[];
    message:string;
  }