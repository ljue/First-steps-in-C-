using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class BooksController : Controller
    {
		MyDbContext db;

		public BooksController()
		{
			db = new MyDbContext();
			db.Database.Log = (s) => Debug.WriteLine(s);
		}
		// GET: Books
		public ActionResult Index()
		{
			ViewBag.errorMsg = "";
			try
			{
				return View(db.Books.ToList());
			}
			catch (Exception ex)
			{
				ViewBag.errorMsg = Kit.SetErrorMsg(ex);
				return View("Error", (object)ViewBag.errorMsg);
			}
		}

		public ActionResult AddBook()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddBook([Bind(Include = "Id,Author,Title,Email,Date")] Book book)
		{
			if (ModelState.IsValid)
			{
				if (book.Date == null)
					book.Date = DateTime.Now;
				db.Books.Add(book);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(book);
		}

		public ActionResult EditBook(int? id)
		{
			Book book = db.Books.Find(id);
			if (book == null)
				return HttpNotFound();
			return View(book);
		}

		[HttpPost]
		public ActionResult EditBook([Bind(Include = "Id,Author,Title,Email,Date")] Book book)
		{
			if (ModelState.IsValid)
			{
				db.Entry(book).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(book);
		}

		public ActionResult DeleteBook(int? id)
		{
			Book book = db.Books.Find(id);
			if (book == null)
				return HttpNotFound();
			return View(book);
		}

		[HttpPost]
		public ActionResult DeleteBook([Bind(Include = "Id,Author,Title,Email,Date")] Book book)
		{
			db.Books.Remove(book);
			db.SaveChanges();
			return RedirectToAction("Index");			
		}

		public ActionResult ShowReview(int id)
		{
			var book = db.Books.Where(b=>b.Id==id).First();
			int count = book.Reviews.Count;
			int	rId = (int)(System.Web.HttpContext.Current.Session["rId"]??0);
			var review = book.Reviews[rId];
			var model = new ReviewVM
			{
				Id = rId,
				Count = count,
				Reviewer = review.Reviewer,
				Text = review.Text,
				BookId = id,
				Title = book.Title,
			};
			System.Web.HttpContext.Current.Session["rId"] = (rId + 1) % count;
			return View(model);
		}
	}
}