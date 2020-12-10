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
            string Fullname, string Address, RolesEnum Role)
        {
            users.Add(Username, new User(Username, Password, Fullname, Address, Role));
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
                    StringToEnum(item.Element("Role").Value));
                foreach (var CrimeElement in item.Descendants("CriminalRecords"))
                {
                    Console.WriteLine(CrimeElement.Element("Crime").Value);
                }
            }
        }

        private static RolesEnum StringToEnum(string input)
        {
            try
            {
                return (RolesEnum)Enum.Parse(typeof(RolesEnum), input, true);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
                throw;
            }
        }

        #endregion
    }
}
