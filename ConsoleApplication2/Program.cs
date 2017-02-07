using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApplication2
{
    struct MyStruct { public int x;}
    class Program
    {
        #region Меню метода выбора одного типа из списка
        public static void OneOfTypes()
        {
            Console.WriteLine(@"Выберите тип из списка:

1 - uint
2 - int
3 - long
4 - float
5 - double
6 - char
7 - string
8 - class Program
9 - MyStruct
0 - Выход в главное меню
");
            while (true)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': //Type t1 = typeof(uint);
                        MyType(typeof(uint)); break;
                    case '2': Type t2 = typeof(int);
                        MyType(t2); break;
                    case '3': Type t3 = typeof(long);
                        MyType(t3); break;
                    case '4': Type t4 = typeof(float);
                        MyType(t4); break;
                    case '5': Type t5 = typeof(double);
                        MyType(t5); break;
                    case '6': Type t6 = typeof(char);
                        MyType(t6); break;
                    case '7': Type t7 = typeof(string);
                        MyType(t7); break;
                    case '8': Type t8 = typeof(Program);
                        MyType(t8); break;
                    case '9': Type t9 = typeof(MyStruct);
                        MyType(t9); break;
                    case '0': MainMenu(); return;
                    default: break;
                }
            }
        }
        #endregion

        #region Инфо о типе 
        public static void MyType(Type t)
        {
            Console.WriteLine("Информация по типу: {0}\n", t.FullName);
            Console.WriteLine("Значимый тип: {0}", t.IsValueType);
            Console.WriteLine("Пространство имен: {0}", t.Namespace);
            Console.WriteLine("Сборка: {0,-30}", t.Assembly.GetName().Name);
            Console.WriteLine("Общее число элементов: {0,-30}", t.GetMembers().Length);
            Console.WriteLine("Число методтов: {0,-30}", t.GetMethods().Length);
            Console.WriteLine("Число свойств: {0,-30}", t.GetProperties().Length);
            Console.WriteLine("Число полей: {0,-30}", t.GetFields().Length);
            
            int nField=t.GetFields().Length;
            string[] fieldNames = new string[nField];
            for (int i = 0; i < nField; i++)
            {
                fieldNames[i] = t.GetFields()[i].Name;
            }
            Console.WriteLine("Список полей: {0,-30}", String.Join(", ",fieldNames));
            
            int nProperties = t.GetProperties().Length;
            string[] PropertiesNames = new string[nProperties];
            for (int i = 0; i < nProperties; i++)
            {
                PropertiesNames[i] = t.GetProperties()[i].Name;
            }
            Console.WriteLine("Список свойств: {0,-30}\n", String.Join(", ", PropertiesNames));

            Console.WriteLine(@"Нажмите 'М' для вывода дополнительной информации по методам:
Дважды нажмите '0' для выхода в главное меню
");
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case 'm': TypeOfMethod(t); break;
                    case '0':  return;
                    default: break;
                }
            }
        }
        #endregion

        #region Дополнительная информация по методам
        public static void TypeOfMethod(Type t)
        {
            //int nMethods = t.GetMethods().Length;
            //string[] MethodsNames = new string[nMethods];
            //for (int i = 0; i < nMethods; i++)
            //{
            //    MethodsNames[i] = t.GetMethods()[i].Name;
            //}
            //Console.WriteLine("Список свойств: {0,-30}\n", String.Join(", ", MethodsNames));

            Console.WriteLine("Методы типа: {0}", t); 
            Console.WriteLine("Название       Число перегрузок      Число параметров");
            var methodGroup = from method in t.GetMethods()
                              group method by method.Name
                                  into g
                                  select new { Name = g.Key, Count = g.Count() };
            

            foreach (var grup in methodGroup)
            {
                var parameters = from method in t.GetMethods()
                                 where method.Name==grup.Name
                                      select new { Count = method.GetParameters().Length };
                List<string> list = new List<string>();
                foreach (var param in parameters)
                {
                    list.Add(param.Count.ToString());
                }
                string str = string.Join(", ", list);
                Console.WriteLine("{0,-20}  {1,-20}  {2}", grup.Name, grup.Count, str);
            }

            Console.WriteLine("Дважды нажмите 0 для выхода в главное меню");
        }
        #endregion

        #region Общая информация по типам
        public static void InfoAboutTypes()
        {
            Assembly myAsm = Assembly.GetExecutingAssembly();
            Type[] thisAssemblyTypes = myAsm.GetTypes();
            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            int nSborka = 0;
            foreach (Assembly asm in refAssemblies)
            {
                types.AddRange(asm.GetTypes());
                nSborka++;
            }
            int nRefTypes = 0, nValueTypes = 0, nInterfaceType = 0, maxnMethods=0;
            Type tMaxMet=typeof(int);
            string maxMetName = "", maxAtrName="";
            int nMaxName = 0;
            int nMaxAtribut = 0;
            foreach (var t in types)
            {
                if (t.IsClass)
                    nRefTypes++;
                else if (t.IsValueType)
                    nValueTypes++;
                else if (t.IsInterface)
                    nInterfaceType++;

                if( maxnMethods < t.GetMethods().Length)
                { maxnMethods = t.GetMethods().Length;
                tMaxMet = t;
                }

                string[] MethodsNames = new string[t.GetMethods().Length];
                for (int i = 0; i < t.GetMethods().Length; i++)
                {
                    MethodsNames[i] = t.GetMethods()[i].Name;
                    if (nMaxName < t.GetMethods()[i].Name.Length)
                    {
                        nMaxName = t.GetMethods()[i].Name.Length;
                        maxMetName = t.GetMethods()[i].Name;
                    }
                    if (nMaxAtribut < t.GetMethods()[i].GetParameters().Length)
                    {
                        nMaxAtribut = t.GetMethods()[i].GetParameters().Length;
                        maxAtrName = t.GetMethods()[i].Name;
                    }
                }


            }


            #region Вывод на экран
            Console.WriteLine("Общая информация по типам\n");
            Console.WriteLine("Подключенные сборки: {0}", nSborka);
            Console.WriteLine("Ссылочные типы: {0}", nRefTypes);
            Console.WriteLine("Значимые типы: {0}", nValueTypes);
            Console.WriteLine("Типы-интерфейсы: {0}", nInterfaceType);
            Console.WriteLine("Тип с максимальным числом методов: {0}", tMaxMet.FullName);
            Console.WriteLine("Самое длинное название метода: {0}", maxMetName);
            Console.WriteLine("Метод с наибольшим числом аргументов: {0} {1}шт", maxAtrName, nMaxAtribut);


            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в главное меню \n");
            Console.ReadKey();
            Console.WriteLine();
            #endregion
            MainMenu();
        }
        #endregion

        #region Ввод имени типа
        public static void SetTypeName()
        {
            Console.WriteLine("Информацию о каком типе Вы хотите узнать?");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "byte": MyType(typeof(byte)); break;
                    case "sbyte": MyType(typeof(sbyte)); break;
                    case "short": MyType(typeof(short)); break;
                    case "int": MyType(typeof(int)); break;
                    case "long": MyType(typeof(long)); break;
                    case "ushort": MyType(typeof(ushort)); break;
                    case "uint": MyType(typeof(uint)); break;
                    case "ulong": MyType(typeof(ulong)); break;
                    case "float": MyType(typeof(float)); break;
                    case "double": MyType(typeof(double)); break;
                    case "bool": MyType(typeof(bool)); break;
                    case "char": MyType(typeof(char)); break;
                    case "decimal": MyType(typeof(decimal)); break;
                    case "IntPtr": MyType(typeof(IntPtr)); break;
                    case "UIntPtr": MyType(typeof(UIntPtr)); break;

                    case "Byte": MyType(typeof(byte)); break;
                    case "Sbyte": MyType(typeof(sbyte)); break;
                    case "Int16": MyType(typeof(short)); break;
                    case "Int32": MyType(typeof(int)); break;
                    case "Int64": MyType(typeof(long)); break;
                    case "UInt16": MyType(typeof(ushort)); break;
                    case "UInt32": MyType(typeof(uint)); break;
                    case "UInt64": MyType(typeof(ulong)); break;
                    case "single": MyType(typeof(float)); break;
                    case "Double": MyType(typeof(double)); break;
                    case "Boolean": MyType(typeof(bool)); break;
                    case "Char": MyType(typeof(char)); break;
                    case "Decimal": MyType(typeof(decimal)); break;

                    case "q": Console.WriteLine("byte, sbyte, short, int, long, \nushort, uint, ulong, float, double, bool, char, \ndecimal, IntPtr, UIntPtr\n"); break;
                    case "0": MainMenu(); return;
                    default: Console.WriteLine("Типа с таким именем не существует.");
                        Console.WriteLine("Для выхода в главное меню введите '0'");
                        Console.WriteLine("Для вывода списка существующих типов введите 'q'\n");
                        break;
                }
            }
        }
        #endregion

        #region Параметры консоли
        public static void ParametresOfConsole()
        {
            Console.WriteLine("Параметры консоли: \n");
            Console.WriteLine("Заголовок: {0}", Console.Title);
            Console.WriteLine("Высота окна: {0}", Console.WindowHeight);
            Console.WriteLine("Ширина окна: {0}", Console.WindowWidth);
            Console.WriteLine("Отступ слева: {0}", Console.WindowLeft);
            Console.WriteLine("Отступ сверху: {0}", Console.WindowTop);
            Console.WriteLine("Ctrl+C как ввод: {0}", Console.TreatControlCAsInput);
            Console.WriteLine("Цвет фона: {0}", Console.BackgroundColor);
            Console.WriteLine("Цвет текста: {0}", Console.ForegroundColor);
            Console.WriteLine("Высота окна буфера: {0}", Console.BufferHeight);
            Console.WriteLine("Ширина окна буфера: {0}", Console.BufferWidth);
            Console.WriteLine("Max число строк: {0}", Console.LargestWindowHeight);
            Console.WriteLine("Max число столбцов: {0}", Console.LargestWindowWidth);
            Console.WriteLine("Кодировка: {0}\n", Console.InputEncoding);

            MainMenu();
        }
        #endregion

        #region Главное меню
        public static void MainMenu()
        {
            Console.WriteLine(@"Информация по типам:
1 - Общая информация по типам
2 - Выбрать из списка
3 - Ввести имя типа
4 - Параметры консоли
0 - Выход из программы
");       
        }
        #endregion

        public static void Main(string[] args)
        {
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.Green;

            MainMenu();
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': InfoAboutTypes(); break;
                    case '2': OneOfTypes(); break;
                    case '3': SetTypeName(); break;
                    case '4': ParametresOfConsole(); break;
                    case '0': return;
                    default: break;
                }
            }
        }
    }  
}
