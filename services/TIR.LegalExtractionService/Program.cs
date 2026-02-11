using Amazon.S3;
using TIR.LegalExtractionService.Application;
using TIR.LegalExtractionService.Extractors;
using TIR.LegalExtractionService.Infrastructure;
using TIR.LegalExtractionService.Rules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddScoped<OcrCompletedEventHandler>();
builder.Services.AddScoped<FactExtractor>();
builder.Services.AddScoped<EventPublisher>();
builder.Services.AddScoped<ILegalFactRule, BorrowerNameRule>();
builder.Services.AddScoped<ILegalFactRule, DeedNumberRule>();

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
