using System;

namespace TCE_API.Entities
{
    public class SessionEntity
    {
        public int id { get; set; }
        public string token { get; set; }
        public DateTime createDate { get; set; }
        public DateTime expirationDate { get; set; }
        public string expirationReason { get; set; }
        public string active { get; set; }
        public int userId { get; set; }
    }
}
