namespace Blue_Botnet
{
	internal class HTTPRequest
	{
		public string GETPiece = "";

		public string POSTPiece = "";

		public string HostPiece = "";

		public string UserAgentPiece = "";

		public string AcceptPiece = "";

		public string AcceptLangPiece = "";

		public string AcceptEncodingPiece = "";

		public string ConnectionPiece = "";

		public string ContentTypePiece = "";

		public string ContentLengthPiece = "";

		public string RequestStr = "";

		private string GETStr = "GET //URL// HTTP/1.1/r/n";

		private string POSTStr = "POST //URL// HTTP/1.1/r/n";

		private string HostStr = "Host: //URL///r/n";

		private string UserAgentStr = "User-Agent: //USERAGENT///r/n";

		private string AcceptStr = "Accept: //ACCEPT///r/n";

		private string AcceptLangStr = "Accept-Language: //ACCEPTLANG///r/n";

		private string AcceptEncodingStr = "Accept-Encoding: //ACCEPTENCODING///r/n";

		private string ContentTypeStr = "Content-Type: //CONTENTTYPE///r/n";

		private string ContentLengthStr = "Content-Length: //CONTENTLENGTH///r/n";

		private string ConnectionStr = "Connection: //CONNECTION///r/n";

		private string POSTBody;

		public void BuildGETRequest()
		{
			RequestStr = "";
			RequestStr += GETPiece;
			RequestStr += HostPiece;
			RequestStr += UserAgentPiece;
			RequestStr += AcceptPiece;
			RequestStr += AcceptLangPiece;
			RequestStr += AcceptEncodingPiece;
			RequestStr += ConnectionPiece;
			RequestStr += "/r/n";
		}

		public void BuildPOSTRequest()
		{
			RequestStr = "";
			RequestStr += POSTPiece;
			RequestStr += HostPiece;
			RequestStr += UserAgentPiece;
			RequestStr += AcceptPiece;
			RequestStr += AcceptLangPiece;
			RequestStr += AcceptEncodingPiece;
			RequestStr += ContentTypePiece;
			RequestStr += ContentLengthPiece;
			RequestStr += ConnectionPiece;
			RequestStr += "/r/n";
			RequestStr += POSTBody;
		}

		public void SetUrl(string url)
		{
			GETPiece = GETStr.Replace("//URL//", url);
			POSTPiece = POSTStr.Replace("//URL//", url);
		}

		public void SetHost(string host)
		{
			HostPiece = HostStr.Replace("//URL//", host);
		}

		public void SetUserAgent(string userAgent)
		{
			UserAgentPiece = UserAgentStr.Replace("//USERAGENT//", userAgent);
		}

		public void SetAccept(string accept)
		{
			AcceptPiece = AcceptStr.Replace("//ACCEPT//", accept);
		}

		public void SetAcceptLang(string acceptLang)
		{
			AcceptLangPiece = AcceptLangStr.Replace("//ACCEPTLANG//", acceptLang);
		}

		public void SetAcceptEncoding(string acceptEncoding)
		{
			AcceptEncodingPiece = AcceptEncodingStr.Replace("//ACCEPTENCODING//", acceptEncoding);
		}

		public void SetConnection(string connection)
		{
			ConnectionPiece = ConnectionStr.Replace("//CONNECTION//", connection);
		}

		public void SetPOSTBody(string body)
		{
			POSTBody = body;
			ContentLengthPiece = ContentLengthStr.Replace("//CONTENTLENGTH//", body.Length.ToString());
		}

		public void SetPOSTContentType(string contentType)
		{
			ContentTypePiece = ContentTypeStr.Replace("//CONTENTTYPE//", contentType);
		}
	}
}
