using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class User
    {
        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
        }
        private string fullname;
        public string Fullname
        {
            get
            {
                return fullname;
            }
        }
        private string address;
        public string Address
        {
            get
            {
                return address;
            }
        }
        private List<string> criminalRecords;
        public List<string> CriminalRecords
        {
            get
            {
                return criminalRecords;
            }
        }
        private RolesEnum role;
        public RolesEnum Role
        {
            get
            {
                return role;
            }
        }
        private bool userIsLoggedIN = false;
        public bool UserIsLoggedIN
        {
            get
            {
                return userIsLoggedIN;
            }
        }
        #endregion

    }
}
