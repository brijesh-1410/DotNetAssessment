using BackendApp.Models;
using BackendApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackendApp.Handler;

namespace BackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService) {
            this.customerService = customerService;
        }

        [HttpGet]
        public Response Get()
        {
            try
            {
                IEnumerable<Customer> result = customerService.GetCustomers();
                return new Response() { StatusCode = HttpStatusCode.OK, Data = result, Message = "Customers listed Successfully!!" };
            }
            catch (Exception e) {
                Console.WriteLine("\nException from API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        public Response Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest, Data = null, Message = "Please validate input!!" };
                }
                // Provided API call giving 500 in case ID not found, which needs to be deferent(as per the requirement need to use)
                //Alternate to fullfill this scenario
                //Customer result = customerService.GetCustomers().FirstOrDefault(x => x.CustomerId == id);
                //if (result is null) {
                //    return new Response { StatusCode = HttpStatusCode.NotFound, Data = result, Message = "Customer Not available!!" };
                //}

                Customer result = customerService.GetCustomer(id);
                return new Response { StatusCode = HttpStatusCode.OK, Data = result, Message = "Customer listed Successfully!!" };
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException from API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        //999

        [HttpPost]
        public Response Post(Customer customer)
        {
            try
            {
                if (customer is null)
                {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest, Data = null, Message = "Please validate input!!" };
                }

                bool result = customerService.AddCustomer(customer);
                return new Response { StatusCode = HttpStatusCode.Created, Data = result, Message = "Customer Added Successfully!!" };
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException from API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        [HttpPut]
        public Response Put(Customer customer)
        {
            try
            {
                if (customer is null && customer.CustomerId <= 0) {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest, Data = null, Message = "Please validate input!!" };
                }

                bool result = customerService.UpdateCustomer(customer);
                return new Response { StatusCode = HttpStatusCode.Accepted, Data = result, Message = "Customer Updated Successfully!!" };
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException from API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response() { StatusCode = HttpStatusCode.BadRequest, Data = null, Message = "Please validate input!!" };
                }

                bool result = customerService.DeleteCustomer(id);
                return new Response { StatusCode = HttpStatusCode.NoContent, Data = result, Message = "Customer Deleted Successfully!!" };
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException from API");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }
    }
}
