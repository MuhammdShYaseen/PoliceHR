using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceHR.Models.SqLiteModels
{
    [Table("Prodects")]
    public class ConnString
    {
        [AutoIncrement, PrimaryKey]
        public int ConnString_ID { get; set; }
        public string ConnactionString { get; set; }
    }
}
