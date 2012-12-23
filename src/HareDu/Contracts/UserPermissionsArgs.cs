namespace HareDu.Contracts
{
    public interface UserPermissionsArgs
    {
        void AssignConfigurePermissions(string configurePermissions);
        void AssignWritePermissions(string writePermissions);
        void AssignReadPermissions(string readPermissions);
    }
}