using System;
using System.Collections.Generic;
using System.Text;

namespace CS_NUnit.Xpath
{
    public class Account
    {
        private String password;
        private String email;
        

        public Account (String email, String password)
        {
            this.email = email;
            this.password = password;
        }

        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
    }
}
