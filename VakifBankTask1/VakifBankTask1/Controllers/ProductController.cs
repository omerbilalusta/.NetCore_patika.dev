using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VakifBankTask1.Application.ProductOperations.Commands.AddProduct;
using VakifBankTask1.Application.ProductOperations.Commands.DeleteProduct;
using VakifBankTask1.Application.ProductOperations.Commands.UpdateProduct;
using VakifBankTask1.Application.ProductOperations.Queries.GetProductById;
using VakifBankTask1.Application.ProductOperations.Queries.GetProducts;
using VakifBankTask1.Application.ProductOperations.Queries.SortProduct;
using VakifBankTask1.Models;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            GetProductsQuery query = new GetProductsQuery(_context, _mapper);
            var productsList = query.Handle();

            return Ok(productsList);
        }

        [HttpGet("ProductsById/{id}")]
        public IActionResult GetProductById(int id)
        {
            GetProductByIdValidator validator = new GetProductByIdValidator();
            GetProductByIdQuery query = new GetProductByIdQuery(_context, _mapper);
            query.Id = id;

            validator.ValidateAndThrow(query);

            var product = query.Handle();

            return Ok(product);

        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductAddViewModel productModel)
        {
            ProductAddValidator validator = new ProductAddValidator();
            validator.ValidateAndThrow(productModel);

            AddProductCommand command = new AddProductCommand(_context, _mapper);
            command.Handle(productModel);

            return Created("api/AddProduct", productModel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateViewModel productModel)
        {
            ProductUpdateValidator validator = new ProductUpdateValidator();
            validator.ValidateAndThrow(productModel);

            UpdateProductCommand command = new UpdateProductCommand(_context, _mapper);
            command.Handle(id, productModel);

            return Created("api/UpdateProduct", productModel);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            DeleteProductValidator validator = new DeleteProductValidator();
            DeleteProductCommand command = new DeleteProductCommand(_context);
            command.id = id;
            validator.ValidateAndThrow(command);

            command.Handle();

            return NoContent();
        }

        [HttpGet("FilterProduct")]
        public IActionResult GetProductByFilter([FromQuery] string filter)
        {
            if(filter == null)
                throw new ArgumentNullException(nameof(filter));

            var productsList = _context.Products.Where(x => x.ProductName.Contains(filter)).ToList();
            if (productsList.IsNullOrEmpty())
                return NotFound(filter + " değişkenini içeren bir kayıt bulunamadı.");

            return Ok(_mapper.Map<List<ProductsViewModel>>(productsList));
        }

        [HttpGet("SortProduct")]
        public IActionResult SortProduct([FromQuery] string sortBy)
        {
            SortProductQuery query = new SortProductQuery(_context, _mapper);
            query.SortBy = sortBy;
            var productsList = query.Handle();

            return Ok(productsList);
        }
    }
}
