namespace RubyFinance.Api.Response.Category;


public class CategoryByIdApiResponse : ApiResponse<CategoryResponse>
{

    public static new CategoryByIdApiResponse SuccessResponse(CategoryResponse data, string message = "Dados da categoria")
    {
        return new CategoryByIdApiResponse { Success = true, Message = message, Data = data };
    }

    public static CategoryByIdApiResponse SuccessDeleteResponse(CategoryResponse data, string message = "Categoria excluida com sucesso")
    {
        return new CategoryByIdApiResponse { Success = true, Message = message, Data = data };
    }

    public static new CategoryByIdApiResponse ErrorResponse(string message)
    {
        return new CategoryByIdApiResponse { Success = false, Message = message, Data = null! };
    }
}