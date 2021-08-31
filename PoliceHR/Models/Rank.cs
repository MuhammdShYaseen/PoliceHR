using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Rank
    {
        public Rank()
        {
            Elements = new HashSet<Element>();
        }

        public int RankId { get; set; }
        public string RankName { get; set; }

        public virtual ICollection<Element> Elements { get; set; }
    }
}
