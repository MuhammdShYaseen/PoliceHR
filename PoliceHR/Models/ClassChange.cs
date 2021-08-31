using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class ClassChange
    {
        public int ClassChangesId { get; set; }
        public string ClassName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
