using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class UpdateWhatsAppNumber : IUpdateWhatsAppNumber
    {
        private readonly IWhatsAppRepository _whatsAppRepository;

        public UpdateWhatsAppNumber(IWhatsAppRepository whatsAppRepository)
        {
            _whatsAppRepository = whatsAppRepository;
        }

        public async Task<bool> Execute(UpdateWhatsAppInput input)
        {
            try
            {
                var result = await _whatsAppRepository.UpdateWhatsAppNumber(input);
                return result;
            }
            catch
            {
                return default!;
            }
        }
    }
}
