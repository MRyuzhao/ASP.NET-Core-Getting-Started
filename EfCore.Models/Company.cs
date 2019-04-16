using System;
using System.Collections.Generic;

namespace EfCore.Domain
{
    public class Company : Entity
    {
        public Company()
        {
            CityCompanies = new List<CityCompany>();
        }

        public string Name { get; set; }
        public DateTime Establish { get; set; }
        public string LegalPerson { get; set; }

        public List<CityCompany> CityCompanies { get; set; }
    }
}