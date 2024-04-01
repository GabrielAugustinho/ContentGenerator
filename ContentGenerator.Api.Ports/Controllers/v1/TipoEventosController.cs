﻿using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Database.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Ports.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEventosController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public TipoEventosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("v1/GetAll")]
        public async Task<ActionResult<List<TipoHomenagem>>> GetAll()
        {
            List<TipoHomenagem> tipoHomenagems = await _dataContext.TipoHomenagem.ToListAsync();

            return Ok(tipoHomenagems);
        }

        [HttpPost("v1/Add")]
        public async Task<ActionResult<List<TipoHomenagem>>> Add(TipoHomenagem input)
        {
            _dataContext.TipoHomenagem.Add(input);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.TipoHomenagem.ToListAsync());
        }
    }
}
