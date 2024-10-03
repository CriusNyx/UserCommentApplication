using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;

namespace UserCommentApplication.Models;

public class AppDbContext : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<UserPost> UserPosts { get; set; }
  public DbSet<UserPostEdit> UserPostEdits { get; set; }
  public DbSet<UserComment> UserComments { get; set; }
  public DbSet<UserCommentEdit> UserCommentEdits { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    // Generate User Schema
    var user = builder.Entity<User>();

    user.HasIndex(x => x.Email).IsUnique();
    // Relate posts with user. 1:m
    user.HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId);

    // Generate Post Schema
    var userPost = builder.Entity<UserPost>();

    userPost.HasIndex(x => x.UserId);
    // Relate edits with post. 1:m
    userPost.HasMany(x => x.Edits).WithOne(x => x.Post).HasForeignKey(x => x.PostId);
    // Relate comments to post. 1:m
    userPost.HasMany(x => x.Comments).WithOne(x => x.Post).HasForeignKey(x => x.PostId);

    // Generate Comment Schema
    var userComment = builder.Entity<UserComment>();

    // Relate edits to comment. 1:m
    userComment.HasMany(x => x.Edits).WithOne(x => x.Comment).HasForeignKey(x => x.CommentId);
  }
}
