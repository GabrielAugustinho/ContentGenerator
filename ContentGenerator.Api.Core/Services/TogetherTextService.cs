using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ContentGenerator.Api.Core.Services
{
    public class TogetherTextService : ITogetherTextService
    {
        private readonly ILoggerService<TogetherTextService> _logger;

        public TogetherTextService(ILoggerService<TogetherTextService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GenerateText(AddContentInput input, string togetherApiKey)
        {
            if (string.IsNullOrEmpty(togetherApiKey))
            {
                _logger.LogWarning("Failed to generate text due to missing Together AI key.");
                return string.Empty;
            }

            string prompt = CreatePrompt(input);

            try
            {
                var requestBody = new
                {
                    model = "meta-llama/Llama-2-13b-chat-hf",
                    messages = new[] { new { role = "user", content = prompt } },
                    max_tokens = 512,
                    temperature = 0.7,
                    top_p = 0.7,
                    top_k = 50,
                    repetition_penalty = 1,
                    stop = new[] { "[/INST]", "</s>" }
                };

                var message = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", togetherApiKey);
                    var response = await httpClient.PostAsync("https://api.together.xyz/v1/chat/completions", message);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        // Parse response to extract generated text
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        return jsonResponse.choices[0].message.content;
                    }
                    else
                    {
                        _logger.LogWarning($"Together API response was not successful. StatusCode: {response.StatusCode}");
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while generating text with Together AI. Input: {input}");
                return string.Empty;
            }
        }

        private string CreatePrompt(AddContentInput input)
        {
            return $"Baseado nas informações abaixo, gere um texto para uma postagem em uma rede social: \n\n" +
                   $"O texto deve se basear no seguinte evento \"{input.Homenagem}\".\n" +
                   $"O humor do texto deve ser \"{input.Humor}\".\n" +
                   $"O texto será publicado no \"{input.Destinos}\" então não deve ser muito grande para se adequar a essa rede.\n" +
                   $"A dia da celebração será {input.DataPostagem:yyyy-MM-dd}.\n" +
                   $"{input.DescricaoUsuario ?? string.Empty}\n" +
                   $"Escreva um texto apropriado para o evento, o texto deve estar em português.";
        }
    }
}
