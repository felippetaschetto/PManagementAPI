using PManagement.Core.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PManagement.Core.ValueObjects
{
    public class Address : ValueObject
    {
        private Address() { }
        public Address(string street, string postCode, string city, string country)
        {
            Street = street;
            PostCode = postCode;
            City = city;
            Country = country;
        }

        public string Street { get; private set; }
        public string PostCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return Street;
        }
    }
}
