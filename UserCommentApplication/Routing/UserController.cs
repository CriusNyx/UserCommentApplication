using Microsoft.AspNetCore.Mvc;
using UserCommentApplication.Models;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly AppDbContext _context;

  public UserController(AppDbContext context)
  {
    _context = context;
  }

  [HttpGet("all")]
  public async Task<ActionResult<IEnumerable<User>>> GetAll()
  {
    return _context.Users.ToArray();
  }

  [HttpGet("{userId}")]
  public async Task<ActionResult<User>> Get(int userId)
  {
    return await _context.Users.FindAsync(userId);
  }
}
