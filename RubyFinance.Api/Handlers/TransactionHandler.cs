using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using RubyFinance.Api.Data;
using RubyFinance.Api.Models;
using RubyFinance.Api.Request.Category;
using RubyFinance.Api.Response.Category;
using RubyFinance.Api.Response.Trnsaction;

namespace RubyFinance.Api.Handlers;

public class TransactionHandler
{

    private readonly AppDbContext _context;
    private readonly JWTHandler _jwtHandler;

    public TransactionHandler(AppDbContext context, JWTHandler jwtHandler)
    {
        _context = context;
        _jwtHandler = jwtHandler;
    }

    public async Task<CreateTransactionApiResponse> CreateTransaction(CreateTransactionRequest request, string token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                return CreateTransactionApiResponse.ErrorResponse("Token não fornecido");
            }

            var userId = _jwtHandler.DecodeToken(token);

            if (userId == null)
            {
                return CreateTransactionApiResponse.ErrorResponse("Token inválido");
            }

            var newTransaction = new Transaction
            {
                Title = request.Title,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Type = request.Type,
                UserId = (long)userId
                
            };

            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();

            // var transactionResponse = new TransactionResponse
            // {
            //     Id = newCategory.Id,
            //     Title = newCategory.Title,
            //     Description = newCategory.Description
            // };

            // return CreateTransactionApiResponse.SuccessResponse(categoryResponse);
        }
        catch
        {
            return CreateTransactionApiResponse.ErrorResponse("Falha ao criar categoria");
        }
    }

    

    

}