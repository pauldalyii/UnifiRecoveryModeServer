var builder = WebApplication.CreateBuilder(args);

// Build the app
var app = builder.Build();

// Create a logger using the app's logger factory
var logger = app.Logger;

// Configure the app to run on port 80
app.Urls.Add("http://192.168.1.99:80");
app.Urls.Add("http://localhost:80");

// Log each request
app.Use(async (context, next) =>
{
    var ipAddress = context.Connection.RemoteIpAddress?.MapToIPv4();
    logger.LogInformation($"{ipAddress} downloaded {context.Request.Path}");
    await next();
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
        Directory.GetCurrentDirectory())
});

// Map "/" to serve the content of index.html
app.MapGet("/", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html";

    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
    await context.Response.SendFileAsync(filePath);
});

app.Run();