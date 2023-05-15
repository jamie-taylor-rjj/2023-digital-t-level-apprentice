﻿using System.Reflection;
using ClacksMiddleware.Extensions;
using Invoice_Gen.ViewModels;
using OwaspHeaders.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTransient<IMapper<ClientNameViewModel, Client>, ClientNameViewModelMapper>()
    .AddTransient(typeof(IClientRepository), typeof(ClientRepository));
builder.Services.AddTransient<IClientService, ClientService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.GnuTerryPratchett();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSecureHeadersMiddleware(
    SecureHeadersMiddlewareExtensions
        .BuildDefaultConfiguration()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Required for integration tests
public partial class Program { }