using AutoMapper;
using Core.Api.ViewModels;
using Core.Business.Models;
using Core.Business.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace Core.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<Supplier, SupplierViewModel>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.IsActive ? "Ativo" : "Inativo"));
            CreateMap<Address, AddressViewModel>().ReverseMap();

            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.NameSupplier, opt => opt.MapFrom(src => src.Supplier.Name));

            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();

            CreateMap<AppAction, AppActionViewModel>().ReverseMap();
            CreateMap<AppController, AppControllerViewModel>().ReverseMap();

            CreateMap<AllUsersDto, AllUsersViewModel>().ReverseMap();

            CreateMap<UserClaimsDto, UserClaimsViewModel>().ReverseMap();

            CreateMap<ComboDto, ComboViewModel>().ReverseMap();

            CreateMap<ClientViewModel, Client>()
                .ForMember(dest => dest.NIF, opt => opt.MapFrom(src => src.NIF.Trim()));
            CreateMap<Client, ClientViewModel>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.IsActive.Value ? "Ativo" : "Inativo"))
                .ForMember(dest => dest.NIF, opt => opt.MapFrom(src => src.NIF.Trim()));
        }
    }
}
