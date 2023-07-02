using Game.Api.Hubs;
using Game.Common.Profiles;
using Game.DataAccess;
using Game.Operations;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

builder.Services.AddScoped(factory => new MongoClient(builder.Configuration["DatabaseSettings:ConnectionString"]));

builder.Services.AddScoped(factory =>
{
    var client = (MongoClient)factory.GetService(typeof(MongoClient));
    return client.GetDatabase(builder.Configuration["DatabaseSettings:DatabaseName"]);
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(
typeof(EventProfile),
typeof(OfferProfile)
);

builder.Services.AddTransient<IEventOperations, EventOperations>();
builder.Services.AddTransient<IOfferOperations, OfferOperations>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(devCorsPolicy);
app.MapHub<NewsHub>("/newsHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
