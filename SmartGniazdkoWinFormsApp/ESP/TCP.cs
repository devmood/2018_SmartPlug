using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ESP
{
	public class TCP
	{
		public TCP()
		{
			Tcp = new TcpClient();
			Stream = Stream.Null;
			Ascii = new ASCIIEncoding();

		}

		public TcpClient Tcp { get; set; }
		public Stream Stream { get; set; }
		public ASCIIEncoding Ascii { get; set; }
	}
}