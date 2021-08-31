using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
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

namespace PoliceHR.Views.LegalStatess
{
    /// <summary>
    /// Interaction logic for LegalStateAddEdit.xaml
    /// </summary>
    public partial class LegalStateAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int LagelState_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public LegalStateAddEdit(int Elm_ID,int LagelState_ID)
        {
            InitializeComponent();
            LagelState_ID2 = LagelState_ID;
            Elm_ID2 = Elm_ID;
            if (LagelState_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.LegalStates;
                        var searchresult = table.Where(c => c.LegalStateId.Equals(LagelState_ID2));
                        foreach (var ss in searchresult)
                        {
                            txtCourtName.Text = ss.CourtName;
                            txtCrimeName.Text = ss.CrimeName;
                            txtDes.Text = ss.LegalDescraption;
                            txtEndDate.SelectedDate = ss.EndDate;
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtNumberDate.Text = ss.LegalNumberDate;
                            txtResult.Text = ss.Result;
                            Elm_ID2 = (int)ss.ElmId;
                            if (ss.LagelCase == true) { txtLegalState.Text = "منظورة"; } else { txtLegalState.Text = "محسومة"; }
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.RulingImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.RulingImage);
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
        BitmapImage Immg;
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            Immg = SS.OpenFromPC();
            ImgElementImage.Source = Immg;
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
            if (LagelState_ID2 != 0)
            {
                EditLegalState(LagelState_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddLegalState(Elm_ID2);
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
        private void AddLegalState(int Element_ID)
        {
            try
            {
                bool IsInRule;
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    if (txtLegalState.Text == "منظورة") { IsInRule = true; } else { IsInRule = false; }
                    object SS = new LegalState { ElmId = Element_ID, EndDate = txtEndDate.SelectedDate, RulingImage = Conv.getJPGFromImageControl(Immg), StartDate = txtStartDate.SelectedDate, CourtName = txtCourtName.Text, CrimeName = txtCrimeName.Text, LegalDescraption = txtDes.Text, LegalNumberDate=txtNumberDate.Text, Result = txtResult.Text, LagelCase= IsInRule };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة ملاحقة قضائية :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditLegalState(int LegalStateId)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                   var ss = HR.LegalStates.First(a => a.LegalStateId == LegalStateId);
                   ss.CourtName = txtCourtName.Text;
                   ss.CrimeName = txtCrimeName.Text;
                   ss.LegalDescraption = txtDes.Text;
                   ss.EndDate = txtEndDate.SelectedDate;
                   ss.StartDate = txtStartDate.SelectedDate;
                   ss.LegalNumberDate = txtNumberDate.Text;
                   if (txtLegalState.Text == "منظورة" ) {ss.LagelCase = true ; } else { ss.LagelCase = false; }
                   if (Immg != null) { ss.RulingImage = Conv.getJPGFromImageControl(Immg); }
                   HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل ملاحقة قضائية رقم :" + LagelState_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }

        private void txtLegalState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtLegalState.SelectedItem == txtLegalState.Items[1])
            {
                txtEndDate.IsEnabled = true;
                txtResult.IsEnabled = true;
            }
            else
            {
                txtEndDate.IsEnabled = false;
                txtResult.IsEnabled = false;
            }
        }
    }
}
