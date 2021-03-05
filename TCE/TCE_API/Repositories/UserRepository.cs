using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using TCE_API.Models;

namespace TCE_API.Repositories
{
    public class UserRepository
    {
        private IConfiguration _config;

        public UserRepository(IConfiguration confg)
        {
            _config = confg;
        }

        public IEnumerable<UserModel> Get()
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Query<UserModel>("SELECT * FROM tce.user;").ToList();
            }
        }

        public UserModel Get(int id)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.QueryFirstOrDefault<UserModel>("SELECT * FROM tce.user WHERE user.id = @Id;", new { id = id });
            }
        }

        public int Insert(UserModel user)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Execute("INSERT INTO `tce`.`user` (`email`, `password`, `isAdmin`) VALUES (@email, @password, @isAdmin);", new {
                    email = user.email,
                    password = user.password,
                    isAdmin = user.isAdmin
                });
            }
        }

        public int Update(UserModel user)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Execute("UPDATE tce.user SET `email` = @email, `password` = @password, `isAdmin` = @isAdmin WHERE `id` = @id;", new
                {
                    email = user.email,
                    password = user.password,
                    isAdmin = user.isAdmin,
                    id = user.id
                });
            }
        }

        public int Delete(int id)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Execute("DELETE FROM tce.user WHERE id = @id;", new { id = id });
            }
        }
    }
}
