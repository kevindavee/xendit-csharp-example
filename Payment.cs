using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace xendit_csharp_example
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Payment
    {
        public String ID { get; set; }

        public String Email { get; set; }

        public String Description { get; set; }

        public int Amount { get; set; }

        public bool ShouldSendEmail { get; set; }
    }
}