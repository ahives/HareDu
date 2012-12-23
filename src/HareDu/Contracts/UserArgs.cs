namespace HareDu.Contracts
{
    public interface UserArgs
    {
        void WithPassword(string password);
        void WithTag(string tags);
    }
}