namespace HareDu.Model
{
    public class MessageStatsInfo
    {
        public int Published { get; set; }
        public int Acknowledged { get; set; }
        public int Delivered { get; set; }
        public int DeliveredOrGet { get; set; }
        public int Unacknowledged { get; set; }
        public int Unconfirmed{ get; set; }
        public int Uncommitted { get; set; }
        public int AcknowledgesUncommitted { get; set; }
    }
}