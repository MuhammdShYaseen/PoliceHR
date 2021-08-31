using PoliceHR.Models;
using PoliceHR.ViewModels;
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

namespace PoliceHR.Views.PrvServices
{
    /// <summary>
    /// Interaction logic for PrvServicesAddEdit.xaml
    /// </summary>
    public partial class PrvServicesAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Elm_ID2;int Prv_ID2;
        public PrvServicesAddEdit(int Elm_ID,int Prv_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID; 
            Prv_ID2 = Prv_ID;
            if (Prv_ID2 != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.PreviousServices;
                        var searchresult = table.Where(c => c.PreviousServicesId.Equals(Prv_ID));
                        foreach (var ss in searchresult)
                        {
                            txtServiceName.Text = ss.ServiceName;
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtEndDate.SelectedDate = ss.EndDate;
                            txtFoundationName.Text = ss.FoundationName;
                            txtLocation.Text = ss.Location;
                            Elm_ID2 = (int)ss.ElmId;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctl).Text == String.Empty)
                    {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            if (Prv_ID2 != 0)
            {
                EditPrv(Prv_ID2);
                this.IsEnabled = true;
                this.Close();

            }
            else
            {
                AddPrv(Elm_ID2);
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
        private void AddPrv(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new PreviousService { ElmId = Element_ID,  FoundationName = txtFoundationName.Text,  Location = txtLocation.Text,  ServiceName = txtServiceName.Text, StartDate = txtStartDate.SelectedDate, EndDate = txtEndDate.SelectedDate};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة خدمة سابقة قبل التطوع  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditPrv(int Previous_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                   var ss = HR.PreviousServices.First(a => a.PreviousServicesId == Previous_ID);

                   ss.ServiceName = txtServiceName.Text;
                   ss.StartDate = txtStartDate.SelectedDate;
                   ss.EndDate = txtEndDate.SelectedDate;
                   ss.FoundationName = txtFoundationName.Text;
                   ss.Location = txtLocation.Text;

                   HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل خدمة سابقة قبل التطوع رقم :" + Prv_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
