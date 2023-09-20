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
    internal class AccountRepo : IAccountRepo
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private QueryHelper _qH;
        public AccountRepo(IDapperContext context)
        {
            _context = context;
            _qH = new QueryHelper();
        }
        public AccountRepo(IDapperContext context, IDbTransaction transaction) : this(context)
        {
            _transaction = transaction;
        }
        public async Task<IEnumerable<Account>> GetData(DefaultParam param)
        {

            string sql;
            string cond="";
            

            if(param.Id.NullToString() != ""){
                cond = _qH.SetConditionAND(cond,string.Format(@"AccountNo = {0} ",param.Id));
            }
            if(param.Name.NullToString() != ""){
                cond = _qH.SetConditionAND(cond,string.Format(@"CardNo LIKE %'{0}'% ",param.Name));
            }


            sql = string.Format(@"SELECT * 
                                    FROM tblMstAccount 
                                    {0}
                                    ORDER BY AccountNo",  
                                    cond ==""? "" : " WHERE " + cond);

            var result = await _context.db.QueryAsync<Account>(sql, commandType:CommandType.Text);
            return result;
        }


        public async Task<Account> SaveData(Account account)
        {


            _context.BeginTransaction();
            _transaction = _context.transaction; 
            try
            { 
                var dP = new DynamicParameters();
                        dP.Add("AccountNo", account.AccountNo, direction: ParameterDirection.InputOutput); 
                        dP.Add("CustomerID", account.CustomerID); 
                        dP.Add("InActive", account.InActive);
                        dP.Add("CardNo", account.CardNo);   
                             
                        dP.Add("IndDelete", false); 
                        dP.Add("UserNameBy", account.UserNameBy); 
                        dP.Add("Flag", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _context.db.QueryAsync<int>("spAccountSave", dP, commandType: CommandType.StoredProcedure, transaction:_context.transaction);

     

                _context.Commit();
                var newUser = await _context.db.QueryFirstOrDefaultAsync<Account>("Select * FROM tblMstAccount WHERE AccountNo = @AccountNo", new { AccountNo = dP.Get<dynamic>("AccountNo")});
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