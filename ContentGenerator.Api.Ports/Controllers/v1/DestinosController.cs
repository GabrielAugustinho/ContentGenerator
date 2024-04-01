using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public DestinosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<Destinos>>> GetAll()
        {
            List<Destinos> destinos = await _dataContext.Destinos.ToListAsync();

            return Ok(destinos);
        }
    }
}
