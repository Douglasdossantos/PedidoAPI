using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PedidoAPI.Models
{
    public class Opcionais
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TempoAdd { get; set; }
        public double ValorAdd { get; set; }
        public IList<OpcionalPedido> Pedidos { get; set; }
    }
}
