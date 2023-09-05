using About;
using Microsoft.AspNetCore.Mvc;
using UseCases.DataStore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Travel.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHotelRepository hotelRepository;

        public ValuesController(IHotelRepository hotelRepository)
        {   
            this.hotelRepository = hotelRepository;
        }
        // GET: api/<ValuesController>
        [HttpGet("GetHotles")]
        public ActionResult<List<Hotel>> Get()
        {
            try
            {
                var hotels = hotelRepository.GetHotel();
                return Ok(hotels);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
