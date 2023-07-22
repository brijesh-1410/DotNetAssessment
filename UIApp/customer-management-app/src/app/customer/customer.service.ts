import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from './customer.model';
import { Repsonse } from '../Response.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = 'https://localhost:44341/api'; // Replace this with your actual API URL

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Repsonse> {
    return this.http.get<Repsonse>(`${this.apiUrl}/customer`);
  }

  addCustomer(customer: Customer): Observable<Repsonse> {
    debugger
    var result = this.http.post<Repsonse>(`${this.apiUrl}/customer`, customer);
    return result;
  }

  editCustomer(customer: Customer): Observable<Repsonse> {
    debugger
    var result =  this.http.put<Repsonse>(`${this.apiUrl}/customer/`, customer);
    return result;
  }

  deleteCustomer(customerId: string): Observable<Repsonse> {
    debugger
    var result = this.http.delete<Repsonse>(`${this.apiUrl}/customer/${customerId}`);
    return result;
  }
}
