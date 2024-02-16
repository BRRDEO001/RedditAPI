using System;
using System.Xml.Linq;

namespace redditAPI.DataModels
{

        public class Post
        {

        private int id;
        private string title;
        private string content;
        private int authorId;
        private int likeCount;
        private int dislikeCount;
        private List<Comment> Comments;

        }
    
}

