using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Upgrade
    {
        public int UpgradeId { get; set; }
        public int? UpgradeName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
