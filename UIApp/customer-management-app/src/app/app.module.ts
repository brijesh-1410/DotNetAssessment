import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCustomerComponent } from './customer/add-customer/add-customer.component';
import { ListCustomersComponent } from './customer/list-customers/list-customers.component';

const routes: Routes = [
  { path: '', component: ListCustomersComponent },
  { path: 'add-customer', component: AddCustomerComponent },
  { path: 'list-customer', component: ListCustomersComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    AddCustomerComponent,
    ListCustomersComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    BsDropdownModule.forRoot(), 
    PaginationModule.forRoot(), 
    ToastrModule.forRoot(),
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
