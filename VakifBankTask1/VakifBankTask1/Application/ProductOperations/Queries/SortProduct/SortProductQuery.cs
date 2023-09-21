using AutoMapper;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Queries.SortProduct
{
    public class SortProductQuery
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;
        public string SortBy { get; set; }

        public SortProductQuery(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductsViewModel> Handle()
        {
            var productsList = _context.Products.ToList();
            var mappedList = _mapper.Map<List<ProductsViewModel>>(productsList);

            if (SortBy.ToLower() == "name")
                return mappedList.OrderBy(x => x.ProductName).ToList();
            else if (SortBy.ToLower() == "id")
                return mappedList.OrderBy(x => x.ProductId).ToList();
            else
                throw new ArgumentException("Hatalı arguman!");
        }
    }
}
