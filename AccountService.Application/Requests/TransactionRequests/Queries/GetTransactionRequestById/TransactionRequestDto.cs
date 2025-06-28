using AccountService.Application.Common.Interfaces;
using AccountService.Domain.Entities;
using AutoMapper;

namespace AccountService.Application.Requests.TransactionRequests.Queries.GetTransactionRequestById;

public class TransactionRequestDto : IMapFrom<TransactionRequest>
{
    public long TransactionRequestId { get; set; }
    public string UserAccountId { get; set; }
    public string TransactionType { get; set; }
    public string TransactionId { get; set; }
    public DateTime CreatedOn { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TransactionRequest, TransactionRequestDto>()
            .ForMember(t => t.TransactionId, f => f.MapFrom(src => src.TransactionId))
            .ForMember(t => t.UserAccountId, f => f.MapFrom(src => src.UserAccountId))
            .ForMember(t => t.TransactionType, f => f.MapFrom(src => src.TransactionType))
            .ForMember(t => t.TransactionId, f => f.MapFrom(src => src.TransactionId))
            .ForMember(t => t.CreatedOn, f => f.MapFrom(src => src.CreatedOn));            
    }

}