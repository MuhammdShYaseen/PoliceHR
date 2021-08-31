using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class PoliceServicesMovement
    {
        public int PoliceServicesId { get; set; }
        public string ServiceName { get; set; }
        public string StateCity { get; set; }
        public string PoliceUnitName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrdarNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderSource { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
