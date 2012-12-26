namespace HareDu.Contracts
{
    public interface HareDuInitArgs
    {
        void ConnectTo(string hostUrl);
        void UsingCredentials(string username, string password);
    }
}