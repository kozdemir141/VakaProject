
using VakaProject.Data.Concrete.Context;
using VakaProject.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();


builder.Services.AddControllers();

builder.Services.AddApplicationServices();

builder.Services.AddHttpClient("SimilarityApi", client =>
{
    client.BaseAddress = new Uri("https://external-similarity-service.com/");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();