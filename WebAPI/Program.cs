using SharedKernel;
using System.Linq;
using Application.Mapping;
using Application.Services;
using Application.Validators;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using SharedKernel.DTOs;
using WebAPI.Middleware;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // rejestracja automappera w kontenerze IoC
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    // rejestracja automatycznej walidacji (FluentValidation waliduje i przekazuje wynik przez ModelState)
    //builder.Services.AddFluentValidationAutoValidation();

    // rejestracja kontekstu bazy w kontenerze IoC
    // var sqliteConnectionString = "Data Source=Kiosk.WebAPI.Logger.db";
    var sqliteConnectionString = @"Data Source = ClinicVisitSystem.db";
    builder.Services.AddDbContext<ClinicDbContext>(options =>
        options.UseSqlite(sqliteConnectionString));

    // rejestracja walidatora 
    builder.Services.AddScoped<IValidator<DoctorCreateDto>, DoctorCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<DoctorUpdateDto>, DoctorUpdateDtoValidator>();
    builder.Services.AddScoped<IValidator<PatientCreateDto>, PatientCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<PatientUpdateDto>, PatientUpdateDtoValidator>();
    builder.Services.AddScoped<IValidator<VisitCreateDto>, VisitCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<VisitUpdateDto>, VisitUpdateDtoValidator>();
    builder.Services.AddScoped<IValidator<VisitCompleteDto>, VisitCompleteDtoValidator>();
    builder.Services.AddScoped<IValidator<PrescriptionCreateDto>, PrescriptionCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<PrescriptionItemCreateDto>, PrescriptionItemCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<MedicalRecordDto>, MedicalRecordUpdateDtoValidator>();
    builder.Services.AddScoped<IValidator<SickLeaveCreateDto>, SickLeaveCreateDtoValidator>();
    builder.Services.AddScoped<IValidator<SickLeaveUpdateDto>, SickLeaveUpdateDtoValidator>();
    builder.Services.AddScoped<IValidator<SickLeaveCompleteDto>, SickLeaveCompleteDtoValidator>();
    builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
    builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();
    builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
    // rejestracja klas
    builder.Services.AddScoped<IClinicUnitOfWork, ClinicUnitOfWork>();
    builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
    builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
    builder.Services.AddScoped<IPatientRepository, PatientRepository>();
    builder.Services.AddScoped<IVisitRepository, VisitRepository>();
    builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
    builder.Services.AddScoped<IPrescriptionItemRepository, PrescriptionItemRepository>();
    builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
    builder.Services.AddScoped<ISickLeaveRepository, SickLeaveRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    
    builder.Services.AddHttpClient();
    builder.Services.AddScoped<IDoctorService, DoctorService>();
    builder.Services.AddScoped<IMedicationService, MedicationService>();
    builder.Services.AddScoped<IMedicationImportService, MedicationImportService>();
    builder.Services.AddScoped<IPatientService, PatientService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IVisitService, VisitService>();
    builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
    builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
    builder.Services.AddScoped<IPrescriptionItemService, PrescriptionItemService>();
    builder.Services.AddScoped<ISickLeaveService, SickLeaveService>();
    builder.Services.AddScoped<IAuthService, AuthService>();

    builder.Services.AddScoped<DataSeeder>();

    // rejestruje w kontenerze zale�no�ci polityk� CORS o nazwie SaleKioks,
    // kt�ra zapewnia dost�p do API z dowolnego miejsca oraz przy pomocy dowolnej metody
    builder.Services.AddCors(o => o.AddPolicy("ClinicVisit", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    var app = builder.Build();

    // Add Exception Middleware
    app.UseMiddleware<ExceptionMiddleware>();

    // Configure the HTTP request pipeline.
    app.UseStaticFiles();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // wstawia polityk? CORS obs?ugi do potoku ??dania
    app.UseCors("ClinicVisit");

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        // dataSeeder.Seed();

        var medicationImport = scope.ServiceProvider.GetRequiredService<IMedicationImportService>();
        medicationImport.ImportAsync().GetAwaiter().GetResult();
    }

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}