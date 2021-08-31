using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class PhonesNumber
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public int? ElmId { get; set; }
        public bool? PhoneType { get; set; }

        public virtual Element Elm { get; set; }
    }
}
