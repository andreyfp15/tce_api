using System;

namespace TCE_API.Entities
{
    public class TextEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string level { get; set; }
        public string content { get; set; }
        public string audioPath { get; set; }
        public string active { get; set; }
        public DateTime createDate { get; set; }
        public int createUser { get; set; }
        public DateTime updateDate { get; set; }
        public int updateUser { get; set; }
        public int categoryId { get; set; }
    }
}
