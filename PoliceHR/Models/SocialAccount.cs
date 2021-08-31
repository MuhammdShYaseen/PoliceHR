using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class SocialAccount
    {
        public int SocialAccountsId { get; set; }
        public string AccountAdress { get; set; }
        public string Descraption { get; set; }
        public int? ElmId { get; set; }

        public virtual Element Elm { get; set; }
    }
}
