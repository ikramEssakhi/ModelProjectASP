using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModelAsp1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ModelAsp1Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ModelAsp1Context") ?? throw new InvalidOperationException("Connection string 'ModelAsp1Context' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Set the default route to your Razor Page
app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/Products");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
