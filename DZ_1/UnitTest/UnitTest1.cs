using System;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DZ_1_Browser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace UnitTest
{
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
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

        }
    }
}
