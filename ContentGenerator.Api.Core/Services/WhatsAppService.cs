using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ContentGenerator.Api.Core.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly ILogger<WhatsAppService> _logger;

        public WhatsAppService(ILogger<WhatsAppService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendWhatsAppMessage(PublicationInput input, Assunto assunto, TwillioKeysModel keys)
        {
            try
            {
                TwilioClient.Init(keys.AccountSid, keys.AuthToken);

                long notSendnumber = 0;

                foreach (var number in input.WhatsAppNumbers)
                {
                    var message = await MessageResource.CreateAsync(
                        body: assunto.PostValidado,
                        mediaUrl: [new Uri("https://demo.twilio.com/owl.png")],
                        from: new Twilio.Types.PhoneNumber($"whatsapp:{keys.TwillioNumber}"),
                        to: new Twilio.Types.PhoneNumber($"whatsapp:{number}"));

                    if (!string.IsNullOrEmpty(message.ErrorCode.ToString()))
                        notSendnumber++;
                }

                _logger.LogInformation($"Sending WhatsApp message for AssuntoId: {assunto.AssuntoId}, have {input.WhatsAppNumbers.Count - notSendnumber} success send and {notSendnumber} fails message.");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro durante o envio da mensagem", ex);
            }
        }
    }
}
