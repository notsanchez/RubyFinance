namespace RubyFinance.Api.Response.Auth;

public class LoginResponse
{

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}

public class LoginApiResponse : ApiResponse<LoginResponse>
{
    public string Token { get; set; } = string.Empty;

    public static LoginApiResponse SuccessResponse(LoginResponse data, string token, string message = "Login realizado com sucesso")
    {
        return new LoginApiResponse { Success = true, Message = message, Data = data, Token = token };
    }

    public static new LoginApiResponse ErrorResponse(string message)
    {
        return new LoginApiResponse { Success = false, Message = message, Data = null!, Token = string.Empty };
    }
}
