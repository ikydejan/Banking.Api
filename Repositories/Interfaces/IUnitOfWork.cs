namespace Banking.Api.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IAuthRepo AuthRepo { get; }
        ICustomerRepo CustomerRepo { get; }
        IAccountRepo AccountRepo { get; }
        ITransTypeRepo TransTypeRepo { get; }
        IAccountMutationRepo AccountMutationRepo { get; }
         

    }
}