using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;

namespace xendit_csharp_example.Controllers
{
    [ApiController]
    [Route("payments")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        
        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public String Post([FromBody] Payment body)
        {   
            // Added minimum parameters to perform a succesful request to /v2/invoices.
            // To add another parameters, go to https://developers.xendit.co/api-reference/#create-invoice
            // and check for detailed parameters.
            var requestBody = new XenditInvoiceRequestBody();
            requestBody.ExternalId = body.ID;
            requestBody.Description = body.Description;
            requestBody.PayerEmail = body.Email;
            requestBody.ShouldSendEmail = body.ShouldSendEmail;
            requestBody.Amount = body.Amount;
            requestBody.Currency = "IDR";
            requestBody.SuccessRedirectUrl = "https://my-site.com/success";
            requestBody.FailureRedirectUrl = "https://my-site.com/failure";

            DefaultContractResolver contractResolver = new DefaultContractResolver{
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            var reqBodyContent = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            
            var xenditAPIKey = Environment.GetEnvironmentVariable("XENDIT_API_KEY");
            var client = new RestClient("https://api.xendit.co/v2/invoices");
            client.Timeout = 30 * 60000;
            client.Authenticator = new HttpBasicAuthenticator(xenditAPIKey, "");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", reqBodyContent,  ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            
            return response.Content;
        }
    }
}