using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Server
{
    public class DataManager
    {
        #region Private variables
        private static Dictionary<string, User> users = new Dictionary<string, User>();
        #endregion

        #region Public methods and functions
        public static void Add(string Username, string Password,
            string Fullname, string Address, RolesEnum Role, List<string> CriminalRecords)
        {
            users.Add(Username, new User(Username, Password, Fullname, Address, Role, CriminalRecords));
        }
        public static void Login(string username, string password, string clientIPAdress)
        {
            if (!users.ContainsKey(username))
                throw new UserNotExistException();

            User tempUser = users[username];

            if (tempUser.Password != username)
                throw new WrongPasswordException();

            tempUser.ClientIPAdress = clientIPAdress;
            tempUser.isLoggedIN = true;
        }
        public static void Logout(string username, string clientIPAdress)
        {
            if (!users.ContainsKey(username))
                throw new UserNotExistException();

            User tempUser = users[username];

            if (tempUser.ClientIPAdress != clientIPAdress)
                throw new UserInvalidIPException();

            tempUser.isLoggedIN = false;
        }
        public static List<string> ListNoCrimeUsers()
        {
            List<string> tempUsers = new List<string>();
            foreach (User user in users.Values)
            {              
                if (user.CriminalRecords.Count == 0)
                    tempUsers.Add(user.Username);                    
            }
            return tempUsers;
        }
        public static string GetUserFullName(string username, string clientIPAdress)
        {
            if (!users.ContainsKey(username))
                throw new UserNotExistException();

            User tempUser = users[username];

            if (!tempUser.isLoggedIN)
                throw new UserInvalidLoginException();

            if (tempUser.ClientIPAdress != clientIPAdress)
                throw new UserInvalidLoginException();

            return tempUser.Fullname;
        }
        public static List<string> GetLocals(string location, string clientIPAdress)
        {
            List<string> tempUsers = new List<string>();
            foreach (User user in users.Values)
            {
                if (user.Address == location)
                    tempUsers.Add(user.Username);
            }
            return tempUsers;
        }
        public static void AddCrime(string username, string crime, string clientIPAdress)
        {
            if (!users.ContainsKey(username))
                throw new UserNotExistException();

            User tempUser = users[username];
            tempUser.AddCriminalRecord(crime);
        }
        public static void ReadUsersFromXml(string FilePath)
        {
            XDocument xml = XDocument.Load(FilePath);
            foreach (var item in xml.Descendants("User"))
            {
                Add(item.Element("Username").Value,
                    item.Element("Password").Value,
                    item.Element("FullName").Value,
                    item.Element("Address").Value,
                    StringToEnum(item.Element("Role").Value),
                    item.Elements("CriminalRecords").Descendants("Crime").Select(r => r.Value).ToList()
                    );
               
            }
        }

        public static void WriteUserToXml(string FilePath)
        {

        }

        private static RolesEnum StringToEnum(string input)
        {
            return (RolesEnum)Enum.Parse(typeof(RolesEnum), input, true);
        }

        #endregion
    }
}
