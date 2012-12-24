using System.Collections.Generic;

namespace HareDu.Model
{
    //(Generated from http://json2csharp.com/) using:
    /*
     * {"management_version":"2.7.0","statistics_level":"fine","message_stats":[],"queue_totals":{"messages":4,"messages_ready":4,"messages_unacknowledged":0},"node":"rabbit@A720KMQ1-W7","statistics_db_node":"rabbit@A720KMQ1-W7","listeners":[{"node":"rabbit@A720KMQ1-W7","protocol":"amqp","ip_address":"0.0.0.0","port":5672},{"node":"rabbit@A720KMQ1-W7","protocol":"amqp","ip_address":"::","port":5672}],"contexts":[{"node":"rabbit@A720KMQ1-W7","description":"RabbitMQ Management","path":"/","port":55672}]}
     */
    public class Overview
    {
        public string management_version { get; set; }
        public string statistics_level { get; set; }
        public List<object> message_stats { get; set; }
        public QueueTotals queue_totals { get; set; }
        public string node { get; set; }
        public string statistics_db_node { get; set; }
        public List<Listener> listeners { get; set; }
        public List<Context> contexts { get; set; }
    }

    public class QueueTotals
    {
        public int messages { get; set; }
        public int messages_ready { get; set; }
        public int messages_unacknowledged { get; set; }
    }

    public class Listener
    {
        public string node { get; set; }
        public string protocol { get; set; }
        public string ip_address { get; set; }
        public int port { get; set; }
    }

    public class Context
    {
        public string node { get; set; }
        public string description { get; set; }
        public string path { get; set; }
        public int port { get; set; }
    }
}