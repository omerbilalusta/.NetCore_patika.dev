using AutoMapper;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Queries.GetProducts
{
    public class GetProductsQuery
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQuery(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductsViewModel> Handle()
        {
            var productsList = _context.Products.ToList<Product>();

            var mappedList = _mapper.Map<List<ProductsViewModel>>(productsList);

            return mappedList;
        }
    }
}
