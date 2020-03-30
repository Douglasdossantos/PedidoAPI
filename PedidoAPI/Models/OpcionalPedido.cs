using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidoAPI.Models
{
    public class OpcionalPedido
    {
        public int PedidoId { get; set; }
        public int OpcionaisId { get; set; }
        public Pedido Pedido { get; set; }
        public Opcionais Opcionais { get; set; }
    }
}
