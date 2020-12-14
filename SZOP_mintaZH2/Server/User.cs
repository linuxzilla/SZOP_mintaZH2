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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public List<string> CriminalRecords { get; set; }
        public RolesEnum Role { get; set; }
        public bool isLoggedIN { get; set; }
        public string ClientIPAdress { get; set; }
        #endregion
        #region Constructor
        public User(string Username, string Password, 
            string Fullname, string Address, RolesEnum Role, List<string> CriminalRecords)
        {
            this.Username = Username;
            this.Password = Password;
            this.Fullname = Fullname;
            this.Address = Address;
            this.Role = Role;
            this.CriminalRecords = CriminalRecords;
        }
        #endregion
        #region Methods and Functions
        public void AddCriminalRecord(string input)
        {
            CriminalRecords.Add(input);
        }
        #endregion
    }
}
