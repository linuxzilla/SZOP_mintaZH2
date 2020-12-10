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
            set
            {
                username = value;
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        private string fullname;
        public string Fullname
        {
            get
            {
                return fullname;
            }
            set
            {
                fullname = value;
            }
        }
        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        private List<string> criminalRecords;
        public List<string> CriminalRecords
        {
            get
            {
                return criminalRecords;
            }
            set
            {
                criminalRecords = value;
            }
        }
        private RolesEnum role;
        public RolesEnum Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
            }
        }
        private bool userIsLoggedIN = false;
        public bool UserIsLoggedIN
        {
            get
            {
                return userIsLoggedIN;
            }
            set
            {
                userIsLoggedIN = value;
            }
        }
        #endregion
        #region Constructor
        public User(string Username, string Password, 
            string Fullname, string Address, List<string> CriminalRecords, RolesEnum Role)
        {
            this.Username = Username;
            this.Password = Password;
            this.Fullname = Fullname;
            this.Address = Address;
            this.CriminalRecords = CriminalRecords;
            this.Role = Role;
        }
        #endregion
    }
}
