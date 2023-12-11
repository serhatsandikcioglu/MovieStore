using Microsoft.EntityFrameworkCore;
using MovieStore.Data.DataBase;
using MovieStore.Data.Interfaces;
using MovieStore.Data.Repositories;
using MovieStore.Service.Interfaces;
using MovieStore.Service.Mapper;
using MovieStore.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddDbContext<AppDbContext>(options =>
{

    options.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), Action => {
        Action.MigrationsAssembly("MovieStore.Data");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
