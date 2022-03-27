using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MWZX0D_HFT_2021222.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineManufacturerController : ControllerBase
    {
        // GET: api/<EngineManufacturerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EngineManufacturerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EngineManufacturerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EngineManufacturerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EngineManufacturerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
