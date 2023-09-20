using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Banking.Api.Dtos;
using Banking.Api.Models; 
using Banking.Api.Repositories.Interfaces;
using Banking.Api.Repositories.Services;


namespace Banking.Api.Controllers.Utilities
{
    [Route("Banking.Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AuthController));
        private IUnitOfWork _uow;
        private IDapperContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;


        public AuthController(IConfiguration config)
        {
            _config = config;
            _httpContext = (IHttpContextAccessor)new HttpContextAccessor();
        }

        /// <summary>
        /// Get All Data Master User.
        /// </summary>
        // [AllowAnonymous]
        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string UserNameBy = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            
            try
            {
                IEnumerable<User> result;
                using (IDapperContext _context = new DapperContext())
                {
                    var _uow = new UnitOfWork(_context);
                    result = await _uow.AuthRepo.GetAll();
                }

                var st = StTrans.SetSt(200, 0, "Data di temukan");
                Log4netSet.SetLogNet(result, UserNameBy);_log.Info(st.Description);
                return Ok(new { Status = st, Results = result });
            }
            catch (System.Exception e)
            {
                User result2 = new User();
                var st2 = StTrans.SetSt(400, 0, e.Message);
                Log4netSet.SetLogNet(result2, UserNameBy);_log.Error(st2.Description);
                return Ok(new { Status = st2 });
            }
        }

        /// <summary>
        /// Register new User.
        /// </summary>
        // [AllowAnonymous]
        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            string UserNameBy = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
 
            try
            {
                var usercreate = new User();
                using (_context = new DapperContext())
                {
                    _uow = new UnitOfWork(_context, _log);
 
                    usercreate = await _uow.AuthRepo.Register(userRegisterDto);
                }

                var st = StTrans.SetSt(200, 0, "User Berhasil Di Buat");
                Log4netSet.SetLogNet(userRegisterDto, UserNameBy);_log.Info(st.Description);
                return Ok(new { Status = st, Results = usercreate });

            }
            catch (System.Exception e)
            {
                var st2 = StTrans.SetSt(400, 0, e.Message);
                Log4netSet.SetLogNet(userRegisterDto, UserNameBy);_log.Error(st2.Description);
                return Ok(new { Status = st2 });
            }
        }

        /// <summary>
        /// User Login.
        /// </summary>
        // [AllowAnonymous]
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var dt = new UserDto();
                using (_context = new DapperContext())
                {
                    _uow = new UnitOfWork(_context, _log);
                    dt = await _uow.AuthRepo.Login(userLoginDto.UserName.ToLower(), userLoginDto.Password);
 
                    if (dt == null)
                        return Unauthorized();

 
                    dt.Token = GenerateJwtToken(dt);

                }
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var x = tokenHandler.ReadJwtToken(dt.Token);
                dt.TokenExpires = x.ValidTo.AddHours(7);
            
                var st = StTrans.SetSt(200, 0, "User Berhasil Login");
                Log4netSet.SetLogNet(userLoginDto, userLoginDto.UserName);_log.Info(st.Description);

                return Ok(new { Status = st, Results = dt });
            }
            catch (System.Exception e)
            {
                var st2 = StTrans.SetSt(400, 0, e.Message); 
                Log4netSet.SetLogNet(userLoginDto, userLoginDto.UserName);_log.Error(st2.Description);
                return Ok(new { Status = st2 });
            }
        }
 
        
        private string GenerateJwtToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName), 
                new Claim(ClaimTypes.Name, user.FirstName) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            

            var token = tokenHandler.CreateToken(tokenDescriptor);
    

            return tokenHandler.WriteToken(token);
        }

    }
}