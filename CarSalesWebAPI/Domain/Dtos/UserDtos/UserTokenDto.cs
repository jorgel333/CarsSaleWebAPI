﻿namespace CarSalesWebAPI.Domain.Dtos.UserDtos
{
    public class UserTokenDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
