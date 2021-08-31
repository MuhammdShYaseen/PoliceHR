using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Accident
    {
        public int AccidentsId { get; set; }
        public DateTime? AccidentsDate { get; set; }
        public int? AccidentsSeizure { get; set; }
        public string MedicReport { get; set; }
        public bool? IsAtWork { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
