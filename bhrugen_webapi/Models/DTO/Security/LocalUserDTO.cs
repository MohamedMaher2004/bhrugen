﻿namespace bhrugen_webapi.Models.DTO.Security
{
    public class LocalUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
    }
}
