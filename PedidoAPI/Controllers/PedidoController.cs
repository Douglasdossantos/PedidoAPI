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
    public class PedidoController : ControllerBase
    {
        IList<OpcionalPedido> opclList;

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Pedido>>> Get([FromServices]Contexto conext)
        {
            var itens = await conext.Pedidos
                .Include(x => x.Sabor)
                .Include(x => x.Tamanho)
               // .Include(x => x.Opcional)
                .AsNoTracking().ToListAsync();
            return Ok(itens);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Pedido>>> GetPedidoId(int id, [FromServices]Contexto context)
        {
            try
            {
                var ped = await context.Pedidos
                .Include(x => x.Sabor)
                .Include(x => x.Tamanho)
                .Include(x => x.Opcional)
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToListAsync();
                return Ok(ped);
            }
            catch (DbUpdateConcurrencyException ex)
            {
            return BadRequest(new { message = " erro " + ex.InnerException.Message });
            }
            
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Pedido>>> PostCriarPedido([FromBody]Pedido model, [FromServices]Contexto context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Pedidos.Add(model);
                context.ChangeTracker.Entries();
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messagem = "não Funcionou " + ex.InnerException.Message });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Pedido>>> PutIncluirOpcionais(int id, [FromBody]Pedido model, [FromServices]Contexto context)
        {
            
            if (id != model.Id)
            {
                return BadRequest(new { messagem = "requisição com conflito entre Código do pedido!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesage = ModelState });
            }
            try
            {
                var ped = await context.Pedidos
                    .Include(x => x.Sabor)
                    .Include(x => x.Tamanho)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                ped.Opcional = model.Opcional;
                
                context.ChangeTracker.Entries();
                //await context.SaveChangesAsync();

                context.Entry<Pedido>(ped).State = EntityState.Modified;            
                               
                //context.ChangeTracker.Entries();
                await context.SaveChangesAsync();
                return Ok(ped);
            }
            catch (DbUpdateConcurrencyException db)
            {
                return BadRequest(new { message = "O Registo Já Está Atualizado " + db.InnerException.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Não Foi Possivel atualizar o Tamanho " + ex.InnerException.Message });
            }
        }

        [HttpPatch]
        [Route("{IdPedido:int}")]
        public async Task<ActionResult<List<Pedido>>> PatchFinalizarPedido(int IdPedido, [FromBody]Pedido model, [FromServices]Contexto context)
        {
            int temp = 0;
            double val = 0;
            if (IdPedido != model.Id)
            {
                return BadRequest(new { messagem = "requisição com conflito entre Código do pedido!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mesage = ModelState });
            }
            try
            {
                var ped = await context.Pedidos
                    .Include( x => x.Sabor)
                    .Include( x => x.Tamanho)
                    .Include( x => x.Opcional)
                    .AsNoTracking()
                    .Where(x => x.Id == IdPedido)
                    .FirstOrDefaultAsync();                
                
                var pd = await context.Pedidos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == IdPedido);

                var op = await context.Opcionais
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var item in ped.Opcional)
                {
                    foreach (var itemOp in op)
                    {
                        if (item.OpcionaisId == itemOp.Id)
                        {
                            temp += itemOp.TempoAdd;
                            val += itemOp.ValorAdd;
                        }
                    }                    
                }
                
                pd.ValorFinal = ped.Tamanho.Valor+val;

                pd.TempoEspera = ped.Sabor.AddTempo + ped.Tamanho.TempoPreparo +temp;
               
                pd.Finalizado = true;
                
                context.Entry<Pedido>(pd).State = EntityState.Modified;

                ped.ValorFinal = pd.ValorFinal;
                ped.TempoEspera = pd.TempoEspera;

                await context.SaveChangesAsync();
                
                return Ok(ped);
            }
            catch (DbUpdateConcurrencyException db)
            {
                return BadRequest(new { message = "O Registo Já Está Atualizado " + db.InnerException.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Não Foi Possivel atualizar o Tamanho " + ex.InnerException.Message });
            }
        }
    }
}