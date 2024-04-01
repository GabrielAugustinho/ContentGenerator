using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomenagemController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public HomenagemController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<Homenagem>>> GetAll()
        {
            List<Homenagem> homenagens = await _dataContext.Homenagem.ToListAsync();

            return Ok(homenagens);
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<Homenagem>> GetById(int id)
        {
            Homenagem? homenagem = await _dataContext.Homenagem.FindAsync(id);

            if (homenagem is null)
                return NotFound("Homenagem não encontrada.");

            return Ok(homenagem);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<Homenagem>> Add(Homenagem input)
        {
            _dataContext.Homenagem.Add(input);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), await _dataContext.Homenagem.FindAsync(input.HomenagemId), input);
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<Homenagem>> Update(Homenagem input)
        {
            Homenagem? dbHomenagem = await _dataContext.Homenagem.FindAsync(input.HomenagemId);
            if (dbHomenagem is null)
                return NotFound("Homenagem não encontrada.");

            dbHomenagem.Update(input.Destinos, input.Humor, input.TipoValidacao, input.TipoHomenagem, input.Dia, input.Mes, input.Ano, input.ObjEveAssunto);

            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), await _dataContext.Homenagem.FindAsync(input.HomenagemId), input);
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<Homenagem>> Delete(int id)
        {
            Homenagem? dbHomenagem = await _dataContext.Homenagem.FindAsync(id);
            if (dbHomenagem is null)
                return NotFound("Homenagem não encontrada.");

            dbHomenagem.Delete();
            await _dataContext.SaveChangesAsync();

            return Ok("Deletado com sucesso!");
        }
    }
}
