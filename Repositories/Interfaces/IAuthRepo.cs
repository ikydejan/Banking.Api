using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Dtos;
using Banking.Api.Models;

namespace Banking.Api.Repositories.Interfaces
{
    public interface IAuthRepo
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Register(UserRegisterDto userRegisterDto);
        Task<UserDto> Login(string userName, string password);
    }
}