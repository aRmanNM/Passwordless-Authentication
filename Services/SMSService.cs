using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using PasswordlessAuthentication.Interfaces;

namespace PasswordlessAuthentication.Services
{
    public class SMSService : ISMSService
    {
        private readonly SMSOptions _options;
        private readonly ILogger<SMSService> _logger;

        public SMSService(IOptions<SMSOptions> options, ILogger<SMSService> logger)
        {
            _logger = logger;
            _options = options.Value;
        }

        public bool SendSMS(string receptor, string authToken)
        {
            // TODO: Implement your own sms routine here

            // try
            // {
            //     var api = new KavenegarApi(_options.SMSAPIKey);
            //     api.VerifyLookup(receptor, authToken, "smsverification");
            // }
            // catch (Kavenegar.Exceptions.ApiException ex)
            // {
            //     _logger.LogError("warning", ex.Message);
            //     return false;
            // }
            // catch (Kavenegar.Exceptions.HttpException ex)
            // {
            //     _logger.LogError("warning", ex.Message);
            //     return false;
            // }

            _logger.LogInformation(authToken);

            return true;
        }
    }
}