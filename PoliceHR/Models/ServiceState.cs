using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class ServiceState
    {
        public ServiceState()
        {
            Elements = new HashSet<Element>();
        }

        public int ServiceStateId { get; set; }
        public string ServiceState1 { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}
