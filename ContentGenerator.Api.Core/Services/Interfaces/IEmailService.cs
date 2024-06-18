using ContentGenerator.Api.Core.Entities;

namespace ContentGenerator.Api.Core.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(Assunto assunto);
    }
}
