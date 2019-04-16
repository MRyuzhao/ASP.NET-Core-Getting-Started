using System.Collections.Generic;

namespace EfCore.Domain
{
    public class City : Entity
    {
        public City()
        {
            CityCompanies = new List<CityCompany>();
        }

        public int Name { get; set; }
        public string AreaCode { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public Mayor Mayor { get; set; }
        public List<CityCompany> CityCompanies { get; set; }
    }
}