using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAssuntoController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TipoAssuntoController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<TipoAssunto>>> GetAll()
        {
            List<TipoAssunto> tipoAssuntos = await _dataContext.TipoAssunto.ToListAsync();

            return Ok(tipoAssuntos);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<List<TipoAssunto>>> Add(TipoAssunto input)
        {
            _dataContext.TipoAssunto.Add(input);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.TipoAssunto.ToListAsync());
        }
    }
}
