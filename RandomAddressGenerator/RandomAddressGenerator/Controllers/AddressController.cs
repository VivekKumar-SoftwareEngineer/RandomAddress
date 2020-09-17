using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Faker;
using System.Text;

namespace RandomAddressGenerator.Controllers
{
    public class AddressController : ApiController
    {
        string[] country = { "US", "Canada", "Mexico", "Netherlands" };
        string[] countryCode = { "USA", "CAN", "MEX", "NLD" };
        bool[] includeOptional = {false,true};

        // Get API that generates random address, returns the response in json or xml
        [HttpGet]
        [Route("randomizer/address")]
        public IHttpActionResult GenerateRandomAddressString()
        {
            // Deciding whether to include optional fields 
            Random random = new Random();
            int optional = random.Next(includeOptional.Length);

            // Getting the random address generated
            Address address = GenerateAddress(optional);

            // returning address with 200 Ok as no other response code was in the requirement
            return Ok(address);
        }

        // Get API that generates random address, returns the response in plaintext
        [HttpGet]
        [Route("randomizer/address")]
        public IHttpActionResult GenerateRandomAddressString(string stringFormat)
        {
            // Deciding whether to include optional fields 
            Random random = new Random();
            int optional = random.Next(includeOptional.Length);

            Address address = GenerateAddress(optional);

            // String builder for plaintext response
            StringBuilder sb = new StringBuilder();
                       
            sb.Append(address.house);
            sb.Append(" ");
            sb.Append(address.street);
            sb.Append(", ");            
            sb.Append(address.city);
            sb.Append(", ");  
            sb.Append(address.county);
            sb.Append(", ");  

            if (optional==1)
            {
                sb.Append(address.state);
                sb.Append(", ");  
                sb.Append(address.stateCode);
                sb.Append(", ");  
                sb.Append(address.country);
                sb.Append(", ");  
            }

            sb.Append(address.postalCode);
            sb.Append(", ");  
            sb.Append(address.countryCode);

            // returning address with 200 Ok as no other response code was in the requirement
            if(stringFormat.Equals("true"))
                return Ok(sb.ToString());
            else
                return Ok(address);
        }
       
        // Method to generate the random address
        public Address GenerateAddress(int optional)
        {
            Random random = new Random();
            Address address = new Address();
            int rand = random.Next(country.Length);

            address.house = random.Next(10000).ToString();

            //Using Faker nuget package to generate the address
            address.street = Faker.Address.StreetName();
            address.city = Faker.Address.City();
            address.county = Faker.Address.UkCounty();

            if (optional==1)
            {
                address.state = Faker.Address.UsState();
                address.stateCode = Faker.Address.UsStateAbbr();
                address.country = country[rand];
            }

            address.postalCode = Faker.Address.ZipCode();
            address.countryCode = countryCode[rand];

            return address;
        }
    }

    //Data model for address 
    public class Address
    {
        public string house { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string stateCode { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }

    }
}
