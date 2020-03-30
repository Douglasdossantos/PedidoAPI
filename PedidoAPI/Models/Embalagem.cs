using System.ComponentModel.DataAnnotations;

namespace PedidoAPI.Models
{
    public class Embalagem
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TempoPreparo { get; set; }
        public double Valor { get; set; }
    }
}
