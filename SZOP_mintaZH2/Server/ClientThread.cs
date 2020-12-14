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
                                    throw new Exception();

                                DataManager.Login(data[1], data[2], clientAddress);
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            break;

                        case "LOGOUT":
                            break;

                        case "NOCRIME":
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
        #endregion
    }
}
