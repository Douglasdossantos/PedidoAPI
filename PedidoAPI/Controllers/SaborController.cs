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
    public class SaborController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<Sabor>>> PostSabor([FromBody]Sabor model, [FromServices]Contexto context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Sabores.Add(model);
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
        public async Task<ActionResult<List<Sabor>>> GetOpcoesSabores([FromServices]Contexto context)
        {
            try
            {
                var sab = from E in context.Embalagems
                          from S in context.Sabores
                          select new
                          {
                              codigoSabor = S.Id,
                              Sabor = S.Nome,
                              DescricaoSabor = S.Descricao,
                              codigoEmba = E.Id,
                              NomeEmbalagem = E.Nome,
                              DescricaoEmbalagem = E.Descricao,
                              Espera = E.TempoPreparo + S.AddTempo,
                              valor = E.Valor
                          };

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

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Sabor>>> Put(int id, [FromBody]Sabor model, [FromServices]Contexto context)
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
                context.Entry<Sabor>(model).State = EntityState.Modified;
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
        public async Task<ActionResult<List<Sabor>>> Del(int id, [FromServices]Contexto context)
        {
            var item = await context.Sabores.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
            {
                return BadRequest(new { message = "Não foi Encontaro o Tamenho" });
            }
            try
            {
                context.Sabores.Remove(item);
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