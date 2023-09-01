using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public AuthorsViewModel model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle(){
            var authorsList = _context.Authors.Include(x=> x.Books).OrderBy(x=> x.Id).ToList();
            List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(authorsList);
            return viewModel;
        }
    }
    public class AuthorsViewModel // ViewModel
    {
        public string Name { get; set; }
        public string Surname {get; set;}
        public DateTime BornDate { get; set; }
    }
}