using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class ActivtisRecourd
    {
        public int ActivityId { get; set; }
        public int? UserId { get; set; }
        public string ActivityDes { get; set; }
        public DateTime? ActivityDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
