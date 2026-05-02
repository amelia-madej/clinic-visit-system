using Application.Mapping;
using Application.Services;
using Application.Validators;
using Domain.Contracts;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedKernel.DTOs;

try
{
    var builder = WebApplication.CreateBuilder(args);

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
    builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidator>();
    builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserValidator>();

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
    
    builder.Services.AddScoped<IDoctorService, DoctorService>();
    builder.Services.AddScoped<IMedicationService, MedicationService>();
    builder.Services.AddScoped<IPatientService, PatientService>();
    builder.Services.AddScoped<IUserService, UserService>();

    // rejestruje w kontenerze zale¿noœci politykê CORS o nazwie SaleKioks,
    // która zapewnia dostêp do API z dowolnego miejsca oraz przy pomocy dowolnej metody
    builder.Services.AddCors(o => o.AddPolicy("ClinicVisit", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseStaticFiles();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    // wstawia politykê CORS obs³ugi do potoku ¿¹dania
    app.UseCors("ClinicVisit");

    // seeding data here

    app.Run();
}
catch (Exception ex)
{

}