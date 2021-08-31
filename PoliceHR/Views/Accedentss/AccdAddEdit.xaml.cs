using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.Seizuress;
using System;
using System.Collections.Generic;
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

namespace PoliceHR.Views.Accedentss
{
    /// <summary>
    /// Interaction logic for AccdAddEdit.xaml
    /// </summary>
    public partial class AccdAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Elm_ID2,
            Acc_ID2,
            Sezure_ID;
        public AccdAddEdit(int Elm_ID, int Acc_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            Acc_ID2 = Acc_ID;
            if (Acc_ID2 != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Accidents;
                        var searchresult = table.Where(c => c.AccidentsId.Equals(Acc_ID));
                        foreach (var ss in searchresult)
                        {
                            txtAccidentsDate.SelectedDate = ss.AccidentsDate;
                            if (ss.IsAtWork == true) { txtIsAtWork.Text = "نعم";}else { txtIsAtWork.Text = "لا"; }
                            txtMedicReport.Text = ss.MedicReport;
                            Sezure_ID = (int)ss.AccidentsSeizure;
                            Elm_ID2 = (int)ss.ElmId;
                        }
                        
                    }
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Seizures;
                        var searchresult = table.Where(c => c.SeizureId.Equals(Sezure_ID));
                        foreach (var ss in searchresult)
                        {
                            txtSelectedSezureUnit.Text = "الجهة منظمة الضبط" + " : " + ss.SeizureUnit;
                            txtSelectedSezureNum.Text= "رقم الضبط" + " : " + ss.SeizureNumber;
                            txtSelectedSezuredate.Text = "تاريخ الضبط" + " : " + ss.SeizureDate.Value.ToString("MM/dd/yyyy");
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Seizures;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                GrdSeizure.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            CloseButton.Background = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#FF222831"));
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Background = System.Windows.Media.Brushes.Red;
        }
        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            foreach (var ctl in GrdTexts.Children)
            {
                if (ctl.GetType() == typeof(TextBox) )
                {
                    if (((TextBox)ctl).Text == String.Empty )
                    {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            if (Acc_ID2 != 0)
            {
                
                EditAcc(Acc_ID2);
                this.IsEnabled = true;
                this.Close();

            }
            else
            {
                AddAcc(Elm_ID2);
                this.IsEnabled = true;
                foreach (var ctl in GrdTexts.Children)
                {
                    try
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Empty;
                    }
                    catch { }

                }
            }
        }
        private void AddAcc(int Element_ID)
        {
            bool IsWork = false;
            if (txtIsAtWork.Text == "نعم") { IsWork = true; }
            if (Sezure_ID == 0)
            {
                MessageBox.Show("قم بتحديد احد الضبوط اولا", "الضبط فارغ", MessageBoxButton.OK, MessageBoxImage.Error);
                this.IsEnabled = true;
                return;
            }
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Accident { ElmId = Element_ID, AccidentsDate = txtAccidentsDate.SelectedDate,  MedicReport = txtMedicReport.Text,IsAtWork = IsWork, AccidentsSeizure=Sezure_ID };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة حادث" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
            
        }

        private void GrdSeizure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Sezure_ID = Sez_ID;
            using (var HR = new SubjectivityContext())
            {
                var table = HR.Seizures;
                var searchresult = table.Where(c => c.SeizureId.Equals(Sezure_ID));
                foreach (var ss in searchresult)
                {
                    txtSelectedSezureUnit.Text = "الجهة منظمة الضبط" + " : " + ss.SeizureUnit;
                    txtSelectedSezureNum.Text = "رقم الضبط" + " : " + ss.SeizureNumber;
                    txtSelectedSezuredate.Text = "تاريخ الضبط" + " : " + ss.SeizureDate.Value.ToString("MM/dd/yyyy");
                }
            }
        }
        int Sez_ID;

        private void BtnAddSeizure_Click(object sender, RoutedEventArgs e)
        {
            var SS = new SeizureAddEdit(Elm_ID2,0);
            SS.Show();
        }

        private void GrdSeizure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdSeizure.SelectedItems.Count > 0)
            {
                Seizure member = new Seizure();
                foreach (var obj in GrdSeizure.SelectedItems)
                {
                    member = obj as Seizure;
                    str = member.SeizureId;
                }
            }
            else
            {

            }
            Sez_ID = str;
        }

        private void EditAcc(int Accident_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                   var ss = HR.Accidents.First(a => a.AccidentsId == Accident_ID);
                   ss.AccidentsDate = txtAccidentsDate.SelectedDate;
                   if (txtIsAtWork.Text == "نعم") { ss.IsAtWork = true; } else { ss.IsAtWork = false; }
                   ss.MedicReport = txtMedicReport.Text;
                   ss.AccidentsSeizure = Sezure_ID;
                   HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل الحادث رقم :" + Acc_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
