namespace HareDu.Internal
{
    using System;
    using HareDu.Contracts;
    using Newtonsoft.Json;

    public class UserPermissionsArgsImpl :
        UserPermissionsArgs
    {
        [JsonProperty(PropertyName = "configure", Order = 1)]
        public string ConfigurePermissions { get; set; }

        [JsonProperty(PropertyName = "write", Order = 2)]
        public string WritePermissions { get; set; }

        [JsonProperty(PropertyName = "read", Order = 3)]
        public string ReadPermissions { get; set; }

        public void AssignConfigurePermissions(string configurePermissions)
        {
            if (configurePermissions == null)
                throw new ArgumentNullException("configurePermissions");

            ConfigurePermissions = configurePermissions;
        }

        public void AssignWritePermissions(string writePermissions)
        {
            if (writePermissions == null)
                throw new ArgumentNullException("writePermissions");

            WritePermissions = writePermissions;
        }

        public void AssignReadPermissions(string readPermissions)
        {
            if (readPermissions == null)
                throw new ArgumentNullException("readPermissions");

            ReadPermissions = readPermissions;
        }
    }
}