using RubyFinance.Api.Response.Trnsaction;

namespace RubyFinance.Api.Response.Trnsaction;


public class CreateTransactionApiResponse : ApiResponse<TransactionResponse>
{

    public static new CreateTransactionApiResponse SuccessResponse(TransactionResponse data, string message = "Transação criada com sucesso")
    {
        return new CreateTransactionApiResponse { Success = true, Message = message, Data = data };
    }

    public static new CreateTransactionApiResponse ErrorResponse(string message)
    {
        return new CreateTransactionApiResponse { Success = false, Message = message, Data = null! };
    }
}