using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Seizure
    {
        public int SeizureId { get; set; }
        public string SeizureWriter { get; set; }
        public string SeizureNumber { get; set; }
        public DateTime? SeizureDate { get; set; }
        public string Conclusion { get; set; }
        public string SeizureUnit { get; set; }
        public byte[] SeizureImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
