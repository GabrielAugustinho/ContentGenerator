using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class DeleteEmail : IDeleteEmail
    {
        private readonly IEmailRepository _emailRepository;

        public DeleteEmail(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<bool> Execute(int id)
        {
            var result = await _emailRepository.DeleteEmail(id);
            return result;
        }
    }
}
