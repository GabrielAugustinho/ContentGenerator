using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.EmailPort;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
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

        public EmailController(IDeleteEmail deleteEmail,
                               IUpdateEmail updateEmail,
                               IAddEmail addEmail,
                               ISearchEmailPaged searchEmailInput)
        {
            _deleteEmail = deleteEmail;
            _updateEmail = updateEmail;
            _addEmail = addEmail;
            _searchEmailPaged = searchEmailInput;
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<SearchEmailOutput>> GetById(int id)
        {
            try
            {
                var result = await _searchEmailPaged.SearchById(id);

                if (result is null)
                    return NotFound("Email não encontrado.");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar o email.");
            }
        }

        [HttpPost("v1/GetAll")]
        public async Task<ActionResult<PageModel<SearchEmailOutput>>> GetAll(SearchEmailInput input)
        {
            try
            {
                input.Active = null;
                PageModel<SearchEmailOutput> result = await _searchEmailPaged.SearchPaged(input);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os Emails.");
            }
        }

        [HttpPost("v1/GetActives")]
        public async Task<ActionResult<PageModel<SearchEmailOutput>>> GetActives(SearchEmailInput input)
        {
            try
            {
                input.Active = true;
                var result = await _searchEmailPaged.SearchPaged(input);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os emails.");
            }
        }

        [HttpPost("v1/GetInactives")]
        public async Task<ActionResult<PageModel<SearchEmailOutput>>> GetInactives(SearchEmailInput input)
        {
            try
            {
                input.Active = false;
                var result = await _searchEmailPaged.SearchPaged(input);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os emails.");
            }
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(AddEmailInput input)
        {
            try
            {
                var result = await _addEmail.Execute(input);

                if (!result)
                    return BadRequest("Ocorreu um erro ao tentar adicionar o Email.");

                return Ok("Email adicionado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao adicionar um Email.");
            }
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateEmailInput input)
        {
            try
            {
                var result = await _updateEmail.Execute(input);

                if (!result)
                    return NotFound("Ocorreu um erro ao tentar atualizar o Email, verifique se o Id é válido.");

                return Ok("Email atualizado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao atualizar o Email.");
            }
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var result = await _deleteEmail.Execute(id);

                if (!result)
                    return NotFound("Ocorreu um erro ao tentar deletar o email, verifique se o Id é válido.");

                return Ok("Email deletado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao deletar o email.");
            }
        }
    }
}
