namespace RubyFinance.Api.Response.Category;


public class CreateCategoryApiResponse : ApiResponse<CategoryResponse>
{

    public static new CreateCategoryApiResponse SuccessResponse(CategoryResponse data, string message = "Categoria criada com sucesso")
    {
        return new CreateCategoryApiResponse { Success = true, Message = message, Data = data };
    }

    public static new CreateCategoryApiResponse ErrorResponse(string message)
    {
        return new CreateCategoryApiResponse { Success = false, Message = message, Data = null! };
    }
}