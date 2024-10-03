using Microsoft.AspNetCore.Mvc;
using UserCommentApplication.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;

[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly ErrorService _errorService;

  public PostController(AppDbContext context, ErrorService errorService)
  {
    _context = context;
    _errorService = errorService;
  }

  [HttpGet("userPosts/{userId}")]
  public async Task<ActionResult<IEnumerable<UserPost>>> GetPostsForUser(int userId)
  {
    return _context.UserPosts
      .Where(x => x.UserId == userId)
      .Include(post => post.Edits)
      .Where(x => x.archivedAt == null)
      .Include(post => post.Comments)
      .ThenInclude(comment => comment.Edits)
      .ToArray();
  }

  public record PostUserPostRequest(string text);

  // In a proper API userId would not be passed in as a param
  // Auth would be handled by a JWT token.
  [HttpPost("post/{userId}")]
  public async Task<ActionResult<UserPost>> PostUserPost(
    int userId,
    [FromBody] PostUserPostRequest body
  )
  {
    var user = _context.Users.Find(userId);

    if (user == null)
    {
      _errorService.ThrowUserNotFoundError(userId);
    }

    // Verify the authorization of the user before allowing them to post.
    // Throw an AuthorizationException if they are not authorized.

    var post = new UserPost()
    {
      Edits = new UserPostEdit[] { new UserPostEdit() { Text = body.text } },
      UserId = userId,
    };

    var output = _context.Add(post).Entity;

    _context.SaveChanges();

    return output;
  }

  public record PatchUserPostRequest(string text);

  [HttpPatch("post/{postId}")]
  public async Task<ActionResult<UserPost>> PatchUserPost(
    int postId,
    [FromBody] PatchUserPostRequest body
  )
  {
    var post = _context.UserPosts.Find(postId);

    if (post == null)
    {
      _errorService.ThrowPostNotFoundError(postId);
    }

    // Verify that the user owns the post before allowing them to create an edit.

    var edit = new UserPostEdit() { PostId = postId, Text = body.text };
    _context.Add(edit);

    _context.SaveChanges();

    return _context.UserPosts.Find(postId);
  }

  [HttpDelete("post/{postId}")]
  public async Task DeleteUserPost(int postId)
  {
    var post = _context.UserPosts.Find(postId);

    if (post == null)
    {
      _errorService.ThrowPostNotFoundError(postId);
    }

    // Verify that the user owns the post, or is an admin before allowing them to delete it.
    post.archivedAt = DateTime.Now;
    _context.Update(post);
    _context.SaveChanges();
  }
}
