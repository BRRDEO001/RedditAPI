using Microsoft.AspNetCore.Mvc;
using redditAPI.DataModels;

namespace redditAPI.Controllers
{
    public class CommentController : ControllerBase
    {

        private FirestoreService firestoreService;

        public CommentController(FirestoreService firestoreService)
        {
            this.firestoreService = firestoreService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            await firestoreService.AddDocument("comments", comment);
            return Ok("Comment successfully created.");

        }

        [HttpPut("{postId}/{commentId}")]
        public async Task<IActionResult> UpdateComment(string postId, string commentId, [FromBody] Comment updatedComment)
        {
            await firestoreService.UpdateComment(postId, commentId, updatedComment);
            return Ok("Comment successfully updated.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await firestoreService.DeleteDocument("comments", id);
            return Ok("Comment successfully deleted.");
        }


        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await firestoreService.GetCollection("comments");
            return Ok(comments);
        }

        [HttpPost("{postId}/{commentId}/like")]
        public async Task<IActionResult> LikeComment(string postId, string commentId)
        {
            await firestoreService.LikeComment(postId, commentId);
            return Ok("Comment successfully liked.");
        }

        [HttpPost("{postId}/{commentId}/unlike")]
        public async Task<IActionResult> UnlikeComment(string postId, string commentId)
        {
            await firestoreService.UnlikeComment(postId, commentId);
            return Ok("Comment successfully unliked.");
        }


    }

}
