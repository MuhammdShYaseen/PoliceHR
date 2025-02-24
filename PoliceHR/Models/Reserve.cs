﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Reserve
    {
        public int ReserveId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrderNum { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElementId { get; set; }

        public virtual Element Element { get; set; }
    }
}
