using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace lab5
{
	[Serializable]
	public class Book
	{
		private String name;
		private List<String> autor;
		private List<String> genre;
		private int year;
		private String company;
		private int pages;
		private String annotation;
		private string picfullname;

		public Book()
		{
			this.name = "";
			this.autor = new List<String>();
			this.genre = new List<String>();
			this.year=DateTime.Now.Year;
			this.company="";
			this.pages=0;
			this.annotation="";
			this.picfullname = "";
		}

		public Book(String name, List<String> autor, List<String> genre, int year, String company, int pages, String annotation="", String picfilepath="")
	{
		this.name = name;
		this.autor = autor;
		this.genre = genre;
		this.year = year;
		this.company = company;
		this.pages = pages;
		this.annotation = annotation;
		this.picfullname = picfilepath;
	}

		[DisplayName("Название")]
		public String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		[DisplayName("Автор")]
		public String Autor
		{
			get
			{
				return String.Join(", ", this.autor);
			}
			set
			{
				string[] separators = { ", " };
				this.autor = value.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
			}
		}

		[DisplayName("Жанр")]
		public String Genre
		{
			get
			{
				return String.Join(", ", this.genre);
			}
			set
			{
				string[] separators = { ", " };
				this.genre = value.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
			}
		}

		[DisplayName("Год")]
		public int Year
		{
			get
			{
				return this.year;
			}
			set
			{
				if ((value >= 1564) && (value < 2050))
					this.year = value;
				else
					this.year = DateTime.Now.Year;
			}
		}

		[DisplayName("Издательство")]
		public String Company
		{
			get
			{
				return this.company;
			}
			set
			{
				this.company = value;
			}
		}

		[DisplayName("Кол-во страниц")]
		public int Pages
		{
			get
			{
				return this.pages;
			}
			set
			{
				if(value>0)
					this.pages = value;
				else
					this.pages = -value;
			}
		}

		[DisplayName("Аннотация")]
		public String Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		[DisplayName("Путь")]
		public String PicFullName
		{
			get
			{
				return this.picfullname;
			}
			set
			{
				this.picfullname = value;
			}
		}
	}
}
