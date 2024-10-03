using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UserCommentEdit
{
  public UserCommentEdit()
  {
    createdAt = DateTime.Now;
  }

  [Key]
  public int Id { get; set; }

  /// <summary>
  /// The id of the comment this edit belongs to.
  /// </summary>
  /// <value></value>
  [Required]
  public int CommentId { get; set; }

  /// <summary>
  /// The comment this edit belongs to.
  /// </summary>
  /// <value></value>
  [JsonIgnore]
  public UserComment Comment { get; set; }

  /// <summary>
  /// The text of the comment at the time of this edit.
  /// </summary>
  /// <value></value>
  [Required]
  public string Text { get; set; }

  /// <summary>
  /// The date that this edit was created at.
  /// </summary>
  /// <value></value>
  [Required]
  public DateTime createdAt { get; set; }
}
