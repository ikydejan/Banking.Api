using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Dtos;
using Banking.Api.Models;

namespace Banking.Api.Repositories.Interfaces
{
    public interface IAccountMutationRepo
    {
        Task<IEnumerable<AccountMutationDto>> GetData(AccountMutationParamDto param); 
        Task<AccountMutation> SaveData(AccountMutation accountMutation);
    }
}