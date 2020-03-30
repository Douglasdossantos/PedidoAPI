using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PedidoAPI.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Opcional = new Collection<OpcionalPedido>();

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int TamanhoId { get; set; }
        public Embalagem Tamanho { get; set; }
        [Required]
        public int SaborID { get; set; }
        public Sabor Sabor { get; set; }
        public double ValorFinal { get; set; }
        public bool Finalizado { get; set; }
        public int TempoEspera { get; set; }
        public IList<OpcionalPedido> Opcional { get; set; }
    }
}
