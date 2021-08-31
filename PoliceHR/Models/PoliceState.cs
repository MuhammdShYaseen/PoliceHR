using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class PoliceState
    {
        public int PoliceStateId { get; set; }
        public string SeizureNumber { get; set; }
        public DateTime? SeizureDate { get; set; }
        public string Result { get; set; }
        public int? ElmId { get; set; }
        public byte[] StateImage { get; set; }
        public string OrderNum { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Element Elm { get; set; }
    }
}
