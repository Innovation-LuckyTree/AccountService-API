using AccountService.Application.Common.Interfaces;
using AccountService.Domain.Entities;
using AutoMapper;

namespace AccountService.Application.Requests.Transactions.Queries;

public class TransactionDto : IMapFrom<Transaction>
{
    public long TransactionId { get; set; }
    public Guid TransactionObjectId { get; set; }
    public string ResponseId { get; set; }
    public Guid UserAccountId { get; set; }
    public string AccountId { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string StatusNotes { get; set; }
    public decimal Amount { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string ClientTransactionId { get; set; }
    public string ClientNotes { get; set; }
    public string CallbackUrl { get; set; }
    public string RedirectUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DateRecieved { get; set; } = DateTime.Now;
    public string PaymentProvider { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Transaction, TransactionDto>()
            .ForMember(t => t.TransactionId, f => f.MapFrom(src => src.TransactionId))
            .ForMember(t => t.TransactionObjectId, f => f.MapFrom(src => src.TransactionObjectId))
            .ForMember(t => t.ResponseId, f => f.MapFrom(src => src.ResponseId))
            .ForMember(t => t.UserAccountId, f => f.MapFrom(src => src.UserAccountId))
            .ForMember(t => t.AccountId, f => f.MapFrom(src => src.AccountId))
            .ForMember(t => t.Type, f => f.MapFrom(src => src.Type))
            .ForMember(t => t.Status, f => f.MapFrom(src => src.Status))
            .ForMember(t => t.StatusNotes, f => f.MapFrom(src => src.StatusNotes))
            .ForMember(t => t.Amount, f => f.MapFrom(src => src.Amount))
            .ForMember(t => t.AccountName, f => f.MapFrom(src => src.AccountName))
            .ForMember(t => t.AccountNumber, f => f.MapFrom(src => src.AccountNumber))
            .ForMember(t => t.ClientTransactionId, f => f.MapFrom(src => src.ClientTransactionId))
            .ForMember(t => t.ClientNotes, f => f.MapFrom(src => src.ClientNotes))
            .ForMember(t => t.CallbackUrl, f => f.MapFrom(src => src.CallbackUrl))
            .ForMember(t => t.RedirectUrl, f => f.MapFrom(src => src.RedirectUrl))
            .ForMember(t => t.CreatedAt, f => f.MapFrom(src => src.CreatedAt))
            .ForMember(t => t.UpdatedAt, f => f.MapFrom(src => src.UpdatedAt))
            .ForMember(t => t.DateRecieved, f => f.MapFrom(src => src.DateRecieved))
            .ForMember(t => t.PaymentProvider, f => f.MapFrom(src => src.PaymentProvider.Name));
    }

}