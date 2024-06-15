using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.ContentCase
{
    public class UpdateContent : IUpdateContent
    {
        private readonly IContentRepository _contentRepository;
        private readonly ILogger<UpdateContent> _logger;

        public UpdateContent(IContentRepository contentRepository, ILogger<UpdateContent> logger)
        {
            _contentRepository = contentRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(UpdateContentInput input)
        {
            try
            {
                _logger.LogInformation("Executing update for content with ID {Id}", input.ContentId);
                var result = await _contentRepository.UpdateContent(input);

                if (!result)
                {
                    _logger.LogWarning("Failed to update content with ID {Id}", input.ContentId);
                    return false;
                }

                _logger.LogInformation("Content with ID {Id} updated successfully.", input.ContentId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to execute update for content with ID {Id}", input.ContentId);
                return false;
            }
        }
    }

}
