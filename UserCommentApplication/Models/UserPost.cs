using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UserPost
{
  [Key]
  public int Id { get; set; }

  /// <summary>
  /// The ID of the user who created this post.
  /// </summary>
  /// <value></value>
  [Required]
  public int UserId { get; set; }

  /// <summary>
  /// The user who created this post.
  /// </summary>
  /// <value></value>
  [JsonIgnore]
  public User User { get; set; }

  /// <summary>
  /// A list of edits made to this post.
  /// A new post has a single edit.
  /// </summary>
  /// <value></value>
  public IEnumerable<UserPostEdit> Edits { get; set; }

  /// <summary>
  /// A list of comments made to this post.
  /// </summary>
  /// <value></value>
  public IEnumerable<UserComment> Comments { get; set; }

  /// <summary>
  /// If not null, the time that the post was deleted.
  /// </summary>
  /// <value></value>
  public DateTime? archivedAt { get; set; }
}
