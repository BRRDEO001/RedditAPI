using System;
namespace redditAPI.DataModels
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string hashedPassword { get; set; }
    }

}

