using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Dtos;
using Banking.Api.Models;

namespace Banking.Api.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetData(DefaultParam param); 
        Task<Customer> SaveData(Customer customer);
    }
}