using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace LINQtoObject
{
    class Program
    {
        static void Main(string[] args)
        {
            //1-	Display book title and its ISBN.
            Console.WriteLine("* book title and its ISBN");
            var books = from b in SampleData.Books
                        select new { b.Title, b.Isbn };
            foreach (var book in books) {
                Console.WriteLine($"Title : {book.Title} , ISBN : {book.Isbn}");
            }
            Console.WriteLine("-------------------------------------------");
            //2-	Display the first 3 books with price more than 25.

            Console.WriteLine("* the first 3 books with price more than 25 : ");

            var more25 = (from b in SampleData.Books
                         where(b.Price>25)
                        select b).Take(3);
            foreach (var book in more25)
            {
                Console.WriteLine($"Title : {book.Title} , ISBN : {book.Isbn} , Price : {book.Price} , publisher : {book.Publisher}");
            }
            Console.WriteLine("-------------------------------------------");

            //3-	Display Book title along with its publisher name. (Using 2 methods).
            Console.WriteLine("* Display Book title along with its publisher name.");
            Console.WriteLine("---------- Method 1 ----------");
            var method1 = from b in SampleData.Books
                        select new { b.Title, pubname=b.Publisher.Name};
            foreach (var book in method1)
            {
                Console.WriteLine($"Title : {book.Title} , Publisher Name : {book.pubname}");
            }

            Console.WriteLine("---------- Method 2 ----------");
            var method2 = SampleData.Books.Select(b => new { b.Title, pubname = b.Publisher.Name });
            foreach (var book in method2)
            {
                Console.WriteLine($"Title : {book.Title} , Publisher Name : {book.pubname}");
            }
            Console.WriteLine("-------------------------------------------------------------------");

            //4-	Find the number of books which cost more than 20.
            Console.WriteLine("* number of books which cost more than 20.");

            Console.WriteLine($"no of books which cost more than 20 = {SampleData.Books.Where(b => b.Price > 20).Count()}");
            Console.WriteLine("------------------------------------------");

            //5-	Display book title, price and subject name sorted by its subject name ascending and by its price descending.
            Console.WriteLine("* book title, price and subject name sorted by its subject name ascending and by its price descending.");

            var query5= SampleData.Books.OrderBy(b => b.Subject.Name).ThenByDescending(b => b.Price).
                Select(b => new {b.Title,b.Price,subname=b.Subject.Name});
            foreach (var book in query5)
            {
                Console.WriteLine($"Title : {book.Title} , Price : {book.Price} , Subject Name = {book.subname}");
            }
            Console.WriteLine("--------------------------------------------------------------");

            //6-	Display All subjects with books related to this subject. (Using 2 methods).
            Console.WriteLine("Display All subjects with books related to this subject.");
            Console.WriteLine("---------- Method 1 ----------");
            var query61 = SampleData.Books.GroupBy(b =>b.Subject.Name);
            foreach (var book in query61)
            {
                Console.WriteLine($"Subject: {book.Key}");  

                foreach (var bo in book)
                {
                    Console.WriteLine($" Title : {bo.Title} , Price : {bo.Price}");
                }
            }
            Console.WriteLine("---------- Method 2 ----------");
            var query62 = from b in SampleData.Books
                          group b by b.Subject.Name;
            foreach (var book in query62)
            {
                Console.WriteLine($"Subject: {book.Key}");

                foreach (var bo in book)
                {
                    Console.WriteLine($" Title : {bo.Title} , Price : {bo.Price}");
                }
            }
            Console.WriteLine("-------------------------------------------------------------");

            //7-	Try to display book title & price (from book objects) returned from GetBooks Function.
            Console.WriteLine("* display book title & price returned from GetBooks Function.");
            Console.WriteLine("---------- Method 1 ----------");
            var query71 = SampleData.GetBooks().Cast<Book>().Select(b => new {b.Title,b.Price});

            foreach (var bo in query71)
            {
                Console.WriteLine($" Title : {bo.Title} , Price : {bo.Price}");
            }
            Console.WriteLine("---------- Method 2 ----------");
            var query72=from Book b in SampleData.GetBooks()
                        select new {b.Title,b.Price};
            foreach (var bo in query72)
            {
                Console.WriteLine($" Title : {bo.Title} , Price : {bo.Price}");
            }
            Console.WriteLine("----------------------------------------------");

            //8-	Display books grouped by publisher & Subject.
            Console.WriteLine("* books grouped by publisher & Subject.");
            var query81 = SampleData.Books.GroupBy(b => new { pubname = b.Publisher.Name, subname = b.Subject.Name });
            foreach ( var book in query81)
            {
                Console.WriteLine($"Publisher name : {book.Key.pubname}");
                Console.WriteLine($"Subject name : {book.Key.subname}");
                Console.WriteLine("Books : ");
                foreach( var bo in book)
                {
                    Console.WriteLine(bo.Title);
                }
                
            }
            Console.WriteLine("---------------------------------------------------");

            // Bonus
            Console.WriteLine("Enter Publisher Name");
            string s= Console.ReadLine();
            Console.WriteLine("Enter what do you want to sort by (Title, Price, Pages, Date):");
            string t= Console.ReadLine();
            Console.WriteLine("if you want to sort Ascending press 1 , descending press 2");
            int i = Convert.ToInt32(Console.ReadLine());
            var final = bonusmethod(s, t, i);

            if (!final.Any())
            {
                Console.WriteLine("No books found with the given info.");
            }
            else
            {
                foreach (var book in final)
                {
                    Console.WriteLine($"Title : {book.Title} , ISBN : {book.Isbn} , Price : {book.Price} ," +
                        $" Pages : {book.PageCount} , Date : {book.PublicationDate}");
                }
            }
               
        }

        public static IEnumerable<Book> bonusmethod(string s1,string s2,int j)
        {
            var ans=SampleData.Books.Where(b=>b.Publisher.Name.ToLower()==s1.ToLower());
            switch (s2.ToLower())
            {
                case "title":
                    ans = (j == 1) ? ans.OrderBy(b => b.Title)
                                  : ans.OrderByDescending(b => b.Title);
                    break;
                case "price":
                    ans = (j == 1) ? ans.OrderBy(b => b.Price)
                                  : ans.OrderByDescending(b => b.Price);
                    break;
                case "isbn":
                    ans = (j == 1) ? ans.OrderBy(b => b.Isbn)
                                  : ans.OrderByDescending(b => b.Isbn);
                    break;
                case "page":
                    ans = (j == 1) ? ans.OrderBy(b => b.PageCount)
                                   : ans.OrderByDescending(b => b.PageCount);
                    break;
                case "date":
                    ans = (j == 1) ? ans.OrderBy(b => b.PublicationDate)
                                   : ans.OrderByDescending(b => b.PublicationDate);
                    break;
                default:
                return Enumerable.Empty<Book>(); 
            }

            return ans;
        }
    }
}
