using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.InputPort.DallE;
using ContentGenerator.Api.Core.OutputPort.DallE;
using ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ContentGenerator.Api.Core.Services
{
    public class DallEService : IDallEService
    {
        private readonly ILoggerService<DallEService> _logger;

        public DallEService(ILoggerService<DallEService> logger)
        {
            _logger = logger;
        }

        public async Task<DalleResponse> GenerateImage(AddContentInput input, string openIAKey)
        {
            if (string.IsNullOrEmpty(openIAKey))
            {
                _logger.LogError(new ArgumentException("OpenAI key is missing"), "Failed to generate image due to missing OpenAI key.");
                return null;
            }

            string prompt = CreatePrompt(input);
            string imageSize = GetImageSizeForPlatform(input.Destinos);

            try
            {
                DalleRequest dalleRequest = new()
                {
                    n = 1,
                    prompt = prompt,
                    size = imageSize
                };

                string url = "https://api.openai.com/v1/images/generations";
                StringContent stringContent = new(content: JsonConvert.SerializeObject(dalleRequest),
                                                  encoding: Encoding.UTF8,
                                                  mediaType: "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openIAKey);

                    var response = await httpClient.PostAsync(url, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var responseContent = JsonConvert.DeserializeObject<DalleResponse>(content);
                        return responseContent;
                    }
                    else
                    {
                        _logger.LogWarning($"DallE API response was not successful. StatusCode: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while generating image with DallE. Input: {input}");
                return null;
            }
        }

        private string CreatePrompt(AddContentInput input)
        {
            return $"Crie uma imagem baseada nas seguintes informações: " +
                   $"\n\n" +
                   $"Evento do Dia (Homenagem): {input.Homenagem}.\n" +
                   $"Tipo de Validação: {input.TipoValidacao}.\n" +
                   $"Humor: {input.Humor}.\n" +
                   $"Destinos (Plataformas): {input.Destinos}.\n" +
                   $"Tipo de Assunto: {input.TipoAssunto}.\n" +
                   $"Data de Postagem: {input.DataPostagem:yyyy-MM-dd}.\n" +
                   $"Descrição do Usuário: {input.DescricaoUsuario ?? "N/A"}.\n\n" +
                   $"A imagem deve ser adequada para as plataformas especificadas e refletir o tom de humor solicitado.";
        }

        private string GetImageSizeForPlatform(string platform)
        {
            return platform.ToLower() switch
            {
                "instagram" => "1080x1080",
                "facebook" => "1200x628",
                "whatsapp" => "800x800",
                "email" => "600x400",
                "twitter" => "1200x675",
                "linkedin" => "1200x627",
                _ => "1024x1024" // Tamanho padrão caso a plataforma não seja reconhecida
            };
        }
    }
}
