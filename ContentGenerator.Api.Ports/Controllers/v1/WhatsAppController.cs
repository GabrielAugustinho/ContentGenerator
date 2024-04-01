using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsAppController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WhatsAppController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<WhatsApp>>> GetAll()
        {
            List<WhatsApp> whatsApps = await _dataContext.WhatsApp.ToListAsync();

            return Ok(whatsApps);
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<WhatsApp>> GetById(int id)
        {
            WhatsApp? whatsApp = await _dataContext.WhatsApp
                // caso tenha uma lista e queira buscar também -> .Include(x => x.Lista)
                .FindAsync(id);

            if (whatsApp is null)
                return NotFound("WhatsApp não encontrado.");

            return Ok(whatsApp);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<WhatsApp>> Add(WhatsApp input)
        {
            _dataContext.WhatsApp.Add(input);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), await _dataContext.WhatsApp.FindAsync(input.WhatsAppId), input);
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<WhatsApp>> Update(WhatsApp input)
        {
            WhatsApp? dbWhatsApp = await _dataContext.WhatsApp.FindAsync(input.WhatsAppId);
            if (dbWhatsApp is null)
                return NotFound("WhatsApp não encontrado.");

            dbWhatsApp.Update(input.Nome, input.Numero_Fone);

            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), await _dataContext.WhatsApp.FindAsync(input.WhatsAppId), input);
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<WhatsApp>> Delete(int id)
        {
            WhatsApp? dbWhatsApp = await _dataContext.WhatsApp.FindAsync(id);
            if (dbWhatsApp is null)
                return NotFound("WhatsApp não encontrado.");

            dbWhatsApp.Delete();
            await _dataContext.SaveChangesAsync();

            return Ok("Deletado com sucesso!");
        }
    }
}
