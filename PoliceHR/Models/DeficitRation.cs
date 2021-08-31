using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class DeficitRation
    {
        public int DeficitRatio { get; set; }
        public DateTime? DeficitRatioOrderDate { get; set; }
        public string DeficitRatioOrderResult { get; set; }
        public int? DeficitRatioPercentage { get; set; }
        public byte[] DeficitRatioOrderImage { get; set; }
        public int? ElmId { get; set; }
        public string OrderNum { get; set; }

        public virtual Element Elm { get; set; }
    }
}
