using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class WiveOrHusband
    {
        public WiveOrHusband()
        {
            Children = new HashSet<Child>();
        }

        public int WiveOrHusbandId { get; set; }
        public string WiveOrHusbandName { get; set; }
        public string WiveOrHusbandLastName { get; set; }
        public string WiveOrHusbandFatherName { get; set; }
        public string WiveOrHusbandMoherName { get; set; }
        public string WiveOrHusbandPersonalIdNumber { get; set; }
        public DateTime? WiveOrHusbandBrithDate { get; set; }
        public string WiveOrHusbandBrithLocation { get; set; }
        public string WiveOrHusbandBlockNumber { get; set; }
        public string WiveOrHusbandNationality { get; set; }
        public string MarriageLicenseNumber { get; set; }
        public DateTime? MarriageLicenseDate { get; set; }
        public byte[] MarriageLicenseImage { get; set; }
        public DateTime? MarriageContractDate { get; set; }
        public DateTime? RegistrationMarriagePoliceDate { get; set; }
        public bool? IsRelationshipExist { get; set; }
        public bool? WiveOrHusbandIsAlive { get; set; }
        public byte[] WiveOrHusbandCivilRegistrationImage { get; set; }
        public string WiveOrHusbandJob { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}
