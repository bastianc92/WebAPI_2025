using Microsoft.EntityFrameworkCore;
using WebApi_2025_jueves.Domain.Interfaces;

namespace WebApi_2025_jueves.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataBaseContext _context;

        public CountryService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            

            try
            {
                Country.Id = Guid.NewGuid(); // Asignar un nuevo GUID al país
                Country.CreatedDate = DateTime.Now; // Asignar la fecha de creación
                _context.Countries.Add(country); // Agregar el país al contexto de la base de datos

                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return country;

            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ??
                    dbUpdateException.Message);
            }
        }

        public Task CreateCountryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {

          

            try
            {
                var country = await GetCountryByIdAsync(id);
                if (country == null)
                {
                    return null; // O lanzar una excepción si el país no se encuentra
                }
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ??
                    dbUpdateException.Message);
            }
        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now; // Asignar la fecha de modificación
                await _context.SaveChangesAsync();

                return country;

            }
            catch (DbUpdateException dbUpdateException)
            {

                 throw new Exception(dbUpdateException.InnerException?.Message ??
                    dbUpdateException.Message);
            }
        }                   

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            
            try
            {
                var countries = await _context.Countries.ToListAsync();

                return countries;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ??
                   dbUpdateException.Message);
            }
        }

        public Task<Country> GetCountryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            try
            {
                var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                // Otras formas de traer un objeto de la BD 
                var country1 = await _context.Countries.FindAsync(id);
                var country2 = await _context.Countries.FirstAsync(c => c.Id == id);


                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {

                throw new Exception(dbUpdateException.InnerException?.Message ??
                   dbUpdateException.Message);
            }
        }

        private class DataBaseContext
        {
            // Fix: Change the type of Countries to DbSet<Country> to support EF Core operations
            public DbSet<Country> Countries { get; set; }

            internal async Task SaveChangesAsync()
            {
                throw new NotImplementedException();
            }
        }
    }


}
