using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace xendit_csharp_example
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class XenditInvoiceRequestBody
    {
        public String ExternalId { get; set; }

        public String PayerEmail { get; set; }

        public String Description { get; set; }

        public int Amount { get; set; }

        public Boolean ShouldSendEmail { get; set; }

        public String SuccessRedirectUrl { get; set; }

        public String FailureRedirectUrl { get; set; }

        public String Currency { get; set; }
    }
}