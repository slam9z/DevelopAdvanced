using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpClient
{
	class Program
	{
		private static AutoResetEvent _event = new AutoResetEvent(false);

		static void Main(string[] args)
		{

			StartTcpClient();
			_event.WaitOne();
		}

		public static async Task StartTcpClient()
		{
			var client = new System.Net.Sockets.TcpClient();
			await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 688);
			var stream = client.GetStream();

			var receiveMessage = string.Empty;
			var sendMessage = string.Empty;

			if (stream.CanWrite)
			{
				Console.WriteLine("input send message");
				var input = Console.ReadLine();
				receiveMessage = input;
				sendMessage = "SendMessage: " + receiveMessage;
				var sendBytes = Encoding.UTF8.GetBytes(receiveMessage);
				stream.Write(sendBytes, 0, sendBytes.Length);
			}

			if (stream.CanRead)
			{
				var readBytes = new Byte[4096];
				var i = stream.Read(readBytes, 0, 4096);
				receiveMessage = Encoding.UTF8.GetString(readBytes, 0, i);
				Console.WriteLine("ReceiveMessage: " + receiveMessage);

			}
			_event.Set();

		}
	}
}
