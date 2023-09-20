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
    internal class CustomerRepo : ICustomerRepo
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private QueryHelper _qH;
        public CustomerRepo(IDapperContext context)
        {
            _context = context;
            _qH = new QueryHelper();
        }
        public CustomerRepo(IDapperContext context, IDbTransaction transaction) : this(context)
        {
            _transaction = transaction;
        }
        public async Task<IEnumerable<Customer>> GetData(DefaultParam param)
        {

            string sql;
            string cond="";
            

            if(param.Id.NullToString() != ""){
                cond = _qH.SetConditionAND(cond,string.Format(@"CustomerID = {0} ",param.Id));
            }
            if(param.Name.NullToString() != ""){
                cond = _qH.SetConditionAND(cond,string.Format(@"CustomerName LIKE %'{0}'% ",param.Name));
            }


            sql = string.Format(@"SELECT * 
                                    FROM tblMstCustomer 
                                    {0}
                                    ORDER BY CustomerID",  
                                    cond ==""? "" : " WHERE " + cond);

            var result = await _context.db.QueryAsync<Customer>(sql, commandType:CommandType.Text);
            return result;
        }


        public async Task<Customer> SaveData(Customer customer)
        {


            _context.BeginTransaction();
            _transaction = _context.transaction; 
            try
            {
                
                var dP = new DynamicParameters();
                        dP.Add("CustomerID", customer.CustomerID, direction: ParameterDirection.InputOutput); 
                        dP.Add("FirstName", customer.FirstName); 
                        dP.Add("MiddleName", customer.MiddleName);
                        dP.Add("LastName", customer.LastName);  
                        dP.Add("Email", customer.Email);
                        dP.Add("PhoneNumber", customer.PhoneNumber); 
                             
                        dP.Add("IndDelete", false); 
                        dP.Add("UserNameBy", customer.UserNameBy); 
                        dP.Add("Flag", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _context.db.QueryAsync<int>("spCustomerSave", dP, commandType: CommandType.StoredProcedure, transaction:_context.transaction);

     

                _context.Commit();
                var newUser = await _context.db.QueryFirstOrDefaultAsync<Customer>("Select * FROM tblMstCustomer WHERE CustomerID = @CustomerID", new { CustomerID = dP.Get<dynamic>("CustomerID")});
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