using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLite
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password {
            get
            {
                return "";
            }
            set 
            {
                password = value;
            }
        }

        private string password;

        public string FullName
        {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }

        public void CreateUsername()
        {
            Username = FirstName + LastName.Substring(0, 1);
        }
    }
}
