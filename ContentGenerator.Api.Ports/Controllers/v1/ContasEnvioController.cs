using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasEnvioController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ContasEnvioController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<ContasEnvio>>> GetAll()
        {
            List<ContasEnvio> contas = await _dataContext.ContasEnvio.ToListAsync();

            return Ok(contas);
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<ContasEnvio>> GetById(int id)
        {
            ContasEnvio? contas = await _dataContext.ContasEnvio.FindAsync(id);

            if (contas is null)
                return NotFound("Conta de envio não encontrada.");

            return Ok(contas);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<ContasEnvio>> Add(ContasEnvio input)
        {
            _dataContext.ContasEnvio.Add(input);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), await _dataContext.ContasEnvio.FindAsync(input.ContasEnvioId), input);
        }
    }
}
