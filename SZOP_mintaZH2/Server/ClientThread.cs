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

            } while (true);
        }
        #endregion
    }
}
