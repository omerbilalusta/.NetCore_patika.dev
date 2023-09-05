using WebApi.DBOperations;
using WebApi.Entites;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context){ //extension metod olmuş oldu.
            context.Genres.AddRange(
                new Genre{
                    Name = "Personal Growth"
                },
                new Genre{
                    Name= "Science Fiction"
                },
                new Genre{
                    Name = "Romance"
                }
            );
        }
    }
}