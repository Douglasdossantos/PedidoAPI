using Microsoft.EntityFrameworkCore;
using PedidoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PedidoAPI.DataContext
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Embalagem>()
               .HasData(
                   new { Id = 1, Nome = "Pequeno", Descricao = "Copo Pequeno de 300 ML", TempoPreparo = 5, Valor = 10d },
                   new { Id = 2, Nome = "Medio", Descricao = "Copo Medio de 500 ML", TempoPreparo = 7, Valor = 13d },
                   new { Id = 3, Nome = "Grande", Descricao = "Copo Grande de 700 ML", TempoPreparo = 10, Valor = 15d }
               );
            modelBuilder.Entity<Sabor>()
                .HasData(
                    new { Id = 1, Nome = "Morango", Descricao = "Sorvete de Açai Sabor Morango", AddTempo = 0 },
                    new { Id = 2, Nome = "Banana", Descricao = "Sorvete com Açai Sabor Banana", AddTempo = 0 },
                    new { Id = 3, Nome = "Kiwi", Descricao = "Sorvete de Açai  Com Kiwi", AddTempo = 5 }
                );
            modelBuilder.Entity<Opcionais>()
                .HasData(
                    new { Id = 1, Nome = "Granola", Descricao = "Será Adicionado Granola Como Opcional!", TempoAdd = 0, ValorAdd = 0d },
                    new { Id = 2, Nome = "Paçoca", Descricao = "Sera Adicionado Paçoca no Sorvete", TempoAdd = 3, ValorAdd = 3d },
                    new { Id = 3, Nome = "Leite Ninho", Descricao = "Sera Adicionado como opcional Leite ninho", TempoAdd = 0, ValorAdd = 3d }
                );

            modelBuilder
                .Entity<OpcionalPedido>()
                .HasKey(OP => new { OP.OpcionaisId, OP.PedidoId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sabor> Sabores { get; set; }
        public DbSet<Embalagem> Embalagems { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Opcionais> Opcionais { get; set; }
    }
}
