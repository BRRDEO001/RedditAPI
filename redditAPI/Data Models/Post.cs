using System;
using System.Xml.Linq;

namespace redditAPI.DataModels
{

        public class Post
        {

        private int id { get; set; }
        private string title{ get; set; }
        private string content{ get; set; }
        private int authorId{ get; set; }
        private int likeCount { get; set; }
        private int dislikeCount{ get; set; }
        private List<Comment> Comments{ get; set; }

        }
    
}

