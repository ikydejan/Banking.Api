using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Dtos;
using Banking.Api.Models;

namespace Banking.Api.Repositories.Interfaces
{
    public interface ITransTypeRepo
    {
        Task<IEnumerable<TransType>> GetData(DefaultParam param); 
        Task<TransType> SaveData(TransType transType);
    }
}