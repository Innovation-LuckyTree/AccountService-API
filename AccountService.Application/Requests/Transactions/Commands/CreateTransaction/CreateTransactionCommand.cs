using MediatR;

namespace AccountService.Application.Requests.Transactions.Commands.CreateTransaction;

public class CreateTransactionCommand : IRequest<CreateTransactionDto>
{
    public string Id { get; set; }
    public long? CoreTransactionId { get; set; }
    public string AccountId { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? StatusNotes { get; set; }
    public decimal Amount { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string? ClientTransactionId { get; set; }
    public string? ClientNotes { get; set; }
    public string? CallbackUrl { get; set; }
    public string? RedirectUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int PaymentProviderId { get; set; }
}
