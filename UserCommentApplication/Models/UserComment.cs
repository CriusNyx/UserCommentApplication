using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UserComment
{
  [Key]
  public int Id { get; set; }

  /// <summary>
  /// The ID of the user who created the comment.
  /// </summary>
  /// <value></value>
  [Required]
  public int UserId { get; set; }

  /// <summary>
  ///  The user who created the comment
  /// </summary>
  /// <value></value>
  public User User { get; set; }

  /// <summary>
  /// The ID of the post that this comment is attached to.
  /// </summary>
  /// <value></value>
  [Required]
  public int PostId { get; set; }

  /// <summary>
  /// The post this comment is attached to.
  /// </summary>
  /// <value></value>
  [JsonIgnore]
  public UserPost Post { get; set; }

  /// <summary>
  /// A list of edits to this comment.
  /// A newly created comment has a single edit.
  /// </summary>
  /// <value></value>
  public IEnumerable<UserCommentEdit> Edits { get; set; }

  /// <summary>
  /// The date this comment was deleted. Null if the comment was not deleted.
  /// </summary>
  /// <value></value>
  public DateTime? archivedAt { get; set; }
}
