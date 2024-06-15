using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.ContentPort;
using ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.ContentCase
{
    public class SearchContent : ISearchContent
    {
        private readonly IContentRepository _contentRepository;
        private readonly ILogger<SearchContent> _logger;

        public SearchContent(IContentRepository contentRepository, ILogger<SearchContent> logger)
        {
            _contentRepository = contentRepository;
            _logger = logger;
        }

        public async Task<SearchContentOutput?> SearchById(int id)
        {
            try
            {
                _logger.LogInformation("Searching for content with ID {Id}", id);
                var assunto = await _contentRepository.GetContentById(id);
                if (assunto == null)
                {
                    _logger.LogWarning("Content with ID {Id} not found.", id);
                    return null;
                }

                _logger.LogInformation("Content with ID {Id} found.", id);
                return AssuntoToOutput(assunto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to search for content with ID {Id}.", id);
                return null;
            }
        }

        public async Task<IEnumerable<SearchContentOutput>> ContentsOfMonth()
        {
            try
            {
                _logger.LogInformation("Fetching contents for the current month.");
                var assuntos = await _contentRepository.GetContentOfMonth();
                return assuntos.Select(AssuntoToOutput);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch contents for the current month.");
                return Enumerable.Empty<SearchContentOutput>();
            }
        }

        private static SearchContentOutput AssuntoToOutput(Assunto assunto)
        {
            return new SearchContentOutput(
                contentId: assunto.AssuntoId,
                searchValidationOutput: assunto.TipoValidacao.Tipo,
                searchHumorOutput: assunto.Humor.Descricao,
                searchDestinyOutput: assunto.Destinos.Descricao,
                searchEventTypeOutput: assunto.TipoAssunto.Assunto,
                criationDate: assunto.DataCriacao,
                userDesc: assunto.ObjEveAssunto,
                generationTextDate: assunto.DataGeracao,
                originalPost: assunto.PostOriginal,
                validationDate: assunto.DataValida,
                postValidated: assunto.PostValidado,
                postDate: assunto.DataPublicacao,
                postImage: assunto.ImagemPost,
                userImage: assunto.IncluirImg,
                active: assunto.Ativo);
        }
    }
}
