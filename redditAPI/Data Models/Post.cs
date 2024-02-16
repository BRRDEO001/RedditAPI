using System;
using System.Xml.Linq;

namespace redditAPI.DataModels
{

        public class Post
        {

        public int id { get; set; }
        public string title{ get; set; }
        public string content{ get; set; }
        public int authorId{ get; set; }
        public int likeCount { get; set; }
        public int dislikeCount{ get; set; }
        public List<Comment> Comments{ get; set; }

        }
    
}

