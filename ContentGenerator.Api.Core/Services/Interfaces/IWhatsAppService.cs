using ContentGenerator.Api.Core.Entities;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface IWhatsAppService
    {
        Task SendWhatsAppMessage(Assunto assunto);
    }
}
