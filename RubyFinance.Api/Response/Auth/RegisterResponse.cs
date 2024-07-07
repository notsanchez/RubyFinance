namespace RubyFinance.Api.Response.Auth;

public class RegisterResponse
{

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}

public class RegisterApiResponse : ApiResponse<RegisterResponse>
{

    public static new RegisterApiResponse SuccessResponse(RegisterResponse data, string message = "Usuário criado com sucesso")
    {
        return new RegisterApiResponse { Success = true, Message = message, Data = data };
    }

    public static new RegisterApiResponse ErrorResponse(string message)
    {
        return new RegisterApiResponse { Success = false, Message = message, Data = null! };
    }
}
