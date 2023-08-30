using LinqPractices.DbOperations;

namespace LinqPractices
{
    class Program
    {
        static void Main(string[] args){
            DataGenerator.Initialize();
            LinqDbContext _context = new LinqDbContext();
            var students = _context.Students.ToList<Student>();

            //Find()
            Console.WriteLine("**** Find ****");
            var student = _context.Students.Where(x=> x.StudentId == 1).FirstOrDefault();
            student = _context.Students.Find(2);
            Console.WriteLine(student.Name);


            //FirstOrDefault()
            Console.WriteLine("\n**** FirstOrDefault ****");
            student = _context.Students.Where(x=> x.Surname == "Arda").FirstOrDefault();
            Console.WriteLine(student.Name);
            //üstteki ve alttaki örnek aynı sonucu getirir.
            student = _context.Students.FirstOrDefault(x=> x.Surname == "Arda");
            Console.WriteLine(student.Name);

            //First() fonksiyonuda aynı şeyi yapar ancak değer bulamazsa hata döndürür.
            //FirstOrDefault() herhangi bir veri bulamazsa null döndürür.

            //SingleOrDefault()
            Console.WriteLine("\n**** SingleOrDefault ****");
            student = _context.Students.SingleOrDefault(x=> x.Name == "Deniz"); // koşul ile eşleşen birden fazla sonuç olursa hata fırlatır.
            Console.WriteLine(student.Name);

            //ToList();
            Console.WriteLine("\n**** ToList() ****");
            var studentList = _context.Students.Where(x=> x.ClassId==2).ToList();
            Console.WriteLine(studentList.Count());

            //OrderBy()
            Console.WriteLine("\n**** OrderBy() ****");
            studentList = _context.Students.OrderBy(x=> x.StudentId).ToList();
            foreach (var item in studentList)
            {
                Console.WriteLine(item.StudentId + " - " + item.Name + " " + item.Surname);
            }


            //OrderByDescending()
            Console.WriteLine("\n**** OrderByDescending() ****");
            studentList = _context.Students.OrderByDescending(x=> x.StudentId).ToList();
            foreach (var item in studentList)
            {
                Console.WriteLine(item.StudentId + " - " + item.Name + " " + item.Surname);
            }


            //Anonymus Object Result
            Console.WriteLine("\n**** Anonymus Object Result ****");
            var anonymousObject = _context.Students
                                .Where(x=> x.ClassId == 2)
                                .Select(x=> new{
                                    Id = x.StudentId,
                                    FullName = x.Name + " " + x.Surname
                                });
            foreach (var item in anonymousObject)
            {
                Console.WriteLine(item.Id + " - " + item.FullName);
            }
        }
    }
}