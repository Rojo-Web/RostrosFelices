using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using RostrosFelices.Modelos;

namespace RostrosFelices.Data
{
    public class RostrosFelicesContext : DbContext
    {
        public RostrosFelicesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
    }
}
