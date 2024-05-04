using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public WhatsAppController(ISearchWhatsAppPaged searchWhatsAppPaged,
                                  IAddWhatsAppNumber addWhatsAppNumber,
                                  IUpdateWhatsAppNumber updateWhatsAppNumber,
                                  IDeleteWhatsApp deleteWhatsApp)
        {
            _searchWhatsAppPaged = searchWhatsAppPaged;
            _addWhatsAppNumber = addWhatsAppNumber;
            _updateWhatsAppNumber = updateWhatsAppNumber;
            _deleteWhatsApp = deleteWhatsApp;
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<SearchWhatsAppOutput>> GetById(int id)
        {
            try
            {
                var result = await _searchWhatsAppPaged.SearchById(id);

                if (result is null)
                    return NotFound("WhatsApp não encontrado.");

                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/GetAll")]
        public async Task<ActionResult<PageModel<SearchWhatsAppOutput>>> GetAll(SearchWhatsAppInput input)
        {
            try
            {
                input.Active = null;
                PageModel<SearchWhatsAppOutput> result = await _searchWhatsAppPaged.SearchPaged(input);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/GetActives")]
        public async Task<ActionResult<PageModel<SearchWhatsAppOutput>>> GetActives(SearchWhatsAppInput input)
        {
            try
            {
                input.Active = true;
                var result = await _searchWhatsAppPaged.SearchPaged(input);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/GetInactives")]
        public async Task<ActionResult<PageModel<SearchWhatsAppOutput>>> GetInactives(SearchWhatsAppInput input)
        {
            try
            {
                input.Active = false;
                var result = await _searchWhatsAppPaged.SearchPaged(input);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao listar os contatos do WhatsApp.");
            }
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<string>> Add(AddWhatsAppInput input)
        {
            try
            {
                var result = await _addWhatsAppNumber.Execute(input);

                if (!result)
                    return BadRequest("Ocorreu um erro ao tentar adicionar o contato do WhatsApp.");

                return Ok("Contato adicionado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao adicionar um contato no WhatsApp.");
            }
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<string>> Update(UpdateWhatsAppInput input)
        {
            try
            {
                var result = await _updateWhatsAppNumber.Execute(input);

                if (!result)
                    return NotFound("Ocorreu um erro ao tentar atualizar o contato do WhatsApp, verifique se o Id é válido.");

                return Ok("Contato atualizado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao atualizar o contato no WhatsApp.");
            }
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                var result = await _deleteWhatsApp.Execute(id);

                if (!result)
                    return NotFound("Ocorreu um erro ao tentar deletar o contato do WhatsApp, verifique se o Id é válido.");

                return Ok("Contato deletado com sucesso.");
            }
            catch
            {
                return BadRequest("Ocorreu uma falha ao deletar o contato no WhatsApp.");
            }
        }
    }
}
