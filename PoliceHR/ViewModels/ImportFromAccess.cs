using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PoliceHR.Models;
using PoliceHR.Models.AccessModels;
using PoliceHR.ViewModels.AccessViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PoliceHR.ViewModels
{
    public class ImportFromAccess
    {
        public string OpenDB(TextBlock txtPath)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "قاعدة بيانات أكسس|*.mdb||*.accdb||*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    string AccFile = fileUri.LocalPath;
                    txtPath.Text = AccFile;
                    return AccFile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
                return "";
            }
        }
       async public void AcyncFromAccessToSql(string DbPath,ProgressBar PrgAyc,Button BtnAyc,Button BtnClose,Button BtnSelectDB)
        {
            PrgAyc.Visibility = Visibility.Visible;
            BtnAyc.IsEnabled = false;
            BtnAyc.Content = "يرجى الانتظار";
            BtnClose.IsEnabled = false;
            BtnSelectDB.IsEnabled = false;
            PrgAyc.Value = 0;
            AccVM ss = new AccVM(DbPath);
            if (ss.ElementsResource() == null)
            {
                BtnAyc.IsEnabled = true;
                BtnAyc.Content = "مزامنة البيانات";
                PrgAyc.Visibility = Visibility.Hidden;
                BtnClose.IsEnabled = true;
                return;
            }
            PrgAyc.Maximum = ss.ElementsResource().Count;
            SubjectivityContext HR = new SubjectivityContext();
            DbSet<Element> table = HR.Elements;
            string NationNum = "";
            foreach (Person InAcc in ss.ElementsResource())
            {
                NationNum = InAcc.NationalNumber;
                List<Element> searchresult =await table.Where(c => c.ElmPersonalIdNumber.Contains(NationNum)).ToListAsync();
                if (searchresult.Count() == 0)
                {
                    using (SubjectivityContext HRR = new SubjectivityContext())
                    {
                        object ELM = new Element { ElmName = InAcc.FirstName, ElmAdress = null, ElmBlockNumber = null, ElmBrithDate = InAcc.BirthDate, ElmBrithLocation = PlacesName((int)InAcc.BirthPlace,DbPath), ElmFamilyState = 0, ElmFather = InAcc.FatherName, ElmGendar = true, ElmIdType = true, ElmIsRecruiter = true, ElmLastName = InAcc.FamilyName, ElmMNumber = InAcc.MilNumber.ToString(), ElmMother = InAcc.MotherName, ElmNoteMNumber = null, ElmPersonalIdNumber = InAcc.NationalNumber, ElmPhoto = null, ElmRank = (int)InAcc.MilRank, ElmRecruitmentDivision = BrName((int)InAcc.EnlistBranch, DbPath), ElmServiceState =ServicStateAsync(InAcc.ServiceState), ElmStartDate = InAcc.MandatoryStartDate, UnitId = 1, CityId = 1 };
                        _ = HR.Add(ELM);
                        _ = await HR.SaveChangesAsync();
                        NationNum = "";
                        PrgAyc.Value += 1;
                    }
                }
                else if (searchresult.Count() == 1)
                {
                    using (SubjectivityContext HRR = new SubjectivityContext())
                    {
                        Element SS = HR.Elements.First(a => a.ElmPersonalIdNumber == NationNum);
                        SS.ElmName = InAcc.FirstName;
                        //if (InAcc.Adress != string.Empty) { SS.ElmAdress = InAcc.Adress; } else if (InAcc.Adress == string.Empty) { SS.ElmAdress = SS.ElmAdress; };
                        SS.ElmBlockNumber = null;
                        SS.ElmBrithDate = InAcc.BirthDate;
                        SS.ElmBrithLocation = PlacesName((int)InAcc.BirthPlace, DbPath);
                        SS.ElmFamilyState = 0;
                        SS.ElmFather = InAcc.FatherName;
                        SS.ElmGendar = true;
                        SS.ElmIdType = true;
                        SS.ElmIsRecruiter = true;
                        SS.ElmLastName = InAcc.FamilyName;
                        SS.ElmMNumber = InAcc.MilNumber.ToString();
                        SS.ElmMother = InAcc.MotherName;
                        SS.ElmNoteMNumber = 0;
                        SS.ElmPersonalIdNumber = InAcc.NationalNumber;
                        SS.ElmRank = RankAsync((int)InAcc.MilRank);
                        SS.ElmRecruitmentDivision = BrName((int)InAcc.EnlistBranch,DbPath);
                        SS.ElmServiceState = ServicStateAsync(InAcc.ServiceState);
                        SS.ElmStartDate = InAcc.MandatoryStartDate;
                        SS.CityId = 1;
                        SS.UnitId = 1;
                        _ = HR.SaveChanges();
                        PrgAyc.Value += 1;
                    }
                }

            }
            _ = MessageBox.Show(" تمت مزامنة " + PrgAyc.Value +" عنصر "+ "من أصل " + PrgAyc.Maximum + " عنصر ","المزامنة",MessageBoxButton.OK,MessageBoxImage.Information);
            BtnAyc.IsEnabled = true;
            BtnAyc.Content = "مزامنة البيانات";
            PrgAyc.Visibility = Visibility.Hidden;
            BtnClose.IsEnabled = true;
            BtnSelectDB.IsEnabled = true;

        }
        private int ServicStateAsync(int AccServiceStateID)
        {
            switch (AccServiceStateID)
            {
                case 11:
                    return 5;
                case 13:
                    return 6;
                case 45:
                    return 2;
                case 80:
                    return 7;
                case 65:
                    return 3;
                default:
                    return 5;      
            }
        }
        private int RankAsync (int AccRankID)
        {
            switch (AccRankID)
            {
                case 1574:
                    return 8;
                case 1572:
                    return 9;
                case 1570:
                    return 3;
                case 1366:
                    return 4;
                case 1364:
                    return 5;
                case 1362:
                    return 6;
                case 1360:
                    return 7;
                default:
                    return 5;
            }
        }
        private string PlacesName (int Codee, string DbPath)
        {
            try
            {
                AccVM ss = new AccVM(DbPath);
                string Placee = "";
                foreach (Places I in ss.PlacesResource())
                {
                    if (I.Code == Codee)
                    {
                        Placee = I.Pname;
                    }
                }
                return Placee;

            }
            catch
            {
                return string.Empty;
            }
            

        }
        private string BrName(int EnCodee, string DbPath)
        {
            try
            {
                AccVM ss = new AccVM(DbPath);
                string BrnName = "";
                foreach (var I in ss.BranchResource())
                {
                    if (I.Encoded == EnCodee)
                    {
                        BrnName = I.Decoded;
                    }
                }
                return BrnName;

            }
            catch
            {
                return string.Empty;
            }


        }
    }
}
