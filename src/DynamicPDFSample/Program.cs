using System.Reflection;
using DynamicPDFSample.Models;
using DynamicPDFSample.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPdfService, PdfService>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddLogging(builder =>
{
    builder
        .ClearProviders()
        .AddSerilog();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGet("/pdf/{page}", ([FromServices] IPdfService pdfService, [FromServices] ILogger<Program> logger, int page) =>
{
    var pdfStream = GetResourceStream("document.pdf");

    var options = new ImageOptions();
    var result = pdfService.Rasterize(pdfStream, page, options);

    logger.LogInformation($"Image generated with orientation: {result.Orientation}, height: {result.Height}, width: {result.Width}");
    
    return Results.File(result.InnerStream, "image/jpeg");
})
.WithName("Image");

app.Run();

static Stream GetResourceStream(string filename)
{
    var assembly = Assembly.GetExecutingAssembly();
    var embeddedResourcePrefix = $"{assembly.GetName().Name}.Resources";

    return assembly.GetManifestResourceStream($"{embeddedResourcePrefix}.{filename}");
}