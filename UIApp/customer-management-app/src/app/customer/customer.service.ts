import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from './customer.model';
import { Repsonse } from '../Response.model';
import { API_BASE_URL } from '../../api.config';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = API_BASE_URL; // Replace this with your actual API URL

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Repsonse> {
    return this.http.get<Repsonse>(`${this.apiUrl}/customer`);
  }

  addCustomer(customer: Customer): Observable<Repsonse> {
    return this.http.post<Repsonse>(`${this.apiUrl}/customer`, customer);
  }

  editCustomer(customer: Customer): Observable<Repsonse> {
    return this.http.put<Repsonse>(`${this.apiUrl}/customer/`, customer);
  }

  deleteCustomer(customerId: string): Observable<Repsonse> {
    return this.http.delete<Repsonse>(`${this.apiUrl}/customer/${customerId}`);
  }
}
