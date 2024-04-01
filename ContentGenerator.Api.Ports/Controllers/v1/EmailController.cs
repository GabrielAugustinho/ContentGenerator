using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public EmailController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<Email>>> GetAll()
        {
            List<Email> emails = await _dataContext.Email.ToListAsync();

            return Ok(emails);
        }

        [HttpGet("v1/GetById/{id}")]
        public async Task<ActionResult<Email>> GetById(int id)
        {
            Email? email = await _dataContext.Email.FindAsync(id);

            if (email is null)
                return NotFound("Email não encontrado.");

            return Ok(email);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<Email>> Add(Email input)
        {
            _dataContext.Email.Add(input);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), await _dataContext.Email.FindAsync(input.EmailId), input);
        }

        [HttpPut("v1/Update")]
        public async Task<ActionResult<Email>> Update(Email input)
        {
            Email? dbEmail = await _dataContext.Email.FindAsync(input.EmailId);
            if (dbEmail is null)
                return NotFound("Email não encontrado.");

            dbEmail.Update(input.NomeCliente, input.EmailCliente);

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Email.FindAsync(input.EmailId));
        }

        [HttpDelete("v1/Delete/{id}")]
        public async Task<ActionResult<Email>> Delete(int id)
        {
            Email? dbEmail = await _dataContext.Email.FindAsync(id);
            if (dbEmail is null)
                return NotFound("Email não encontrado.");

            dbEmail.Delete();
            await _dataContext.SaveChangesAsync();

            return Ok("Deletado com sucesso!");
        }
    }
}
