using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TCE_API.Entities;
using TCE_API.Models;
using TCE_API.Repositories;
using TCE_DOMAIN;

namespace TCE_API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<UserModel> Get([FromServices]UserRepository userRepository)
        {
            return userRepository.Get();
        }

        [HttpGet("{id}")]
        public UserModel Get([FromServices] UserRepository userRepository, int id)
        {
            return userRepository.Get(id);
        }

        [HttpPost]
        public IActionResult Post([FromServices] UserRepository UserRepository, [FromBody] UserModel UserParam)
        {
            try
            {
                UserParam.password = Helper.Enc(UserParam.password);

                var IdUserInsert = UserRepository.Insert(UserParam);

                if (IdUserInsert == 0)
                    return BadRequest("Erro ao inserir usuário");

                return Ok(UserRepository.Get(IdUserInsert));
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao inserir usuário");
            }
        }

        [HttpPut]
        public void Put([FromServices] UserRepository userRepository, [FromBody] UserModel user)
        {
            userRepository.Update(user);
        }

        [HttpDelete("{id}")]
        public void Delete([FromServices] UserRepository userRepository, int id)
        {
            userRepository.Delete(id);
        }

        [Route("[action]")]
        public SessionModel Login([FromServices] UserRepository UserRepository, [FromServices] SessionRepository SessionRepository, [FromBody] UserModel UserParam)
        {
            var User = UserRepository.GetLogin(UserParam.email, Helper.Enc(UserParam.password));

            if (User != null)
            {
                var Session = new SessionEntity()
                {
                    token = Guid.NewGuid().ToString(),
                    createDate = DateTime.Now,
                    expirationDate = UserParam.keepSigned == "Y" ? DateTime.Now.AddYears(100) : DateTime.Now.AddHours(2),
                    active = "Y",
                    keepSigned = UserParam.keepSigned,
                    userId = User.id
                };

                var IdSession = SessionRepository.Insert(Session);

                if (IdSession == 0) return new SessionModel();

                var SessionInserted = SessionRepository.Get(IdSession);

                SessionInserted.User = User;
            }

            return new SessionModel();
        }

    }
}
