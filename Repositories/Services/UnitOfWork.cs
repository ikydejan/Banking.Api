using log4net;
using Banking.Api.Repositories.Interfaces;

namespace Banking.Api.Repositories.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private ILog _log;
        private IDapperContext _context;
        private IAuthRepo _authRepo;
        private ICustomerRepo _customerRepo;
        private IAccountRepo _accountRepo;
        private ITransTypeRepo _transTypeRepo;
        private IAccountMutationRepo _accountMutationRepo;
        
        public UnitOfWork(IDapperContext context)
        {
            _context = context;
        }
        public UnitOfWork(IDapperContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IAuthRepo AuthRepo {
            get { return _authRepo ?? (_authRepo = new AuthRepo(_context)); }
        }
        public ICustomerRepo CustomerRepo {
            get { return _customerRepo ?? (_customerRepo = new CustomerRepo(_context)); }
        }
        public IAccountRepo AccountRepo {
            get { return _accountRepo ?? (_accountRepo = new AccountRepo(_context)); }
        }
        public ITransTypeRepo TransTypeRepo {
            get { return _transTypeRepo ?? (_transTypeRepo = new TransTypeRepo(_context)); }
        }
        public IAccountMutationRepo AccountMutationRepo {
            get { return _accountMutationRepo ?? (_accountMutationRepo = new AccountMutationRepo(_context)); }
        }

    }
}   