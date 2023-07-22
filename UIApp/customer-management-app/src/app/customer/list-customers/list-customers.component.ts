import { Component } from '@angular/core';
import { Customer } from '../customer.model';
import { Router } from '@angular/router';
import { CustomerService } from '../customer.service';
import { NotificationService } from '../../notification.service';
import { StatusCode } from '../../http-status.enum';

@Component({
  selector: 'app-list-customers',
  templateUrl: './list-customers.component.html',
  styleUrls: ['./list-customers.component.css']
})


export class ListCustomersComponent {

  filterParam : any = {
    sortedColumn : 'email',
    isAscending  : true,
    itemsPerPage : 10,
    currentPage : 1
  }

  customers: Customer[] = [
  ];

  searchTerm: string = '';
  filteredCustomers: Customer[] = [];
  sortedColumn: keyof Customer = 'firstName';
  sortOrder: SortOrder = SortOrder.Ascending;

  constructor(private router: Router,private customerService: CustomerService,private notification: NotificationService) {}

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers(): void {
    this.customerService.getCustomers().subscribe(
      (Response) => {
        debugger;
        if(Response.statusCode == StatusCode.InternalServerError){
          this.notification.error('Internal Server Error!!', 'Error');
        }
        this.customers = Response.data;
        this.notification.success('Customers loaded successfully!', 'Success');
        this.filterCustomers();
      },
      (error) => {
        // Handle error if needed
        console.error(error);
        this.notification.error('Internal Server Error!!', 'Error');
      }
    );
  }

  onPageChanged(event: any): void {
    this.filterParam.currentPage = event.page;
  }
  
  sortCustomers(column: keyof Customer, order: SortOrder): void {
    this.customers.sort((a, b) => {
      const valueA = a[column];
      const valueB = b[column];

      if (order === SortOrder.Ascending) {
        return valueA < valueB ? -1 : valueA > valueB ? 1 : 0;
      } else {
        return valueB < valueA ? -1 : valueB > valueA ? 1 : 0;
      }
    });
  }
  onSort(column: keyof Customer): void {
    if (column === this.sortedColumn) {
      this.sortOrder = this.sortOrder === SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
    } else {
      this.sortedColumn = column;
      this.sortOrder = SortOrder.Ascending;
    }
    this.sortCustomers(this.sortedColumn, this.sortOrder);
  }

  // Helper method to check if a customer contains the search term in any field
  private customerContainsSearchTerm(customer: Customer, searchTerm: string): boolean {
    var result =  (
      customer.salutation.toLowerCase().includes(searchTerm) ||
      customer.firstName.toLowerCase().includes(searchTerm) ||
      customer.lastName.toLowerCase().includes(searchTerm) ||
      customer.email.toLowerCase().includes(searchTerm) ||
      customer.phoneNumber.toLowerCase().includes(searchTerm) ||
      customer.countryCode.toLowerCase().includes(searchTerm) ||
      customer.balance.toString().includes(searchTerm)
    );
    return result;
  }

   // Method to filter customers based on the search term
   filterCustomers(): void {
    const lowerCaseSearchTerm = this.searchTerm.toLowerCase().trim();
    if (lowerCaseSearchTerm === '') {
      this.filteredCustomers = this.customers;
    } else {
      this.filteredCustomers = this.customers.filter((customer) => this.customerContainsSearchTerm(customer, lowerCaseSearchTerm));
    }
  }

  onEdit(customer: Customer): void {
    // Redirect to the add-customer component with the customer data to edit
    //this.router.navigate(['/add-customer', { state: { editCustomer: customer } }]);
    this.router.navigateByUrl('/add-customer', {
      state: {editCustomer: customer}
    });
  }

  // onDelete(index: number): void {
  //   // Delete the customer from the list
  //   this.customers.splice(index, 1);
  // }
  onDelete(customerId: string): void {
    // Delete the customer from the list and also delete from the server
    this.customerService.deleteCustomer(customerId).subscribe(
      (Response) => {
        
        if(Response.statusCode == StatusCode.NoContent){
          this.notification.success('Customer deleted successfully!', 'Success');
          this.customers = this.customers.filter((customer) => customer.customerId !== customerId);
        }
        if(Response.statusCode == StatusCode.BadRequest){
          this.notification.error('Bad input!!', 'Error');
        }
      },
      (error) => {
        // Handle error if needed
        console.error(error);
        this.notification.error('Internal Server Error!!', 'Error');
      }
    );
  }
}

enum SortOrder {
  Ascending = 'asc',
  Descending = 'desc',
}

