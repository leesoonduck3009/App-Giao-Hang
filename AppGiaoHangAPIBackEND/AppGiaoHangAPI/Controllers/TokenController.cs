using AppGiaoHangAPI.Interface.IRepository;
using AppGiaoHangAPI.Model.HelperModel;
using AppGiaoHangAPI.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppGiaoHangAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        public TokenController(IConfiguration configuration, IAccountRepository accountRepository)
        {

            _configuration = configuration;
            _accountRepository = accountRepository;
        }
        [HttpPost]
        public async Task<ActionResult<ResponeInfo>> Post(Account account)
        {
            ResponeInfo responeInfo = new ResponeInfo();

            if (!string.IsNullOrEmpty(account.UserName) && !string.IsNullOrEmpty(account.Password))
            {
                ErrorMessageInfo errorMessageInfo = await _accountRepository.getAccount(account.UserName, account.Password);
                try
                {
                    Account accountFind = (Account)errorMessageInfo.data;
                    if (accountFind != null)
                    {
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMinutes(10).ToString()),
                        new Claim(ClaimTypes.Role,accountFind.Roles)
                    };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn);
                        responeInfo.data = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(responeInfo);
                    }
                    else
                    {
                        return BadRequest(responeInfo);
                    }
                }
                catch (Exception ex)
                {
                    responeInfo.statusCode = System.Net.HttpStatusCode.InternalServerError;
                    responeInfo.message = ex.Message;
                    return BadRequest(responeInfo);
                }
            }
            responeInfo.statusCode = System.Net.HttpStatusCode.BadRequest;
            responeInfo.message = "NOT FOUND";
            return BadRequest(responeInfo);
        }            
    } 
}
