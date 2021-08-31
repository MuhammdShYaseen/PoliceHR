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

namespace PoliceHR.Views.MilitaryConscriptionss
{
    /// <summary>
    /// Interaction logic for MilitaryConscriptionAddEdit.xaml
    /// </summary>
    public partial class MilitaryConscriptionAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Military_ID2;
        int Elm_ID2;
        public MilitaryConscriptionAddEdit(int Elm_ID,int Military_ID)
        {
            InitializeComponent();
            Military_ID2 = Military_ID;
            Elm_ID2 = Elm_ID;
            if (Military_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.MilitaryConscriptions;
                        var searchresult = table.Where(c => c.MilitaryConscriptionId.Equals(Military_ID));
                        foreach (var ss in searchresult)
                        {
                            txtCycleNumber.Text = ss.CycleNumber;
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtEndDate.SelectedDate = ss.EndDate;
                            Elm_ID2 = (int)ss.ElementId;
                            txtDuring.Text = (ss.EndDate - ss.StartDate).ToString();
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
        
        private void BtnAddEditV_Click(object sender, RoutedEventArgs e)
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
            if (Military_ID2 != 0)
            {
                EditMilitary(Military_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else if (MCount()==0)
            {
                AddMilitary(Elm_ID2);
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
            }else if (MCount() > 0)
            {
                MessageBox.Show("لا يمكن اضافة اكثر من خدمة الزامية واحدة فقط لكل عنصر", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.IsEnabled = true;
                return;
            }
        }
        private void AddMilitary(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new MilitaryConscription { ElementId = Element_ID, CycleNumber = txtCycleNumber.Text, StartDate = txtStartDate.SelectedDate, EndDate = txtEndDate.SelectedDate};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc(" اضافة خدمة الزامية  :"  + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditMilitary(int Militaryy_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.MilitaryConscriptions.First(a => a.MilitaryConscriptionId == Militaryy_ID);
                        ss.CycleNumber = txtCycleNumber.Text;
                        ss.StartDate = txtStartDate.SelectedDate;
                        ss.EndDate = txtEndDate.SelectedDate;
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc(" تعديل خدمة الزامية رقم :" + Military_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int MCount()
        {
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.MilitaryConscriptions;
                var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID2));
                int SearchCount = searchresult.Count();
                return SearchCount;
            }
            catch
            {
                return -1;
            }
        }

    }
}
