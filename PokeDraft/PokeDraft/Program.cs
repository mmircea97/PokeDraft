using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokeDraft.Data;
using PokeDraft.Helpers.Validators;
using PokeDraft.Services.SpeciesService;
using PokeDraft.Services.TypesServices;
using Type = PokeDraft.Models.Type;
using TypeValidator = PokeDraft.Helpers.Validators.TypeValidator;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<ITypesService, TypesService>();
builder.Services.AddTransient<ISpeciesService, SpeciesService>();
builder.Services.AddTransient<IValidator<Type>, TypeValidator>();


builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
