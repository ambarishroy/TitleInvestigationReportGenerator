using TIR.DocumentService.Application;
using TIR.DocumentService.Events;
using TIR.DocumentService.Infrastructure.Storage;
using TIR.DocumentService.Persistence;
using TIR.SharedKernel.Audit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IS3StorageService, S3StorageService>();
builder.Services.AddScoped<IDomainEventPublisher, EventBridgePublisher>();
builder.Services.AddScoped<IAuditPublisher, AuditPublisher>();
builder.Services.AddScoped<DocumentUploadService>();
builder.Services.AddScoped<OcrCompletedEventHandler>();

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
