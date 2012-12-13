namespace HareDu.Model
{
    using System;

    public class ChannelInfo
    {
        public string Name { get; set; }
        public int ConsumerCount { get; set; }
        public int PrefetchCount { get; set; }
        public DateTime IdleSince { get; set; }
        public bool IsTransactional { get; set; }
        public bool Confirm { get; set; }
        public bool IsClientFlowBlocked { get; set; }
        public string Node { get; set; }
        public string PeerAddress { get; set; }
        public int PeerPort { get; set; }
        public string VirtualHost { get; set; }
        public string User { get; set; }
        public MessageStatsInfo MessageStats { get; set; }
    }
}