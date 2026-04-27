using AutoMapper;
using Domain.Contracts;
using FluentValidation;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PatientService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
        }
    }
}
