using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieStore.Data.DataBase;
using MovieStore.Data.DTOs;
using MovieStore.Data.Entities;
using MovieStore.Data.Interfaces;
using MovieStore.Data.Repositories;
using MovieStore.Service;
using MovieStore.Service.Interfaces;
using MovieStore.Service.Mapper;
using MovieStore.Service.Services;
using MovieStore.Service.Validatior;
using System.Text;

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
builder.Services.AddScoped<IValidator<ActorCreateDTO>, ActorCreateDTOValidator>();
builder.Services.AddScoped<IValidator<ActorUpdateDTO>, ActorUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<DirectorCreateDTO>, DirectorCreateDTOValidator>();
builder.Services.AddScoped<IValidator<DirectorUpdateDTO>, DirectorUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<MovieCreateDTO>, MovieCreateDTOValidator>();
builder.Services.AddScoped<IValidator<MovieUpdateDTO>, MovieUpdateDTOValidator>();
builder.Services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders()
        .AddRoles<AppRole>();
builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddDbContext<AppDbContext>(options =>
{

    options.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), Action => {
        Action.MigrationsAssembly("MovieStore.Data");
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptionsModel>();
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenOptions:SecurityKey").Value)),
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero

    };

});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!dbContext.Roles.Any())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        await roleManager.CreateAsync(new() { Name = "admin" });
        await roleManager.CreateAsync(new() { Name = "user" });
    }
    if (!dbContext.Users.Any())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var user = new AppUser() { UserName = "admin1"};
        await userManager.CreateAsync(user, "Asd123*");
        await userManager.AddToRoleAsync(user, "admin");

    }
}
// Configure the HTTP request pipeline.
app.UseLoggingMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

public partial class Program { }