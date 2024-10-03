using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class UserPostEdit
{
  public UserPostEdit()
  {
    createdAt = DateTime.Now;
  }

  [Key]
  public int Id { get; set; }

  /// <summary>
  /// The ID of post that this edit belongs to.
  /// </summary>
  /// <value></value>
  [Required]
  public int PostId { get; set; }

  /// <summary>
  /// The post that this edit belong to.
  /// </summary>
  /// <value></value>
  [JsonIgnore]
  public UserPost Post { get; set; }

  /// <summary>
  /// The text of the edit.
  /// </summary>
  /// <value></value>
  [Required]
  public string Text { get; set; }

  /// <summary>
  /// The time that the this edit was created at.
  /// </summary>
  /// <value></value>
  [Required]
  public DateTime createdAt { get; set; }
}
