// See https://aka.ms/new-console-template for more information

using EFGetStarted;
using System.Linq;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using var db = new BloggingContext();

Console.WriteLine($"Database Path: {db.DbPath}");

// Create
Console.WriteLine("Inserting a New Blog...");
db.Add(new BloggingContext.Blog
{
    Url = "http://bhenchod.com"
});
await db.SaveChangesAsync();

// Read
Console.WriteLine("Reading 1st Blog...");
var blog = await db.Blogs
    .Include(b => b.Posts)
    .OrderBy(b => b.BlogId)
    .FirstAsync();
Console.WriteLine(blog.Url);

// Update
Console.WriteLine("Updating 1st Blog...");
blog.Url = "https://madarchod.com";
blog.Posts.Add(new BloggingContext
    .Post
    {
        Title = "Hello Guys", 
        Content = "Bitchless guy"
    });
await db.SaveChangesAsync();
Console.WriteLine(blog.Url);

// Read Posts
Console.WriteLine("Reading Posts from 1st Blog...");
foreach (var post in blog.Posts)
{
   Console.WriteLine(post.Title);
   Console.WriteLine(post.Content);
}

// Delete
Console.WriteLine("Deleting the 1st blog...");
db.Remove(blog);
await db.SaveChangesAsync();