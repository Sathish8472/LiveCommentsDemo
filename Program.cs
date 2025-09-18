using LiveCommentsDemo.Data;
using LiveCommentsDemo.Hubs;
using LiveCommentsDemo.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CommentsDbContext>(options =>
    options.UseSqlite("DataSource=:memory:"));  // In-memory DB

var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();

builder.Services.AddDbContext<CommentsDbContext>(options =>
    options.UseSqlite(connection));

// Add SignalR
builder.Services.AddSignalR();

// Optional: DispatcherService if using later
builder.Services.AddSingleton<DispatcherService>();

var app = builder.Build();

// Migrate database automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CommentsDbContext>();
    db.Database.Migrate();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<CommentsHub>("/commentsHub");

app.Run();
