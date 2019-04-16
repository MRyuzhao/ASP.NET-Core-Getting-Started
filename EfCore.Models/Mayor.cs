using System;
using EfCore.Common.Enums;

namespace EfCore.Domain
{
    public class Mayor : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public City City { get; set; }
        public int CityId { get; set; }
    }
}