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
        private static Random random = new Random();
        #endregion

        #region Public methods and functions
        public static void Add(string Username, string Password,
            string Fullname, string Address, RolesEnum Role, List<string> CriminalRecords)
        {
            users.Add(Username, new User(Username, Password, Fullname, Address, Role, CriminalRecords));
        }
        public static string Login(string username, string password, string clientIPAdress)
        {
            if (!users.ContainsKey(username))
                throw new UserNotExistException();

            User tempUser = users[username];

            if (tempUser.Password != username)
                throw new WrongPasswordException();

            tempUser.UserIsLoggedIN = true;
            tempUser.ClientIPAdress = clientIPAdress;
            tempUser.Key = random.Next(10000, 100000).ToString();

            return tempUser.Key;
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
                    item.Elements("CriminalRecords").Descendants("Crime").Select(r => r.Value as string) as List<string>
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
