using Microsoft.EntityFrameworkCore;

namespace WebApi_2025_jueves.DAL.Entities
{
    public class DataBaseContext : DbContext
    {

      public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        // Este metodo es propio de EF CORE y me sirve para configurar unos indices de cada campo de una tabla en BD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); // indece del campo Name para la tabla Countries
        }

        #region DbSets

        public DbSet<Country> Countries { get; set; }

        #endregion
    }
}
