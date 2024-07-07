namespace RubyFinance.Api.Response.Category
{
    

    public class ListCategoryApiResponse : ApiResponse<List<CategoryResponse>>
    {
        public static ListCategoryApiResponse SuccessResponse(List<CategoryResponse> data)
        {
            return new ListCategoryApiResponse { Success = true, Message = "Lista de categorias", Data = data };
        }

        public static new ListCategoryApiResponse ErrorResponse(string message)
        {
            return new ListCategoryApiResponse { Success = false, Message = message, Data = null };
        }
    }
}
