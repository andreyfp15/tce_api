﻿using System;

namespace TCE_API.Entities
{
    public class UserEntity
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string isAdmin { get; set; }
        public string active { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}
