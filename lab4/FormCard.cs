using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class FormCard : Form
    {
        public FormCard()
        {
             InitializeComponent();
        }

				public EnumTypeOfWord.TypeOfWord newtp = EnumTypeOfWord.TypeOfWord.None;
        public List<String> newtrnsl;
        public String trnsl;
        public String newid;
        


        public FormCard(Form1 f)
        {
            InitializeComponent();
            bOK = false;
						newtrnsl = new List<String>();
						//listBox1.Items.AddRange(newtrnsl.ToArray());
                       
        }

        public bool bOK;
        
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Выбор части речи
        {
            newtp = (EnumTypeOfWord.TypeOfWord)comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e) // Добавить перевод
        {
            newtrnsl.Add(trnsl);
						listBox1.Items.Add(trnsl);
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e) // Удалить перевод
        {
					int id = listBox1.SelectedIndex;
					newtrnsl.RemoveAt(id);
					listBox1.Items.RemoveAt(id);
             
        }

        private void textBox1_TextChanged(object sender, EventArgs e)  // Ввод слова
        {
            newid = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Ввод перевода
        {
            trnsl = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e) // OK сохранить
        {            
            bOK = true;
						if ((textBox1.Text != "") && (newtrnsl.Count != 0) && (newtp != EnumTypeOfWord.TypeOfWord.None))
							this.Close();
						else
							MessageBox.Show("Не все поля заполнены", "Предупреждение"); 
        }

				private void button4_Click(object sender, EventArgs e)
				{
					this.Close();
				}
             
    }
}
