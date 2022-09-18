using Application.Dto.Common;
using Application.Features.Employee.Commands;
using Application.Features.Employee.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace WebApi.Controllers;

public class AccountController : BaseApiController
{
    readonly IConfiguration configuration;

    public AccountController(IConfiguration _configuration)
    {
        configuration = _configuration;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] AuthUserDto user)
    {

        if (user is null)
        {
            return BadRequest("Invalid user request!!!");
        }
        if (user.Username == configuration.GetSection("JWT").GetSection("UserName").Value &&
            user.Password == configuration.GetSection("JWT").GetSection("Password").Value)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT").GetSection("Secret").Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: configuration.GetSection("JWT").GetSection("ValidIssuer").Value, audience: configuration.GetSection("JWT").GetSection("ValidAudience").Value, claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new JWTTokenResponseDto
            {
                Token = tokenString
            });
        }
        return Unauthorized();
    }
}
