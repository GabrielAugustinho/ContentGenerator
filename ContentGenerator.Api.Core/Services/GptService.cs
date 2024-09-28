using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using OpenAI_API;
using OpenAI_API.Completions;

namespace ContentGenerator.Api.Core.Services
{
    public class GptService : IGptService
    {
        private readonly ILoggerService<GptService> _logger;

        public GptService(ILoggerService<GptService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GenerateText(AddContentInput input, string openAiApiKey)
        {
            if (string.IsNullOrEmpty(openAiApiKey))
            {
                _logger.LogWarning("Failed to generate text due to missing OpenAI key.");
                return string.Empty;
            }

            APIAuthentication aPIAuthentication = new APIAuthentication(openAiApiKey);
            OpenAIAPI openAiApi = new OpenAIAPI(aPIAuthentication);

            try
            {
                string prompt = CreatePrompt(input);
                string model = "text-davinci-003";
                int maxTokens = 50;

                var completionRequest = new CompletionRequest
                {
                    Prompt = prompt,
                    Model = model,
                    MaxTokens = maxTokens
                };

                var completionResult = await openAiApi.Completions.CreateCompletionAsync(completionRequest);
                var generatedText = completionResult.Completions[0].Text; //completionResult.Choices[0].Text.Trim();

                if (!string.IsNullOrEmpty(generatedText))
                    return generatedText;

                _logger.LogWarning($"OpenAI response was empty. Input: {input}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while generating text with OpenAI. Input: {input}");
                return string.Empty;
            }
        }

        private string CreatePrompt(AddContentInput input)
        {
            return $"Você é um assistente de geração de conteúdo para redes sociais. " +
                   $"Baseado nas informações abaixo, gere um texto apropriado para uma postagem: " +
                   $"\n\n" +
                   $"Evento do Dia (Homenagem): {input.Homenagem}.\n" +
                   $"Tipo de Validação: {input.TipoValidacao}.\n" +
                   $"Humor: {input.Humor}.\n" +
                   $"Destinos (Plataformas): {input.Destinos}.\n" +
                   $"Tipo de Assunto: {input.TipoAssunto}.\n" +
                   $"Data de Postagem: {input.DataPostagem:yyyy-MM-dd}.\n" +
                   $"Descrição do Usuário: {input.DescricaoUsuario ?? "N/A"}.\n\n" +
                   $"Por favor, escreva um texto para uma publicação considerando as informações acima, adequado para as plataformas especificadas e no tom de humor solicitado.";
        }
    }
}
