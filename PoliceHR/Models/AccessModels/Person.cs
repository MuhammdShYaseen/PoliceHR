using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceHR.Models.AccessModels
{
    public partial class Person
    {
        public string NationalNumber { get; set; }
        public int MilNumber { get; set; }
        public string MilNumberSlice { get; set; }
        public int ServiceType { get; set; }
        public int ServiceState { get; set; }
        public int? MilRank { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string FamilyName { get; set; }
        public string MotherName { get; set; }
        public int CurrUnit { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BirthPlace { get; set; }
        public int? EnlistBranch { get; set; }
        public int? Education1 { get; set; }
        public DateTime? MilRankDate { get; set; }
        public DateTime? VolunteerDate { get; set; }
        public DateTime? MandatoryStartDate { get; set; }
        public DateTime? MandatoryEndDate { get; set; }
        public DateTime? EhtStartDate { get; set; }
        public int? MilSpec { get; set; }
        public string Adress { get; set; }
    }
}
