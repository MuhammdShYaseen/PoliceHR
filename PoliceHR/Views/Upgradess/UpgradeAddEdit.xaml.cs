﻿using PoliceHR.Models;
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

namespace PoliceHR.Views.Upgradess
{
    /// <summary>
    /// Interaction logic for UpgradeAddEdit.xaml
    /// </summary>
    public partial class UpgradeAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Up_ID2;
        int Elm_ID2;
        int RankDD;
        ConvertImage Conv = new ConvertImage();
        public UpgradeAddEdit(int Elm_ID,int Up_ID)
        {
            InitializeComponent();
            Up_ID2 = Up_ID;
            Elm_ID2 = Elm_ID;
            using (var HR = new SubjectivityContext())
            {
                List<Models.Rank> Rs = new List<Models.Rank>(HR.Ranks);
                txtRank.ItemsSource = Rs;
                txtRank.DisplayMemberPath = "RankName";
                txtRank.SelectedValuePath = "RankId";

            }
            if (Up_ID != 0)
            {
                try
                {
                   
                   using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Upgrades;
                        var searchresult = table.Where(c => c.UpgradeId.Equals(Up_ID));
                        foreach (var ss in searchresult)
                        {
                           
                            RankDD = (int)ss.UpgradeName;
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtEndDate.SelectedDate = ss.EndDate;
                            txtOrderDate.SelectedDate = ss.OrderDate;
                            txtOrderNum.Text = ss.OrderNumber;
                            Elm_ID2 = (int)ss.ElmId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
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
            if (Up_ID2 != 0)
            {
                EditUpgrade(Up_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddUpgrade(Elm_ID2);
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
        private void AddUpgrade(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Upgrade { ElmId = Element_ID, UpgradeName = int.Parse(txtRank.SelectedValue.ToString()), OrderImage = Conv.getJPGFromImageControl(Immg), OrderDate = txtOrderDate.SelectedDate, OrderNumber = txtOrderNum.Text,  EndDate = txtEndDate.SelectedDate, StartDate = txtStartDate.SelectedDate};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة ترفيعة  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditUpgrade(int Upgrade_Id)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Upgrades.First(a => a.UpgradeId == Upgrade_Id);

                   ss.StartDate = txtStartDate.SelectedDate;
                   ss.EndDate = txtEndDate.SelectedDate;
                   ss.OrderDate = txtOrderDate.SelectedDate;
                   ss.OrderNumber = txtOrderNum.Text;
                   ss.UpgradeName= int.Parse(txtRank.SelectedValue.ToString());
                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل ترفيعة رقم :" + Up_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
    }
}