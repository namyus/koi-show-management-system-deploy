using System;
using System.Collections.Generic;
using KoiShow.Data.Base;


namespace KoiShow.Data.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByToken { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;
    }
}
