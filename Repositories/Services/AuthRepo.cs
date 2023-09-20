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
    internal class AuthRepo : IAuthRepo
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        public AuthRepo(IDapperContext context)
        {
            _context = context;
        }
        public AuthRepo(IDapperContext context, IDbTransaction transaction) : this(context)
        {
            _transaction = transaction;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.db.GetAllAsync<User>();
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            var PasswordKey = _context.GetGUID();
            var Password = CreatePasswordHash(userRegisterDto.RegisterPwd,PasswordKey); 

            _context.BeginTransaction();
            _transaction = _context.transaction; 
            try
            {
                
                var dP = new DynamicParameters();
                        dP.Add("UserName", userRegisterDto.UserName); 
                        dP.Add("FirstName", userRegisterDto.FirstName); 
                        dP.Add("MiddleName", userRegisterDto.MiddleName);
                        dP.Add("LastName", userRegisterDto.LastName);  
                        dP.Add("Password", Password);
                        dP.Add("PasswordKey", PasswordKey); 
                             
                        dP.Add("IndDelete", false); 
                        dP.Add("UserNameBy", userRegisterDto.UserNameBy); 
                        dP.Add("Flag", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _context.db.QueryAsync<int>("spUserSave", dP, commandType: CommandType.StoredProcedure, transaction:_context.transaction);

     

                _context.Commit();
                var newUser = await _context.db.QueryFirstOrDefaultAsync<User>("Select * FROM tblMstUser WHERE UserName = @username", new { username = userRegisterDto.UserName });
                return newUser;
            }
            catch (System.Exception e)
            {
                _context.Rollback();
                throw new System.Exception(e.Message);
            }
        }

        public async Task<UserDto> Login(string username, string password)
        {
            try
            {
                var user = await _context.db.QueryFirstOrDefaultAsync<UserDto>("Select * FROM tblMstUser WHERE UserName = @username", new { username = username });

                if (user == null)
                    throw new Exception("Account doesn't exists !!!");

                if (!VerifyPassword(password, user.Password, user.PasswordKey))
                    throw new Exception("Wrong Password !!!");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool VerifyPassword(string pass, string password, string passwordKey)
        {
            var passUser = CreatePasswordHash(pass, passwordKey);

            if (passUser != password)
                return false;

            return true;
        }

        private string CreatePasswordHash(string plainText, string key)
        {
            if (key.Length > 0)
                plainText += key;

            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bs = System.Text.Encoding.UTF8.GetBytes(plainText);
            bs = x.ComputeHash(bs);

            var s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
         
    }
}