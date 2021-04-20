using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using TCE_API.Entities;
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
                return _conn.Query<UserModel>("SELECT * FROM user;").ToList();
            }
        }

        public UserModel Get(int id)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.QueryFirstOrDefault<UserModel>("SELECT * FROM user WHERE user.id = @Id;", new { id = id });
            }
        }

        public int Insert(UserEntity user)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Execute("INSERT INTO user (email, password, isAdmin, active, createDate) VALUES ( @email, @password, @isAdmin, @active, @createDate);", new {
                    email = user.email,
                    password = user.password,
                    isAdmin = user.isAdmin,
                    active = user.active,
                    createDate = DateTime.Now
                });
            }
        }

        public int Update(UserEntity user)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Execute("UPDATE user SET email = @email, password = @password, isAdmin = @isAdmin, active = @active, updateDate = @updateDate WHERE id = @id;", new
                {
                    email = user.email,
                    password = user.password,
                    isAdmin = user.isAdmin,
                    active = user.active,
                    updateDate = DateTime.Now,
                    id = user.id
                });
            }
        }

        public int Delete(int id)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.Execute("DELETE FROM user WHERE id = @id;", new { id = id });
            }
        }
    }
}
