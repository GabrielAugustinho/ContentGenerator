using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumorController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public HumorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<Humor>>> GetAll()
        {
            List<Humor> humores = await _dataContext.Humor.ToListAsync();

            return Ok(humores);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<List<Humor>>> Add(Humor input)
        {
            _dataContext.Humor.Add(input);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Humor.ToListAsync());
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<List<Humor>>> Delete(int id)
        {
            Humor? dbHumor = await _dataContext.Humor.FindAsync(id);
            if (dbHumor is null)
                return NotFound("Humor não encontrada.");

            _dataContext.Humor.Remove(dbHumor);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Humor.ToListAsync());
        }
    }
}
