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

namespace PoliceHR.Views.FinancialLevelss
{
    /// <summary>
    /// Interaction logic for FinancialLevelAddEdit.xaml
    /// </summary>
    public partial class FinancialLevelAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int FinLevel_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public FinancialLevelAddEdit(int Elm_ID,int FinLevel_ID)
        {
            InitializeComponent();
            FinLevel_ID2 = FinLevel_ID;
            Elm_ID2 = Elm_ID;
            if (FinLevel_ID2 != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.FinancialLevels;
                        var searchresult = table.Where(c => c.FinancialLevel1.Equals(FinLevel_ID));
                        foreach (var ss in searchresult)
                        {
                            txtFinName.Text = ss.FinancialName;
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtEndDate.SelectedDate = ss.EndDate;
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
            if (FinLevel_ID2 != 0)
            {
                EditFinLevel(FinLevel_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddFinLevel(Elm_ID2);
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
        private void AddFinLevel(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new FinancialLevel { ElmId = Element_ID, FinancialName = txtFinName.Text, StartDate = txtStartDate.SelectedDate, EndDate = txtEndDate.SelectedDate};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة ترفيعة مالية :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditFinLevel(int FinL_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.FinancialLevels.First(a => a.FinancialLevel1 == FinL_ID);
                   ss.FinancialName = txtFinName.Text;
                   ss.StartDate = txtStartDate.SelectedDate;
                   ss.EndDate = txtEndDate.SelectedDate;
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل ترفيعة مالية رقم :" + FinLevel_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}

