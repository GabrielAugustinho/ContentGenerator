using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.OutputPort.ContentPort;
using ContentGenerator.Api.Core.OutputPort.DallE;
using ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.ContentCase
{
    public class AddContent : IAddContent
    {
        private readonly IContentRepository _contentRepository;
        private readonly IGptService _gptService;
        private readonly ILlamaService _llamaService;
        private readonly IDallEService _dallEService;
        private readonly ILoggerService<AddContent> _logger;

        public AddContent(IContentRepository contentRepository,
                          IGptService gptService,
                          ILlamaService llamaService,
                          IDallEService dallEService,
                          ILoggerService<AddContent> logger)
        {
            _contentRepository = contentRepository;
            _gptService = gptService;
            _llamaService = llamaService;
            _dallEService = dallEService;
            _logger = logger;
        }

        public async Task<ContentGeneratorResponse?> Execute(AddContentInput input, string openIAKey, string llamaIAKey)
        {
            try
            {
                ValidateInput(input);

                string originalPost = await GenerateText(input, openIAKey, llamaIAKey);
                if (string.IsNullOrEmpty(originalPost))
                {
                    _logger.LogWarning($"Falha ao gerar o conteudo. Input: {input}");
                    return null;
                }

                var dalleResponse = await _dallEService.GenerateImage(input, openIAKey);
                if (dalleResponse?.Data == null || dalleResponse.Data.Count == 0)
                {
                    _logger.LogWarning($"Failed to generate image. Input: {input}");
                    return null;
                }

                var contentInput = ParseToContentInput(input, originalPost, dalleResponse);
                var contentId = await _contentRepository.AddContent(contentInput);

                return contentId != null ? new ContentGeneratorResponse(contentId.Value, originalPost, dalleResponse) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while adding content. Input: {input}");
                return null;
            }
        }

        private void ValidateInput(AddContentInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
        }

        private async Task<string> GenerateText(AddContentInput input, string openIAKey, string llamaIAKey)
        {
            string originalPost = await _gptService.GenerateText(input, openIAKey);
            return !string.IsNullOrEmpty(originalPost)
                ? originalPost
                : await _llamaService.GenerateText(input, llamaIAKey);
        }

        private ContentInput ParseToContentInput(AddContentInput input, string originalPost, DalleResponse dalleResponse)
        {
            string imagePost = dalleResponse.Data.FirstOrDefault()?.Url;
            return new ContentInput
            {
                TipoValidacaoId = input.TipoValidacaoId,
                HumorId = input.HumorId,
                DestinosId = input.DestinosId,
                TipoAssuntoId = input.TipoAssuntoId,
                DataCriacao = input.DataPostagem,
                ObjEveAssunto = input.DescricaoUsuario,
                DataGeracao = DateTime.UtcNow,
                PostOriginal = originalPost,
                ImagemPost = imagePost
            };
        }
    }
}