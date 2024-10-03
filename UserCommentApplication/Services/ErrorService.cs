public class ErrorService
{
  private readonly IWebHostEnvironment _environment;

  public ErrorService(IWebHostEnvironment environment)
  {
    this._environment = environment;
  }

  public void ThrowUserNotFoundError(int userId)
  {
    ThrowSensitiveError(new Exception($"No user {userId}"));
  }

  public void ThrowPostNotFoundError(int postId)
  {
    ThrowSensitiveError(new Exception($"No post {postId}"));
  }

  public void ThrowCommentNowFoundError(int commentId)
  {
    ThrowSensitiveError(new Exception($"No comment {commentId}"));
  }

  public void ThrowSensitiveError(Exception exception)
  {
    if (_environment.IsDevelopment())
    {
      throw exception;
    }
    else
    {
      throw new Exception();
    }
  }
}
