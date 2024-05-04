using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class AddWhatsAppNumber : IAddWhatsAppNumber
    {
        private readonly IWhatsAppRepository _whatsAppRepository;

        public AddWhatsAppNumber(IWhatsAppRepository whatsAppRepository)
        {
            _whatsAppRepository = whatsAppRepository;
        }

        public async Task<bool> Execute(AddWhatsAppInput input)
        {
            var result = await _whatsAppRepository.AddWhatsAppNumber(input);
            return result;
        }
    }
}
