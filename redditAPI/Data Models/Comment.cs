using System;
namespace redditAPI.DataModels
{
    public class Comment
    {
        public int id{ get; set; }
        public int postId{ get; set; }
        public int authorId{ get; set; }
        public string content{ get; set; }
        public int likeCount { get; set; }
        public int dislikeCount { get; set; }
    }

}

