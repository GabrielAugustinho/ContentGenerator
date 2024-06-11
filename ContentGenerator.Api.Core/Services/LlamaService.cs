using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.InputPort.Llama;
using ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ContentGenerator.Api.Core.Services
{
    public class LlamaService : ILlamaService
    {
        private readonly ILoggerService<LlamaService> _logger;

        public LlamaService(ILoggerService<LlamaService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GenerateText(AddContentInput input, string llamaIAKey)
        {
            if (string.IsNullOrEmpty(llamaIAKey))
            {
                _logger.LogError(new ArgumentException("Llama key is missing"), "Failed to generate text due to missing Llama key.");
                return string.Empty;
            }

            string prompt = CreatePrompt(input);

            try
            {
                LlamaRequest llamaRequest = new(prompt);
                var url = "https://api.together.xyz/v1/chat/completions";
                var Message = new StringContent(content: JsonConvert.SerializeObject(llamaRequest),
                                                encoding: Encoding.UTF8,
                                                mediaType: "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", llamaIAKey);

                    var response = await httpClient.PostAsync(url, Message);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        _logger.LogWarning($"Llama API response was not successful. StatusCode: {response.StatusCode}");
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while generating text with Llama. Input: {input}");
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
