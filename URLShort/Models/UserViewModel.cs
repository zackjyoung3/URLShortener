using System;
namespace URLShort.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Logins { get; set; }
        public DateTime LastLogIn { get; set; }
    }
}
