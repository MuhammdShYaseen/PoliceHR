using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceHR.Models.AccessModels
{
    public partial class EnlistBranch
    {
        public string TableName { get; set; }
        public double? Encoded { get; set; }
        public string Decoded { get; set; }
        public double? FatherCode { get; set; }
        public double? RecState { get; set; }
        public double? DispOrder { get; set; }
    }
}
