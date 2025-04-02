namespace HealthcareApi.SmsService
{
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;

    public class SmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
            var smsSettings = _configuration.GetSection("SmsSettings");

            TwilioClient.Init(smsSettings["AccountSid"], smsSettings["AuthToken"]);
        }

        public async Task SendSmsAsync(string toNumber, string message)
        {
            var fromNumber = _configuration["SmsSettings:FromNumber"];
            var messageResource = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(fromNumber),
                to: new Twilio.Types.PhoneNumber(toNumber)
            );

            // Optionally, you can log or handle the messageResource as needed
        }
    }
}