using PoliceHR.Models;
using PoliceHR.Views.Accedentss;
using PoliceHR.Views.Decorationss;
using PoliceHR.Views.Movementss;
using PoliceHR.Views.PrvServices;
using PoliceHR.Views.Seizuress;
using PoliceHR.Views.vecations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for DetailsElementInfo.xaml
    /// </summary>
    public partial class DetailsElementInfo : Window
    {
        
        int ServiceSS;
        int RankDD;
        int ELMid2;
        public DetailsElementInfo(int ELMid)
        {
            InitializeComponent();
            ELMid2 = ELMid;
            try
            {
               using (SubjectivityContext HR = new SubjectivityContext())
               {

                List<Models.Rank> Rs = new List<Models.Rank>(HR.Ranks);
                txtRank.ItemsSource = Rs;
                txtRank.DisplayMemberPath = "RankName";
                txtRank.SelectedValuePath = "RankId";
                List<ServiceState> Rs1 = new List<ServiceState>(HR.ServiceStates);
                txtServicState.ItemsSource = Rs1;
                txtServicState.DisplayMemberPath = "ServiceState1";
                txtServicState.SelectedValuePath = "ServiceStateId";
               }

            using (var HR = new SubjectivityContext())
            {
                var table = HR.Elements;
                var searchresult = table.Where(c => c.ElmId.Equals(ELMid2));
                foreach (var ss in searchresult)
                {
                    txtName.Text ="الاسم الثلاثي : "+ ss.ElmName +" "+ss.ElmFather+" "+ss.ElmLastName;
                    txtIDNumber.Text ="الرقم الوطني : "+ ss.ElmPersonalIdNumber;
                    if (ss.ElmGendar == true) { txtGender.Text = "ذكر"; } else { txtGender.Text = "انثى"; }
                    if (ss.ElmIsRecruiter == true) { txtIsRes.Text = "مجند"; } else { txtIsRes.Text = "متطوع"; }
                    RankDD = (int)ss.ElmRank;
                    txtMNumber.Text = "الرقم العسكري : " + ss.ElmMNumber;
                    ServiceSS = (int)ss.ElmServiceState;
                }
            }
            using (var HR = new SubjectivityContext())
            {
                var table = HR.Ranks;
                var searchresult = table.Where(c => c.RankId.Equals(RankDD));
                foreach (var ss in searchresult)
                {
                    txtRank.Text = ss.RankName;
                }
            }
            using (var HR = new SubjectivityContext())
            {
                var table = HR.ServiceStates;
                var searchresult = table.Where(c => c.ServiceStateId.Equals(ServiceSS));
                foreach (var ss in searchresult)
                {
                    txtServicState.Text = ss.ServiceState1;
                }
            } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            try
            {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
            }
            catch
            {
                return null;
            }
            
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    this.DragMove();
                }
        }
        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                // MaxButton.Content = "1";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                // MaxButton.Content = "2";
            }

        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {

            CloseButton.Background = (Brush)(new BrushConverter().ConvertFrom("#FF222831"));
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Background = Brushes.Red;
        }

        private void BtnPlts_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Palantes(ELMid2));
        }

        private void BtmMovements_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new DMovements(ELMid2));
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new DecorationView(ELMid2));
        }

        private void BtnVecations_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new VecationsView(ELMid2));
        }

        private void PrvServices_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new PrvServicesView(ELMid2));
        }

        private void BtnAcc_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new AccdView(ELMid2));
        }

        private void BtnSeizure_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new SeizureView(ELMid2));
        }

        private void BtnRotio_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new DeficitRatioss.DeficitRatioView(ELMid2));
        }

        private void BtnConvc_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Convalescencess.ConvalescencesView(ELMid2));
        }

        private void BtnClassChange_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new ClassChangeds.ClassChangedView(ELMid2));
        }

        private void BtnPhoneNum_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new PhoneNums.PhonesView(ELMid2));
        }

        private void BtnSocial_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new SocialAcc.SocialView(ELMid2));
        }

        private void BtnDes_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Descriptionss.DescriptionView(ELMid2));
        }

        private void BtnCycle_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Cycless.CycleView(ELMid2));
        }

        private void BtnDesertion_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Desertionss.DesertionView(ELMid2));
        }

        private void BtnDegree_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Degreess.DegreeView(ELMid2));
        }

        private void BtnFinancialLevel_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new FinancialLevelss.FinancialLevelView(ELMid2));
        }

        private void BtnFired_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Firedss.FiredView(ELMid2));
        }

        private void BtnImp_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Imprisonss.ImprisonView(ELMid2));
        }

        private void BtnLayOff_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Layoffss.LayoffView(ELMid2));
        }

        private void BtnLegalState_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new LegalStatess.LegalStateView(ELMid2));
        }

        private void BtnLost_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Lostss.LostView(ELMid2));
        }

        private void BtnMartyr_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Martyrss.MartyrView(ELMid2));
        }

        private void BtnMilitaryConscription_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new MilitaryConscriptionss.MilitaryConscriptionView(ELMid2));
        }

        private void BtnPoliceState_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new PoliceStatess.PoliceStateView(ELMid2));
        }

        private void BtnUpgrades_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Upgradess.UpgradeView(ELMid2));
        }

        private void BtnHW_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new HusbandOrWives.HWView(ELMid2));
        }

        private void BtnChild_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Children.ChildView(ELMid2));
        }

        private void BtnReserve_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Reservess.ReserveView(ELMid2));
        }

        private void BtnRetention_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Retentionss.RetentionView(ELMid2));
        }
    }
}
