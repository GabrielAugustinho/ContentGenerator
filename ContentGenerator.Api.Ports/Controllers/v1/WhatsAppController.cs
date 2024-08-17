using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppController : ControllerBase
    {
        private readonly ISearchWhatsAppPaged _searchWhatsAppPaged;
        private readonly IAddWhatsAppNumber _addWhatsAppNumber;
        private readonly IUpdateWhatsAppNumber _updateWhatsAppNumber;
        private readonly IDeleteWhatsApp _deleteWhatsApp;
        private readonly ILogger<WhatsAppController> _logger;

        public WhatsAppController(ISearchWhatsAppPaged searchWhatsAppPaged,
                                  IAddWhatsAppNumber addWhatsAppNumber,
                                  IUpdateWhatsAppNumber updateWhatsAppNumber,
                                  IDeleteWhatsApp deleteWhatsApp,
                                  ILogger<WhatsAppController> logger)
        {
            _searchWhatsAppPaged = searchWhatsAppPaged;
            _addWhatsAppNumber = addWhatsAppNumber;
            _updateWhatsAppNumber = updateWhatsAppNumber;
            _deleteWhatsApp = deleteWhatsApp;
            _logger = logger;
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<SearchWhatsAppOutput>> GetById(int id)
        {
            _logger.LogInformation($"Iniciando a busca do contato WhatsApp com ID {id}.");

            try
            {
                var result = await _searchWhatsAppPaged.SearchById(id);

                if (result is null)
                {
                    _logger.LogWarning($"Contato WhatsApp com ID {id} não encontrado.");
                    return NotFound("WhatsApp não encontrado.");
                }

                _logger.LogInformation($"Contato WhatsApp com ID {id} encontrado com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o contato WhatsApp com ID {id}.");
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/GetAll")]
        public async Task<ActionResult<PageModel<SearchWhatsAppOutput>>> GetAll(SearchWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a busca de todos os contatos WhatsApp.");

            try
            {
                input.Active = null;
                PageModel<SearchWhatsAppOutput> result = await _searchWhatsAppPaged.SearchPaged(input);
                _logger.LogInformation("Busca de todos os contatos WhatsApp concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar todos os contatos WhatsApp.");
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/GetActives")]
        public async Task<ActionResult<PageModel<SearchWhatsAppOutput>>> GetActives(SearchWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a busca dos contatos WhatsApp ativos.");

            try
            {
                input.Active = true;
                var result = await _searchWhatsAppPaged.SearchPaged(input);
                _logger.LogInformation("Busca de contatos WhatsApp ativos concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar contatos WhatsApp ativos.");
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/GetInactives")]
        public async Task<ActionResult<PageModel<SearchWhatsAppOutput>>> GetInactives(SearchWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a busca dos contatos WhatsApp inativos.");

            try
            {
                input.Active = false;
                var result = await _searchWhatsAppPaged.SearchPaged(input);
                _logger.LogInformation("Busca de contatos WhatsApp inativos concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar contatos WhatsApp inativos.");
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(AddWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a adição de um novo contato WhatsApp.");

            try
            {
                var result = await _addWhatsAppNumber.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Erro ao adicionar o contato WhatsApp.");
                    return BadRequest("Ocorreu um erro ao tentar adicionar o contato do WhatsApp.");
                }

                _logger.LogInformation("Contato WhatsApp adicionado com sucesso.");
                return Ok("Contato adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar um novo contato WhatsApp.");
                return BadRequest("Ocorreu uma falha ao adicionar um contato no WhatsApp.");
            }
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a atualização do contato WhatsApp.");

            try
            {
                var result = await _updateWhatsAppNumber.Execute(input);

                if (!result)
                {
                    _logger.LogWarning($"Erro ao atualizar o contato WhatsApp com ID {input.Id}.");
                    return NotFound("Ocorreu um erro ao tentar atualizar o contato do WhatsApp, verifique se o Id é válido.");
                }

                _logger.LogInformation($"Contato WhatsApp com ID {input.Id} atualizado com sucesso.");
                return Ok("Contato atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o contato WhatsApp.");
                return BadRequest("Ocorreu uma falha ao atualizar o contato no WhatsApp.");
            }
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            _logger.LogInformation($"Iniciando a exclusão do contato WhatsApp com ID {id}.");

            try
            {
                var result = await _deleteWhatsApp.Execute(id);

                if (!result)
                {
                    _logger.LogWarning($"Erro ao deletar o contato WhatsApp com ID {id}.");
                    return NotFound("Ocorreu um erro ao tentar deletar o contato do WhatsApp, verifique se o Id é válido.");
                }

                _logger.LogInformation($"Contato WhatsApp com ID {id} deletado com sucesso.");
                return Ok("Contato deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar o contato WhatsApp com ID {id}.");
                return BadRequest("Ocorreu uma falha ao deletar o contato no WhatsApp.");
            }
        }
    }
}
