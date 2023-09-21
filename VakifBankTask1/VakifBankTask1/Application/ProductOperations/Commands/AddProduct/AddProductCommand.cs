using AutoMapper;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Commands.AddProduct
{
    public class AddProductCommand
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public AddProductCommand(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(ProductAddViewModel productModel)
        {
            var product = _context.Products.SingleOrDefault(x => x.ProductName == productModel.ProductName);
            if (product != null)
                throw new InvalidCastException("Aynı isimde ürün mevcut.");

            _context.Products.Add(_mapper.Map<Product>(productModel));
            _context.SaveChanges();
        }
    }
}
