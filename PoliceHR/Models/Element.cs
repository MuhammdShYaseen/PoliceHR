using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class Element
    {
        public Element()
        {
            Accidents = new HashSet<Accident>();
            Adjectives = new HashSet<Adjective>();
            Children = new HashSet<Child>();
            ClassChanges = new HashSet<ClassChange>();
            Convalescences = new HashSet<Convalescence>();
            Cycles = new HashSet<Cycle>();
            Decorations = new HashSet<Decoration>();
            DeficitRations = new HashSet<DeficitRation>();
            Descriptions = new HashSet<Description>();
            Desertions = new HashSet<Desertion>();
            Dgrees = new HashSet<Dgree>();
            FinancialLevels = new HashSet<FinancialLevel>();
            Fireds = new HashSet<Fired>();
            Imprisons = new HashSet<Imprison>();
            Layoffs = new HashSet<Layoff>();
            LegalStates = new HashSet<LegalState>();
            Losts = new HashSet<Lost>();
            Martyrs = new HashSet<Martyr>();
            MilitaryConscriptions = new HashSet<MilitaryConscription>();
            Penalties = new HashSet<Penalty>();
            PhonesNumbers = new HashSet<PhonesNumber>();
            PoliceServicesMovements = new HashSet<PoliceServicesMovement>();
            PoliceStates = new HashSet<PoliceState>();
            PreviousServices = new HashSet<PreviousService>();
            Reserves = new HashSet<Reserve>();
            Retentions = new HashSet<Retention>();
            Seizures = new HashSet<Seizure>();
            SocialAccounts = new HashSet<SocialAccount>();
            Upgrades = new HashSet<Upgrade>();
            Vacations = new HashSet<Vacation>();
            WiveOrHusbands = new HashSet<WiveOrHusband>();
        }

        public int ElmId { get; set; }
        public string ElmName { get; set; }
        public string ElmLastName { get; set; }
        public string ElmFather { get; set; }
        public string ElmMother { get; set; }
        public string ElmBrithLocation { get; set; }
        public DateTime? ElmBrithDate { get; set; }
        public string ElmBlockNumber { get; set; }
        public string ElmAdress { get; set; }
        public string ElmMNumber { get; set; }
        public string ElmPersonalIdNumber { get; set; }
        public int? ElmFamilyState { get; set; }
        public string ElmRecruitmentDivision { get; set; }
        public int? ElmServiceState { get; set; }
        public bool? ElmIdType { get; set; }
        public bool? ElmIsRecruiter { get; set; }
        public byte[] ElmPhoto { get; set; }
        public bool? ElmGendar { get; set; }
        public int? ElmNoteMNumber { get; set; }
        public DateTime? ElmStartDate { get; set; }
        public int? ElmRank { get; set; }
        public int? UnitId { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Rank ElmRankNavigation { get; set; }
        public virtual ServiceState ElmServiceStateNavigation { get; set; }
        public virtual Unite Unit { get; set; }
        public virtual ICollection<Accident> Accidents { get; set; }
        public virtual ICollection<Adjective> Adjectives { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<ClassChange> ClassChanges { get; set; }
        public virtual ICollection<Convalescence> Convalescences { get; set; }
        public virtual ICollection<Cycle> Cycles { get; set; }
        public virtual ICollection<Decoration> Decorations { get; set; }
        public virtual ICollection<DeficitRation> DeficitRations { get; set; }
        public virtual ICollection<Description> Descriptions { get; set; }
        public virtual ICollection<Desertion> Desertions { get; set; }
        public virtual ICollection<Dgree> Dgrees { get; set; }
        public virtual ICollection<FinancialLevel> FinancialLevels { get; set; }
        public virtual ICollection<Fired> Fireds { get; set; }
        public virtual ICollection<Imprison> Imprisons { get; set; }
        public virtual ICollection<Layoff> Layoffs { get; set; }
        public virtual ICollection<LegalState> LegalStates { get; set; }
        public virtual ICollection<Lost> Losts { get; set; }
        public virtual ICollection<Martyr> Martyrs { get; set; }
        public virtual ICollection<MilitaryConscription> MilitaryConscriptions { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }
        public virtual ICollection<PhonesNumber> PhonesNumbers { get; set; }
        public virtual ICollection<PoliceServicesMovement> PoliceServicesMovements { get; set; }
        public virtual ICollection<PoliceState> PoliceStates { get; set; }
        public virtual ICollection<PreviousService> PreviousServices { get; set; }
        public virtual ICollection<Reserve> Reserves { get; set; }
        public virtual ICollection<Retention> Retentions { get; set; }
        public virtual ICollection<Seizure> Seizures { get; set; }
        public virtual ICollection<SocialAccount> SocialAccounts { get; set; }
        public virtual ICollection<Upgrade> Upgrades { get; set; }
        public virtual ICollection<Vacation> Vacations { get; set; }
        public virtual ICollection<WiveOrHusband> WiveOrHusbands { get; set; }
    }
}
