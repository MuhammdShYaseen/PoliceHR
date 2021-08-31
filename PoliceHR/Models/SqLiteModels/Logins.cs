using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceHR.Models.SqLiteModels
{
    [Table("Logins")]
    public class Logins
    {
        [AutoIncrement, PrimaryKey]
        public int User_ID { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
    }
}
