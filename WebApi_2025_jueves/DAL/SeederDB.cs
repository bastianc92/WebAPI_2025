using WebApi_2025_jueves.DAL.Entities;

namespace WebApi_2025_jueves.DAL
{
    public class SeederDB
    {
        private readonly DataBaseContext _context;
        public SeederDB(DataBaseContext context)
        {
            _context = context;
        }

        //Metodo SeederAsync, este metodo es una especie de MAID()
        //Este metodo tendra la responsabilidad de repoblar mis diferentes tablas de la base de datos
        public async Task SeedAsync()
        {    //primero agregare un metodo propio de EF que hace las veces del comando update-database 
             //En otra palabras un metodo que me creara la BD inmediatamente pongo en ejecucion mi API
            await _context.Database.EnsureCreatedAsync();

            //Apartir de aqui vamos a ir creando un metodo que me sirva para repoblar mi BD
            await PopulateCountriesAsync();
            await _context.SaveChangesAsync();//Esta line me guardara los cambios que hice en la BD
        }


        private async Task PopulateCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                //Asi creo yo un objeto pais con sus respectivos estados
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Colombia",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Cundinamarca",
                            CreatedDate = DateTime.Now
                        },
                        new State
                        {
                            Name = "Antioquia",
                            CreatedDate = DateTime.Now
                        },

                    }
                });

                //Aqui creo otro pais con sus respectivos estados
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Argentina",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Buenos Aires",
                            CreatedDate = DateTime.Now
                        },


                    }
                });

            }
        }
    }
}