using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController: ControllerBase{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBooksById(int id){
            // try
            // {
                GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
                GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
                query.id = id;
                validator.ValidateAndThrow(query);
                var result = query.Handle();
                return Ok(result);
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
        }
        // [HttpGet]
        // public Book GetById([FromQuery] string id){
        //     var book = BookList.Where(x=> x.Id == Int32.Parse(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            // try
            // {
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                command.model = newBook;

                // ilgili kitap bilgilerini aldıktan sonra validasyon yapmalıyız.
                CreateBookCommandValidator validator  = new CreateBookCommandValidator(); // validator nesnesini ve onun constructor'ında bulunan kuralları etkinleştirmiş olduk.
                //ValidationResult result = validator.Validate(command); // validator nesnesine dıışarıdan alıp maplediğimiz kitap nesnesini göndermiş olduk.
                // if(!result.IsValid)
                //     foreach (var item in result.Errors)
                //         Console.WriteLine("Özellik "+item.PropertyName+"- Errır Message: "+item.ErrorMessage); // eğer validasyondan geçemeyen kısımlar var ise onların hataları burada ekrana döner.
                // else
                //     command.Handle(); // eğer validasyonda hata almazsak nesneyi veritabanına geçiyoruz.
                //     return Ok();

                //Üstte yazdığımız kodlarda sistem validasyonun başarılı olmaması durumunda veri tabaanına kaydetmiyordu ancak bu hatayı kullanıcıya göstermiyordu.
                //Aşağıdaki kısımda hatayı kullanıcıya döndürecek kodu yazıyoruz.
                validator.ValidateAndThrow(command);
                command.Handle(); // validasyon başarılı olmazsa zaten handle metodu çalıştırılmayacak ancak başarılı olursa handle metodu çalışacak.
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook){
            // try
            // {
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
                command.model = updatedBook;
                command.id = id;
                validator.ValidateAndThrow(command);
                command.Handle();
            // }
            // catch (System.Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            // try
            // {
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                DeleteBookCommand command = new DeleteBookCommand(_context,_mapper);
                command.id = id;
                validator.ValidateAndThrow(command);
                command.Handle();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }

            return Ok();
        }
    }
}