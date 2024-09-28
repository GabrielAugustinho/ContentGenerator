using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.Services.Interfaces.ContentGenerator.Api.Core.Services.Interfaces;
using ContentGenerator.Api.Core.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ContentGenerator.Api.Core.Services
{
    public class TogetherImageService : ITogetherImageService
    {
        private readonly ILoggerService<TogetherImageService> _logger;

        public TogetherImageService(ILoggerService<TogetherImageService> logger)
        {
            _logger = logger;
        }

        public async Task<string?> GenerateImage(AddContentInput input, string togetherApiKey)
        {
            if (string.IsNullOrEmpty(togetherApiKey))
            {
                _logger.LogWarning("Failed to generate image due to missing Together AI key.");
                return null;
            }

            string prompt = CreatePrompt(input);
            string imageSize = GetImageSizeForPlatform(input.Destinos);

            try
            {
                var requestBody = new
                {
                    model = "stabilityai/stable-diffusion-xl-base-1.0",  // Modelo para gerar a imagem
                    prompt = prompt,
                    width = int.Parse(imageSize.Split('x')[0]),
                    height = int.Parse(imageSize.Split('x')[1]),
                    steps = 40,
                    n = 1
                };

                var message = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", togetherApiKey);
                    var response = await httpClient.PostAsync("https://api.together.xyz/inference", message);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        return jsonResponse.output.choices[0].image_base64;
                    }
                    else
                    {
                        _logger.LogWarning($"Together API image generation failed. StatusCode: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred while generating image with Together AI. Input: {input}");
                return null;
            }
        }

        // Método para criar o prompt com base nas informações do input
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

        // Método para obter o tamanho da imagem com base na plataforma de destino
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
