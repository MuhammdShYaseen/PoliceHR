using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceHR.Models.AccessModels
{
    public partial class Places
    {
        public int? Code { get; set; }
        public string Pname { get; set; }
        public int? PlaceUpcode { get; set; }
        public string DetailName { get; set; }
        public short Flag { get; set; }
        public DateTime? _22082016 { get; set; }
    }
}
