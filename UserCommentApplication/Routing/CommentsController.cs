using Microsoft.AspNetCore.Mvc;
using UserCommentApplication.Models;

[Route("api/comments")]
[ApiController]
public class CommentsController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly ErrorService _errorService;

  public CommentsController(AppDbContext context, ErrorService errorService)
  {
    _context = context;
    _errorService = errorService;
  }

  public record PostUserCommentRequest(string text);

  // In a proper API userId would not be passed in as a param
  // Auth would be handled by a JWT token.
  [HttpPost("comment/{userId}/{postId}")]
  public async Task<ActionResult<UserComment>> PostUserComment(
    int userId,
    int postId,
    [FromBody] PostUserCommentRequest body
  )
  {
    var user = _context.Users.Find(userId);

    if (user == null)
    {
      _errorService.ThrowUserNotFoundError(userId);
    }

    var post = _context.UserPosts.Find(postId);

    if (post == null)
    {
      _errorService.ThrowPostNotFoundError(postId);
    }

    // Verify the authorization of the user before allowing them to post.
    // Throw an AuthorizationException if they are not authorized.
    // Verify that they have authorization to view the post.
    var comment = new UserComment()
    {
      PostId = postId,
      UserId = userId,
      Edits = new UserCommentEdit[] { new UserCommentEdit() { Text = body.text } }
    };

    var output = _context.Add(comment).Entity;

    _context.SaveChanges();

    return output;
  }

  public record PatchUserCommentRequest(string text);

  [HttpPatch("comment/{commentId}")]
  public async Task<ActionResult<UserComment>> PatchUserComment(
    int commentId,
    [FromBody] PatchUserCommentRequest body
  )
  {
    var comment = _context.UserComments.Find(commentId);

    if (comment == null)
    {
      _errorService.ThrowPostNotFoundError(commentId);
    }

    // Verify that the user owns the comment before allowing them to create an edit.

    var edit = new UserCommentEdit() { CommentId = commentId, Text = body.text };
    _context.Add(edit);

    _context.SaveChanges();

    return _context.UserComments.Find(commentId);
  }

  [HttpDelete("comment/{commentId}")]
  public async Task DeleteUserPost(int commentId)
  {
    var comment = _context.UserPosts.Find(commentId);

    if (comment == null)
    {
      _errorService.ThrowCommentNowFoundError(commentId);
    }

    // Verify that the user owns the comment, or is an admin before allowing them to delete it.
    comment.archivedAt = DateTime.Now;
    _context.Update(comment);
    _context.SaveChanges();
  }
}
