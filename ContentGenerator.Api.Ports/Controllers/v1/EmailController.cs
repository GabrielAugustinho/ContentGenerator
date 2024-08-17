using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.EmailPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IDeleteEmail _deleteEmail;
        private readonly IUpdateEmail _updateEmail;
        private readonly IAddEmail _addEmail;
        private readonly ISearchEmailPaged _searchEmailPaged;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IDeleteEmail deleteEmail,
                               IUpdateEmail updateEmail,
                               IAddEmail addEmail,
                               ISearchEmailPaged searchEmailInput,
                               ILogger<EmailController> logger)
        {
            _deleteEmail = deleteEmail;
            _updateEmail = updateEmail;
            _addEmail = addEmail;
            _searchEmailPaged = searchEmailInput;
            _logger = logger;
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<SearchEmailOutput>> GetById(int id)
        {
            _logger.LogInformation("Buscando email pelo ID: {Id}", id);

            try
            {
                var result = await _searchEmailPaged.SearchById(id);

                if (result is null)
                {
                    _logger.LogWarning("Email com ID {Id} não encontrado.", id);
                    return NotFound("Email não encontrado.");
                }

                _logger.LogInformation("Email com ID {Id} encontrado com sucesso.", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o email com ID {Id}.", id);
                return BadRequest("Ocorreu uma falha ao listar o email.");
            }
        }

        [HttpPost("v1/GetAll")]
        public async Task<ActionResult<PageModel<SearchEmailOutput>>> GetAll(SearchEmailInput input)
        {
            _logger.LogInformation("Buscando todos os emails.");

            try
            {
                input.Active = null;
                var result = await _searchEmailPaged.SearchPaged(input);
                _logger.LogInformation("Busca de todos os emails concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os emails.");
                return BadRequest("Ocorreu uma falha ao listar os Emails.");
            }
        }

        [HttpPost("v1/GetActives")]
        public async Task<ActionResult<PageModel<SearchEmailOutput>>> GetActives(SearchEmailInput input)
        {
            _logger.LogInformation("Buscando emails ativos.");

            try
            {
                input.Active = true;
                var result = await _searchEmailPaged.SearchPaged(input);
                _logger.LogInformation("Busca de emails ativos concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar emails ativos.");
                return BadRequest("Ocorreu uma falha ao listar os emails.");
            }
        }

        [HttpPost("v1/GetInactives")]
        public async Task<ActionResult<PageModel<SearchEmailOutput>>> GetInactives(SearchEmailInput input)
        {
            _logger.LogInformation("Buscando emails inativos.");

            try
            {
                input.Active = false;
                var result = await _searchEmailPaged.SearchPaged(input);
                _logger.LogInformation("Busca de emails inativos concluída com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar emails inativos.");
                return BadRequest("Ocorreu uma falha ao listar os emails.");
            }
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(AddEmailInput input)
        {
            _logger.LogInformation("Adicionando novo email.");

            try
            {
                var result = await _addEmail.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Erro ao adicionar email.");
                    return BadRequest("Ocorreu um erro ao tentar adicionar o Email.");
                }

                _logger.LogInformation("Email adicionado com sucesso.");
                return Ok("Email adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar email.");
                return BadRequest("Ocorreu uma falha ao adicionar um Email.");
            }
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateEmailInput input)
        {
            _logger.LogInformation("Atualizando email com ID: {Id}", input.Id);

            try
            {
                var result = await _updateEmail.Execute(input);

                if (!result)
                {
                    _logger.LogWarning("Erro ao atualizar email com ID {Id}.", input.Id);
                    return NotFound("Ocorreu um erro ao tentar atualizar o Email, verifique se o Id é válido.");
                }

                _logger.LogInformation("Email com ID {Id} atualizado com sucesso.", input.Id);
                return Ok("Email atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar email com ID {Id}.", input.Id);
                return BadRequest("Ocorreu uma falha ao atualizar o Email.");
            }
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            _logger.LogInformation("Deletando email com ID: {Id}", id);

            try
            {
                var result = await _deleteEmail.Execute(id);

                if (!result)
                {
                    _logger.LogWarning("Erro ao deletar email com ID {Id}.", id);
                    return NotFound("Ocorreu um erro ao tentar deletar o email, verifique se o Id é válido.");
                }

                _logger.LogInformation("Email com ID {Id} deletado com sucesso.", id);
                return Ok("Email deletado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar email com ID {Id}.", id);
                return BadRequest("Ocorreu uma falha ao deletar o email.");
            }
        }
    }
}
