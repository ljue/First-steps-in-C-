using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 f)
        {
            InitializeComponent();

            button1.Click += button1_Click;
            colorDialog1.FullOpen = true;        
            colorDialog1.Color = this.BackColor;
            trackBar1.Value = (int)(f.myPen.Width);
            trackBar2.Value = (int)(f.pointSize);
            SolidBrush newbrush = (SolidBrush)(f.myBrush);
            tempCol = newbrush.Color;
            
        }
        Color tempCol;
     
        void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            this.BackColor = colorDialog1.Color;
            tempCol = colorDialog1.Color;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton1 = (RadioButton)sender;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton2 = (RadioButton)sender;           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
       
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = String.Format("Толщина линии: {0}", trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = String.Format("Размер точки: {0}", trackBar2.Value);
        }

        public int newPointSize
        {
            get
            {
                return trackBar2.Value;
            }
        }

        public int newPenWidth
        {
            get
            {
                return trackBar1.Value;
            }
        }
        public Color newColor
        {
            get
            {
                return tempCol;
            }
        }

        public bool ChFig2
        {
            get
            {
                return radioButton2.Checked;
            }
        }
        public bool ChFig1
        {
            get
            {
                return radioButton1.Checked;
            }
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
      
    }
}
