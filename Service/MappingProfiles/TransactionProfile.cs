using AutoMapper;
using Domain.Models;
using Shared.TransactionDto;

namespace Service.MappingProfiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction,TransactionDto>()
                    .ForMember(dest => dest.ProductName,opt => opt.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.AppUserName,opt => opt.MapFrom(src => src.AppUser.UserName));
            CreateMap<Payment,PaymentDto>();
            CreateMap<CreateTransactionDto,Transaction>();
        }
    }
}
