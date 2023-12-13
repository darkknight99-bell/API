using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/cities")] //
    public class CitiesController : ControllerBase
    {
        [HttpGet] //http attribute
        public ActionResult<IEnumerable<CityDTO>> GetCities() //returns the list of CityDTO
        {  
            return Ok(CitiesDataStore.Current.Cities);
        }
         

        [HttpGet("{id}")] //curly brackets are used to work with parameters in routing templates
        public ActionResult<CityDTO> GetCity(int id) 
        {
            var cityToReturn = CitiesDataStore.Current.
                Cities.FirstOrDefault(c => c.Id == id);

            if(cityToReturn == null) 
            {
                return NotFound(); //error 404 result
            }

            return Ok(cityToReturn); //ok here is an object that produces the status code 200
        }
    }
}
