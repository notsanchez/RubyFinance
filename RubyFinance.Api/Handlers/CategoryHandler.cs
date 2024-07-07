using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using RubyFinance.Api.Data;
using RubyFinance.Api.Models;
using RubyFinance.Api.Request.Category;
using RubyFinance.Api.Response.Category;

namespace RubyFinance.Api.Handlers;

public class CategoryHandler
{

    private readonly AppDbContext _context;
    private readonly JWTHandler _jwtHandler;

    public CategoryHandler(AppDbContext context, JWTHandler jwtHandler)
    {
        _context = context;
        _jwtHandler = jwtHandler;
    }

    public async Task<CreateCategoryApiResponse> CreateCategory(CreateCategoryRequest request, string token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                return CreateCategoryApiResponse.ErrorResponse("Token não fornecido");
            }

            var userId = _jwtHandler.DecodeToken(token);

            if (userId == null)
            {
                return CreateCategoryApiResponse.ErrorResponse("Token inválido");
            }

            var newCategory = new Category
            {
                Title = request.Title,
                Description = request.Description,
                UserId = (long)userId
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            var categoryResponse = new CategoryResponse
            {
                Id = newCategory.Id,
                Title = newCategory.Title,
                Description = newCategory.Description
            };

            return CreateCategoryApiResponse.SuccessResponse(categoryResponse);
        }
        catch
        {
            return CreateCategoryApiResponse.ErrorResponse("Falha ao criar categoria");
        }
    }

    public async Task<ListCategoryApiResponse> ListCategories(string token)
    {
        try
        {

            var userId = _jwtHandler.DecodeToken(token);

            if (userId == null)
            {
                return ListCategoryApiResponse.ErrorResponse("Token inválido");
            }

            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description
                }).ToListAsync();

            return ListCategoryApiResponse.SuccessResponse(categories);

        }
        catch
        {
            return ListCategoryApiResponse.ErrorResponse("Falha ao buscar categorias");
        }
    }

    public async Task<CategoryByIdApiResponse> GetCategoryById(string id, string token)
    {

        try
        {
            long.TryParse(id, out long categoryId);

            var userId = _jwtHandler.DecodeToken(token);

            if (userId == null)
            {
                return CategoryByIdApiResponse.ErrorResponse("Token inválido");
            }

            var category = await _context.Categories
                .Where(c => c.Id == categoryId)
                .Where(c => c.UserId == userId)
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description
                }).FirstOrDefaultAsync();


            return CategoryByIdApiResponse.SuccessResponse(category);
        }
        catch
        {
            return CategoryByIdApiResponse.ErrorResponse("Falha ao buscar categoria");
        }
    }

    public async Task<CategoryByIdApiResponse> DeleteCategoryById(string id, string token)
    {

        try
        {
            long.TryParse(id, out long categoryId);

            var userId = _jwtHandler.DecodeToken(token);

            if (userId == null)
            {
                return CategoryByIdApiResponse.ErrorResponse("Token inválido");
            }

            var category = await _context.Categories
                .Where(c => c.Id == categoryId)
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return CategoryByIdApiResponse.ErrorResponse("Categoria não encontrada");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return CategoryByIdApiResponse.SuccessDeleteResponse(null);
        }
        catch
        {
            return CategoryByIdApiResponse.ErrorResponse("Falha ao buscar categoria");
        }
    }

}