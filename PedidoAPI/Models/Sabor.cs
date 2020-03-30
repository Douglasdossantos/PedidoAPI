using System.ComponentModel.DataAnnotations;

namespace PedidoAPI.Models
{
    public class Sabor
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int AddTempo { get; set; }
    }
}
