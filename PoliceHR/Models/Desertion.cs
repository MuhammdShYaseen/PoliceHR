using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Desertion
    {
        public int DesertionId { get; set; }
        public DateTime? DesertionDate { get; set; }
        public DateTime? Rejoin { get; set; }
        public int? ElementId { get; set; }
        public string TelegramNumber { get; set; }
        public DateTime? TelegramDate { get; set; }
        public byte[] TelegramImage { get; set; }
        public string RejoinTelegram { get; set; }
        public byte[] RejoinImage { get; set; }

        public virtual Element Element { get; set; }
    }
}
