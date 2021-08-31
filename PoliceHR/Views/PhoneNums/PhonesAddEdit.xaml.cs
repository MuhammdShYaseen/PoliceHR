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

namespace PoliceHR.Views.PhoneNums
{
    /// <summary>
    /// Interaction logic for PhonesAddEdit.xaml
    /// </summary>
    public partial class PhonesAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Phone_ID2,Elm_ID2;
        public PhonesAddEdit(int Elm_ID,int Phone_ID)
        {
            InitializeComponent();
            Phone_ID2 = Phone_ID;
            Elm_ID2 = Elm_ID;
            if (Phone_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.PhonesNumbers;
                        var searchresult = table.Where(c => c.PhoneId.Equals(Phone_ID));
                        foreach (var ss in searchresult)
                        {
                            txtPhoneNumber.Text = ss.PhoneNumber;
                            if (ss.PhoneType == true) { txtType.Text = "أرضي"; }else if (ss.PhoneType == false) { txtType.Text = "موبايل"; }
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
            if (Phone_ID2 != 0)
            {
                EditPhone(Phone_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddPhone(Elm_ID2);
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
        private void AddPhone(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new PhonesNumber { ElmId = Element_ID, PhoneNumber = txtPhoneNumber.Text, PhoneType = PhoneType(txtType.Text) };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة رقم الهاتف  :"  + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }

    private bool PhoneType (string Typee)
        {
            switch (Typee)
            {
                case "أرضي":
                    return  true;
                case "موبايل":
                    return  false;
                default:
                    return true; 
            }
        }

        private void EditPhone(int Phon_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.PhonesNumbers.First(a => a.PhoneId == Phon_ID);
                    ss.PhoneNumber = txtPhoneNumber.Text;
                    ss.PhoneType = PhoneType(txtType.Text);
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل رقم الهاتف رقم :" + Phone_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
