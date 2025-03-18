using Serilog;
using VatCalculator.Api.Application.Interfaces;
using VatCalculator.Api.Application.Mappers;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Services;
using VatCalculatorApi.Application.Validators;
using VatCalculatorApi.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IValidator<VatCalculation>, VatCalculationValidator>();
builder.Services.AddTransient<IMapper<VatCalculationDto, VatCalculation>, VatCalculationDtoMapper>();
builder.Services.AddScoped<IVatCalculatorService,VatCalculatorService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var app = builder.Build();

app.UseCors("OpenCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
