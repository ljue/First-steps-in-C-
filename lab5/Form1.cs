using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace lab5
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();

			books = new List<Book>();
			bs = new BindingSource();
			bs.DataSource = books;

			bs.Add(new Book("Война и мир", new List<String>() { "Л.Н.Толстой" }, new List<String>() { "роман" }, 1865, "Русский вестник", 1300,
				"Роман \"Война и мир\" классика мировой литературы Л. Н. Толстого, посвященный Отечественной войне 1812 года, является эпопеей славы русского народа, величественной летописью его подвигов, вместивший в себя огромное разнообразие человеческих жизней и человеческих чувств.",
				""));
			bs.Add(new Book("Преступление и наказание", new List<String>() { "Ф.М.Достоевский" }, new List<String>() { "роман" }, 1866, "Русский вестник", 336,
				"Родион Раскольников — стеснённый в средствах студент. Он ютится в крохотной комнате и размышляет о справедливости. Ради неё он осмысленно совершает преступление и получает наказание: страшась расплаты, испытывая муки совести, невольно стремясь к раскаянию. Главный герой окружён людьми: хорошими и плохими, великодушными и подлыми, несчастными в жизни и светлыми в душе. И лишь один из них — Порфирий Петрович — способен Раскольникова раскрыть. И лишь одна из них — Соня Мармеладова — призвать раскаяться.",
				""));

			grid.DataSource = bs;
			nav.BindingSource = bs;

			chart.DataSource = bs;
			chart.Series[0].XValueMember = "Name";
			chart.Series[0].YValueMembers = "Year";
			chart.Legends.Clear();
			chart.Titles.Add("Год издания");

			grid.Columns["PicFullName"].Visible = false;
			pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBox1.DataBindings.Add("ImageLocation", bs, "PicFullName", true);

			toolStripTextBox1.TextChanged += new EventHandler(toolStripTextBox1_TextChanged);

			propertyGrid1.DataBindings.Add("SelectedObject", bs, "");

			bs.CurrentChanged += (o, e) => chart.DataBind();

		}
		BindingSource bs;
		List<Book> books;

		private void toolStripButton1_Click(object sender, EventArgs e) // Открыть файл
		{
			var ffd = new OpenFileDialog();
			ffd.Filter = "Xml файлы (*.xml)|*.xml";
			ffd.InitialDirectory = Directory.GetCurrentDirectory();
			if (ffd.ShowDialog() == DialogResult.OK)
			{
				string ffdname = ffd.FileName;
				Stream sr = new FileStream(ffdname, FileMode.Open);
				XmlSerializer xmlSer = new XmlSerializer(bs.DataSource.GetType());
				bs.DataSource = xmlSer.Deserialize(sr);
				sr.Close();
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)  // Сохранить файл
		{
			var ffd = new SaveFileDialog();
			ffd.Filter = "Xml файлы (*.xml)|*.xml";
			ffd.InitialDirectory = Directory.GetCurrentDirectory();
			if (ffd.ShowDialog() == DialogResult.OK)
			{
				string ffdname = ffd.FileName;
				string ffdFormat = Path.GetExtension(ffdname);
				Stream sr = new FileStream(ffdname, FileMode.Create);
				XmlSerializer xmlSer = new XmlSerializer(bs.DataSource.GetType());
				string s = (bs.Current as Book).PicFullName;
				xmlSer.Serialize(sr, bs.DataSource);
				sr.Close();
			}

		}

		private void pictureBox1_DoubleClick(object sender, EventArgs e)  // Добавить картинку
		{
			var opf = new OpenFileDialog();
			opf.InitialDirectory = Directory.GetCurrentDirectory();
			if (opf.ShowDialog() == DialogResult.OK)
			{
				(bs.Current as Book).PicFullName = opf.FileName;
				//bs.ResetCurrentItem();
				bs.ResetBindings(false);
			}
		}

		private void toolStripTextBox1_TextChanged(object sender, EventArgs e) // Поиск
		{
			string strfind = toolStripTextBox1.Text;
			if (strfind=="")
			{ }
			else
			{
				grid.ClearSelection();
				var find = (bs.DataSource as List<Book>).Find(book => book.Name.StartsWith(strfind));
				if (find !=null)
				{
					int idx = (bs.DataSource as List<Book>).IndexOf(find);
					grid.Rows[idx].Selected = true;
				}
			}
		}
	}
}
