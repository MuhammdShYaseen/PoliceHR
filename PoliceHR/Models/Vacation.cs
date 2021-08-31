using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Vacation
    {
        public int VacationsId { get; set; }
        public int? VacationType { get; set; }
        public string Source { get; set; }
        public string VacationsNumber { get; set; }
        public DateTime? VacationsDate { get; set; }
        public string VacationsLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte[] VacationsImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
