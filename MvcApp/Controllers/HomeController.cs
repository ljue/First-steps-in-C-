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
	public class HomeController : Controller
	{
		//MyDbContext db = new MyDbContext();
		MyDbContext db;

		public HomeController()
		{
			db = new MyDbContext();
			db.Database.Log = (s) => Debug.WriteLine(s);
		}

		public ActionResult CreateDb()
		{
			Database.SetInitializer(new MyDbInitializer());
			var list = db.Books.ToList();
			return View();
		}

		public ActionResult DropDb()
		{
			db.Database.Delete();
			return View();
		}

		public ActionResult Index()
		{
			ViewData["Browser"] = "Your browser: " + Request.Browser.Browser;
			if (db.Database.Exists())
				ViewData["DB"] = true;
			return View();
		}

		public string Welcome(string name, int id = 20)
		{
			return "<span style='color:red;font-size:large;'>Welcome to ASP.NET MVC</span><br/>"
				+ HttpUtility.HtmlEncode("Hello " + name + ", Age: " + id);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				db.Dispose();
			base.Dispose(disposing);
		}
	}

}