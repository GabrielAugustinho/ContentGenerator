using ContentGenerator.Api.Core.InputPort.WhatsAppPort;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces
{
    public interface IUpdateWhatsAppNumber
    {
        Task<bool> Execute(UpdateWhatsAppInput input);
    }
}
