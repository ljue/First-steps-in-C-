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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Рисовалка";
            KeyPreview = true;
            KeyDown += Form1_KeyDown;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;           
        }
        
        #region Переменные

        public List<Point> arPoints=new List<Point>(); // Лист точек
        int iPointToDrag;

        bool bAddEllipses = false; // Рисуем точки\Не рисуем точки
        bool bPicFigure = false; // Рисуем\Не рисуем фигуру
        bool bClear = false;
        bool bCharactFigure = false; // Тип движения фигуры (true - с сохранением)
        bool bAct = false; // Движется\Не движется
        bool bDrag = false; // Перемещение точки мышкой

        public int pointSize = 5; // Диаметр точки

        int vPointX=1; // Скорость движения
        int vPointY = 1;
        List<int[]> vPointArr= new List<int[]>();


        public Pen myPen = new Pen(Brushes.ForestGreen, 5);
        public Brush myBrush = Brushes.ForestGreen;
        enum eLineType { None, Curved, Filled, Polygone, Beizers };
        eLineType lineType;

        Timer moveTimer = new Timer();
        Random rand = new Random();

        #endregion

        #region Обработчик клавиш

        void Form1_KeyDown(object sender, KeyEventArgs e) // Обработчик клавиш
        {
            switch (e.KeyCode)
            {
                case (Keys.Space):
                    bAct = !bAct;
                    if (!bAct)
                        moveTimer.Stop();
                    else moveTimer.Start();
                    break;
                case (Keys.Escape):
                    ClearList();
                    break;
                case (Keys.Add):
                    if (vPointX>=0)
                        vPointX += 1;
                    else
                        vPointX -= 1;
                    if (vPointY <= 0)
                            vPointY -= 1;
                    else
                        vPointY += 1;
                    break;
                case (Keys.Subtract):
                   if (vPointX>=0)
                        vPointX -= 1;
                    else
                        vPointX += 1;
                    if (vPointY >= 0)
                            vPointY -= 1;
                    else
                        vPointY += 1;
                    break;
                default:
                    break;
            }
            Refresh();
            e.Handled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Down):
                    if (bAct == false)
                        newArrList(0, 2);
                    else
                    {
                        if (vPointY > 0)
                            vPointY += 1;
                        else vPointY = 2;
                    }
                    Refresh();
                    return true;
                case (Keys.Up):
                    if (bAct == false)
                        newArrList(0, -2);
                    else
                    {
                        if (vPointY < 0)
                            vPointY -= 1;
                        else vPointY = -2;
                    }
                    Refresh();
                    return true;
                case (Keys.Left):
                    if (bAct == false)
                        newArrList(-2, 0);
                    else
                    {
                        if (vPointX < 0)
                            vPointX -= 1;
                        else vPointX = -2;
                    }
                    Refresh();
                    return true;
                case (Keys.Right):
                    if (bAct == false)
                        newArrList(2, 0);
                    else
                    {
                        if (vPointX > 0)
                            vPointX += 1;
                        else vPointX = 2;
                    }
                    Refresh();
                    return true;

                default:
                    break;
            }
           
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Мышиные события

        void Form1_MouseClick(object sender, MouseEventArgs e) // Ставим точки кликами
        {
            if (bAddEllipses)
            {
                arPoints.Add(e.Location);
                Refresh();
            }
        }

        void Form1_MouseDown(object sender, MouseEventArgs e) // Хватаем точку
        {
            foreach(var p in arPoints)
            {
                if(Math.Sqrt(Math.Pow(e.Location.X - p.X, 2) + Math.Pow(e.Location.Y - p.Y, 2))<10)
                {
                    bDrag = true;
                    bAddEllipses = false;
                    iPointToDrag = arPoints.IndexOf(p);
                    return;
                }
            }
        }

        void Form1_MouseMove(object sender, MouseEventArgs e) // Перемещаем точку
        {
            if(bDrag)
            {
                arPoints[iPointToDrag] = e.Location; 
                Refresh();
            }
        }

        void Form1_MouseUp(object sender, MouseEventArgs e) // Отпускаем точку
        {
            bDrag = false;
            bAddEllipses = true;
        }

        #endregion

        #region Доп Функции
        private void ClearList() // Функция очистки
        {
            arPoints.Clear();       
            bClear = !bClear;
            bPicFigure = false;
            Refresh();            
        }

        private void newArrList(int i, int j) // Переписываем Лист
        {
            List<Point> tmp = new List<Point>();
            foreach (var p in arPoints)
            {
                if (p.X + i <= 0 )
                    i = this.Width - 2 * p.X + i;
                if (p.Y + j <= 0 )
                    j = this.Height - 2 * p.Y + j;
                if (p.X + i >= this.Width)
                    i = -2 * p.X + i + this.Width;
                if (p.Y + j >= this.Height)
                    j = -2 * p.Y + j + this.Height;
                tmp.Add(new Point(Math.Abs(p.X + i), Math.Abs(p.Y + j)));                
            }
            arPoints.Clear();
            foreach (var p in tmp)
                arPoints.Add(p);
        }

        private void newArrList(List<int[]> vList) // Переписываем Лист разнодвижущийся
        {
            List<Point> tmp = new List<Point>();
            foreach (var p in arPoints)
            {
                var i = vList[arPoints.IndexOf(p)][0];
                var j = vList[arPoints.IndexOf(p)][1];
                if (p.X + i <= 0)
                    i = this.Width - 2 * p.X + i;
                if (p.Y + j <= 0)
                    j = this.Height - 2 * p.Y + j;
                if (p.X + i >= this.Width)
                    i = -2 * p.X + i + this.Width;
                if (p.Y + j >= this.Height)
                    j = -2 * p.Y + j + this.Height;
                tmp.Add(new Point(Math.Abs(p.X + i), Math.Abs(p.Y + j)));
            }
            arPoints.Clear();
            foreach (var p in tmp)
                arPoints.Add(p);
        }

        //private void newArrListRand(int i, int j) // Переписываем Лист скачущий
        //{
        //    List<Point> tmp = new List<Point>();
        //    foreach (var p in arPoints)
        //    {
        //        var ir = rand.Next(0, 2);
        //        var jr = rand.Next(0, 2);
        //        i = (ir == 0) ? i : -i;
        //        j = (jr == 0) ? j : -j;
        //        i *= rand.Next(0, 5);
        //        j *= rand.Next(0, 5);

        //        if (p.X + i <= 0)
        //            i = this.Width - 2 * p.X + i;
        //        if (p.Y + j <= 0)
        //            j = this.Height - 2 * p.Y + j;
        //        if (p.X + i >= this.Width)
        //            i = -2 * p.X + i + this.Width;
        //        if (p.Y + j >= this.Height)
        //            j = -2 * p.Y + j + this.Height;
        //        tmp.Add(new Point(Math.Abs(p.X + i), Math.Abs(p.Y + j)));
        //    }
        //    arPoints.Clear();
        //    foreach (var p in tmp)
        //        arPoints.Add(p);
        //}
        #endregion

        #region Кнопки

        private void button1_Click(object sender, EventArgs e) // Выключен режим точек, выключено движение
        {
            if (bAct)
            {
                ClearList();
                bAct = false;
                moveTimer.Stop();
            }
            bAddEllipses = !bAddEllipses;
            if (bAddEllipses)
            {
                Text = "Рисуем точки";
            }
            else
            {
                Text = "";
            }                      
        }

        private void button2_Click(object sender, EventArgs e) // Параметры
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
            pointSize = form2.newPointSize;
            
            myPen = new Pen(form2.newColor, form2.newPenWidth);
            myBrush = new SolidBrush(form2.newColor);
            
            if (form2.ChFig2)
                bCharactFigure = true;
            else if (form2.ChFig1)
                bCharactFigure = false;
            
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e) // Движение
        {
            bAct = !bAct;
            bAddEllipses = false;
            moveTimer.Start();
            moveTimer.Interval = 30;

            
                List<int[]> vPointArr1 = new List<int[]>();
                foreach (var p in arPoints)
                {
                    int[] temp1 = { rand.Next(-5, 5), rand.Next(-5, 5) };
                    vPointArr1.Add(temp1);
                }
                vPointArr.Clear();
                foreach (var p1 in vPointArr1)
                    vPointArr.Add(p1);



            if (bAct)
            {
                moveTimer.Tick += new EventHandler(TimerTickHandler);
            }
            else
                moveTimer.Stop();
        }

        private void TimerTickHandler(object sender, EventArgs e) // Тики движения
        {
            if (bCharactFigure)
            {
                newArrList(vPointX, vPointY);               
            }
            else
            {
                newArrList(vPointArr);
            }
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e) // Очистить поле 
        {
            ClearList();
            Text = "Поле очищено";
        }

        private void button5_Click(object sender, EventArgs e) // Кривая
        {
            bPicFigure = true;
            lineType = eLineType.Curved;
            Refresh();
            Text = "Построена область";
            bAddEllipses = false;
        }

        private void button6_Click(object sender, EventArgs e) // Ломанная
        {
            bPicFigure = true;
            lineType = eLineType.Polygone;
            Refresh();
            Text = "Построен многоугольник";
        }

        private void button7_Click(object sender, EventArgs e) // Бейзеры
        {
            bPicFigure = true;
            lineType = eLineType.Beizers;
            Refresh();
        }

        private void button8_Click(object sender, EventArgs e) // Закрашенная кривая
        {
            bPicFigure = true;
            lineType = eLineType.Filled;
            Refresh();
            Text = "Построена закрашенная кривая";
        }

        #endregion

        #region Графика

        private void Form1_Paint(object sender, PaintEventArgs e) // Рисовалка
        {           
            Graphics g = e.Graphics;
            if (bClear)
            {
                g.Clear(this.BackColor);
                bClear = !bClear;
                bAddEllipses = false;
            }
            if (arPoints.Count>0)
            {
                foreach (var p in arPoints)// рисуем точки
                {
                    g.FillEllipse(myBrush, p.X - pointSize / 2, p.Y - pointSize / 2, pointSize, pointSize);
                }
                if (bPicFigure)
                {
                    if (lineType == eLineType.Curved)
                        g.DrawClosedCurve(myPen, arPoints.ToArray());
                    if (lineType == eLineType.Polygone)
                        g.DrawPolygon(myPen, arPoints.ToArray());
                    if (lineType == eLineType.Filled)
                        g.FillClosedCurve(myBrush, arPoints.ToArray());
                    if (lineType == eLineType.Beizers)
                    {
                        if (arPoints.Count % 3 == 1)
                        {
                            g.DrawBeziers(myPen, arPoints.ToArray());
                            Text = "Построены бейзеры";
                        }
                        else
                        {
                            Text = "Добавьте точку";
                        }
                    }
                }
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}

