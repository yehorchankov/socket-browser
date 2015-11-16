using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketsDel
{
    class Program
    {
        private static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                tempSocket.Connect(ipe);

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }

        // This method requests the home page content for the specified server.
        private static string SocketSendReceive(string server, int port)
        {
            string request = "GET / HTTP/1.1\r\nHost: " + server +
                "\r\nConnection: Close\r\n\r\n";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[1024];

            // Create a socket connection with the specified server and port.
            Socket s = ConnectSocket(server, port);

            if (s == null)
                return ("Connection failed");

            // Send request to the server.
            s.Send(bytesSent, bytesSent.Length, 0);

            // Receive the server home page content.
            int bytes = 0;
            string page = "Default HTML page on " + server + ":\r\n";

            // The following will block until te page is transmitted.
            do
            {
                bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);

            return page;
        }

        public static void Main(string[] args)
        {
            //string host;
            //int port = 443;
            //host = "www.google.com";

            //string result = SocketSendReceive(host, port);
            string result = SocketListener();
            Console.WriteLine(result);
        }


        public static string SocketListener()
        {
            Uri uri = new Uri("http://bionic-university.com/");
            string message = string.Format(
                "GET {0} HTTP/1.1\r\nHost: {1}\r\nConnection: close\r\nAccept: text/html\r\ntUser-Agent: MyBrowser\r\n\r\n", uri.PathAndQuery, uri.Host);
            string output = string.Empty;
            IPHostEntry IpHost = Dns.GetHostEntry(uri.Host);
            IPAddress[] ips = IpHost.AddressList;
            IPEndPoint ipe = new IPEndPoint(ips[0], uri.Port);
            using (Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(ipe);
                byte[] bytesSent = Encoding.ASCII.GetBytes(message);
                byte[] received = new byte[1024];
                socket.Send(bytesSent);
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(received, received.Length, 0);
                    output += Encoding.ASCII.GetString(received);
                } while (bytes > 0);
                socket.Disconnect(false);
            }
            return output;
        }
    }
}
