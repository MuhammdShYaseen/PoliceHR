using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Layoff
    {
        public int LayoffsId { get; set; }
        public string Reason { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public byte[] OrderImage { get; set; }
        public string SeizureNumber { get; set; }
        public DateTime? SeizureDate { get; set; }
        public byte[] SeizureImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
