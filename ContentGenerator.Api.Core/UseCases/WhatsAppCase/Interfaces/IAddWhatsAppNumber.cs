using ContentGenerator.Api.Core.InputPort.WhatsAppPort;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces
{
    public interface IAddWhatsAppNumber
    {
        Task<bool> Execute(AddWhatsAppInput input);
    }
}
