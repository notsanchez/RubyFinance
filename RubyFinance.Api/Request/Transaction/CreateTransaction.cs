using RubyFinance.Api.Enums;

namespace RubyFinance.Api.Request.Category;

public class CreateTransactionRequest
{

    public string Title { get; set; } = string.Empty;

    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;

    public decimal Amount { get; set; }

    public long CategoryId { get; set; }

}