using WebApi.DBOperations;
using WebApi.Entites;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author{
                    Name = "Jhon",
                    Surname = "Marston",
                    BornDate = new DateTime(1873,01,01)
                },
                new Author{
                    Name = "Arthur",
                    Surname = "Morgan",
                    BornDate = new DateTime(1863,01,01)
                },
                new Author{
                    Name = "Dutch",
                    Surname = "Van Der Linde",
                    BornDate = new DateTime(1858,01,01)
                }
            );
        }
    }
}