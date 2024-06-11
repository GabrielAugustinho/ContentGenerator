using ChatGPT.Net;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ContentGenerator.Api.Core.Services
{
    public class GptService : IGptService
    {
        private readonly ILoggerService<GptService> _logger;

        public GptService(ILoggerService<GptService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GenerateText(AddContentInput input, string openIAKey)
        {
            if (string.IsNullOrEmpty(openIAKey))
            {
                _logger.LogError(new ArgumentException("OpenAI key is missing"), "Failed to generate text due to missing OpenAI key.");
                return string.Empty;
            }

            string prompt = CreatePrompt(input);

            try
            {
                var openai = new ChatGpt(openIAKey);
                var responseContent = await openai.Ask(prompt);

                if (!string.IsNullOrEmpty(responseContent))
                    return responseContent;

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
