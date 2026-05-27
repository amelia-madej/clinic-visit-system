using Application.Mapping;
using Application.Services;
using Application.Validators;
using ApexCharts;
using Domain.Contracts;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using NLog;
using NLog.Web;
using SharedKernel.DTOs;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("BlazorServer startup initialized.");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

// Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddApexCharts();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var dbPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "ClinicVisitSystem.db"));
// Registers the database context in the dependency injection container.
// var sqliteConnectionString = "Data Source=Kiosk.WebAPI.Logger.db";
var sqliteConnectionString = $"Data Source = {dbPath}";
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlite(sqliteConnectionString));

// Validators
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
builder.Services.AddScoped<IValidator<UpdateUserProfileDto>, UpdateUserProfileValidator>();
builder.Services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();

// Repositories
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

// Blazor app state
builder.Services.AddScoped<BlazorServer.Services.AppStateService>();
builder.Services.AddScoped<DataSeeder>();

// Application services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();
builder.Services.AddScoped<IMedicationImportService, MedicationImportService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPrescriptionItemService, PrescriptionItemService>();
builder.Services.AddScoped<ISickLeaveService, SickLeaveService>();
builder.Services.AddScoped<IAnomalyDetectionService, AnomalyDetectionService>();
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IPasswordMigrationService, PasswordMigrationService>();

var app = builder.Build();
app.Logger.LogInformation("BlazorServer application starting.");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    app.Logger.LogInformation("Ensuring baseline anomaly data.");
    seeder.EnsureBaselineAnomalyData();

    var passwordMigrationService = scope.ServiceProvider.GetRequiredService<IPasswordMigrationService>();
    var updatedPasswordCount = passwordMigrationService.HashPlainTextPasswords();
    app.Logger.LogInformation("Password hash migration completed. Updated users: {UpdatedPasswordCount}", updatedPasswordCount);
}

app.Logger.LogInformation("BlazorServer application configured. Running host.");
app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped BlazorServer because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
