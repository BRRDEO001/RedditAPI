using Microsoft.AspNetCore.Mvc;
using redditAPI.DataModels;
using redditAPI.Services;

namespace redditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{

    private FirestoreService firestoreService;

    public PostController(FirestoreService firestoreService)
    {
        firestoreService = firestoreService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        await firestoreService.AddDocument("posts", post);
        return Ok("Post successfully created.");

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(string id, [FromBody] Post updatedPost)
    {
        await firestoreService.UpdateDocument("posts", id, updatedPost);
        return Ok("Post successfully updated.");
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeletePost(string id)
    {
        await firestoreService.DeleteDocument("posts", id);
        return Ok("Post successfully deleted.");
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await firestoreService.GetCollection("posts");
        return Ok(posts);
    }

    [HttpPost("{id}/like")]
    public async Task<IActionResult> LikePost(string id)
    {
        await firestoreService.LikePost(id);
        return Ok("Post successfully liked.");
    }

    [HttpPost("{id}/unlike")]
    public async Task<IActionResult> UnlikePost(string id)
    {
        await firestoreService.UnlikePost(id);
        return Ok("Post successfully unliked.");
    }

}




