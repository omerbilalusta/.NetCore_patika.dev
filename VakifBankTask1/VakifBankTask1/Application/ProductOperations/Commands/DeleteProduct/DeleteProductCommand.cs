using AutoMapper;
using VakifBankTask1.Models;

namespace VakifBankTask1.Application.ProductOperations.Commands.DeleteProduct
{
    public class DeleteProductCommand
    {
        private readonly NorthwindDbContext _context;
        public int id { get; set; }

        public DeleteProductCommand(NorthwindDbContext context)
        {
            _context = context;
        }


        public void Handle()
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product == null)
                throw new InvalidOperationException();

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
