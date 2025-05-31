using Microsoft.AspNetCore.Mvc;
using WebApi_2025_jueves.Domain.Interfaces;

namespace WebApi_2025_jueves.Controllers
{
    [Route("api/[controller]")]//Nombre inicial de mi ruta, URL O PATH
    [ApiController]
    public class CountriesController : Controller
    {
       private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost, ActionName("Get")]
        [Route("GeyAll")]
        public async Task<IActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync();

            if (countries == null || !countries.Any()) return NotFound();
            {
                return Ok(countries);
            }
            
        }

        [HttpPost, ActionName("Get")]
        [Route("GetById/{id}")] //URL: api/countries/get
        public async Task<IActionResult<Country>> GetCountryByIdAsync(Guid id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);

            if (country == null) return NotFound(); // NotFound = Status 404
            
                return Ok(country); // Ok = Status Code 200  
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<IActionResult<Country>> CreateCountryAsync(Country country)
        {
            try
            {
                var newCountry = await _countryService.CreateCountryAsync(country);
                if (newCountry == null) return NotFound();
                return Ok(newCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                  return Conflict(String.Format("El país {0} ya existe", country.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpPost, ActionName("Edit")]
        [Route("Edit")]
        public async Task<IActionResult<Country>> EditCountryAsync(Country country)
        {
            try
            {
                var editedCountry = await _countryService.EditCountryAsync(country);
                if (editedCountry == null) return NotFound();
                return Ok(editedCountry);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("El país {0} ya existe", country.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete")]
        public async Task<IActionResult<Country>> DeleteCountryAsync(Guid id)
        {
            if (id == null) return BadRequest();

            var deletedCountry = await _countryService.DeleteCountryAsync(id);

            if (deletedCountry == null) return NotFound();

            return Ok(deletedCountry);
            
        }
    }


}
