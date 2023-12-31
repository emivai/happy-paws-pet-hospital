﻿namespace HappyPaws.Core.Auth
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; } = DateTime.UtcNow.AddHours(1);
    }
}
