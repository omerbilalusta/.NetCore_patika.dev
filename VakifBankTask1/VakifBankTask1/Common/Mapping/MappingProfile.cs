using AutoMapper;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductsViewModel>();
            CreateMap<Product, ProductGetByIdViewModel>();
            CreateMap<ProductAddViewModel, Product>();
            CreateMap<ProductUpdateViewModel, Product>();
        }
    }
}
