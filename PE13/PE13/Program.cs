using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;


// attempts to crash a server
// WARNING THIS MADE MY COMPUTER ALMOST RUN OUT OF MEMORY

namespace PE13
{
    class NetworkClient
    {
        // port numbers
        const int port = 2112;

        static void Main(string[] args)
        {
            // get IP address for the server
            // this next line works if the client and server are on the same machine
            string ipAddr = "127.0.0.1"; // localhost

            // create a networkClient object
            NetworkClient nc = new NetworkClient();

            // connect to the server
            nc.Connect(ipAddr);

        }

        // handles the connection to the server
        private void Connect(string ipAddr)
        {
            // set up a Socket
            TcpClient[] socket = new TcpClient[500000];
            StreamWriter sw = null;

            // try to create a new client
            try
            {
                for(int x = 0; x < socket.Length; x ++)
                {
                    socket[x] = new TcpClient(ipAddr, port);
                }
                               
            }
            catch (Exception ex)
            {
                // list the error and exit
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Connected to the server");

            // try to send some code
            // use new code here
            for (int x = 0; x < socket.Length; x++)
            {
                try
                {

                    // streamwriter to send data to the internet
                    sw = new StreamWriter(socket[x].GetStream());
                    sw.WriteLine("Beep Boop");   
                    
                    sw.Flush();

                    // stream reader to get the response
                    StreamReader sr = new StreamReader(socket[x].GetStream());
                    string fromServer = sr.ReadLine();
                    Console.WriteLine("From server: " + fromServer);

                    // close the connection
                    socket[x].Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending message: " + ex.Message);
                }
            }


        }
    }
}
