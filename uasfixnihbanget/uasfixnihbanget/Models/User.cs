using System.Text.RegularExpressions;

namespace uasfixnihbanget.Models
{
    public class User
    {
        public string email { get; set; }
        public string userName { get; set; }
        private string _password { get; set; }

        public string fullName { get; set; }

        
    }
}
