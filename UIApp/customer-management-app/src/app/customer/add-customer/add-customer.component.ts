import { Component,OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { NotificationService } from '../../notification.service';
import { dropdownValidator } from '../../custom-validator'; 
import { StatusCode } from '../../http-status.enum';

@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.css']
})

//to Add Customer
export class AddCustomerComponent {
  customerForm: FormGroup;
  editCustomer: Customer | undefined;
  state : any;
  submitted = false;
  customers: any;
 
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService,
    private notification: NotificationService) {
    this.customerForm = this.formBuilder.group({
      customerId: ['0', Validators.required],
      salutation: ['',dropdownValidator()], // Add salutation field
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
      countryCode: ['', dropdownValidator()],
      gender: ['', dropdownValidator()],
      balance: ['', [Validators.required, Validators.pattern('^[0-9]+(\\.[0-9]{1,2})?$')]],
      password: ['', Validators.required], // Add password field
      language: ['', dropdownValidator()], // Add language field
      currency: ['', dropdownValidator()], // Add currency field
    });

    //console.log("this.router.getCurrentNavigation():" + this.router.getCurrentNavigation()?.extras);
    
    this.state = this.router.getCurrentNavigation()?.extras.state;
  }

  ngOnInit(): void {
    // Check if we have the editCustomer data in the state
    const state = this.state;
    if (state && state['editCustomer']) {
      this.editCustomer = state['editCustomer'];

      // Pre-fill the form with the editCustomer data
      this.customerForm.patchValue(this.editCustomer!);
    }
  }

  onClear(): void {
    this.submitted = false;
    this.customerForm.reset();
    this.ngOnInit();
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.customerForm.valid) {
      const newCustomer: Customer = this.customerForm.value;
      if (this.editCustomer) {
        // Update the customer data on the server
        this.customerService.editCustomer(newCustomer).subscribe(
          (Response) => {
            if(Response.statusCode == StatusCode.Updated){
              this.notification.success('Customer Updated successfully!', 'Success');
               // Redirect back to the list-customers component after successful update
              this.router.navigate(['/']);
            }
            if(Response.statusCode == StatusCode.BadRequest){
              this.notification.warning('Invalid input details!!', 'Error');
            }
            if(Response.statusCode == StatusCode.InternalServerError){
              this.notification.error('Internal Server error!!', 'Error');
            }
          },
          (error) => {
            // Handle error if needed
            console.error(error);
            this.notification.error('Internal server error!!', 'Error');
          }
        );
      } else {
        // Add the new customer to the server
        this.customerService.addCustomer(newCustomer).subscribe(
          (Response) => {
            if(Response.statusCode == StatusCode.Created){
              this.notification.success('Customer added successfully!', 'Success');
               // Redirect back to the list-customers component after successful update
              this.router.navigate(['/']);
            }
            if(Response.statusCode == StatusCode.BadRequest){
              this.notification.warning('Invalid input details!!', 'Error');
            }
            if(Response.statusCode == StatusCode.InternalServerError){
              this.notification.error('Internal Server Error!!', 'Error');
            }
          },
          (error) => {
            // Handle error if needed
            console.error(error);
            this.notification.error('Internal server error!!', 'Error');
          }
        );
      }
    }
    else{
      this.notification.warning('Invalid input details!!', 'Error');
    }
  }

  // New method to get access to form controls for validation messages
  get formControls() {
    return this.customerForm.controls;
  }

   listCustomers: Customer[] = [];
}