﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Decoration
    {
        public int DecorationId { get; set; }
        public string DecorationsName { get; set; }
        public string Source { get; set; }
        public string Reason { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public byte[] OrderImage { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}