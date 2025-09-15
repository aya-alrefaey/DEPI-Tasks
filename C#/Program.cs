using Code_First.Models;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Code_First
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NewsContext db = new NewsContext();

            var authors = new List<Author>
            {
                new Author { Name = "Aya", Age = 21, UserName = "aya21", Password = "12345", JoinDate = DateTime.Now },
                new Author { Name = "Doha", Age = 25, UserName = "doha25", Password = "abcde", JoinDate = DateTime.Now},
                new Author { Name = "Sara", Age = 23, UserName = "sara23", Password = "sara123", JoinDate = DateTime.Now }
            };

            // Categories
            var categories = new List<Category>
            {
                new Category { Name = "Technology", Description = "Latest tech news" },
                new Category { Name = "Sports", Description = "All about sports" },
                new Category { Name = "Health", Description = "Health and lifestyle tips" }
            };

            // News
            var newsList = new List<News>
            {
                new News { Title = "AI Revolution", bref = "AI is changing the world", Description = "Detailed article about AI", Date = DateTime.Now, author = authors[0], category = categories[0] },
                new News { Title = "Football Finals", bref = "Exciting match coming", Description = "Match details and analysis", Date = DateTime.Now, author = authors[1], category = categories[1] },
                new News { Title = "Healthy Living", bref = "10 Tips for a healthy life", Description = "Article about lifestyle", Date = DateTime.Now, author = authors[2], category = categories[2] },
                new News { Title = "Cybersecurity", bref = "New threats emerging", Description = "How to protect your data", Date = DateTime.Now, author = authors[0], category = categories[0] },
                new News { Title = "Olympics 2025", bref = "Highlights from day 1", Description = "Medals and performance review", Date = DateTime.Now, author = authors[1], category = categories[1] }
            };

            if (!db.Author.Any())
            {
                db.Author.AddRange(authors);
                db.Category.AddRange(categories);
                db.News.AddRange(newsList);
                db.SaveChanges();
            }

            //-------------------------------------------------------------------------------------
            Console.WriteLine("Welcome To Our System");
            Console.WriteLine("Enter Your ID");
            int i = Convert.ToInt32(Console.ReadLine());
            int option = 100;
            Console.WriteLine("Enter Your Username");
            string user=Console.ReadLine();
            Console.WriteLine("Enter Your Password");
            string pass=Console.ReadLine();
            if (Login(db,i, user, pass))
            {
               
                while(option!=0)
                {
                    Console.WriteLine("Press 1 to view Your Profile\n2 to add a new news \n3 to display your News \n4 to edit specific news\n5 to delete specific news \n6 to Display all News \nPress 0 to Exit");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            ViewProfile(db, i);
                            break;
                        case 2:
                            Console.WriteLine("Enter News Title");
                            string tit=Console.ReadLine();
                            Console.WriteLine("Enter News Bref");
                            string b = Console.ReadLine();
                            Console.WriteLine("Enter News Description");
                            string desc = Console.ReadLine();
                            Console.WriteLine("All Categories :");
                            DisplayallCategories(db);
                            Console.WriteLine("Enter Category Id");
                            int cat = Convert.ToInt32(Console.ReadLine());
                            Addnew(db,i,tit,b,desc,cat);
                            break;
                        case 3:
                            Displaynews(db,i);
                            break;
                        case 4:
                            Console.WriteLine("Enter News Id");
                            int newid = Convert.ToInt32(Console.ReadLine());
                            EditNew(db,i,newid);
                            break;
                            case 5:
                            Console.WriteLine("Enter Id of news You want to delete");
                            int nid=Convert.ToInt32(Console.ReadLine());
                                DeleteNews(db,i,nid);
                            break;
                        case 6:
                            DisplayallNews(db);
                            break;
                        case 0:
                            Console.WriteLine("Thank You for Using Our System");
                            break;
                        default:
                            Console.WriteLine("Not Valid Option \n Thank Youuuu ");
                            return;
                    }
               
                }
            }
            else
            {
                Console.WriteLine("invalid User \n" +
                    "Thank You for using our system");
                return;
            }
           
        }

        public static bool Login (NewsContext db,int id,string user, string pass)
        {
            var person = db.Author.FirstOrDefault(a=> a.Id==id&&a.UserName == user && a.Password == pass);
            if (person != null)
            {
                Console.WriteLine($"Welcome {person.Name}, Login Successful!");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ViewProfile(NewsContext db, int id)
        {
          var user=db.Author.Include(a => a.news).ThenInclude(n => n.category).FirstOrDefault(a=>a.Id==id);
            Console.WriteLine($" id : {user.Id}");
            Console.WriteLine($" Name : {user.Name}");
            Console.WriteLine($" User Name: {user.UserName}");
            Console.WriteLine($" Age : {user.Age}");
            Console.WriteLine($" Join Dat : {user.JoinDate}");
            var c = 1;
            if (user.news != null && user.news.Count > 0) {
                foreach (var n in user.news)
                {
                    Console.WriteLine($" News number {c++}");
                    Console.WriteLine($" Id: {n.Id}");
                    Console.WriteLine($" Title: {n.Title}");
                    Console.WriteLine($" Description: {n.Description}");
                    Console.WriteLine($" Bref: {n.bref}");
                    Console.WriteLine($" Date: {n.Date}");
                    Console.WriteLine($"Category : {n.category.Name}");
                }
                
            }
            else { Console.WriteLine("No Published News "); }
           
        }
        public static void Addnew( NewsContext db,int id,string tit,string b,string des,int cid)
        {
            var newnew = new News { Title = tit, bref = b, Description = des, Date = DateTime.Now, authorId = id, Cat_Id = cid };
            db.News.Add(newnew);   
            db.SaveChanges();
            Console.WriteLine("News Added Successfully");
        }

        public static void DisplayallCategories(NewsContext db) {
            var cat = db.Category;
            foreach (var c in cat) {
                Console.WriteLine($"{c.Id}   {c.Name}")
                    ;
            }
        }

        public static void Displaynews(NewsContext db, int id)
        {
            var news = db.News.Include(a=> a.category).Where(a => a.authorId == id).ToList();
            var c = 1;
            if (news.Count >0)
            {
                foreach (var n in news)
                {
                    Console.WriteLine($" News number {c++}");
                    Console.WriteLine($" Id: {n.Id}");
                    Console.WriteLine($" Title: {n.Title}");
                    Console.WriteLine($" Description: {n.Description}");
                    Console.WriteLine($" Bref: {n.bref}");
                    Console.WriteLine($" Date: {n.Date}");
                    Console.WriteLine($"Category : {n.category.Name}");
                }
            }
            else
            {
                Console.WriteLine("No News for this Author");
            }

        }

        public static void EditNew(NewsContext db, int id,int newid)
        {
            var usernews = db.News.FirstOrDefault(n=>n.authorId==id &&n.Id==newid);
            if (usernews == null)
            {
                Console.WriteLine("News not found for this author.");
            }
            else
            {
                int o = 0;
                string s;
                Console.WriteLine("press 1 to edit title \n      2 to edit bref \n      3 to edit description \n      4 to change category");
                o=Convert.ToInt32(Console.ReadLine());
                switch (o)
                {
                    case 1:
                        Console.WriteLine("Enter New Title");
                        s=Console.ReadLine();
                        usernews.Title = s;
                        break;
                    case 2:
                        Console.WriteLine("Enter New Bref");
                        s = Console.ReadLine();
                        usernews.bref = s;
                        break;
                     case 3:
                        Console.WriteLine("Enter New Description");
                        s = Console.ReadLine();
                        usernews.Description = s;
                        break;
                     case 4:
                        Console.WriteLine("All Categories :");
                        DisplayallCategories(db);
                        Console.WriteLine("Enter New Category Id: ");
                        int newCat = Convert.ToInt32(Console.ReadLine());
                        usernews.Cat_Id = newCat;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        return;
                }
                db.SaveChanges();
                Console.WriteLine("---------- News updated successfully! ----------");

                Console.WriteLine("News After Update :");
                Console.WriteLine($"ID : {usernews.Id}");
                Console.WriteLine($"Title : {usernews.Title}");
                Console.WriteLine($"Bref : {usernews.bref}");
                Console.WriteLine($"Description : {usernews.Description}");
                Console.WriteLine($"Author Id : {usernews.authorId}");
                Console.WriteLine($"Category Id : {usernews.Cat_Id}");
            }
        }

        public static void DisplayallNews(NewsContext db)
        {
            var news = db.News.Include(a=> a.category).Include(a=>a.author);
            foreach (var n in news)
            {
                Console.WriteLine($" Id: {n.Id}");
                Console.WriteLine($" Title: {n.Title}");
                Console.WriteLine($" Description: {n.Description}");
                Console.WriteLine($" Bref: {n.bref}");
                Console.WriteLine($" Date: {n.Date}");
                Console.WriteLine($"Author : {n.author.Name}");
                Console.WriteLine($"Category : {n.category.Name}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        public static void DeleteNews(NewsContext db,int id,int nid)
        {
            var news = db.News.FirstOrDefault(a=>a.authorId==id && a.Id==nid);
            if (news != null) {
                db.News.Remove(news);   
                db.SaveChanges();       
                Console.WriteLine("News deleted successfully.");
            }
            else
            {
                Console.WriteLine("You Cannot delete this News");
            }
        }
    }
}
