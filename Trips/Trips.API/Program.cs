using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using Trips.API.Mapping;
using Trips.Data.Context;
using Trips.Data.Inferfaces;
using Trips.Data.Services;
using Trips.ML.API.Extensions;
using Trips.ML.API.Models;
using Trips.ML.Interfaces;
using Trips.ML.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TripDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TripDbContext")));
builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddPredictionEnginePool<TripRating, TripRatingPrediction>().FromFile("model.zip".AbsolutePath(), true);
builder.Services.AddTransient<ILearningService, LearningService>();
builder.Services.AddTransient<IPredictingService, PredictingService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TripDbContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
