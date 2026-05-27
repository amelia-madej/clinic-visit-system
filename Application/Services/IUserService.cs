using Domain.Models;
using SharedKernel;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        int Create(CreateUserDto dto);
        void Update(UpdateUserDto dto);
        void UpdateProfile(UpdateUserProfileDto dto);
        void UpdatePhoto(UpdateUserPhotoDto dto);
        void ChangePassword(ChangePasswordDto dto);
        void DeletePhoto(int id);
        void Delete(int id);

        List<UserDto> GetAll();
        UserDto? GetById(int id);
        UserDto? GetByEmail(string email);
        UserDto? GetByPhoneNumber(string phoneNumber);
        List<UserDto> GetByRole(UserRole role);
    }
}
