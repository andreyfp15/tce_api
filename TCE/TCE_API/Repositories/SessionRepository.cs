using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TCE_API.Entities;
using TCE_API.Models;

namespace TCE_API.Repositories
{
    public class SessionRepository
    {
        private IConfiguration _config;

        public SessionRepository(IConfiguration confg)
        {
            _config = confg;
        }

        public SessionModel Get(string Token)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.QueryFirstOrDefault<SessionModel>("SELECT * FROM tce.session WHERE session.token = @token;", new { token = Token });
            }
        }

        public SessionModel Get(int Id)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.QueryFirstOrDefault<SessionModel>("SELECT * FROM tce.session WHERE session.id = @id;", new { id = Id });
            }
        }

        public int Insert(SessionEntity Entity)
        {
            using (MySqlConnection _conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return _conn.QuerySingle<int>(@"
                INSERT INTO tce.session (token, createDate, expirationDate, expirationReason, active, userId, keepSigned) VALUES (@token, @createDate, @expirationDate, @expirationReason, @active, @userId, @keepSigned);
                SELECT id FROM tce.session ORDER BY id DESC LIMIT 1;", new
                {
                    token = Entity.token,
                    createDate = Entity.createDate,
                    expirationDate = Entity.expirationDate,
                    expirationReason = Entity.expirationReason,
                    active = Entity.active,
                    userId = Entity.userId,
                    keepSigned = Entity.keepSigned
                });
            }
        }

    }
}
