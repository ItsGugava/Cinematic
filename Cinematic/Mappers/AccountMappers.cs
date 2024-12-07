using Cinematic.Dtos.AccountDtos;
using Cinematic.Models;
using System.Runtime.CompilerServices;

namespace Cinematic.Mappers
{
    public static class AccountMappers
    {
        public static AppUser FormRegisterToAppUser(this RegisterDto registerDto)
        {
            return new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
        }

        public static AppUserDto FromAppUserToAppUserDto(this AppUser appUser, string token)
        {
            return new AppUserDto
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                Token = token
            };
        }
    }
}
