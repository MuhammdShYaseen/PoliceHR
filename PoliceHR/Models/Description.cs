using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Description
    {
        public int DescriptionsId { get; set; }
        public string DescriptionOfficer { get; set; }
        public DateTime? DescriptionDate { get; set; }
        public int? FunctionalValue { get; set; }
        public int? EthicsValue { get; set; }
        public int? AppearanceValue { get; set; }
        public int? PhysicalValue { get; set; }
        public string SpecialsValue { get; set; }
        public string Others { get; set; }
        public byte[] DescriptionImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
