using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
	class Program
	{
		static void Main(string[] args)
		{
			StartTcpServer();
			Console.WriteLine("Server Start \n Input any ket exit");
			Console.ReadLine();
		}

		public static async Task StartTcpServer()
		{
			var server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 688));
			server.Start();
			var client = await server.AcceptTcpClientAsync();
			var stream = client.GetStream();
			var receiveMessage = string.Empty;
			var sendMessage = string.Empty;

			if (stream.CanRead)
			{

				var readBytes = new Byte[4096];
				var i = stream.Read(readBytes, 0, 4096);
				receiveMessage = Encoding.UTF8.GetString(readBytes, 0, i);
				Console.WriteLine("receiveMessage: " + receiveMessage);

			}
			if (stream.CanWrite)
			{
				sendMessage = "sendMessage: " + receiveMessage;
				var sendBytes = Encoding.UTF8.GetBytes(sendMessage);
				stream.Write(sendBytes, 0, sendBytes.Length);
				Console.WriteLine("sendMessage: " + sendMessage);
			}



		}
	}
}
