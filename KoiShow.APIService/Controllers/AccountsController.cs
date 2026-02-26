using KoiShow.APIService.Helper;
using KoiShow.Common;
using KoiShow.Data.DTO.Authentication;
using KoiShow.Data.Models;
using KoiShow.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KoiShow.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly JwtHelper _jwtHelper;

        public AccountsController(JwtHelper jwtHelper, AccountService accountService)
        {
            _accountService = accountService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
        {
            var account = await _accountService
                .ValidateUserAsync(loginModel.UserName, loginModel.Password);

            if (account == null)
                return Unauthorized();

            var accessToken = _jwtHelper.GenerateToken(account);
            var refreshTokenString = _jwtHelper.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenString,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                AccountId = account.Id,
                CreatedTime = DateTime.UtcNow
            };

            await _accountService.SaveRefreshToken(refreshToken);

            return Ok(new
            {
                accessToken,
                refreshToken = refreshTokenString
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO request)
        {
            var storedToken = await _accountService
                .GetRefreshTokenAsync(request.RefreshToken);

            if (storedToken == null ||
                storedToken.IsRevoked ||
                storedToken.ExpiryDate < DateTime.UtcNow)
                return Unauthorized();

            var account = await _accountService
                .GetByIdAsync(storedToken.AccountId);

            if (account == null)
                return Unauthorized();

            var newAccessToken = _jwtHelper.GenerateToken(account);

            return Ok(new
            {
                accessToken = newAccessToken
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (idClaim == null)
                return Unauthorized();

            var account = await _accountService.GetByIdAsync(int.Parse(idClaim));

            if (account == null)
                return NotFound();

            return Ok(new
            {
                account.Id,
                account.UserName,
                account.Email,
                account.FullName,
                account.BirthDay,
                account.Phone,
                account.Address,
                account.Role
            });
        }
    }
}
