using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Child
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public int? WiveOrHusbandId { get; set; }
        public DateTime? ChildBrithDate { get; set; }
        public string ChildBrithLocation { get; set; }
        public string ChildPersonalIdNumber { get; set; }
        public byte[] CivilRegistrationImage { get; set; }
        public bool? IsAlive { get; set; }
        public int? ElmId { get; set; }
        public bool? Gender { get; set; }

        public virtual Element Elm { get; set; }
        public virtual WiveOrHusband WiveOrHusband { get; set; }
    }
}
