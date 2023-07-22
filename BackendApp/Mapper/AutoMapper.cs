using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Models;
using System.ComponentModel;
using System.Reflection;

namespace BackendApp.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Customer, CustomerAPI>()
                .ForMember(o => o.id, b => b.MapFrom(z => z.CustomerId.ToString()))
                .ForMember(o => o.salutation, b => b.MapFrom(z => z.Salutation))
                .ForMember(o => o.initials, b => b.MapFrom(z => z.FirstName.Substring(0, 1).ToString()))
                .ForMember(o => o.firstname, b => b.MapFrom(z => z.FirstName))
                .ForMember(o => o.firstname_ascii, b => b.MapFrom(z => z.FirstName.ToLower()))
                .ForMember(o => o.lastname, b => b.MapFrom(z => z.LastName))
                .ForMember(o => o.lastname_ascii, b => b.MapFrom(z => z.LastName.ToLower()))
                .ForMember(o => o.gender, b => b.MapFrom(z => z.Gender))
                .ForMember(o => o.email, b => b.MapFrom(z => z.Email))
                .ForMember(o => o.password, b => b.MapFrom(z => z.Password))
                .ForMember(o => o.country_code, b => b.MapFrom(z => z.CountryCode))
                .ForMember(o => o.country_code_alpha, b => b.MapFrom(z => GetCountryCodeAlpha(z.CountryCode)))
                .ForMember(o => o.country_name, b => b.MapFrom(z => GetCountryName(z.CountryCode)))
                .ForMember(o => o.primary_language, b => b.MapFrom(z => z.Language))
                .ForMember(o => o.primary_language_code, b => b.MapFrom(z => GetLanguageCode(z.Language)))
                .ForMember(o => o.balance, b => b.MapFrom(z => z.Balance))
                .ForMember(o => o.phone_Number, b => b.MapFrom(z => z.PhoneNumber))
                .ForMember(o => o.currency, b => b.MapFrom(z => z.Currency))
                .ForMember(o => o.partitionKey, b => b.MapFrom(z => z.CountryCode));
                //.ForMember(o => o.firstname_country_rank, b => b.MapFrom(z => "100"))
                //.ForMember(o => o.firstname_country_frequency, b => b.MapFrom(z => "100"))
                //.ForMember(o => o.lastname_country_rank, b => b.MapFrom(z => "100"))
                //.ForMember(o => o.lastname_country_frequency, b => b.MapFrom(z => "100"));

            CreateMap<CustomerAPI, Customer>()
               .ForMember(o => o.CustomerId, b => b.MapFrom(z => int.Parse(z.id)))
               .ForMember(o => o.Salutation, b => b.MapFrom(z => z.salutation))
               //.ForMember(o => o.initials, b => b.MapFrom(z => z.FirstName.Substring(0, 1).ToString()))
               .ForMember(o => o.FirstName, b => b.MapFrom(z => z.firstname))
               //.ForMember(o => o.firstname_ascii, b => b.MapFrom(z => z.FirstName.ToLower()))
               .ForMember(o => o.LastName, b => b.MapFrom(z => z.lastname))
               //.ForMember(o => o.lastname_ascii, b => b.MapFrom(z => z.LastName.ToLower()))
               .ForMember(o => o.Gender, b => b.MapFrom(z => z.gender))
               .ForMember(o => o.Email, b => b.MapFrom(z => z.email))
               .ForMember(o => o.Password, b => b.MapFrom(z => z.password))
               .ForMember(o => o.CountryCode, b => b.MapFrom(z => z.country_code))
               //.ForMember(o => o.country_code_alpha, b => b.MapFrom(z => z.CountryCode))
               //.ForMember(o => o.country_name, b => b.MapFrom(z => z.CountryCode))
               //.ForMember(o => o.country_name, b => b.MapFrom(z => z.CountryCode))
               .ForMember(o => o.Language, b => b.MapFrom(z => z.primary_language))
               //.ForMember(o => o.primary_language_code, b => b.MapFrom(z => z.Language))
               .ForMember(o => o.Balance, b => b.MapFrom(z => z.balance))
               .ForMember(o => o.PhoneNumber, b => b.MapFrom(z => z.phone_Number))
               .ForMember(o => o.Currency, b => b.MapFrom(z => z.currency))
               //.ForMember(o => o.partitionKey, b => b.MapFrom(z => z.Currency))
               ;
        }

        //public string GetEnumDescription(this Enum value)
        //{
        //    FieldInfo fi = value.GetType().GetField(value.ToString());

        //    DescriptionAttribute[] attributes =
        //        (DescriptionAttribute[])fi.GetCustomAttributes(
        //        typeof(DescriptionAttribute),
        //        false);

        //    if (attributes != null &&
        //        attributes.Length > 0)
        //        return attributes[0].Description;
        //    else
        //        return value.ToString();
        //}
        public string GetCountryCodeAlpha(string key)
        {
            Dictionary<string, string> CountryCodeAlphaData = new Dictionary<string, string>();
            CountryCodeAlphaData["US"] = "USA";
            CountryCodeAlphaData["IND"] = "IND";
            CountryCodeAlphaData["UK"] = "UK";
            return CountryCodeAlphaData[key].ToString();
        }

        public string GetCountryName(string key)
        {
            Dictionary<string, string> CountryNameData = new Dictionary<string, string>();
            CountryNameData["US"] = "United States";
            CountryNameData["IND"] = "India";
            CountryNameData["UK"] = "United Kingdom";
            return CountryNameData[key].ToString();
        }

        public string GetLanguageCode(string key)
        {
            Dictionary<string, string> langCodeData = new Dictionary<string, string>();
            langCodeData["English"] = "en"; 
            langCodeData["Spanish"] = "sp";
            langCodeData["French"] = "fr";
            langCodeData["Hindi"] = "hi";
            return langCodeData[key].ToString();
        }
    }
}
