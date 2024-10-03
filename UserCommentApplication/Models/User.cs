using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;

public class User
{
  [Key]
  public int Id { get; set; }

  /// <summary>
  /// The name the user has entered to be displayed on their profile.
  /// </summary>
  /// <value></value>
  [Required]
  public string DisplayName { get; set; }

  /// <summary>
  /// A list of posts this user has created.
  /// </summary>
  /// <value></value>
  public IEnumerable<UserPost> Posts { get; set; }

  /// <summary>
  /// The email this user used to sign up.
  /// </summary>
  /// <value></value>
  [Required]
  public string Email { get; set; }
}
