using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using DZ_1;
using NUnit.Framework;

namespace DZ_1_Browser.Models
{
    public class WebListener
    {
        readonly IRequestBuilder _requestBuilder;
        private Socket _socket;

        public WebListener(IRequestBuilder requestBuilder)
        {
            _requestBuilder = requestBuilder;
        }

        public void InstantiateRequest(string uri)
        {
            _requestBuilder.CreateStartingLine("GET", uri, "1.1");
            _requestBuilder.CreateHeader();
            //_requestBuilder.CreateBody("<html><head><title>lol</title></head><body>sgsgfgdf</body></html>");
        }

        public string GetResponse(Request request)
        {
            string connectionMessage = request.ToString();
            byte[] sentBytes = Encoding.ASCII.GetBytes(connectionMessage);
            byte[] receivedBytes = new byte[1024];
            string output = string.Empty;
            int receivedBytesCount = 0;

            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostEntry(request.Uri.Host);       //Looking for URI IP-address
                IPAddress[] ipAddresses = ipHostEntry.AddressList;
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddresses[0], request.Uri.Port);   //Creating an endpiont to get messages from server

                _socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(ipEndPoint);
                _socket.Send(sentBytes);

                do
                {
                    receivedBytesCount = _socket.Receive(receivedBytes, receivedBytes.Length, 0);   //Receiving data until it's done
                    output += Encoding.UTF8.GetString(receivedBytes);
                } while (receivedBytesCount > 0);
            }
            catch (SocketException exc)
            {
                output = exc.Message;
                _socket.Dispose();
                throw new Exception(exc.Message);
            }
            finally
            {
                _socket.Disconnect(false);
                _socket.Close();
            }
            return output;
        }
    }
}