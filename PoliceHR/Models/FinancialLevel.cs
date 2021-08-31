using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class FinancialLevel
    {
        public int FinancialLevel1 { get; set; }
        public string FinancialName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
