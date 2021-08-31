using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoliceHR.Models;
using PoliceHR.ViewModels.SqLiteViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PoliceHR.ViewModels
{
   public class AddActiveRec
    {
        int UserIDD;
        string ELM_Num;
        public void AddActiveRecc(string Activityy)
        {
            DateTime Dt = ServerDate();
            int UserIDs = UserID();
            using (SubjectivityContext HR = new SubjectivityContext())
            {
                object SS = new ActivtisRecourd { UserId= UserIDs, ActivityDes = Activityy, ActivityDateTime = Dt};
                HR.Add(SS);
                HR.SaveChanges();
            }
        }

        private DateTime ServerDate()
        {
            SqLiteVm Sq = new SqLiteVm();
            using (var conn = new SqlConnection(Sq.ConnStringSource()))
            {
                var cmd = new SqlCommand("SELECT GETDATE()", conn);
                conn.Open();
                var dt = (DateTime)cmd.ExecuteScalar();
                if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }
                return dt;
                
            };
        }
        private int UserID()
        {
            
            var ss = new SqLiteVm();
            foreach(var xx in ss.UsersSource())
            {
                using (var HR = new SubjectivityContext())
                {
                    var table = HR.Users;
                    var searchresult = table.Where(c => c.UserName.Equals(xx.User_Name)&&c.Password.Equals(xx.Password));
                    foreach(var IDD in searchresult)
                    {
                        UserIDD=IDD.UserId;
                        return UserIDD;
                    }
                    
                }
               
            } 
            return UserIDD;
        }
        public string ELmMNimber(int Elm_IDD)
        {
            using (var HR = new SubjectivityContext())
            {
                var table = HR.Elements;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_IDD));
                foreach (var IDD in searchresult)
                {
                    ELM_Num = IDD.ElmMNumber;
                    return IDD.ElmMNumber;
                }
                return ELM_Num;
            }
        }
    }
    
}
