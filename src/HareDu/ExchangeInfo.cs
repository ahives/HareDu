namespace HareDu
{
    public class ExchangeInfo
    {
        public string Name { get; set; }
        public string VirtualHost { get; set; }
        public string Type { get; set; }
        public bool IsDurable { get; set; }
        public bool IsSetToAutoDelete { get; set; }
        public bool IsInternal { get; set; }
    }
}