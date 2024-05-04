using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class DeleteWhatsApp : IDeleteWhatsApp
    {
        private readonly IWhatsAppRepository _whatsAppRepository;

        public DeleteWhatsApp(IWhatsAppRepository whatsAppRepository)
        {
            _whatsAppRepository = whatsAppRepository;
        }

        public async Task<bool> Execute(int id)
        {
            var result = await _whatsAppRepository.DeleteWhatsAppNumber(id);
            return result;
        }
    }
}
