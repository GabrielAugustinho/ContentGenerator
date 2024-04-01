using ChatGPT.Net;
using ContentGenerator.Api.Core.InputPort.ChatGpt;
using ContentGenerator.Api.Core.InputPort.DallE;
using ContentGenerator.Api.Core.InputPort.Llama;
using ContentGenerator.Api.Core.OutputPort.ChatGpt;
using ContentGenerator.Api.Core.OutputPort.DallE;
using ContentGenerator.Api.Core.OutputPort.Llama;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IAController : ControllerBase
    {
        private IConfiguration _configuration;

        private string OpenAiKey = string.Empty;
        private string LlamaAiKey = string.Empty;

        public IAController(ILogger<IAController> logger, IConfiguration configuration)
        {
            _configuration = configuration;

            OpenAiKey = _configuration["OPENAI_API_KEY"];
            LlamaAiKey = _configuration["LLAMA_API_KEY"];
        }

        [HttpPost("v1/GetGptContent")]
        public async Task<ActionResult<string>> GetGptContent([FromBody] ChatGptRequest chatGptRequest)
        {
            try
            {
                if (OpenAiKey == null)
                    return NotFound("Chave da OpenIA API não encontrada.");

                var openai = new ChatGpt(OpenAiKey);

                var fixedSentence = await openai.Ask($"escrever aqui um prompt que seja bom definir depois: {chatGptRequest.Prompt}"); // ATENÇÃO NO PROMPT
                if (fixedSentence == null)
                    return NotFound("Não foi possível conectar com o ChatGpt.");

                return Ok(new ChatGptResponse() { GeneratedContent = fixedSentence });
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao criar a mensagem.");
            }
        }

        [HttpPost("v1/GetLlamaContent")]
        public async Task<ActionResult<string>> GetLlamaContent([FromBody] ChatGptRequest request)
        {
            try
            {
                LlamaRequest llamaRequest = new(request.Prompt);
                string responseContent = string.Empty;
                var url = "https://api.together.xyz/v1/chat/completions";
                var Message = new StringContent(content: JsonConvert.SerializeObject(llamaRequest),
                                                encoding: Encoding.UTF8,
                                                mediaType: "application/json");

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LlamaAiKey);

                    var response = await httpClient.PostAsync(url, Message);

                    if (response.IsSuccessStatusCode)
                    {
                        responseContent = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return BadRequest($"Falha ao gerar o texto. Status code: {response.StatusCode}");
                    }
                }

                return Ok(new LlamaResponse() { GeneratedContent = responseContent });
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao criar a mensagem.");
            }
        }

        [HttpPost("v1/GetDallEContent")]
        public async Task<ActionResult<DalleResponse>> GetDallEContent([FromBody] DalleRequest dalleRequest)
        {
            var resp = new DalleResponse();
            var url = "https://api.openai.com/v1/images/generations";
            var stringContent = new StringContent(content: JsonConvert.SerializeObject(dalleRequest),
                                                  encoding: Encoding.UTF8,
                                                  mediaType: "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAiKey);

                var Message = await httpClient.PostAsync(url, stringContent);

                if (Message.IsSuccessStatusCode)
                {
                    var content = await Message.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<DalleResponse>(content);
                }
                else
                {
                    return BadRequest($"Falha ao gerar a imagem. Status code: {Message.StatusCode}");
                }
            }

            return Ok(resp);
        }
    }
}
