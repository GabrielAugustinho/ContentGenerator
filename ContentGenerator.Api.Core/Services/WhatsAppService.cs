using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                var imageName = $"publicacaoConteudo{assunto.AssuntoId}";
                //var imageUrl = await UploadBase64ImageToImgBB(assunto.ImagemPost, keys.ImgBBApiKeys, imageName);


                TwilioClient.Init(keys.AccountSid, keys.AuthToken);

                long notSendnumber = 0;

                foreach (var number in input.WhatsAppNumbers)
                {
                    var message = await MessageResource.CreateAsync(
                        body: assunto.PostValidado,
                        mediaUrl: [new("https://i.ibb.co/3dGChtq/publicacao-Conteudo3.jpg")],
                        from: new Twilio.Types.PhoneNumber($"whatsapp:{keys.TwillioNumber}"),
                        to: new Twilio.Types.PhoneNumber($"whatsapp:+55{number}"));

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

        private async Task<string> UploadBase64ImageToImgBB(string base64Image, string imgbbApiKey, string imageName)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var requestContent = new MultipartFormDataContent
                    {
                        { new StringContent(imgbbApiKey), "key" },
                        { new StringContent(base64Image), "image" },
                        { new StringContent("31536000"), "expiration" },  // 1 ano em segundos (opcional)
                        { new StringContent(imageName), "name" }  // Nome da imagem
                    };

                    var response = await httpClient.PostAsync("https://api.imgbb.com/1/upload", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        return jsonResponse.data.url;
                    }
                    else
                    {
                        throw new Exception($"Falha no upload para o ImgBB: {response.StatusCode}, {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar fazer upload da imagem para ImgBB", ex);
            }
        }

    }
}
