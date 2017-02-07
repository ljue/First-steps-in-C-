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
using System.Runtime.Serialization.Formatters.Binary;


namespace lab4
{
    public partial class Form1 : Form
    {
        public List<Card> bd;
				bool bdeleteCard = false;
        


        public Form1()
        {
            InitializeComponent();

            bd = new List<Card>();
						bd.Add(new Card( "apple",EnumTypeOfWord.TypeOfWord.Noun,new List<String>{"яблоко"}));
						listBox1.Items.Add(bd[0]._id);						
        }

        private void button1_Click(object sender, EventArgs e) // Добавление слова
        {
            FormCard formCard = new FormCard(this);
            formCard.ShowDialog();
            if (formCard.bOK)
            {
							Card nmc=new Card(formCard.newid, formCard.newtp, formCard.newtrnsl);
               bd.Add(nmc);
							 listBox1.Items.Add(nmc._id);								
            }            
        }

        private void button2_Click(object sender, EventArgs e)  // Удаление слова
        {
					bdeleteCard = true;

					int id = listBox1.SelectedIndex;
					bd.RemoveAt(id);
					listBox1.Items.RemoveAt(id);

					label4.Text = "";
					label3.Text = "";
					label2.Text = "";

					bdeleteCard = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) // Просмотр карточки
        {
					if (bdeleteCard == false)
					{
						int id = listBox1.SelectedIndex;
						label4.Text = bd[id]._id;
						label3.Text = EnumTypeOfWord.valueEnum(bd[id]._typeW);
						label2.Text = String.Join(",\n", bd[id]._translate);
					}

				}

				private void StatisticsToolStripMenuItem_Click(object sender, EventArgs e)
				{
					MessageBox.Show("Общее число карточек: " + bd.Count
						+ "\n\nЧисло существительных: " + (from t in bd where t._typeW.Equals(EnumTypeOfWord.TypeOfWord.Noun) select t).Count()
						+ "\nЧисло прилагательных: " + (from t in bd where t._typeW.Equals(EnumTypeOfWord.TypeOfWord.Adjective) select t).Count()
						+ "\n\nСредняя длина слов в словаре: " + (from t in bd select t._id.Length).Sum() / bd.Count, "Статистика");
				}


				private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e) //Открыть файл
				{
					var ffd = new OpenFileDialog();
					ffd.Filter = "Xml файлы (*.xml)|*.xml|Binary (*.bin)|*.bin";
					ffd.InitialDirectory = Directory.GetCurrentDirectory();
					if (ffd.ShowDialog() == DialogResult.OK)
					{
						string ffdname = ffd.FileName;
						string ffdFormat = Path.GetExtension(ffdname);
						if (ffdFormat.Equals(".xml"))
						{
							Stream sr = new FileStream(ffdname, FileMode.Open);
							XmlSerializer xmlSer = new XmlSerializer(typeof(List<Card>));
							bd = (List<Card>)xmlSer.Deserialize(sr);
							sr.Close();
							listBox1.Items.Clear();
							listBox1.Items.AddRange((from t in bd select t._id).ToArray());
						}
						else if (ffdFormat.Equals(".bin"))
						{
							BinaryFormatter fmt = new BinaryFormatter();
							FileStream str = new FileStream(ffdname, FileMode.Open);
							bd = (List<Card>)fmt.Deserialize(str);							
							str.Close();
							listBox1.Items.Clear();
							listBox1.Items.AddRange((from t in bd select t._id).ToArray());
						}
					}
				}

				private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e) // Сохранить файл
				{
					var ffd = new SaveFileDialog();
					ffd.Filter = "Xml файлы (*.xml)|*.xml|Binary (*.bin)|*.bin";
					ffd.InitialDirectory = Directory.GetCurrentDirectory();
					if (ffd.ShowDialog() == DialogResult.OK)
					{
						string ffdname = ffd.FileName;
						string ffdFormat = Path.GetExtension(ffdname);
						if (ffdFormat.Equals(".xml"))
						{
							Stream sr = new FileStream(ffdname, FileMode.Create);
							XmlSerializer xmlSer = new XmlSerializer(typeof(List<Card>));
							xmlSer.Serialize(sr, bd);
							sr.Close();
						}
						else if (ffdFormat.Equals(".bin"))
						{
							BinaryFormatter fmt = new BinaryFormatter();
							FileStream str = new FileStream(ffdname, FileMode.Create);
							fmt.Serialize(str, bd);
							str.Close();							
						}
					}
					
				}

				private void TestToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (bd.Count > 0)
					{
						TestForm testform = new TestForm(this);
						testform.ShowDialog();
					}
					else
						MessageBox.Show("Словарь пуст","Предупреждение");
				}
    }
}



//listBox1.DataSource = bd;
//listBox1.DisplayMember = "_id";
//listBox1.ValueMember = "_id";


//// получаем id выделенного объекта
//String id = listBox1.SelectedValue.ToString();

//// получаем весь выделенный объект
//Card selectedCard = (Card)listBox1.SelectedItem;
//label4.Text = selectedCard._id;
//label3.Text = EnumTypeOfWord.valueEnum(selectedCard._typeW);
//label2.Text = String.Join(",\n", selectedCard._translate);