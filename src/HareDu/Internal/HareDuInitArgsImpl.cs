namespace HareDu
{
    using Contracts;

    public class HareDuInitArgsImpl :
        HareDuInitArgs
    {
        public void ConnectTo(string hostUrl)
        {
            HostUrl = hostUrl;
        }

        public void UsingCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string HostUrl { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}