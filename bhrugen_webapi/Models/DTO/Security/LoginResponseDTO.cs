namespace bhrugen_webapi.Models.DTO.Security;

public class LoginResponseDTO
{
    public LocalUser User { get; set; }
    public string Token { get; set; }
}