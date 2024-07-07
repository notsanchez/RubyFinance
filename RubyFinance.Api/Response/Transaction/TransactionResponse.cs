namespace RubyFinance.Api.Response.Trnsaction;


public class TransactionResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}