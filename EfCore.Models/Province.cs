using System.Collections.Generic;

namespace EfCore.Domain
{
    public class Province : Entity
    {
        public Province()
        {
            Cities = new List<City>();
        }

        public string Name { get; set; }
        public int Population { get; set; }

        public IList<City> Cities { get; set; }
    }
}
