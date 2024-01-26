using BloodDonation.BLL.IServices;
using BloodDonation.BLL.Services;
using BloodDonation.DAL.AppContext;
using BloodDonation.DAL.Entities.Doner;
using BloodDonation.DAL.Implementation;
using BloodDonation.DAL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddScoped(typeof(IRepository<DonerDetailsEntity>), typeof(Repository<DonerDetailsEntity>));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
