using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.OutputPort.ContentPort;
using ContentGenerator.Api.Core.UseCases.ContentCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConteudoController : ControllerBase
    {
        private readonly IAddContent _addContent;
        private readonly ISearchContent _searchContent;
        private readonly IUpdateContent _updateContent;
        private readonly ILogger<ConteudoController> _logger;
        private readonly string _openAiKey;
        private readonly string _llamaAiKey;

        public ConteudoController(ISearchContent searchContent,
                                  IAddContent addContent,
                                  IUpdateContent updateContent,
                                  IConfiguration configuration,
                                  ILogger<ConteudoController> logger)
        {
            _searchContent = searchContent;
            _addContent = addContent;
            _updateContent = updateContent;
            _logger = logger;

            _openAiKey = configuration["OPENAI_API_KEY"] ?? throw new ArgumentNullException(nameof(configuration), "OpenAI API key is missing.");
            _llamaAiKey = configuration["LLAMA_API_KEY"] ?? throw new ArgumentNullException(nameof(configuration), "Llama API key is missing.");
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<ContentGeneratorResponse>> Add(AddContentInput input)
        {
            try
            {
                var result = await _addContent.Execute(input, _openAiKey, _llamaAiKey);

                if (result == null)
                {
                    _logger.LogWarning("Falha ao adicionar o conteudo. Input: {@input}", input);
                    return BadRequest("Ocorreu uma falha ao tentar adicionar o conteúdo.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uma exception ocorreu durante a adição. Input: {@input}", input);
                return StatusCode(500, "Ocorreu uma exception ao adicionar o conteúdo.");
            }
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<SearchContentOutput>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Searching for content with ID {Id}", id);
                var result = await _searchContent.SearchById(id);

                if (result is null)
                {
                    _logger.LogWarning("Content with ID {Id} not found.", id);
                    return NotFound("Conteúdo não encontrado.");
                }

                _logger.LogInformation("Content with ID {Id} found.", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to search for content with ID {Id}.", id);
                return BadRequest("Ocorreu uma falha ao buscar o conteúdo pelo ID.");
            }
        }

        [HttpPost("v1/GetAllContentOfMonth")]
        public async Task<ActionResult<List<SearchContentOutput>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all content for the current month.");
                var result = await _searchContent.ContentsOfMonth();

                if (!result.Any())
                {
                    _logger.LogWarning("No content found for the current month.");
                    return NotFound("Nenhum conteúdo encontrado para este mês.");
                }

                _logger.LogInformation("Fetched all content for the current month.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch content for the current month.");
                return BadRequest("Ocorreu uma falha ao listar os conteúdos deste mês.");
            }
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateContentInput input)
        {
            try
            {
                _logger.LogInformation("Updating content with ID {Id}", input.ContentId);
                var result = await _updateContent.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Failed to update content with ID {Id}", input.ContentId);
                    return NotFound("Ocorreu um erro ao tentar atualizar o conteúdo, verifique se o ID é válido.");
                }

                _logger.LogInformation("Content with ID {Id} updated successfully.", input.ContentId);
                return Ok("Conteúdo atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update content with ID {Id}", input.ContentId);
                return BadRequest("Ocorreu uma falha ao atualizar o conteúdo.");
            }
        }
    }
}
