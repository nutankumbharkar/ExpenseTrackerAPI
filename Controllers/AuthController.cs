using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services; 
using ExpenseTrackerAPI.DTOs;     
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtHelper _jwt;

    public AuthController(AppDbContext context, JwtHelper jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    // ✅ REGISTER
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        // 🔴 NULL CHECK
        if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
        {
            return BadRequest("Username and Password are required");
        }

        // 🔴 USER ALREADY EXISTS CHECK
        var existingUser = _context.Users.FirstOrDefault(x => x.Username == dto.Username);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        // 🔐 HASH PASSWORD
        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = hash
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Registered Successfully");
    }

    // ✅ LOGIN
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        // 🔴 NULL CHECK
        if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
        {
            return BadRequest("Invalid input");
        }

        var user = _context.Users.FirstOrDefault(x => x.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid username or password");
        }

        // 🔐 GENERATE JWT
        var token = _jwt.GenerateToken(user.Id);

        return Ok(new { token });
    }
}