using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Banking.Api.Dtos;
using Banking.Api.Models;
using Banking.Api.Repositories.Interfaces;

namespace Banking.Api.Repositories.Services
{
    internal class AccountMutationRepo : IAccountMutationRepo
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private QueryHelper _qH;
        public AccountMutationRepo(IDapperContext context)
        {
            _context = context;
            _qH = new QueryHelper();
        }
        public AccountMutationRepo(IDapperContext context, IDbTransaction transaction) : this(context)
        {
            _transaction = transaction;
        }
        public async Task<IEnumerable<AccountMutationDto>> GetData(AccountMutationParamDto param)
        {
            var bhdr = new Dictionary<string, AccountMutationDto>();   
            var bhdtl = new Dictionary<string, AccountMutation>();
            await _context.db.QueryAsync<AccountMutationDto>(
                param.query, 
                new[]
                {
                    typeof(AccountMutationDto), 
                    typeof(AccountMutation),  
                    typeof(Customer), 
                    
                },obj =>
            {

                AccountMutationDto biHdr; 
                AccountMutation biDtl;  

                AccountMutationDto hdr = obj[0] as AccountMutationDto;  
                AccountMutation dtl = obj[1] as AccountMutation;    
                Customer customer = obj[2] as Customer;  




                if (!bhdr.TryGetValue(hdr.AccountNo.NullToString(), out biHdr))
                {
                    biHdr = hdr;  
                    biHdr.lst_dtl = new List<AccountMutation>(); 
                    biHdr.customer =customer;     
                    bhdr.Add(biHdr.AccountNo.NullToString(), biHdr);

                }
                
 
                if (dtl != null && dtl.DetailID.NullToString() != "") {
                    
                    if (!bhdtl.TryGetValue(string.Concat(dtl.DetailID), out biDtl))
                    {
                        
                        dtl.AccountNo = hdr.AccountNo;
                        biDtl = dtl;  
                        bhdtl.Add(string.Concat(dtl.DetailID), biDtl);

                    }

 

                    if (!biHdr.lst_dtl.Exists(x => string.Concat(x.DetailID) == string.Concat(biDtl.DetailID)))
                    {
                        biHdr.lst_dtl.Add(biDtl);
                    }

                    
                }
 

 
                return biHdr;
 
            }, splitOn: "AccountNo, ID_Mutation,ID_Customer");

            return bhdr.Values;

        }


        public async Task<AccountMutation> SaveData(AccountMutation accountMutation)
        {


            _context.BeginTransaction();
            _transaction = _context.transaction; 
            try
            { 

 
                var dP = new DynamicParameters();
                        dP.Add("DocumentNo", accountMutation.DocumentNo.NullToString(), direction: ParameterDirection.InputOutput); 
                        dP.Add("TransDate", accountMutation.TransDate); 
                        dP.Add("TransTypeID", accountMutation.TransTypeID);
                        dP.Add("AccountNo", accountMutation.AccountNo);   
                        dP.Add("Amount", accountMutation.Amount);  
                             
                        dP.Add("IndDelete", false); 
                        dP.Add("UserNameBy", accountMutation.UserNameBy); 
                        dP.Add("Flag", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _context.db.QueryAsync<int>("spAccountMutationSave", dP, commandType: CommandType.StoredProcedure, transaction:_context.transaction);

     

                _context.Commit();
                var newUser = await _context.db.QueryFirstOrDefaultAsync<AccountMutation>("Select * FROM tblTrnAccountMutation WHERE DocumentNo = @DocumentNo", new { DocumentNo = dP.Get<dynamic>("DocumentNo")});
                return newUser;
            }
            catch (System.Exception e)
            {
                _context.Rollback();
                throw new System.Exception(e.Message);
            }
        }

         
    }
}