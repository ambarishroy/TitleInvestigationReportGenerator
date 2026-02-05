using Amazon.EventBridge;
using Amazon.S3;
using TIR.OcrService.Application;
using TIR.OcrService.Infrastructure;
using TIR.SharedKernel.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonEventBridge>();

builder.Services.AddSingleton<OCREngine>();
builder.Services.AddSingleton<EventPublisher>();
builder.Services.AddSingleton<DocumentUploadHandler>();

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
