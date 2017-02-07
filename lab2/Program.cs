using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{

    class Matrix
    {
        private double[,] data;
        private int rows;
        private int columns;

        #region Конструкторы
        
        public Matrix(int nRows, int nCols){
            if (nRows > 0 && nCols > 0)
            {
                rows = nRows;
                columns = nCols;
                data = new double[nRows, nCols];
                Console.WriteLine("matrix created ({0} x {1})..",rows,columns);
            }
        }
        public Matrix(double [,] initData){
            rows = initData.GetLength(0);
            columns = initData.Length / rows;
            data = new double[rows, columns];
            data = (double[,])initData.Clone();
            Console.WriteLine("matrix created ({0} x {1})..", rows, columns);

            //data = initData; 
            // data=Array.Copy(initData);
        }

        #endregion

        #region Свойства
        
        public double this[int i, int j]
        {
            get { return data[i, j]; }
            set { data[i, j] = value; }
        }

        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }
        public int? Size // размер квадратной матрицы
        {
            get
            {
                if (rows == columns) return rows;
                else
                {
                    System.Console.WriteLine("Not a square matrix");
                    return null;
                }
            }
        }
        public bool IsSquared // является ли матрица квадратной
        {
            get { return (rows == columns) ? true : false; }
        }
        // является ли матрица нулевой
       // private double[] datar;
        // public bool IsEmpty { get { return Array.TrueForAll(datar, value => { return value == 0; }); } } // работает только для одномерных массивов
        public bool IsEmpty { 
            get {
                for(int i=0;i<rows;i++)
                    for(int j=0;j<columns;j++)
                        if (data[i, j] != 0)
                        {
                            return false;
                        }
                return true;
                }
        }
        // является ли матрица единичной
         public bool IsUnity
        {
            get
            {
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++)
                        if (i == j)
                        {
                            if (data[i, j] != 1)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (data[i, j] != 0)
                            {
                                return false;
                            }
                        }

                return true;
            }
        }
         // является ли матрица диагональной
         public bool IsDiagonal
         {
             get
             {
                 if (rows!=columns)
                 {
                     return false;
                 }
                 for (int i = 0; i < rows; i++)
                     for (int j = 0; j < columns; j++)
                         if (i != j)
                         {
                             if (data[i, j] != 0)
                             {
                                 return false;
                             }
                         }
                         else
                             if (data[i, j] == 0)
                             {
                                 return false;
                             }
                 return true;              
             }
         }

         // является ли матрица симметричной
         public bool IsSymmetric
         {
             get
             {
                 if (rows != columns)
                 {
                     return false;
                 }
                 for (int i = 0; i < rows; i++)
                     for (int j = 0; j < rows-i; j++)
                         if (data[i,j]!=data[j,i])
                         { 
                             return false;
                         }                        
                 return true;
             }
         }
        #endregion

        #region Операторы

         public static Matrix operator +(Matrix m1, Matrix m2)
         {
             if (m1.rows != m2.rows || m1.columns != m2.columns)
                 throw new System.ArgumentException("Нельзя складывать матрицы разных размерностей");
             Matrix m=new Matrix(m1.rows,m1.columns);
             for (int i = 0; i < m1.rows; i++)
                 for (int j = 0; j < m1.columns; j++)
                     m.data[i, j] = m1.data[i, j] + m2.data[i, j];
             Console.WriteLine("matrix calculation completed..");
             return m;
         }

         public static Matrix operator -(Matrix m1, Matrix m2)
         {
             if (m1.rows != m2.rows || m1.columns != m2.columns)
                 throw new System.ArgumentException("Нельзя вычитать матрицы разных размерностей");
             Matrix m = new Matrix(m1.rows, m1.columns);
             for (int i = 0; i < m1.rows; i++)
                 for (int j = 0; j < m1.columns; j++)
                     m.data[i, j] = m1.data[i, j] - m2.data[i, j];
             Console.WriteLine("matrix subtraction completed..");
             return m;
         }

         public static Matrix operator *(Matrix m1, double d)
         {
             Matrix m = new Matrix(m1.rows, m1.columns);
             for (int i = 0; i < m1.rows; i++)
                 for (int j = 0; j < m1.columns; j++)
                     m.data[i, j] = d*m1.data[i, j];
             Console.WriteLine("multiplication completed..");
             return m;
         }

         public static Matrix operator *(Matrix m1, Matrix m2)
         {
             if (m1.columns != m2.rows)
                 throw new System.ArgumentException("Умножать матрицы можно только если число столбцов первой равно числу строк второй");
             Matrix m = new Matrix(m1.rows, m2.columns);
             for (int i = 0; i < m1.rows; i++)
                 for (int j = 0; j < m2.columns; j++)
                 {
                     m.data[i, j] = 0;
                     for (int k = 0; k < m1.columns; k++)
                         m.data[i, j] += m1.data[i,k] * m2.data[k, j];
                 }
             Console.WriteLine("matrix multiplication completed..");
             return m;
         }

         public static explicit operator Matrix(double[,] arr)
         {
             Matrix m = new Matrix(arr.GetLength(0), arr.Length / arr.GetLength(0));
             m.data = (double[,])arr.Clone();
             Console.WriteLine("type cast completed..");
             return m;
         }
        #endregion

        #region Методы

         public  Matrix Transpose()
         {
             Matrix m = new Matrix(this.columns, this.rows);
             for (int i = 0; i < this.rows; i++)
                 for (int j = 0; j < this.columns; j++)
                 {
                     m.data[j, i] = this.data[i, j];
                 }
             Console.WriteLine("matrix transposed..");
             return m;
         }

         public double Trace()
         {
             double d = 0;
             for (int i = 0; i < this.rows; i++)
                     d += this.data[i, i];
             Console.WriteLine("calculation trace completed..");
             return d;
         }

         public override string ToString()
         {
             string s = this.rows + " rows " + this.columns + " columns \n";
             for (int i = 0; i < this.rows; i++)
             {
                 for (int j = 0; j < this.columns; j++)
                 {
                     s = s + " " + this.data[i, j];
                 }
                 s += "\n";
             }
             Console.WriteLine("cast matrix to string completed..");
             return s;
         }
         
        #endregion

        #region Статические методы

        // единичная матрица размера Size
         public static Matrix GetUnity(int Size)
         {
             Matrix m = new Matrix(Size, Size);
             for (int i = 0; i < Size; i++)
                 for (int j = 0; j < Size; j++)
                     m.data[i, j] = (i==j)?1:0;
             return m;
         }
         // нулевая матрица размера Size
         public static Matrix GetEmpty(int Size)
         {
             Matrix m = new Matrix(Size, Size);
             for (int i = 0; i < Size; i++)
                 for (int j = 0; j < Size; j++)
                     m.data[i, j] = 0;
             return m;  
         }
        // создание матрицы по строчке
         public static Matrix Parse(string s) // на костылях, но зато ходит!
         {
             char[] ch1 = { ',' };
             char[] ch2 = { ' ' };
             string[] subs = s.Split(ch1, StringSplitOptions.RemoveEmptyEntries);
             int leng = subs[0].Split(ch2, StringSplitOptions.RemoveEmptyEntries).Length;
                 for (int i = 0; i < subs.Length; i++)
                 {
                     if (leng != subs[i].Split(ch2, StringSplitOptions.RemoveEmptyEntries).Length)
                         throw new System.FormatException("Нельзя привести строку к матрице");
                 }

                 string[][] mysubs = new string[leng][];
                 for (int i = 0; i < leng; i++)
                 {
                     mysubs[i] = subs[i].Split(ch2, StringSplitOptions.RemoveEmptyEntries);
                 }

                 double result;
                 double[,] dsubs=new double[leng, subs.Length]; 
             for (int i = 0; i < leng; i++)
                 for (int j = 0; j < subs.Length; j++)
                 {
                     if (Double.TryParse(mysubs[i][j], out result))
                     dsubs[i, j] = result;
                     else throw new System.FormatException("Не число");
                 }

             Matrix m = new Matrix(dsubs);
             return m;             
         }

         public static bool TryParse(string s, out Matrix m)
         {
             char[] ch1 = { ',' };
             char[] ch2 = { ' ' };
             string[] subs = s.Split(ch1, StringSplitOptions.RemoveEmptyEntries);
             int leng = subs[0].Split(ch2, StringSplitOptions.RemoveEmptyEntries).Length;

             for (int i = 0; i < subs.Length; i++)
             {
                 if (leng != subs[i].Split(ch2, StringSplitOptions.RemoveEmptyEntries).Length)
                 {
                     m= Matrix.GetEmpty(leng);
                     return false;
                 }
             }

             string[][] mysubs = new string[leng][];
             for (int i = 0; i < leng; i++)
             {
                 mysubs[i] = subs[i].Split(ch2, StringSplitOptions.RemoveEmptyEntries);
             }

             double result;
             double[,] dsubs = new double[leng, subs.Length];
             for (int i = 0; i < leng; i++)
                 for (int j = 0; j < subs.Length; j++)
                 {
                     if (Double.TryParse(mysubs[i][j], out result))
                         dsubs[i, j] = result;
                     else
                     {
                         m = Matrix.GetEmpty(leng);
                         return false;
                     }
                 }

             m =(Matrix)dsubs;
             return true;
         }

        #endregion
    }
    class Program
    {

        #region Функции Меню
        public static Matrix VvodMas()
        {
            Console.WriteLine("Введите размерность матрицы");
            Console.WriteLine("число строк:");
            int m = Int32.Parse(Console.ReadLine());
            Console.WriteLine("число столбцов:");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите значения");
            double[,] mas = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mas[i, j] = Double.Parse(Console.ReadLine());
                }
            }
            Matrix ma = new Matrix(mas);
            return ma;
        }

        public static Matrix VvodMas1(Matrix m1)
        {
            Console.WriteLine("Введите значения для матрицы размерностью {0} x {1}", m1.Rows, m1.Columns);
            double[,] mas = new double[m1.Rows, m1.Columns];
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    mas[i, j] = Double.Parse(Console.ReadLine());
                }
            }
            Matrix ma = new Matrix(mas);
            return ma;
        }

        public static Matrix VvodMas2(Matrix m1)
        {
            Console.WriteLine("Введите число столбцов:");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите значения для матрицы размерностью {0} x {1}", m1.Columns, n );
            double[,] mas = new double[m1.Columns, n];
            for (int i = 0; i < m1.Columns; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mas[i, j] = Double.Parse(Console.ReadLine());
                }
            }
            Matrix ma = new Matrix(mas);
            return ma;
        }

        public static void MatrixOperations(ref Matrix m1)
        {
           
                Console.WriteLine(@"Операции с матрицами
------------------------------------
1 - Сложение
2 - Вычитание
3 - Умножение матриц
4 - Умножение матрицы на число
0 - Выход
------------------------------------
");
                
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':
                        Console.WriteLine("Выбрано сложение");
                        m1 = m1 + VvodMas1(m1);
                        Console.WriteLine("Нажмите любой символ");
                        Console.ReadKey();
                        break;
                    case '2': 
                        Console.WriteLine("Выбрано вычитание");
                        m1 = m1 + VvodMas1(m1);
                        Console.WriteLine("Нажмите любой символ");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.WriteLine("Выбрано умножение"); 
                        m1 = m1 * VvodMas2(m1); 
                        Console.WriteLine("Нажмите любой символ");
                        Console.ReadKey();
                        break;
                    case '4': Console.WriteLine("Введите число, на которое будем умножать матрицу");
                        double mad = Int32.Parse(Console.ReadLine());
                        m1 = m1 * mad;
                        Console.WriteLine("Нажмите любой символ");
                        Console.ReadKey();
                        break;
                    case '0': return;
                    default: break;
                }
            
        }
        #endregion

        #region Главное меню
        public static void MainMenu()
        {
            Console.WriteLine(@"Работа с матрицами
------------------------------------
1 - Ввод матрицы
2 - Операции
3 - Вывод результатов
0 - Выход
------------------------------------
");
        }
        #endregion

        static void Main(string[] args)
        {

            double[,] B = { { 7, 8, 9 }, { 10, 11, 12 } };
            
            Matrix may = new Matrix(B);
            Console.WriteLine("Исходная матрица: \n {0}", may.ToString());
            while (true)
            {
                MainMenu();
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':  may = VvodMas(); break;
                    case '2':  MatrixOperations(ref may); break;
                    case '3': Console.WriteLine(may.ToString()); break;
                    case '0': return;
                    default: break;
                }
            }


            #region А здесь проверена работа всего, что только можно (раскомментить, запустить вместо менюшки)
            //double[,] A = { { 1, 4 }, { 2, 5 }, { 3, 6 } };
            //double[,] B = { { 7, 8, 9 }, { 10, 11, 12 } };
            //Matrix a1 = new Matrix(A);
            //Matrix b1 = new Matrix(B);
            //int n=2,w=3;
            
            //Console.WriteLine(a1.ToString());
            //Console.WriteLine(b1.ToString());
            //Console.WriteLine(b1.Rows);
            //Console.WriteLine(b1.Columns);
            
            //Matrix x1 = new Matrix(n, w);
            //Console.WriteLine(x1.ToString());

            //Matrix a2= Matrix.GetUnity(w);
            //Console.WriteLine(a2.IsUnity);
            //Console.WriteLine(a2.IsEmpty);
            //Console.WriteLine(a2.IsDiagonal);
            //Console.WriteLine(a2.IsSymmetric);

            //Matrix b2 = Matrix.GetEmpty(w);
            //Console.WriteLine(b2.IsUnity);
            //Console.WriteLine(b2.IsEmpty);

            //Matrix c1 = a2 + b2;
            //Console.WriteLine(c1.ToString());

            //Matrix c2 = c1*9;
            //Console.WriteLine(c2.ToString());

            //Matrix c3 = c2-a2*3;
            //Console.WriteLine(c3.ToString());

            //Matrix c4 = a1*b1;
            //Console.WriteLine(c4.ToString());
            //Console.WriteLine(c4.Trace());
            //Console.WriteLine(c4.Transpose().ToString());
            //Console.WriteLine(c4.IsSymmetric);
            //Console.WriteLine(c4.IsDiagonal);
            //Console.WriteLine(c4.IsSquared);
            //Console.WriteLine(c4.Size);

            //Matrix c5 = (Matrix)A;
            //Console.WriteLine(c5.ToString());

            //string s1 = "1 2 3 , 4 5 6 ,7 8 9";
            //Matrix c6 = Matrix.Parse(s1);
            //Console.WriteLine(s1);
            //Console.WriteLine(c6.ToString());

            //string s2 = "4 2 8 , 4 4 3 ,3 1 9";
            //bool v1 = Matrix.TryParse(s2,out c6);
            //Console.WriteLine(s2);
            //Console.WriteLine(c6.ToString());
            //Console.WriteLine(v1);

            //string s3 = "4 2 8, a 4 3 ,t 1 9y";
            //bool v2 = Matrix.TryParse(s3, out c6);
            //Console.WriteLine(s3);
            //Console.WriteLine(c6.ToString());
            //Console.WriteLine(v2);

            //string s4 = "4 2 8 9, a 4 3 ,t 1 9y";
            //bool v23 = Matrix.TryParse(s4, out c6);
            //Console.WriteLine(s3);
            //Console.WriteLine(c6.ToString());
            //Console.WriteLine(v23);


            //Console.ReadKey();
            #endregion
        }
    }
}
