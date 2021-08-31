using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Dgree
    {
        public int DgreeId { get; set; }
        public string DgreeName { get; set; }
        public string DgreeDuration { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public byte[] CertifiedGraduatedImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
