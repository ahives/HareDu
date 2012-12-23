namespace HareDu
{
    public class HareDuClientParameters
    {
        private readonly string _url;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public HareDuClientParameters(string url, int port, string username, string password)
        {
            _url = url;
            _port = port;
            _username = username;
            _password = password;
        }

        public string Url
        {
            get { return _url; }
        }

        public int Port
        {
            get { return _port; }
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }
    }
}