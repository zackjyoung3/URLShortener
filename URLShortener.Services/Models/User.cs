using System;
using System.Collections.Generic;

namespace URLShortener.Services.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Logins { get; set; }
        public DateTime LastLogIn { get; set; }
        public List<URL> URLs { get; set; }
    }
}
