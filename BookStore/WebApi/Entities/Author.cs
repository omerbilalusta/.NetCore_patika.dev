namespace WebApi.Entites
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname {get; set;}
        public DateTime BornDate { get; set; }
        public List<Book> Books {get; set;}
    }
}
