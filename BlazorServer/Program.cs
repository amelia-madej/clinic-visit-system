using Application.Mapping;
using Application.Services;
using Application.Validators;
using Domain.Contracts;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SharedKernel.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// EF Core + SQLite
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlite(@"Data Source=ClinicVisitSystem.db"));

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
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPrescriptionItemService, PrescriptionItemService>();
builder.Services.AddScoped<ISickLeaveService, SickLeaveService>();

var app = builder.Build();

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
    // seeder.Seed();

    var medicationImport = scope.ServiceProvider.GetRequiredService<IMedicationImportService>();
    medicationImport.ImportAsync().GetAwaiter().GetResult();
}

app.Run();
