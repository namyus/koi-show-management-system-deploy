using KoiShow.Common;
using KoiShow.Data;
using KoiShow.Data.Models;
using KoiShow.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShow.Service
{
    public interface IAccountService
    {
        Task<Account?> ValidateUserAsync(string userName, string password);
        Task<Account?> GetByIdAsync(int id);

        Task SaveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
    }

    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;

        public AccountService()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        // ✅ Login logic (query thẳng DB)
        public async Task<Account?> ValidateUserAsync(string userName, string password)
        {
            var accounts = await _unitOfWork.AccountRepository
                .GetByConditionWithIncludeAsync(
                    x => x.UserName == userName && x.Password == password
                );

            return accounts.FirstOrDefault();
        }

        // ✅ Get account theo id (dùng cho profile)
        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _unitOfWork.AccountRepository.GetByIdAsync(id);
        }

        // ✅ Save refresh token
        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            await _unitOfWork.RefreshTokenRepository.CreateAsync(refreshToken);
        }

        // ✅ Get refresh token
        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            var tokens = await _unitOfWork.RefreshTokenRepository
                .GetByConditionWithIncludeAsync(x => x.Token == token);

            return tokens.FirstOrDefault();
        }
    }
}
