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
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
		}

		public TestForm(Form1 f)
    {
        InitializeComponent();

				isum = 1;
				imistake = 0;

				List<Card> fcard = new List<Card>(f.bd.Count);
				f.bd.ForEach((item) => { fcard.Add(item); });

				testlist = new List<Card>();
				Random rand = new Random();
				while (fcard.Count > 0)
				{
					int i=rand.Next(0, fcard.Count - 1);
					testlist.Add(fcard[i]);
					fcard.RemoveAt(i);
				}
				label1.Text = testlist[0]._id;
    }

		int isum;
		int imistake;
		List<Card> testlist;
		
		private void textBox1_TextChanged(object sender, EventArgs e) // Ввод перевода на проверку
		{

		}

		private void button1_Click(object sender, EventArgs e) // Продолжить
		{
			
			if (testlist.Count-1 > 0)
			{
				if (testlist[0]._translate.Contains(textBox1.Text))
				{										
				}
				else  
				{
					bool bv = false;
					for (int i = 1; i < testlist.Count; i++)
					{
						if ((testlist[i]._id==testlist[0]._id)&&(testlist[i]._translate.Contains(textBox1.Text)))
						{
							bv = true;
							testlist.RemoveAt(i);
							testlist.Insert(i, testlist[0]);							
						}
					}
					if(bv==false)
					{
						imistake++;
					}
				}
			
				testlist.RemoveAt(0);
				isum++;
				textBox1.Clear();
				
				if (testlist.Count > 0)
				{
					label1.Text = testlist[0]._id;
				}
				
				
			}
			else
			{ 
				MessageBox.Show("Из " + isum + " проверенных слов\n" + imistake + " переведено неверно", "Результат");
				this.Close();
			}

		}

		private void button2_Click(object sender, EventArgs e) // Завершить
		{
			MessageBox.Show("Из " + isum + " проверенных слов\n" + imistake + " переведены неверно", "Результат");
			this.Close();
		}
	}
}
