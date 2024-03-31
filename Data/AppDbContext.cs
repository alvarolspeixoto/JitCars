using JitCars.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JitCars.Data
{
    public class AppDbContext : IdentityDbContext<Funcionario>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set;}
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Telefone>()
                .HasIndex(e => e.Numero)
                .IsUnique();

            builder.Entity<Funcionario>()
                .HasIndex(e => e.Cpf)
                .IsUnique();

        }


    }
}
