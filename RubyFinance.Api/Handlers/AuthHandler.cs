using Microsoft.EntityFrameworkCore;
using RubyFinance.Api.Data;
using RubyFinance.Api.Models;
using RubyFinance.Api.Request.Auth;
using RubyFinance.Api.Response.Auth;

namespace RubyFinance.Api.Handlers;

public class AuthHandler
{
    private readonly AppDbContext _context;
    private readonly JWTHandler _jwtHandler;


    public AuthHandler(AppDbContext context, JWTHandler jwtHandler)
    {
        _context = context;
        _jwtHandler = jwtHandler;

    }

    public async Task<LoginApiResponse> Login(LoginRequest request)
    {

        var user = await _context.Users.FromSqlRaw("SELECT * FROM Users WHERE Email = {0}", request.Email).FirstOrDefaultAsync();

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return LoginApiResponse.ErrorResponse("Credenciais Inválidas");
        }


        var response = new LoginResponse
        {
            Username = user.Username,
            Email = user.Email
        };

        string token = _jwtHandler.GenerateJwtToken(user);

        return LoginApiResponse.SuccessResponse(response, token);

    }


    public async Task<RegisterApiResponse> Register(RegisterRequest request)
    {
        if (
            string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.ConfirmPassword)
        )
        {
            return RegisterApiResponse.ErrorResponse("Campos são obrigatórios");
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (request.Password != request.ConfirmPassword)
        {
            return RegisterApiResponse.ErrorResponse("Senhas devem ser iguais");
        }

        if (existingUser != null)
        {
            return RegisterApiResponse.ErrorResponse("Esse email já está em uso");
        }

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Users.Add(newUser);

        await _context.SaveChangesAsync();

        var response = new RegisterResponse
        {
            Email = newUser.Email,
            Username = newUser.Username
        };

        return RegisterApiResponse.SuccessResponse(response);
    }



}