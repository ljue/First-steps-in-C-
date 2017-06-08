using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
	//public class MyModel
	//{

	//}

	public class Reader
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Pass { get; set; }
		public DateTime? Date { get; set; }
	}
	public class Book
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Author name is required")]//[StringLength(31, ErrorMessage = "Field can not belonger than 31 characters")]		
		public string Author { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public string Title { get; set; }
		public DateTime? Date { get; set; }
		public virtual List<Review> Reviews { get; set; }
	}
	public class Review
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string Reviewer { get; set; }
		public string Text { get; set; }
	}

	public class ReviewVM
	{
		public int Id { get; set; }
		public int Count { get; set; }
		public string Reviewer { get; set; }
		public string Text { get; set; }
		public int BookId { get; set; }
		public string Title { get; set; }
	}

	internal class MyDbContext : DbContext
	{
		public DbSet<Reader> Readers { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public MyDbContext() : base() { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) // ограничения на некоторые поля данных
		{
			modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(63));
			var book = modelBuilder.Entity<Book>();
			book.Property(x => x.Title).HasMaxLength(127);
			book.Property(x => x.Date).IsOptional();
			var rev = modelBuilder.Entity<Review>();
			rev.Property(x => x.Text).HasMaxLength(2047);
			base.OnModelCreating(modelBuilder);
		}

	}


	internal class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext> // убьет то что было насильно при инициализации
	{
		protected override void Seed(MyDbContext db)
		{
			db.Books.AddRange(new List<Book>()
			{
				new Book(){ Title = "Entity Framework 6 Recipes", Author = "Driscoll B, Gupta N",
					Date = new DateTime(2013,1,1), Email="a@avalon.ru" },
				new Book(){ Title = "Data Structures and Algorithms with JavaScript", Author = "McMillan M",
					Date = new DateTime(2014,1,1), Email="a@avalon.ru" },
				new Book(){ Title = "Entity Framework Step by Step", Author = "John Paul Mueller",
					Date = new DateTime(2013,1,1), Email="a@avalon.ru" },
				new Book(){ Title = "Programming JavaScript Applications", Author = "Eric Elliott",
					Date = new DateTime(2014,1,1), Email="a@avalon.ru" },
				new Book(){ Title = "Secrets Of JavaScript Ninja", Author = "John Resig",
					Date = new DateTime(2013,1,1), Email="a@avalon.ru" },
			});
			db.SaveChanges();
			db.Reviews.AddRange(new List<Review>()
			{
				new Review(){ BookId = 1, Reviewer = "Byte", Text = "The book teaches the core concepts of Entity Framework through a broad range of clear and concise solutions to everyday data access tasks. Entity Framework 6 Recipes is for anyone learning Microsoft’s Entity Framework—Microsoft’s primary data access platform in the .NET Framework. If you have ever struggled to learn a new technology, programming model, or way of doing something, you know how helpful simple and real-world examples can be. For the beginning developer, this book provides concrete examples for common data access tasks. For developers having experience with previous Microsoft data access platforms, this book provides a task-by-task mapping between previous approaches and the patterns used in Entity Framework." },
				new Review(){ BookId = 1, Reviewer = "IT Everywhere", Text = "This book provides an exhaustive collection of ready-to-use code solutions for Entity Framework, Microsoft's model-centric. Zeeshan Hirani actively uses Entity framework in the development of an e-commerce web site for a top-300 ecommerce retailer. He has written several articles and maintains and active and influential Entity framework blog at http://weblogs.asp.net/zeeshanhirani." },
				new Review(){ BookId = 1, Reviewer = "Tim Anderson's ITWriting", Text = "I like ASP.NET MVC, Microsoft’s modern web application framework, it seems to be badly documented. Even the word “badly” is not quite right; there is lots of documentation, some of high quality, but finding your way around it is challenging, thanks to the many different pieces involved. When I completed an ASP.NET MVC project recently, I found it frustrating thanks to over-reliance on sample projects (hey, here is a an application we did that works, see if you can figure out how we did it), many out of date articles relating to old versions; and the opposite, posts and samples which include preview software that does not seem wise to use in production." },
				new Review(){ BookId = 2, Reviewer = "Silicon Valley", Text = "As an experienced JavaScript developer moving to server-side programming, you need to implement classic data structures and algorithms associated with conventional object-oriented languages like C# and Java. This practical guide shows you how to work hands-on with a variety of storage mechanisms—including linked lists, stacks, queues, and graphs—within the constraints of the JavaScript environment. Determine which data structures and algorithms are most appropriate for the problems you’re trying to solve, and understand the tradeoffs when using them in a JavaScript program. An overview of the JavaScript features used throughout the book is also included." },
				new Review(){ BookId = 2, Reviewer = "Byte", Text = "Learn how to use the most used data structures such as array, stack, list, tree, and graphs with real-world examples. If you’re using JavaScript on the server-side, you need to implement classic data structures that conventional object-oriented programs (such as C# and Java) provide. This practical book shows you how to use linked lists, stacks, queues, and graphs, as well as classic algorithms for sorting and searching data in your JavaScript programs." },
				new Review(){ BookId = 3, Reviewer = "Computer Science", Text = "Expand your expertise—and teach yourself the fundamentals of the Microsoft ADO.NET Entity Framework 5. Gaining access to data in a managed way without a lot of coding—that’s a tall order! The Entity Framework fulfills this promise and far more. Each version of the Entity Framework is more capable than the last. The latest version, Entity Framework version 5, provides you with access to far more database features with less work than ever before, and Microsoft ADO.NET Entity Framework Step by Step is your gateway to finding just how to use these phenomenal new features. In this book, you get hands-on practice with all the latest functionality that the Entity Framework provides. By the time you finish, you’ll be ready to tackle some of the most difficult database management tasks without the heavy-duty coding that past efforts required." },
				new Review(){ BookId = 3, Reviewer = "App Developers", Text = "John Mueller is a freelance author and technical editor. He has writing in his blood, having produced 90 books and over 300 articles to date. The book concentrates heavily on a combination of Windows Forms and the Model First modeling method, with only brief mentions of the alternative Database First and Code First options. If you’re not familiar with Entity Framework, what you need to know is that you have a choice of three ways to model your data. If you’ve got an existing database that you’re wanting to work with, then you would usually generate the ADO.NET Entity Data Model from the database – Database First. If you want to start from Entity Framework and create the model there, you choose Model First, create the entities, relationships and inheritance hierarchies within Entity Framework, then get EF to generate the Data Definition Language (DDL) to execute in your database server to create the database." },
				new Review(){ BookId = 4, Reviewer = "IT News", Text = "This book is JavaScript evangelist Eric Elliott’s introduction to serious web application development with JS. Take your existing JavaScript skills to the next level and learn how to build complete web scale or enterprise applications that are easy to extend and maintain. By applying the design patterns outlined in this book, you’ll learn how to write flexible and resilient code that’s easier—not harder—to work with as your code base grows." },
				new Review(){ BookId = 4, Reviewer = "Digital Universe", Text = "Take advantage of JavaScript’s power to build robust web-scale or enterprise applications that are easy to extend and maintain. The book is a rapid fire survey of the latest tricks and best practices rather than a detailed tutorial. But depending on the audience, sometimes that's exactly what the doctor ordered. The content of the early release edition is good - Elliott really does lay down some excellent best practices and design patterns and provides a good review of the technical abilities of the language; but, the real take-away for me is to stop buying early release editions. Honestly, I don't think it's even a good idea for O'Reilly to be selling them. It'd be one thing if all the content was done (and simply unedited); but, these books are incomplete and can result in an unfair assessment of the material." },
				new Review(){ BookId = 5, Reviewer = "Byte", Text = "This book takes you on a journey towards mastering modern JavaScript development in three phases: design, construction, and maintenance. For someone who has written a lot of Javascript code, there will be chapters you can skip or skim. I’ve seen some of the material before – having read library documentation, I’ve read numerous discussions of OOP in Javascript, but I found the chapter on Timers invaluable. The authors are big proponents of regular expressions, which I’ve found to be atypical in most code I’ve seen, and thus presents an interesting perspective." },
			});
			db.SaveChanges();
		}
	}

}