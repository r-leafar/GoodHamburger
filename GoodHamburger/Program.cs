using FluentValidation;
using GoodHamburger;
using GoodHamburger.Api.Controllers;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Services;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultClassType = typeof(Program);

//clean default settings of controller
builder.Services.AddControllers()
    .PartManager.ApplicationParts.Clear();

builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISandwichService, SandwicheService>();
builder.Services.AddScoped<IExtraService, ExtraService>();

builder.Services.AddValidatorsFromAssembly(defaultClassType.Assembly,includeInternalTypes:true);

//Setting new location path for controller
builder.Services.AddControllers()
    .AddApplicationPart(typeof(SandwichesController).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(defaultClassType);

builder.Services.AddDbContext<AppDbContext>(opts => 
opts.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultDB")));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

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
