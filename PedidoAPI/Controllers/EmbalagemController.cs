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
    public class EmbalagemController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<Embalagem>>> Post([FromBody]Embalagem model, [FromServices]Contexto context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Embalagems.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messagem = "não Funcionou " + ex.Message });
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Embalagem>>> Get([FromServices]Contexto context)
        {
            var itens = await context.Embalagems.AsNoTracking().ToListAsync();
            return Ok(itens);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Embalagem>>> GetById(int id, [FromBody]Embalagem model, [FromServices]Contexto context)
        {
            var itens = await context.Embalagems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(itens);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Embalagem>>> Put(int id, [FromBody]Embalagem model, [FromServices]Contexto context)
        {
            if (id != model.Id)
            {
                return BadRequest(new { messagem = "requisição com conflito entre valores!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesage = ModelState });
            }
            try
            {
                context.Entry<Embalagem>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException db)
            {
                return BadRequest(new { message = "O Registo Já Está Atualizado " + db.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Não Foi Possivel atualizar o Tamanho " + ex.Message });
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Embalagem>>> Del(int id, [FromServices]Contexto context)
        {
            var item = await context.Embalagems.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return BadRequest(new { message = "Não foi Encontaro o Tamenho" });
            }
            try
            {
                context.Embalagems.Remove(item);
                await context.SaveChangesAsync();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messagem = "ocorreu um erro Durante a Exclusão " + ex.Message });
            }
        }
    }
}