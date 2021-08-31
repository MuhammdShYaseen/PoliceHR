using PoliceHR.Models;
using PoliceHR.ViewModels.SqLiteViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoliceHR.ViewModels
{
    public class CheckUserRole
    {   
        int IntRole=0;
        public int CheckUserRolee()
        {
            string UserNm = "";
            string pass = "";
            string Role = "";
            SqLiteVm Slite = new SqLiteVm();
            if (Slite.UsersSource().Count != 0)
            {
                foreach (var ss in Slite.UsersSource())
                {
                    UserNm = ss.User_Name;
                    pass = ss.Password;
                }
                using (var HR = new SubjectivityContext())
                {
                    var table = HR.Users;
                    var searchresult = table.Where(c => c.UserName.Equals(UserNm) && c.Password.Equals(pass));
                    foreach (var ss in searchresult)
                    {
                        Role = ss.Role;
                        switch (Role)
                        {
                            case "Admin":
                                IntRole= 1;
                                return IntRole; 
                            case "CUARD":
                                IntRole = 2;
                                return IntRole;
                            case "ADD_ONLY":
                                IntRole = 3;
                                return IntRole;
                            case "READ_ONLY":
                                IntRole = 4;
                                return IntRole;
                            default:
                                IntRole = 0;
                                return IntRole;
                        }

                    }
                    return IntRole;
                }
                
            }
            else
            {
                return 0;
            }
        }

    }
}
