using BackendApp.Common;
using BackendApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BackendApp.Mapper;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace BackendApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IApiCaller apiCaller;
        private readonly IMapper mapper;
        public CustomerService(IApiCaller caller,IMapper mapper) {
            apiCaller = caller;
            this.mapper = mapper;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            try
            {
                string? result = apiCaller.Get("Customers").Result;
                IEnumerable<CustomerAPI> customerObj =  JsonSerializer.Deserialize<IEnumerable<CustomerAPI>>(result);
                var customerResult = mapper.Map<IEnumerable<Customer>>(customerObj);
                return customerResult;
            }
            catch (Exception e) {
                Console.WriteLine("\nException while fetching Service operations");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public bool AddCustomer(Customer customer) {
            try
            {
                var customerResult = mapper.Map<CustomerAPI>(customer);

                int maxCustomerId = 1;
                IEnumerable<Customer> customers = this.GetCustomers();
                if (customers.Count() > 0) {
                    maxCustomerId = customers.Select(x => x.CustomerId).Max();
                }

                customerResult.id = (maxCustomerId + 1).ToString();
                customerResult.firstname_country_frequency = (customers.Where(x => x.FirstName == customer.FirstName && x.CountryCode == customer.CountryCode).Count() + 1).ToString();
                customerResult.firstname_country_rank = (customers.Where(x => x.CountryCode == customer.CountryCode).Count() + 1).ToString();
                customerResult.lastname_country_frequency = (customers.Where(x => x.LastName == customer.LastName && x.CountryCode == customer.CountryCode).Count() + 1).ToString();
                customerResult.lastname_country_rank = (customers.Where(x => x.CountryCode == customer.CountryCode).Count() + 1).ToString();

                bool result = apiCaller.Post("Customer", JObject.FromObject(customerResult)).Result;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException while fetching Service operations!!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public Customer GetCustomer(int id) {
            try
            {
                string? result = apiCaller.Get("Customer/"+id.ToString()).Result;
                CustomerAPI customerObj = JsonSerializer.Deserialize<CustomerAPI>(result);
                var customerResult = mapper.Map<Customer>(customerObj);
                return customerResult;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException while fetching Service operations!!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public bool UpdateCustomer(Customer customer) {
            try
            {
                //Get DB object
                var existingCustomer = GetCustomer(customer.CustomerId);

                if (existingCustomer.FirstName.ToLower().Trim() != customer.FirstName.ToLower().Trim()) {
                    existingCustomer.FirstName = customer.FirstName.Trim();
                }
                if (existingCustomer.LastName.ToLower().Trim() != customer.LastName.ToLower().Trim())
                {
                    existingCustomer.LastName = customer.LastName.Trim();
                }
                if (existingCustomer.Salutation.ToLower().Trim() != customer.Salutation.ToLower().Trim())
                {
                    existingCustomer.Salutation = customer.Salutation.Trim();
                }
                if (existingCustomer.Email.ToLower().Trim() != customer.Email.ToLower().Trim())
                {
                    existingCustomer.Email = customer.Email.Trim();
                }
                if (existingCustomer.PhoneNumber.ToLower().Trim() != customer.PhoneNumber.ToLower().Trim())
                {
                    existingCustomer.PhoneNumber = customer.PhoneNumber.Trim();
                }
                if (existingCustomer.CountryCode.ToLower().Trim() != customer.CountryCode.ToLower().Trim())
                {
                    existingCustomer.CountryCode = customer.CountryCode.Trim();
                }
                if (existingCustomer.Gender.ToLower().Trim() != customer.Gender.ToLower().Trim())
                {
                    existingCustomer.Gender = customer.Gender.Trim();
                }
                if (existingCustomer.Password.ToLower().Trim() != customer.Password.ToLower().Trim())
                {
                    existingCustomer.Password = customer.Password.Trim();
                }
                if (existingCustomer.Language.ToLower().Trim() != customer.Language.ToLower().Trim())
                {
                    existingCustomer.Language = customer.Language.Trim();
                }
                if (existingCustomer.Currency.ToLower().Trim() != customer.Currency.ToLower().Trim())
                {
                    existingCustomer.Currency = customer.Currency.Trim();
                }
                if (existingCustomer.Balance != customer.Balance)
                {
                    existingCustomer.Balance = customer.Balance;
                }

                var customerResult = mapper.Map<CustomerAPI>(existingCustomer);
                bool result = apiCaller.Post("Customer/" + customerResult.id, JObject.FromObject(customerResult)).Result;

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException while fetching Service operations");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public bool DeleteCustomer(int id) {
            try
            {
                bool result = apiCaller.Delete("Customer/" + id.ToString()).Result;
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException while fetching Service operations");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }
    }
}
