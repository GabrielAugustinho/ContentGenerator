using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;

namespace ContentGenerator.Api.Core.UseCases.ValidationCase
{
    public class SearchValidation : ISearchValidation
    {
        private readonly IValidationRepository _validationRepository;

        public SearchValidation(IValidationRepository validationRepository)
        {
            _validationRepository = validationRepository;
        }

        public async Task<IEnumerable<SearchValidationOutput>?> Execute()
        {
            var outputList = await _validationRepository.GetAllValidationType();

            return outputList;
        }
    }
}
