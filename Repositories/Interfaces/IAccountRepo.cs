using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Dtos;
using Banking.Api.Models;

namespace Banking.Api.Repositories.Interfaces
{
    public interface IAccountRepo
    {
        Task<IEnumerable<Account>> GetData(DefaultParam param); 
        Task<Account> SaveData(Account account);
    }
}