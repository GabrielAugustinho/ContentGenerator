using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.Models;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface IWhatsAppService
    {
        Task<bool> SendWhatsAppMessage(PublicationInput input, Assunto assunto, TwillioKeysModel keys);
    }
}
