using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.ContentPort;
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
        private readonly ITogetherTextService _togetherTextService;
        private readonly ITogetherImageService _togetherImageService;
        private readonly ILoggerService<AddContent> _logger;

        public AddContent(IContentRepository contentRepository,
                          IGptService gptService,
                          ILlamaService llamaService,
                          ITogetherTextService togetherTextService,
                          ITogetherImageService togetherImageService,
                          ILoggerService<AddContent> logger)
        {
            _contentRepository = contentRepository;
            _gptService = gptService;
            _llamaService = llamaService;
            _togetherTextService = togetherTextService;
            _togetherImageService = togetherImageService;
            _logger = logger;
        }

        public async Task<ContentGeneratorResponse?> Execute(AddContentInput input, KeysModel keysMode)
        {
            try
            {
                ValidateInput(input);

                string originalPost = await _togetherTextService.GenerateText(input, keysMode.TogetherApiKey);
                if (string.IsNullOrEmpty(originalPost))
                    originalPost = await GenerateText(input, keysMode.OpenIAKey, keysMode.LlamaIAKey);

                string? originalImage = await _togetherImageService.GenerateImage(input, keysMode.TogetherApiKey);
                if (string.IsNullOrEmpty(originalImage))
                    throw new Exception(message: "Failed to generate image with Together.");

                var contentInput = ParseToContentInput(input, originalPost, originalImage);
                var contentId = await _contentRepository.AddContent(contentInput);

                return contentId != null ? new ContentGeneratorResponse(contentId.Value, originalPost, originalImage) : null;
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

        private ContentInput ParseToContentInput(AddContentInput input, string originalPost, string originalImage)
        {
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
                ImagemPost = originalImage
            };
        }
    }
}
