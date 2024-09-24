using HitSums.API.Abstractions;
using HitSums.API.Clients;
using HitSums.API.Configurations;
using HitSums.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GoogleSearchClientOptions>(
    builder.Configuration.GetSection(nameof(GoogleSearchClientOptions)));
builder.Services.Configure<BingSearchClientOptions>(
    builder.Configuration.GetSection(nameof(BingSearchClientOptions)));

builder.Services.AddHttpClient<ISearchEngineClient, GoogleSearchClient>();
builder.Services.AddHttpClient<ISearchEngineClient, BingSearchClient>();

builder.Services.AddTransient<ISearchService, SearchService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
