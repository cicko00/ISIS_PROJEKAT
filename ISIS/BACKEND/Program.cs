using ISIS_PROJEKAT.DB;
using ISIS_PROJEKAT.Repository;
using ISIS_PROJEKAT.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:4200") // Add the origin of your Angular app
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("YourDbContext")));
builder.Services.AddScoped<IAppService,AppService>();
builder.Services.AddScoped<IAppRepository, AppRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowOrigin");

app.Use(async (context, next) =>
{
    // Add your custom headers here
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

    await next.Invoke();
});
app.Run();
