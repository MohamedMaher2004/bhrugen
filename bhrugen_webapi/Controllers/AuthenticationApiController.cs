using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bhrugen_webapi.Data;
using bhrugen_webapi.Models;
using bhrugen_webapi.Models.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace bhrugen_webapi.Controllers;
[Route("api/UsersAuthApi")]
[ApiController]
public class AuthenticationApiController:ControllerBase
{
    private readonly ApplicationDBContext _DB;
    private string secretkey;
    
    public AuthenticationApiController(ApplicationDBContext db,IConfiguration _configuration)
    {
        _DB = db;
        secretkey = _configuration.GetValue <string>("ApiSettings:Secret");
    }
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> Login([FromBody]LoginRequestDTO loginRequestDTO)
    {
        var user =await _DB.LocalUsers.FirstOrDefaultAsync(x=>x.UserName==loginRequestDTO.UserName 
                                                              && x.Password==loginRequestDTO.Password);
        if (user == null)
        {
            return BadRequest(new{message="UserName or Password is incorrect"});
        }
        //IF USER WAS FOUND GENERATE JWT TOKEN
        var tokenHandler = new JwtSecurityTokenHandler();
        //convert the secretkey to bytes
        var key = Encoding.ASCII.GetBytes(secretkey);
        //create token descriptor(contains every thing like what are all the claims in a token(id,name,role,dateexpire)
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var loginResponseDto = new LoginResponseDTO()
        {
            User = user,
            //this will serialize the token
            Token = tokenHandler.WriteToken(token)
        };
        return Ok(loginResponseDto);
    }

    [HttpPost("register")]
    public async Task<ActionResult<LocalUserDTO>> Register([FromBody]RegisterationRequestDTO registerationRequestDTO)
    {
        if (Helpers.Helpers.IsUniqueUser(registerationRequestDTO.UserName) == false)
        {
            return BadRequest("Username already existing");

        }
        if (registerationRequestDTO == null)
        {
            return BadRequest(registerationRequestDTO);
        }

        LocalUser model = new()
        {
            UserName = registerationRequestDTO.UserName,
            Password = registerationRequestDTO.Password,
            Name = registerationRequestDTO.Name,
            Role = registerationRequestDTO.Role
            
        };
       await _DB.LocalUsers.AddAsync(model);
       await _DB.SaveChangesAsync();
       model.Password = "";
      return Ok(model);
    }
   
}