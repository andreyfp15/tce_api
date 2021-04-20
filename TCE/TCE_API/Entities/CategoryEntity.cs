using System;

namespace TCE_API.Entities
{
    public class CategoryEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string active { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime updateDate { get; set; }
        public int updateUser { get; set; }
    }
}
