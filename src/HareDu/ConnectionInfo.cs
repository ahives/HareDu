namespace HareDu
{
    public class ConnectionInfo
    {
        public string Name { get; set; }
        public string VirtualHost { get; set; }
        public string Type { get; set; }
        public int Port { get; set; }
        public string State { get; set; }
        public int Channels { get; set; }
        public string Node { get; set; }
        public string Address { get; set; }
        public string PeerAddress { get; set; }
        public int PeerPort { get; set; }
        public bool IsRunningWithSSL { get; set; }
        public string SslProtocol { get; set; }
        public string SslKeyExchange { get; set; }
        public string SslCipher { get; set; }
        public string SslHash { get; set; }
        public string AuthenticationMechanismUsed { get; set; }
        public string PeerCertificateIssuer { get; set; }
        public string Protocol { get; set; }
        public string User { get; set; }
        public int Timeout { get; set; }
    }
}