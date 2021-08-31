using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Unite
    {
        public Unite()
        {
            Elements = new HashSet<Element>();
        }

        public string UnitName { get; set; }
        public int UnitId { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}
