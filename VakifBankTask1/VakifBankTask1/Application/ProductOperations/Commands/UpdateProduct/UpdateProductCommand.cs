using AutoMapper;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Commands.UpdateProduct
{
    public class UpdateProductCommand
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommand(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(int id, ProductUpdateViewModel productModel)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
                throw new InvalidOperationException("İlgili ürün bulunamadı.");

            _context.Products.Update(_mapper.Map<Product>(productModel));
            _context.SaveChanges();
        }
    }
}
