using Serilog;
using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Services;
using VatCalculatorApi.Application.Validators;
using VatCalculatorApi.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IValidator<VatCalculation>, VatCalculationValidator>();
builder.Services.AddScoped<IVatCalculatorService,VatCalculatorService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
