using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApp.Models
{
    public class Customer
    {
    public int CustomerId { get; set; }
    public string Salutation { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CountryCode { get; set; }
    public string Gender { get; set; }
    public decimal Balance { get; set; }
    public string Password { get; set; }
    public string Language { get; set; }
    public string Currency { get; set; }
    }

    public class CustomerAPI
    { 
        public string id { get; set; }
        public string salutation { get; set; }
        public string initials { get; set; }
        public string firstname { get; set; }
        public string firstname_ascii { get; set; }
        public string gender { get; set; }
        public string firstname_country_rank { get; set; }
        public string firstname_country_frequency { get; set; }
        public string lastname { get; set; }
        public string lastname_ascii { get; set; }
        public string lastname_country_rank { get; set; }
        public string lastname_country_frequency { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string country_code { get; set; }
        public string country_code_alpha { get; set; }
        public string country_name { get; set; }
        public string primary_language_code { get; set; }
        public string primary_language { get; set; }
        public decimal balance { get; set; }
        public string phone_Number { get; set; }
        public string currency { get; set; }
        public string partitionKey { get; set; }
        public string rowKey { get; set; }
        //public string timestamp { get; set; }
        //public string eTag { get; set; }
    }
}
