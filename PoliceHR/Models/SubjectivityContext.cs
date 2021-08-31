using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PoliceHR.ViewModels.SqLiteViewModels;

#nullable disable

namespace PoliceHR.Models
{
    public partial class SubjectivityContext : DbContext
    {
        public SubjectivityContext()
        {
        }

        public SubjectivityContext(DbContextOptions<SubjectivityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accident> Accidents { get; set; }
        public virtual DbSet<ActivtisRecourd> ActivtisRecourds { get; set; }
        public virtual DbSet<Adjective> Adjectives { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ClassChange> ClassChanges { get; set; }
        public virtual DbSet<Convalescence> Convalescences { get; set; }
        public virtual DbSet<Cycle> Cycles { get; set; }
        public virtual DbSet<Decoration> Decorations { get; set; }
        public virtual DbSet<DeficitRation> DeficitRations { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<Desertion> Desertions { get; set; }
        public virtual DbSet<Dgree> Dgrees { get; set; }
        public virtual DbSet<Doc> Docs { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<FinancialLevel> FinancialLevels { get; set; }
        public virtual DbSet<Fired> Fireds { get; set; }
        public virtual DbSet<Imprison> Imprisons { get; set; }
        public virtual DbSet<Layoff> Layoffs { get; set; }
        public virtual DbSet<LegalState> LegalStates { get; set; }
        public virtual DbSet<Lost> Losts { get; set; }
        public virtual DbSet<Martyr> Martyrs { get; set; }
        public virtual DbSet<MilitaryConscription> MilitaryConscriptions { get; set; }
        public virtual DbSet<Penalty> Penalties { get; set; }
        public virtual DbSet<PhonesNumber> PhonesNumbers { get; set; }
        public virtual DbSet<PoliceServicesMovement> PoliceServicesMovements { get; set; }
        public virtual DbSet<PoliceState> PoliceStates { get; set; }
        public virtual DbSet<PreviousService> PreviousServices { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<Reserve> Reserves { get; set; }
        public virtual DbSet<Retention> Retentions { get; set; }
        public virtual DbSet<Seizure> Seizures { get; set; }
        public virtual DbSet<ServiceState> ServiceStates { get; set; }
        public virtual DbSet<SocialAccount> SocialAccounts { get; set; }
        public virtual DbSet<Unite> Unites { get; set; }
        public virtual DbSet<Upgrade> Upgrades { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<WiveOrHusband> WiveOrHusbands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                SqLiteVm ss = new SqLiteVm();
                if (ss.ConnStringSource() == "") { return; }
                optionsBuilder.UseSqlServer(ss.ConnStringSource());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Accident>(entity =>
            {
                entity.HasKey(e => e.AccidentsId);

                entity.Property(e => e.AccidentsId).HasColumnName("Accidents_ID");

                entity.Property(e => e.AccidentsDate)
                    .HasColumnType("date")
                    .HasColumnName("Accidents_Date");

                entity.Property(e => e.AccidentsSeizure).HasColumnName("Accidents_Seizure");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.IsAtWork).HasColumnName("Is_At_Work");

                entity.Property(e => e.MedicReport)
                    .HasMaxLength(100)
                    .HasColumnName("Medic_Report");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Accidents)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Accidents_Elements");
            });

            modelBuilder.Entity<ActivtisRecourd>(entity =>
            {
                entity.HasKey(e => e.ActivityId);

                entity.ToTable("Activtis_Recourds");

                entity.Property(e => e.ActivityId).HasColumnName("Activity_ID");

                entity.Property(e => e.ActivityDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Activity_DateTime");

                entity.Property(e => e.ActivityDes)
                    .HasMaxLength(120)
                    .HasColumnName("Activity_Des");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActivtisRecourds)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Activtis_Recourds_Users");
            });

            modelBuilder.Entity<Adjective>(entity =>
            {
                entity.ToTable("Adjective");

                entity.Property(e => e.AdjectiveId).HasColumnName("Adjective_ID");

                entity.Property(e => e.AdjectiveName)
                    .HasMaxLength(20)
                    .HasColumnName("Adjective_Name");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Adjectives)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Adjective_Elements");
            });

            modelBuilder.Entity<Child>(entity =>
            {
                entity.Property(e => e.ChildId).HasColumnName("Child_ID");

                entity.Property(e => e.ChildBrithDate)
                    .HasColumnType("date")
                    .HasColumnName("Child_Brith_Date");

                entity.Property(e => e.ChildBrithLocation)
                    .HasMaxLength(50)
                    .HasColumnName("Child_Brith_Location");

                entity.Property(e => e.ChildName)
                    .HasMaxLength(50)
                    .HasColumnName("Child_Name");

                entity.Property(e => e.ChildPersonalIdNumber)
                    .HasMaxLength(70)
                    .HasColumnName("Child_Personal_ID_Number");

                entity.Property(e => e.CivilRegistrationImage)
                    .HasColumnType("image")
                    .HasColumnName("Civil_Registration_Image");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.IsAlive).HasColumnName("Is_Alive");

                entity.Property(e => e.WiveOrHusbandId).HasColumnName("WiveOrHusband_ID");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Children_Elements");

                entity.HasOne(d => d.WiveOrHusband)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.WiveOrHusbandId)
                    .HasConstraintName("FK_Children_WiveOrHusband");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.Cities).HasMaxLength(20);
            });

            modelBuilder.Entity<ClassChange>(entity =>
            {
                entity.HasKey(e => e.ClassChangesId);

                entity.ToTable("Class_Changes");

                entity.Property(e => e.ClassChangesId).HasColumnName("Class_Changes_ID");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("Class_Name");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Order_Number");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.ClassChanges)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Class_Changes_Elements");
            });

            modelBuilder.Entity<Convalescence>(entity =>
            {
                entity.ToTable("Convalescence");

                entity.Property(e => e.ConvalescenceId).HasColumnName("Convalescence_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IsReview).HasColumnName("Is_review");

                entity.Property(e => e.MedicReport)
                    .HasMaxLength(50)
                    .HasColumnName("Medic_Report");

                entity.Property(e => e.MedicReportImage)
                    .HasColumnType("image")
                    .HasColumnName("Medic_Report_Image");

                entity.Property(e => e.Reason).HasMaxLength(20);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Convalescences)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Convalescence_Elements");
            });

            modelBuilder.Entity<Cycle>(entity =>
            {
                entity.Property(e => e.CycleId).HasColumnName("Cycle_ID");

                entity.Property(e => e.CycleEndDate)
                    .HasColumnType("date")
                    .HasColumnName("Cycle_EndDate");

                entity.Property(e => e.CycleName)
                    .HasMaxLength(50)
                    .HasColumnName("Cycle_Name");

                entity.Property(e => e.CycleStartDate)
                    .HasColumnType("date")
                    .HasColumnName("Cycle_StartDate");

                entity.Property(e => e.Descraption).HasMaxLength(50);

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Order_Number");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Cycles)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cycles_Elements");
            });

            modelBuilder.Entity<Decoration>(entity =>
            {
                entity.Property(e => e.DecorationId).HasColumnName("Decoration_ID");

                entity.Property(e => e.DecorationsName)
                    .HasMaxLength(20)
                    .HasColumnName("Decorations_Name");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Order_Number")
                    .IsFixedLength(true);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.Source).HasMaxLength(20);

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Decorations)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Decorations_Elements");
            });

            modelBuilder.Entity<DeficitRation>(entity =>
            {
                entity.HasKey(e => e.DeficitRatio);

                entity.ToTable("Deficit_ration");

                entity.Property(e => e.DeficitRatio).HasColumnName("Deficit_ratio");

                entity.Property(e => e.DeficitRatioOrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Deficit_ratio_Order_Date");

                entity.Property(e => e.DeficitRatioOrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Deficit_ratio_Order_Image");

                entity.Property(e => e.DeficitRatioOrderResult)
                    .HasMaxLength(50)
                    .HasColumnName("Deficit_ratio_Order_Result");

                entity.Property(e => e.DeficitRatioPercentage).HasColumnName("Deficit_ratio_percentage");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(15)
                    .HasColumnName("Order_Num");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.DeficitRations)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Deficit_ration_Elements");
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasKey(e => e.DescriptionsId);

                entity.Property(e => e.DescriptionsId).HasColumnName("Descriptions_ID");

                entity.Property(e => e.AppearanceValue).HasColumnName("Appearance_value");

                entity.Property(e => e.DescriptionDate)
                    .HasColumnType("date")
                    .HasColumnName("Description_Date");

                entity.Property(e => e.DescriptionImage)
                    .HasColumnType("image")
                    .HasColumnName("Description_image");

                entity.Property(e => e.DescriptionOfficer)
                    .HasMaxLength(70)
                    .HasColumnName("Description_Officer");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EthicsValue).HasColumnName("Ethics_value");

                entity.Property(e => e.FunctionalValue).HasColumnName("Functional_value");

                entity.Property(e => e.Others).HasMaxLength(100);

                entity.Property(e => e.PhysicalValue).HasColumnName("Physical_value");

                entity.Property(e => e.SpecialsValue)
                    .HasMaxLength(20)
                    .HasColumnName("Specials_value");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Descriptions)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Descriptions_Elements");
            });

            modelBuilder.Entity<Desertion>(entity =>
            {
                entity.Property(e => e.DesertionId).HasColumnName("Desertion_ID");

                entity.Property(e => e.DesertionDate)
                    .HasColumnType("date")
                    .HasColumnName("Desertion_Date");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.Rejoin).HasColumnType("date");

                entity.Property(e => e.RejoinImage)
                    .HasColumnType("image")
                    .HasColumnName("Rejoin_Image");

                entity.Property(e => e.RejoinTelegram)
                    .HasMaxLength(30)
                    .HasColumnName("Rejoin_Telegram");

                entity.Property(e => e.TelegramDate)
                    .HasColumnType("date")
                    .HasColumnName("Telegram_Date");

                entity.Property(e => e.TelegramImage)
                    .HasColumnType("image")
                    .HasColumnName("Telegram_Image");

                entity.Property(e => e.TelegramNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Telegram_number");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Desertions)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Desertions_Elements");
            });

            modelBuilder.Entity<Dgree>(entity =>
            {
                entity.Property(e => e.DgreeId).HasColumnName("Dgree_ID");

                entity.Property(e => e.CertifiedGraduatedImage)
                    .HasColumnType("image")
                    .HasColumnName("Certified_graduated_Image");

                entity.Property(e => e.DgreeDuration)
                    .HasMaxLength(20)
                    .HasColumnName("Dgree_Duration");

                entity.Property(e => e.DgreeName)
                    .HasMaxLength(50)
                    .HasColumnName("Dgree_Name");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.GraduationDate)
                    .HasColumnType("date")
                    .HasColumnName("Graduation_Date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Dgrees)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Dgrees_Elements");
            });

            modelBuilder.Entity<Doc>(entity =>
            {
                entity.Property(e => e.DocId).HasColumnName("Doc_ID");

                entity.Property(e => e.DocImage)
                    .HasColumnType("image")
                    .HasColumnName("Doc_Image");

                entity.Property(e => e.DocInfo)
                    .HasMaxLength(120)
                    .HasColumnName("Doc_Info");

                entity.Property(e => e.DocName)
                    .HasMaxLength(20)
                    .HasColumnName("Doc_Name");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElmId);

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.ElmAdress)
                    .HasMaxLength(100)
                    .HasColumnName("ELM_Adress");

                entity.Property(e => e.ElmBlockNumber)
                    .HasMaxLength(30)
                    .HasColumnName("ELM_Block_Number");

                entity.Property(e => e.ElmBrithDate)
                    .HasColumnType("date")
                    .HasColumnName("ELM_Brith_Date");

                entity.Property(e => e.ElmBrithLocation)
                    .HasMaxLength(20)
                    .HasColumnName("ELM_Brith_Location");

                entity.Property(e => e.ElmFamilyState).HasColumnName("ELM_Family_State");

                entity.Property(e => e.ElmFather)
                    .HasMaxLength(20)
                    .HasColumnName("ELM_Father");

                entity.Property(e => e.ElmGendar).HasColumnName("ELM_Gendar");

                entity.Property(e => e.ElmIdType).HasColumnName("ELM_ID_Type");

                entity.Property(e => e.ElmIsRecruiter).HasColumnName("ELM_is_Recruiter");

                entity.Property(e => e.ElmLastName)
                    .HasMaxLength(50)
                    .HasColumnName("ELM_Last_Name");

                entity.Property(e => e.ElmMNumber)
                    .HasMaxLength(25)
                    .HasColumnName("ELM_M_Number");

                entity.Property(e => e.ElmMother)
                    .HasMaxLength(20)
                    .HasColumnName("ELM_Mother");

                entity.Property(e => e.ElmName)
                    .HasMaxLength(20)
                    .HasColumnName("ELM_Name");

                entity.Property(e => e.ElmNoteMNumber).HasColumnName("ELM_Note_M_Number");

                entity.Property(e => e.ElmPersonalIdNumber)
                    .HasMaxLength(50)
                    .HasColumnName("ELM_Personal_ID_Number");

                entity.Property(e => e.ElmPhoto)
                    .HasColumnType("image")
                    .HasColumnName("ELM_Photo");

                entity.Property(e => e.ElmRank).HasColumnName("ELM_Rank");

                entity.Property(e => e.ElmRecruitmentDivision)
                    .HasMaxLength(50)
                    .HasColumnName("ELM_Recruitment_Division");

                entity.Property(e => e.ElmServiceState).HasColumnName("ELM_Service_State");

                entity.Property(e => e.ElmStartDate)
                    .HasColumnType("date")
                    .HasColumnName("ELM_StartDate");

                entity.Property(e => e.UnitId).HasColumnName("Unit_ID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Elements_Cities");

                entity.HasOne(d => d.ElmRankNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.ElmRank)
                    .HasConstraintName("FK_Elements_Rank");

                entity.HasOne(d => d.ElmServiceStateNavigation)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.ElmServiceState)
                    .HasConstraintName("FK_Elements_Service_State");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Elements)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Elements_Unites");
            });

            modelBuilder.Entity<FinancialLevel>(entity =>
            {
                entity.HasKey(e => e.FinancialLevel1);

                entity.ToTable("Financial_levels");

                entity.Property(e => e.FinancialLevel1).HasColumnName("Financial_level");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FinancialName)
                    .HasMaxLength(20)
                    .HasColumnName("Financial_Name");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.FinancialLevels)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Financial_levels_Elements");
            });

            modelBuilder.Entity<Fired>(entity =>
            {
                entity.ToTable("Fired");

                entity.Property(e => e.FiredId).HasColumnName("Fired_ID");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Order_Number");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Fireds)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Fired_Elements");
            });

            modelBuilder.Entity<Imprison>(entity =>
            {
                entity.ToTable("Imprison");

                entity.Property(e => e.ImprisonId).HasColumnName("Imprison_ID");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.EndOrderNum)
                    .HasMaxLength(30)
                    .HasColumnName("End_Order_Num");

                entity.Property(e => e.ImprisonDate)
                    .HasColumnType("date")
                    .HasColumnName("Imprison_Date");

                entity.Property(e => e.ImprisonEntity)
                    .HasMaxLength(50)
                    .HasColumnName("Imprison_Entity");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Order_Number");

                entity.Property(e => e.Reason).HasMaxLength(40);

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Imprisons)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Imprison_Elements");
            });

            modelBuilder.Entity<Layoff>(entity =>
            {
                entity.HasKey(e => e.LayoffsId);

                entity.Property(e => e.LayoffsId).HasColumnName("Layoffs_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Order_Number");

                entity.Property(e => e.Reason).HasMaxLength(20);

                entity.Property(e => e.SeizureDate)
                    .HasColumnType("date")
                    .HasColumnName("Seizure_Date");

                entity.Property(e => e.SeizureImage)
                    .HasColumnType("image")
                    .HasColumnName("Seizure_Image");

                entity.Property(e => e.SeizureNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Seizure_Number");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Layoffs)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Layoffs_Elements");
            });

            modelBuilder.Entity<LegalState>(entity =>
            {
                entity.ToTable("Legal_state");

                entity.Property(e => e.LegalStateId).HasColumnName("Legal_state_ID");

                entity.Property(e => e.CourtName)
                    .HasMaxLength(40)
                    .HasColumnName("Court_Name");

                entity.Property(e => e.CrimeName)
                    .HasMaxLength(30)
                    .HasColumnName("Crime_Name");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.LagelCase).HasColumnName("Lagel_Case");

                entity.Property(e => e.LegalDescraption)
                    .HasMaxLength(50)
                    .HasColumnName("Legal_Descraption");

                entity.Property(e => e.LegalNumberDate)
                    .HasMaxLength(50)
                    .HasColumnName("Legal_Number_Date");

                entity.Property(e => e.Result).HasMaxLength(100);

                entity.Property(e => e.RulingImage)
                    .HasColumnType("image")
                    .HasColumnName("Ruling_Image");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.LegalStates)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Legal_state_Elements");
            });

            modelBuilder.Entity<Lost>(entity =>
            {
                entity.ToTable("Lost");

                entity.Property(e => e.LostId).HasColumnName("Lost_ID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.Location).HasMaxLength(30);

                entity.Property(e => e.LostDate)
                    .HasColumnType("date")
                    .HasColumnName("Lost_Date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Order_Number");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Losts)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Lost_Elements");
            });

            modelBuilder.Entity<Martyr>(entity =>
            {
                entity.ToTable("Martyr");

                entity.Property(e => e.MartyrId).HasColumnName("martyr_ID");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.Location).HasMaxLength(25);

                entity.Property(e => e.MartyrDate)
                    .HasColumnType("date")
                    .HasColumnName("martyr_Date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Order_Number");

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Martyrs)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Martyr_Elements");
            });

            modelBuilder.Entity<MilitaryConscription>(entity =>
            {
                entity.ToTable("Military_Conscription");

                entity.Property(e => e.MilitaryConscriptionId).HasColumnName("Military_Conscription_ID");

                entity.Property(e => e.CycleNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Cycle_Number");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.MilitaryConscriptions)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Military_Conscription_Elements");
            });

            modelBuilder.Entity<Penalty>(entity =>
            {
                entity.HasKey(e => e.PenaltiesId);

                entity.Property(e => e.PenaltiesId).HasColumnName("Penalties_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(70)
                    .HasColumnName("Name_");

                entity.Property(e => e.PenaltieDate)
                    .HasColumnType("date")
                    .HasColumnName("Penaltie_date");

                entity.Property(e => e.PenaltieImage)
                    .HasColumnType("image")
                    .HasColumnName("Penaltie_image");

                entity.Property(e => e.PenaltieNotes)
                    .HasMaxLength(10)
                    .HasColumnName("Penaltie_Notes")
                    .IsFixedLength(true);

                entity.Property(e => e.PenaltieNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Penaltie_number");

                entity.Property(e => e.PenaltieType).HasColumnName("Penaltie_type");

                entity.Property(e => e.PenaltiesReason)
                    .HasMaxLength(50)
                    .HasColumnName("Penalties_reason");

                entity.Property(e => e.Rank).HasMaxLength(20);

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Penalties)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Penalties_Elements");
            });

            modelBuilder.Entity<PhonesNumber>(entity =>
            {
                entity.HasKey(e => e.PhoneId);

                entity.ToTable("Phones_Numbers");

                entity.Property(e => e.PhoneId).HasColumnName("Phone_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_Number");

                entity.Property(e => e.PhoneType).HasColumnName("Phone_Type");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.PhonesNumbers)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Phones_Numbers_Elements");
            });

            modelBuilder.Entity<PoliceServicesMovement>(entity =>
            {
                entity.HasKey(e => e.PoliceServicesId)
                    .HasName("PK_Previous_Services");

                entity.ToTable("Police_Services_Movements");

                entity.Property(e => e.PoliceServicesId).HasColumnName("Police_services_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.OrdarNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Ordar_Number");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderSource)
                    .HasMaxLength(20)
                    .HasColumnName("Order_source");

                entity.Property(e => e.PoliceUnitName)
                    .HasMaxLength(50)
                    .HasColumnName("Police_unit_Name");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(20)
                    .HasColumnName("Service_Name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.StateCity)
                    .HasMaxLength(20)
                    .HasColumnName("State_City");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.PoliceServicesMovements)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Police_Services_Movements_Elements");
            });

            modelBuilder.Entity<PoliceState>(entity =>
            {
                entity.ToTable("Police_State");

                entity.Property(e => e.PoliceStateId).HasColumnName("Police_State_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(20)
                    .HasColumnName("Order_Num");

                entity.Property(e => e.Result).HasMaxLength(100);

                entity.Property(e => e.SeizureDate)
                    .HasColumnType("date")
                    .HasColumnName("Seizure_Date");

                entity.Property(e => e.SeizureNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Seizure_number");

                entity.Property(e => e.StateImage)
                    .HasColumnType("image")
                    .HasColumnName("State_Image");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.PoliceStates)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Police_State_Elements");
            });

            modelBuilder.Entity<PreviousService>(entity =>
            {
                entity.HasKey(e => e.PreviousServicesId)
                    .HasName("PK_Previous_Services_1");

                entity.ToTable("Previous_Services");

                entity.Property(e => e.PreviousServicesId).HasColumnName("Previous_services_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FoundationName)
                    .HasMaxLength(30)
                    .HasColumnName("Foundation_Name");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(50)
                    .HasColumnName("Service_Name");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.PreviousServices)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Previous_Services_Elements");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("Rank");

                entity.Property(e => e.RankId).HasColumnName("Rank_ID");

                entity.Property(e => e.RankName)
                    .HasMaxLength(30)
                    .HasColumnName("Rank_Name");
            });

            modelBuilder.Entity<Reserve>(entity =>
            {
                entity.ToTable("Reserve");

                entity.Property(e => e.ReserveId).HasColumnName("Reserve_ID");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(50)
                    .HasColumnName("Order_Num");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reserve_Elements");
            });

            modelBuilder.Entity<Retention>(entity =>
            {
                entity.ToTable("Retention");

                entity.Property(e => e.RetentionId).HasColumnName("Retention_ID");

                entity.Property(e => e.ElementId).HasColumnName("Element_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_image");

                entity.Property(e => e.OrderNum)
                    .HasMaxLength(50)
                    .HasColumnName("Order_Num");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.Element)
                    .WithMany(p => p.Retentions)
                    .HasForeignKey(d => d.ElementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Retention_Elements");
            });

            modelBuilder.Entity<Seizure>(entity =>
            {
                entity.Property(e => e.SeizureId).HasColumnName("Seizure_ID");

                entity.Property(e => e.Conclusion).HasMaxLength(100);

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.SeizureDate)
                    .HasColumnType("date")
                    .HasColumnName("Seizure_Date");

                entity.Property(e => e.SeizureImage)
                    .HasColumnType("image")
                    .HasColumnName("Seizure_Image");

                entity.Property(e => e.SeizureNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Seizure_number");

                entity.Property(e => e.SeizureUnit)
                    .HasMaxLength(40)
                    .HasColumnName("Seizure_Unit");

                entity.Property(e => e.SeizureWriter)
                    .HasMaxLength(20)
                    .HasColumnName("Seizure_writer");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Seizures)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Seizures_Elements");
            });

            modelBuilder.Entity<ServiceState>(entity =>
            {
                entity.ToTable("Service_State");

                entity.Property(e => e.ServiceStateId).HasColumnName("ServiceState_ID");

                entity.Property(e => e.ServiceState1)
                    .HasMaxLength(30)
                    .HasColumnName("ServiceState");
            });

            modelBuilder.Entity<SocialAccount>(entity =>
            {
                entity.HasKey(e => e.SocialAccountsId);

                entity.ToTable("Social_accounts");

                entity.Property(e => e.SocialAccountsId).HasColumnName("Social_accounts_ID");

                entity.Property(e => e.AccountAdress)
                    .HasMaxLength(100)
                    .HasColumnName("Account_Adress");

                entity.Property(e => e.Descraption).HasMaxLength(20);

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.SocialAccounts)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Social_accounts_Elements");
            });

            modelBuilder.Entity<Unite>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.Property(e => e.UnitId).HasColumnName("Unit_ID");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(40)
                    .HasColumnName("Unit_Name");
            });

            modelBuilder.Entity<Upgrade>(entity =>
            {
                entity.Property(e => e.UpgradeId).HasColumnName("Upgrade_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OrderImage)
                    .HasColumnType("image")
                    .HasColumnName("Order_Image");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Order_Number");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_date");

                entity.Property(e => e.UpgradeName).HasColumnName("Upgrade_Name");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Upgrades)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Upgrades_Elements");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.Password).HasMaxLength(25);

                entity.Property(e => e.Role).HasMaxLength(10);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<Vacation>(entity =>
            {
                entity.HasKey(e => e.VacationsId);

                entity.Property(e => e.VacationsId).HasColumnName("Vacations_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Source).HasMaxLength(15);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.VacationType).HasColumnName("Vacation_Type");

                entity.Property(e => e.VacationsDate)
                    .HasColumnType("date")
                    .HasColumnName("Vacations_date");

                entity.Property(e => e.VacationsImage)
                    .HasColumnType("image")
                    .HasColumnName("Vacations_Image");

                entity.Property(e => e.VacationsLocation)
                    .HasMaxLength(20)
                    .HasColumnName("Vacations_Location");

                entity.Property(e => e.VacationsNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Vacations_Number");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.Vacations)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Vacations_Elements");
            });

            modelBuilder.Entity<WiveOrHusband>(entity =>
            {
                entity.ToTable("WiveOrHusband");

                entity.Property(e => e.WiveOrHusbandId).HasColumnName("WiveOrHusband_ID");

                entity.Property(e => e.ElmId).HasColumnName("ELM_ID");

                entity.Property(e => e.IsRelationshipExist).HasColumnName("Is_Relationship_Exist");

                entity.Property(e => e.MarriageContractDate)
                    .HasColumnType("date")
                    .HasColumnName("Marriage_Contract_Date");

                entity.Property(e => e.MarriageLicenseDate)
                    .HasColumnType("date")
                    .HasColumnName("Marriage_license_Date");

                entity.Property(e => e.MarriageLicenseImage)
                    .HasColumnType("image")
                    .HasColumnName("Marriage_license_Image");

                entity.Property(e => e.MarriageLicenseNumber)
                    .HasMaxLength(30)
                    .HasColumnName("Marriage_license_Number");

                entity.Property(e => e.RegistrationMarriagePoliceDate)
                    .HasColumnType("date")
                    .HasColumnName("Registration_marriage_Police_Date");

                entity.Property(e => e.WiveOrHusbandBlockNumber)
                    .HasMaxLength(30)
                    .HasColumnName("WiveOrHusband_Block_Number");

                entity.Property(e => e.WiveOrHusbandBrithDate)
                    .HasColumnType("date")
                    .HasColumnName("WiveOrHusband_Brith_date");

                entity.Property(e => e.WiveOrHusbandBrithLocation)
                    .HasMaxLength(50)
                    .HasColumnName("WiveOrHusband_Brith_location");

                entity.Property(e => e.WiveOrHusbandCivilRegistrationImage)
                    .HasColumnType("image")
                    .HasColumnName("WiveOrHusband_Civil_Registration_Image");

                entity.Property(e => e.WiveOrHusbandFatherName)
                    .HasMaxLength(25)
                    .HasColumnName("WiveOrHusband_Father_Name");

                entity.Property(e => e.WiveOrHusbandIsAlive).HasColumnName("WiveOrHusband_Is_Alive");

                entity.Property(e => e.WiveOrHusbandJob)
                    .HasMaxLength(25)
                    .HasColumnName("WiveOrHusband_Job");

                entity.Property(e => e.WiveOrHusbandLastName)
                    .HasMaxLength(50)
                    .HasColumnName("WiveOrHusband_Last_Name");

                entity.Property(e => e.WiveOrHusbandMoherName)
                    .HasMaxLength(70)
                    .HasColumnName("WiveOrHusband_Moher_Name");

                entity.Property(e => e.WiveOrHusbandName)
                    .HasMaxLength(25)
                    .HasColumnName("WiveOrHusband_Name");

                entity.Property(e => e.WiveOrHusbandNationality)
                    .HasMaxLength(30)
                    .HasColumnName("WiveOrHusband_Nationality");

                entity.Property(e => e.WiveOrHusbandPersonalIdNumber)
                    .HasMaxLength(70)
                    .HasColumnName("WiveOrHusband_Personal_ID_Number");

                entity.HasOne(d => d.Elm)
                    .WithMany(p => p.WiveOrHusbands)
                    .HasForeignKey(d => d.ElmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_WiveOrHusband_Elements");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
