using KoiShow.Data.Base;
using KoiShow.Data.Models;

namespace KoiShow.Data.Repository
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>
    {
        public RefreshTokenRepository()
        {
        }

        public RefreshTokenRepository(FA24_SE171442_PRN231_AS_KoiShowContext context)
        {
            _context = context;
        }
    }
}