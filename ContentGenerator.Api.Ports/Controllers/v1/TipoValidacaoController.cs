using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoValidacaoController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TipoValidacaoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<TipoValidacao>>> GetAll()
        {
            List<TipoValidacao> tipoValidacoes = await _dataContext.TipoValidacao.ToListAsync();

            return Ok(tipoValidacoes);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<List<TipoValidacao>>> Add(TipoValidacao input)
        {
            _dataContext.TipoValidacao.Add(input);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.TipoValidacao.ToListAsync());
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<List<TipoValidacao>>> Delete(int id)
        {
            TipoValidacao? dbTipoValidacao = await _dataContext.TipoValidacao.FindAsync(id);
            if (dbTipoValidacao is null)
                return NotFound("Tipo validacao não encontrada.");

            _dataContext.TipoValidacao.Remove(dbTipoValidacao);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.TipoValidacao.ToListAsync());
        }
    }
}
