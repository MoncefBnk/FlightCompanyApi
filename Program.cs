﻿using FlightCompanyApi.Models;
using FlightCompanyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;


var builder = WebApplication.CreateBuilder(args);

//Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        });



});

//Add Api Versioning
builder.Services.AddApiVersioning(options => {
    // Returns all version with depricated versions
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = ApiVersionReader.Combine(
        //Query Strying type
        new QueryStringApiVersionReader("api-version"),
        //Request Heardes Type
        new HeaderApiVersionReader("Accept-Version"),
        //Media Type
        new MediaTypeApiVersionReader("api-version"));
});



// Add services to the container.
builder.Services.Configure<FlightCompanyApiSettings>(
    builder.Configuration.GetSection("FlightCompanyApi"));



// Add configuration for Basic Authentication
//builder.Configuration["BasicAuth:Username"] = "username";
//builder.Configuration["BasicAuth:Password"] = "password";

builder.Services.AddSingleton<VolsService>();
builder.Services.AddSingleton<AvionsService>();
builder.Services.AddSingleton<ClientsService>();
builder.Services.AddSingleton<ReservationsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

//app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

