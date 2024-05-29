using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class AddEmail : IAddEmail
    {
        private readonly IEmailRepository _emailRepository;

        public AddEmail(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<bool> Execute(AddEmailInput input)
        {
            var result = await _emailRepository.AddEmail(input);
            return result;
        }
    }
}
