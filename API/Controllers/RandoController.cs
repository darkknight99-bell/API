using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/rando")]
    [ApiController]
    public class RandoController : ControllerBase
    {
        // GET: api/<RandoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RandoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RandoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RandoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RandoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
