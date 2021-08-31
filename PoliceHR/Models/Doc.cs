using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Doc
    {
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string DocInfo { get; set; }
        public byte[] DocImage { get; set; }
    }
}
