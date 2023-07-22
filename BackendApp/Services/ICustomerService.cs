using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Models;

namespace BackendApp.Services
{
    public interface ICustomerService
    {
        public IEnumerable<Customer> GetCustomers();
        public bool AddCustomer(Customer customer);
        public Customer GetCustomer(int id);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(int id);


    }
}
