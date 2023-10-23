namespace Schema.Dto
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }

    public class MovieRequestActors
    {
        public List<int> ActorActressId { get; set; }
    }


    public class MovieResponse
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }

        public virtual List<ActorActressResponse> ActorActress { get; set; }
    }
}
