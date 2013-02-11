namespace HareDu.Internal
{
    using Concerns;
    using Contracts;

    internal class ResourceRequestImpl : ResourceRequest
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public void Using(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}