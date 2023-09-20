using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Banking.Api.Models;
using Banking.Api.Repositories.Interfaces;
using Banking.Api.Repositories.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 

namespace Banking.Api.Controllers
{
    [Route("Banking.Api/[controller]")]
    [ApiController]
    public class AccountController :  ControllerBase
    {
        private IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;
        private IDapperContext _context;

        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountController));

        public AccountController(IConfiguration config)
        {
            _config = config;
            _httpContext = (IHttpContextAccessor)new HttpContextAccessor();
        }

        /// <summary>
        /// Get Data Master Account.
        /// </summary>
        // [AllowAnonymous]
        [Authorize(Policy="RequireAdmin")]
        [HttpPost("GetData")]
        public async Task<IActionResult> GetData(DefaultParam param){
            string UserNameBy = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            try
            {
                IEnumerable<Account> result;
                using(_context = new DapperContext()){
                    _uow = new UnitOfWork(_context);
                    result = await _uow.AccountRepo.GetData(param);
                }

                var st = StTrans.SetSt(200, 0,  result.Count().ToString() + " Data di temukan!");
                Log4netSet.SetLogNet(result, UserNameBy);_log.Info(st.Description);
                return Ok(new { Status = st, Results = result });
            }
            catch (System.Exception e)
            {   
                var st2 = StTrans.SetSt(400, 0, e.Message);
                Log4netSet.SetLogNet(param, UserNameBy);_log.Error(st2.Description);
                return Ok(new { Status = st2, Results = new List<Account>() });

 
            }
        }
         
        /// <summary>
        /// Save Account.
        /// </summary>
        // [AllowAnonymous]
        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("SaveData")]
        public async Task<IActionResult> SaveData(Account param)
        {
            string UserNameBy = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
 
            try
            {
                var customer = new Account();
                using (_context = new DapperContext())
                {
                    _uow = new UnitOfWork(_context, _log);
 
                    customer = await _uow.AccountRepo.SaveData(param);
                }

                var st = StTrans.SetSt(200, 0, "Account Berhasil Di Simpan");
                Log4netSet.SetLogNet(param, UserNameBy);_log.Info(st.Description);
                return Ok(new { Status = st, Results = customer });

            }
            catch (System.Exception e)
            {
                var st2 = StTrans.SetSt(400, 0, e.Message);
                Log4netSet.SetLogNet(param, UserNameBy);_log.Error(st2.Description);
                return Ok(new { Status = st2 });
            }
        }
 
 
    }    
}