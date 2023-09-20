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
    internal class TransTypeRepo : ITransTypeRepo
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private QueryHelper _qH;
        public TransTypeRepo(IDapperContext context)
        {
            _context = context;
            _qH = new QueryHelper();
        }
        public TransTypeRepo(IDapperContext context, IDbTransaction transaction) : this(context)
        {
            _transaction = transaction;
        }
        public async Task<IEnumerable<TransType>> GetData(DefaultParam param)
        {

            string sql;
            string cond="";
            

            if(param.Id.NullToString() != ""){
                cond = _qH.SetConditionAND(cond,string.Format(@"TransTypeID = {0} ",param.Id));
            }
            if(param.Name.NullToString() != ""){
                cond = _qH.SetConditionAND(cond,string.Format(@"TransTypeName LIKE %'{0}'% ",param.Name));
            }


            sql = string.Format(@"SELECT * 
                                    FROM tblMstTransType 
                                    {0}
                                    ORDER BY TransTypeID",  
                                    cond ==""? "" : " WHERE " + cond);

            var result = await _context.db.QueryAsync<TransType>(sql, commandType:CommandType.Text);
            return result;
        }


        public async Task<TransType> SaveData(TransType transType)
        {


            _context.BeginTransaction();
            _transaction = _context.transaction; 
            try
            { 
                var dP = new DynamicParameters();
                        dP.Add("TransTypeID", transType.TransTypeID, direction: ParameterDirection.InputOutput); 
                        dP.Add("TransTypeName", transType.TransTypeName); 
                        dP.Add("IO", transType.IO);  
                             
                        dP.Add("IndDelete", false); 
                        dP.Add("UserNameBy", transType.UserNameBy); 
                        dP.Add("Flag", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _context.db.QueryAsync<int>("spTransTypeSave", dP, commandType: CommandType.StoredProcedure, transaction:_context.transaction);

     

                _context.Commit();
                var newUser = await _context.db.QueryFirstOrDefaultAsync<TransType>("Select * FROM tblMstTransType WHERE TransTypeID = @TransTypeID", new { TransTypeID = dP.Get<dynamic>("TransTypeID")});
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