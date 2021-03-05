using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TCE_API.Models;
using TCE_API.Repositories;

namespace TCE_API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserModel> Get([FromServices]UserRepository userRepository)
        {
            return userRepository.Get();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserModel Get([FromServices] UserRepository userRepository, int id)
        {
            return userRepository.Get(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromServices] UserRepository userRepository, [FromBody] UserModel user)
        {
            userRepository.Insert(user);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public void Put([FromServices] UserRepository userRepository, [FromBody] UserModel user)
        {
            userRepository.Update(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete([FromServices] UserRepository userRepository, int id)
        {
            userRepository.Delete(id);
        }
    }
}
