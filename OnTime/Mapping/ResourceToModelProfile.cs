using AutoMapper;
using OnTime.Model.BusinessEntities;
using OnTime.Model.ViewModel;
using OnTime.Model.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<RegisterViewModel, ClientPersonal>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductType, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AddOrderViewModel, Order>()
                .ForMember(dest => dest.ClientPersonal, opt => opt.Ignore())
                .ForMember(dest => dest.ProductType, opt => opt.Ignore());
        }
        
    }
}
