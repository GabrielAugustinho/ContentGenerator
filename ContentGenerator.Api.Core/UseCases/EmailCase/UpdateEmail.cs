using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class UpdateEmail : IUpdateEmail
    {
        private readonly IEmailRepository _emailRepository;

        public UpdateEmail(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<bool> Execute(UpdateEmailInput input)
        {
            try
            {
                var result = await _emailRepository.UpdateEmail(input);
                return result;
            }
            catch
            {
                return default!;
            }
        }
    }
}
