﻿using PoliceHR.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoliceHR.Views.FinancialLevelss
{
    /// <summary>
    /// Interaction logic for FinancialLevelView.xaml
    /// </summary>
    public partial class FinancialLevelView : Page
    {
        int Elm_ID2;
        public FinancialLevelView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.FinancialLevels;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdFinLevel.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MIDelete_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.FinancialLevels.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        FinancialLevel Del = ss.FinancialLevels.Find(FinLevel_ID);
                        ss.FinancialLevels.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.FinancialLevels;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdFinLevel.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }
        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var SS = new FinancialLevelAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdFinLevel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (FinLevel_ID != 0)
            {
                var SS = new FinancialLevelAddEdit(0, FinLevel_ID);
                SS.Show();
            }
        }
        int FinLevel_ID;
        private void GrdFinLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdFinLevel.SelectedItems.Count > 0)
            {
                FinancialLevel member = new FinancialLevel();
                foreach (var obj in GrdFinLevel.SelectedItems)
                {
                    member = obj as FinancialLevel;
                    str = member.FinancialLevel1;
                }
            }
            else
            {

            }
            FinLevel_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdFinLevel, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الترفيعات المالية  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
