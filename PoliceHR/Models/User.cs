using System;
using System.Collections.Generic;

#nullable disable

namespace PoliceHR.Models
{
    public partial class User
    {
        public User()
        {
            ActivtisRecourds = new HashSet<ActivtisRecourd>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual ICollection<ActivtisRecourd> ActivtisRecourds { get; set; }
    }
}
