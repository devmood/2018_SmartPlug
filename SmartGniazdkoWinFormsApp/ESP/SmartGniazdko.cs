using System;
using System.ComponentModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ESP
{
	public partial class MainWindow : Form
	{

		public MainWindow()
		{
			InitializeComponent();
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
				if(Connected)
				{
					_clientTCP.Tcp.Close();
				}

		}

		private TCP _clientTCP = new TCP();

		private string Ip { get; set; } = "192.168.4.1";
		private int Port { get; set; } = 1234;
		private bool Connected { get; set; }

		private const int sizeBuffer = 100;


		private void SendMsg(string text)
		{
                //zamiana wiadomosci na bajty
				var message = _clientTCP.Ascii.GetBytes(text + "\r\n");
				_clientTCP.Stream.Write(message, 0, message.Length);
		}

		private void MessageReader_DoWork(object sender, DoWorkEventArgs e)
		{
			while(Connected)
			{
				var byteBuffer = new byte[sizeBuffer];
				var message = "";


					/*	Read bytes & numer of bytes from TCP stream	*/
					_clientTCP.Stream = _clientTCP.Tcp.GetStream();
					var recivedNumber = _clientTCP.Stream.Read(byteBuffer, 0, sizeBuffer);

					/*	Convert to message to string	*/
					for(var i = 0; i < recivedNumber; i++)
					{
						var character = Convert.ToChar(byteBuffer[i]);
						message += character;
					}
				
			}
		}

		private void przek1_Click(object sender, EventArgs e)
		{
			if(!Connected) return;
			SendMsg(@"setRelay(10)");
		}

		private void przek2_Click(object sender, EventArgs e)
		{
			if(!Connected) return;
			SendMsg(@"setRelay(01)");
		}

		private void polacz_Click(object sender, EventArgs e)
		{
			Ip = IPAddress.Parse(ipTextArea.Text).ToString();
				if(!Connected)
                { 
					_clientTCP.Tcp.Connect(Ip, Port);
					Connected = true;
					MessageReader.RunWorkerAsync();
				}
				/*	The	connecton IS set & trying to disconnect	*/
				else
				{
					MessageReader.CancelAsync();
					polacz.Enabled = false;
					_clientTCP.Tcp.Close();
					Connected = false;
					_clientTCP = new TCP();
				}
	
			polacz.Text = Connected ? "Rozlacz" : "Polacz";
			if(!Connected) return;
			Thread.Sleep(1000);
			SendMsg(@"getRelay()");
		}
		private void port_TextChanged(object sender, EventArgs e)
		{
					Port = Convert.ToInt32(sender.ToString().Split(' ')[2]);
		}
    }
}