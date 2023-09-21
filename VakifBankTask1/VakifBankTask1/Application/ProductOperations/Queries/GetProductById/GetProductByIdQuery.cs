using AutoMapper;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Queries.GetProductById
{
    public class GetProductByIdQuery
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;
        public int Id { get; set; }

        public GetProductByIdQuery(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ProductGetByIdViewModel Handle()
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == Id);
            if (product == null)
                throw new InvalidOperationException("İlgili ürün bulunamadı.");

            var mappedProduct = _mapper.Map<ProductGetByIdViewModel>(product);

            return mappedProduct;
        }
    }
}
