using System.Collections.Generic;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UserCommentApplication.Models;

public static class Scripts
{
  private static Dictionary<string, Func<WebApplication, Task>> scripts = new Dictionary<
    string,
    Func<WebApplication, Task>
  >()
  {
    { nameof(SeedUsers), SeedUsers },
    { nameof(SeedPosts), SeedPosts },
    { nameof(SeedComments), SeedComments }
  };

  public static async void RunScript(string scriptName, WebApplication app)
  {
    if (scripts.TryGetValue(scriptName, out var func))
    {
      Console.WriteLine($"Running script {scriptName}");
      try
      {
        await func(app);
        Console.WriteLine("Script finished");
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
    else
      throw new ArgumentException(
        $"Could not find a script with the name {scriptName}. Please check your command and try again."
      );
  }

  private static async Task SeedUsers(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    Console.WriteLine("Generating users");

    var users = Enumerable
      .Range(0, 100)
      .Select(i => $"{Faker.Name.Last()}-{Faker.Name.First()}")
      .Distinct()
      .Select(username => new User() { DisplayName = username, Email = $"{username}@example.com" });

    Console.WriteLine("Appending users to database");

    dbContext.AddRange(users);

    Console.WriteLine("Finalizing commit");

    dbContext.SaveChanges();
  }

  private static async Task SeedPosts(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    Console.WriteLine("Generating User Posts");

    // Get a random assortment of users to seed comments for.
    var users = dbContext.Users.OrderBy(x => Guid.NewGuid()).Take(20);

    List<UserPost> posts = new List<UserPost>();

    foreach (var user in users)
    {
      for (int i = 0; i < 5; i++)
      {
        var post = new UserPost()
        {
          User = user,
          Edits = new UserPostEdit[]
          {
            new UserPostEdit() { Text = Faker.Lorem.Sentence(), createdAt = DateTime.Now }
          }
        };
        posts.Add(post);
      }
    }

    Console.WriteLine("Appending user posts to database");

    dbContext.AddRange(posts);

    Console.WriteLine("Finalizing commit");

    dbContext.SaveChanges();
  }

  public static async Task SeedComments(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var random = new Random();

    int len = 20;

    var users = dbContext.Users.OrderBy(x => Guid.NewGuid()).Take(len).ToArray();

    // Get a random assortment of posts to edit.
    var posts = dbContext.UserPosts.OrderBy(x => Guid.NewGuid()).Take(len).ToArray();

    Console.WriteLine("Generating user comments");

    Console.WriteLine($"{users.Count()} {posts.Count()}");

    List<UserComment> comments = new List<UserComment>();
    // Zip throws an exception unless you convert posts and users to array.
    // Not ideal for larger queries, but good enough for seeding comments.
    foreach (var (post, user) in posts.Zip(users))
    {
      Console.WriteLine($"{post} {user}");
      for (int i = 0; i < random.Next(0, 4); i++)
      {
        var comment = new UserComment()
        {
          Post = post,
          User = user,
          Edits = new UserCommentEdit[]
          {
            new UserCommentEdit() { Text = Faker.Lorem.Sentence(), createdAt = DateTime.Now }
          }
        };

        comments.Add(comment);
      }
    }

    Console.WriteLine("Appending user comments to database");

    dbContext.AddRange(comments);

    Console.WriteLine("Finalizing commit");

    dbContext.SaveChanges();
  }
}
