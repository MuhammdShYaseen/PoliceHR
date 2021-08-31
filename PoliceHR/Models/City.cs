using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class City
    {
        public City()
        {
            Elements = new HashSet<Element>();
        }

        public string Cities { get; set; }
        public int CityId { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}
