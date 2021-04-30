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
                if (UserRepository.GetByEmail(UserParam.email) != null)
                    return BadRequest("O Email: " + UserParam.email + " já está cadastrado.");

                UserParam.active = "Y";
                UserParam.isAdmin = "N";
                UserParam.createDate = DateTime.Now;
                UserParam.password = Helper.Enc(UserParam.password);

                var IdUserInsert = UserRepository.Insert(UserParam);

                if (IdUserInsert == 0)
                    return BadRequest("Erro ao inserir usuário");

                return Ok(UserRepository.Get(IdUserInsert));
            }
            catch (Exception)
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

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromServices] UserRepository UserRepository, [FromServices] SessionRepository SessionRepository, [FromBody] UserModel UserParam)
        {
            try
            {
                var User = UserRepository.GetLogin(UserParam.email, Helper.Enc(UserParam.password));

                if (User == null)
                    return BadRequest("Usuário ou senha não encontrados.");

                var Session = new SessionModel()
                {
                    token = Guid.NewGuid().ToString(),
                    createDate = DateTime.Now,
                    //expirationDate = UserParam.keepSigned == "Y" ? DateTime.Now.AddYears(100) : DateTime.Now.AddHours(2),
                    expirationDate = DateTime.Now.AddMinutes(1),
                    active = "Y",
                    keepSigned = UserParam.keepSigned,
                    userId = User.id
                };

                var IdSession = SessionRepository.Insert(Session);

                if (IdSession == 0)
                    return BadRequest("Erro ao iniciar sessão.");

                Session.id = IdSession;

                return Ok(Session);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno ao efetuar o login.");
            }
        }

    }
}
