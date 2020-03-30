using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedidoAPI.DataContext;
using PedidoAPI.Models;

namespace PedidoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpcionaisController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Opcionais>>> GetOpcoesSabores([FromServices]Contexto context)
        {
            try
            {
                var sab = await context.Opcionais.AsNoTracking().ToListAsync();
                return Ok(sab);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Não Foi Possivel trazer os itens " + ex.InnerException.Message });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Sabor>>> GetById(int id, [FromBody]Sabor model, [FromServices]Contexto context)
        {
            var itens = await context.Sabores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(itens);
        }

    }
}