using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ClientThread
    {
        #region Private variables
        private StreamReader reader;
        private StreamWriter writer;
        private string clientAddress;
        #endregion

        #region Constructor
        public ClientThread(TcpClient client)
        {
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            clientAddress = client.Client.RemoteEndPoint.ToString();
        }
        #endregion

        #region Methods
        public void StartThread()
        {
            do
            {
                try
                {
                    string text = reader.ReadLine();

                    string[] data = text.Split('|');

                    switch (data[0])
                    {
                        case "LOGIN":
                            try
                            {
                                if (data.Length != 3)
                                    throw new ArgumentOutOfRangeException();
                                DataManager.Login(data[1], data[2], clientAddress);
                            }
                            catch (Exception e)
                            {
                                WrtieOutExceptions(e);
                            }
                            break;

                        case "LOGOUT":
                            try
                            {
                                if (data.Length != 2)
                                    throw new ArgumentOutOfRangeException();
                                DataManager.Logout(data[1], clientAddress);
                            }
                            catch (Exception e)
                            {
                                WrtieOutExceptions(e);
                            }
                            break;

                        case "NOCRIME":
                            try
                            {
                               writer.WriteLine(string.Join(",", DataManager.ListNoCrimeUsers().ToArray()));
                            }
                            catch (Exception e)
                            {
                                WrtieOutExceptions(e);
                            }
                            break;

                        case "EXIT":
                            break;

                        case "NAME":
                            break;

                        case "LOCALS":
                            break;

                        case "ADDCRIME":
                            break;

                        case "PARDON":
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            } while (true);
        }
        private void WrtieOutExceptions(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (e is ArgumentOutOfRangeException)
                Console.WriteLine("Not enough argument!");
            else if (e is UserNotExistException)
                Console.WriteLine("User not exist!");
            else if (e is WrongPasswordException)
                Console.WriteLine("Wrong password!");
            else if (e is UserInvalidIPException)
                Console.WriteLine("Invalid IP!");
            else if (e is UserInvalidLoginException)
                Console.WriteLine("Invalid security key!");
            else if (e is UseRolerNotExistException)
                Console.WriteLine("User role not exist!");
            else if (e is UserInvalidRoleException)
                Console.WriteLine("Invalid user role!");
            else
            {
                Console.WriteLine("Fatal error!");
                throw e;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        #endregion
    }
}
