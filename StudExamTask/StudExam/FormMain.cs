using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using Kit;




namespace StudExam
{
	public partial class FormMain : Form
	{
		#region Data

		DataSet ds;
		BindingSource[] bs;
		DataGridView[] grids;
		DataView view;
		int searchTableID, searchColumnID, searchRowID;
		string dirName, fileName, findWhat;
		Form formFound;
		Random rand;
		bool bFindNext;
		static string[] courses = { "Math", "Mechanics", "Physics", "English", "Oracle", "OpenGL", "History" };

		#endregion

		#region Init

		public FormMain()
		{
			InitializeComponent();

			rand = new Random();
			dirName = MyKit.FindFolder("Data");
			fileName = "Students.xml";
			searchRowID = searchTableID = searchColumnID = 0;
			grids = new DataGridView[] { gridStud, gridExam };
			bs = new BindingSource[2];
			
			HandleEvents();
		}

		void DoLoad() // При загрузке формы
		{
			SetDesktopLocation(20, 20);
			InitDataSet();		// Создайте заглушку этого метода с помощью Intellisense
			InitTables();		// Заполнение таблиц DataTable (создайте заглушку)
			RelateAndBind();	// Связывание таблиц DataTable (создайте заглушку)
		}

		void RelateAndBind()
		{
			//=== Добавляем адрес функции обработки события в коллекцию делегатов, поддерживаемую событием RowChanged
			ds.Tables[0].RowChanged += OnStudsRowChanged;


			ds.Tables[1].Constraints.Add(new ForeignKeyConstraint("CS", ds.Tables[0].Columns[0], ds.Tables[1].Columns[1])
			{
				DeleteRule = Rule.Cascade,
				UpdateRule = Rule.Cascade
			});
			ds.EnforceConstraints = true; // включает ограничения. Иногда их надо выключать, например, при начальном заполнении таблицы, или при чтении данных из файла. В эти моменты целостность данных по ссылкам (временно) может отсутствовать. По завершении этих операций ограничения следует вновь включить.

			ds.Relations.Add(new DataRelation("StudExam", ds.Tables[0].Columns[0], ds.Tables[1].Columns[1])); // для синхронизации отображения данных связанных таблиц

			ds.Tables[0].Columns.Add("ExamsNo", typeof(int), "Count(Child.StudID)"); // количество сданных им экзаменов
			ds.Tables[0].Columns[4].ColumnMapping = MappingType.Hidden; // чтоб данные не попадали в xml-файл
			ds.Tables[0].Columns.Add("Average", typeof(float), "Sum(Child.Mark) / Count(Child.StudID)"); // средний балл по оценкам
			ds.Tables[0].Columns[5].ColumnMapping = MappingType.Hidden; // чтоб данные не попадали в xml-файл

			AddStyle(0);
			AddStyle(1);
			// Привязка DataGridView к данным DataSet. Включаем механизм DataBinding
			bs[0] = new BindingSource(ds, ds.Tables[0].TableName);
			bs[1] = new BindingSource(bs[0], ds.Relations[0].RelationName);
			gridStud.DataSource = bs[0];
			gridExam.DataSource = bs[1];

			bn.BindingSource = bs[0];
			//gridStud.DataSource = ds;
			//gridStud.DataMember = ds.Tables[0].TableName;	// Настройкамеханизма DataBinding
			//gridExam.DataSource = ds;
			//gridExam.DataMember = ds.Tables[1].TableName;

			listTable.SelectedIndex = 0;

		}

		void OnStudsRowChanged(object sender, DataRowChangeEventArgs e)
		{
			ds.Tables[0].RowChanged -= OnStudsRowChanged; // снимаем обработчик чтоб при изменении имени не перейти в бесконечный цикл вызовами реакции на изменение строки.
			e.Row["Name"] = MyKit.Trim(e.Row["Name"].ToString());
			ds.Tables[0].RowChanged += OnStudsRowChanged;
			var exams = ds.Tables[1];
			if (e.Action == DataRowAction.Add)	// Если в таблице студентов появилась новая строка, пишем студенту экзамены
			{
				int num = 0;
				foreach (string c in courses)
				{
					if (num++ != rand.Next(courses.Length))
						ds.Tables[1].Rows.Add(GetRandomExam((int)e.Row["ID"], c));
				}
			}

		}


		void InitTables()
		{
			object[] studs = 
			{
				new object[] {1, "Charlie Parker", "123-4455", "Broadway, 52a"},
				new object[] {2, "Alex Black", "555-1122", "5-th Avenue, 74" },
				new object[] {3, "Andy Williams", "430-5125", "Back st., 22" },
				new object[] {4, "Charley Mingus", "230-1466", "Hi st., 30" },
				new object[] {5, "Chet Baker", "320-8656", "Howard st., 15" },
				new object[] {6, "Lou Rowles", "552-4233", "Basin st., 10" },
			};
			foreach (object[] o in studs)
				ds.Tables[0].Rows.Add(o);

			foreach (object[] stud in studs)
			{
				int num = 0;
				foreach (var c in courses)
				{
					if (num++ != rand.Next(courses.Length))
						ds.Tables[1].Rows.Add(GetRandomExam((int)stud[0], c));
				}
			}

		}

		DataRow GetRandomExam(int studID, string course)
		{
			// Такой способ создания строки надежен, так как новая строка уже имеет определенный нами ранее набор колонок
			var row = ds.Tables[1].NewRow();
			row[1] = studID;
			row[2] = course;
			row[3] = rand.Next(10000) < 9000;// Напишите логическое выражение (сдал-ли студент зачет). Успех должен быть в 90% случаев.
			row[4] = 2;
			if ((bool)row[3])
			{
				row[4] = rand.Next(3, 6);// Используйте rand
				row[5] = new DateTime(rand.Next(2015, 2017), rand.Next(1, 13), rand.Next(1, 28));// Новая дата, отстающаяся от DateTime.Now на случайное число лет (не более 2), случайный месяц и день
			}
			return row;
		}


		void InitDataSet()
		{
			ds = new DataSet();	// Создайте источник данных (аналог базы данных) DataSet с именем "StudentsSet"
			var studs = ds.Tables.Add("Studs");// Создание таблицы данных с именем "Studs"
			SetPrimaryColumn(studs);	// Этот метод создадим позже
			//======= Определяем схему таблицы
			studs.Columns.Add("Name");		// Добавляем колонку "Имя студента"
			studs.Columns.Add("Phone");  //==== Добавьте колонки Phone и Addr
			studs.Columns.Add(" Addr");

			var exams = ds.Tables.Add("Exams");  // Создайте вторую таблицу с именем "Exams"			
			// Она должна иметь такой набор полей: 
			//ID (primary key). Вызовите вспомогательный метод SetPrimaryColumn (создадим позже)
			SetPrimaryColumn(exams);
			exams.Columns.Add("StudID", typeof(int)); // StudID (foreignkey) (типаint), 
			exams.Columns.Add("Course", typeof(string)); //Course - имя курса лекций, 
			exams.Columns.Add("Credit", typeof(bool)); // Credit (зачет - незачет), 
			exams.Columns.Add("Mark", typeof(byte));// Mark - оценка за экзамен (типа byte),
			exams.Columns.Add("Date", typeof(DateTime));//Date - дата сдачи экзамена


		}

		void AddStyle(int tableID)
		{
			var grid = grids[tableID]; // передали индекс таблицы
			grid.AutoGenerateColumns = false; // Ваш код. . . Установите свойство AutoGenerateColumns для объекта grid // выключаем автогенерацию колонок, т.к мы их уже создали.
			var cols = ds.Tables[tableID].Columns;// Добудьте ссылку на множество колонок таблицы с индексом tableID
			foreach (DataColumn dc in cols)
			{
				DataGridViewColumn col = null;
				if (dc.DataType == typeof(bool))
					col = new DataGridViewCheckBoxColumn();
				else if (dc.DataType == typeof(DateTime))
				{
					col = new CalendarColumn();
					col.DefaultCellStyle.Format = "d"; //  убираем время, оставляем только дату
				}
				else if (dc.DataType == typeof(float))
				{
					col = new DataGridViewTextBoxColumn();
					col.DefaultCellStyle.Format = "f2";
				}
				else if (dc.ColumnName == "Course")
				{
					var combo = new DataGridViewComboBoxColumn();
					combo.DataSource = courses;
					col = combo;
				}
				else if (dc.ColumnName == "Mark")
				{
					dc.DefaultValue = (byte)2;
					var combo = new DataGridViewComboBoxColumn();
					combo.Items.Add((byte)2);
					combo.Items.Add((byte)3);
					combo.Items.Add((byte)4);
					combo.Items.Add((byte)5);
					col = combo;
				}
				else
					col = new DataGridViewTextBoxColumn();

				col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
				if (dc == cols[cols.Count - 1])
					col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				col.DataPropertyName = dc.ColumnName;
				col.HeaderText = dc.ColumnName;
				grid.Columns.Add(col);
			}
			grid.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
			grid.DefaultCellStyle.SelectionForeColor = Color.Black;
			grid.AlternatingRowsDefaultCellStyle.BackColor = tableID == 0 ? Color.Wheat : Color.FromArgb(200, 255, 200);
		}


		void SetPrimaryColumn(DataTable table)
		{
			var dc = new DataColumn("ID", typeof(int))
			{
				// (Здесь Ваш код . . .)Установите значения для следующих свойств:	AutoIncrement, AutoIncrementSeed, ReadOnly
				AutoIncrement = true, // автоматическая нумерация
				AutoIncrementSeed = 1, // начальное значение
				ReadOnly = true // только для чтения, изменять нельзя
			};
			table.Columns.Add(dc); // (Здесь Ваш код . . .) Добавьте колонку dc в коллекцию колонок таблицы
			table.PrimaryKey = new DataColumn[] { dc };
		}
		#endregion

		#region Methods

		void HandleEvents()
		{
			Shown += (s, e) => DoLoad();
			toolStrip.ItemClicked += toolStrip_ItemClicked;
			comboFind.TextUpdate += (s, a) => UpdateFindPanel();  // обработка ввода в поиск
			comboFind.SelectedIndexChanged += (s, a) => UpdateFindPanel();

			listTable.SelectedIndexChanged += listTable_SelectedIndexChanged; //изменяет значениее searchTableID
			listColumn.SelectedIndexChanged += (s, a) => searchColumnID = listColumn.SelectedIndex; //изменяет значениее searchColumnID
		}

		void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			switch (e.ClickedItem.Text)
			{
				case "Open": Open(); break;
				case "Save": Save(); break;
				case "Find Next": Find(); break;
				case "Find List": ShowList(); break;
				case "Find View": ShowGrid(listColumn.Text + GetSearchCriteria()); break;
				case "Average": ShowAverage(); break;
			}
		}


		void listTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			searchTableID = listTable.SelectedIndex;
			listColumn.Items.Clear();			
			foreach (DataColumn dc in ds.Tables[searchTableID].Columns)  // Заново заполняем listColumn
				listColumn.Items.Add(dc.ColumnName);
			listColumn.SelectedIndex = 1;
		}

		#region Findings

		void UpdateFindPanel()
		{
			bool enabled = MyKit.Trim(comboFind.Text).Equals("") ? false : true;// Логическое выражение: Текст comboFind (освобожденный от whitespace) непуст. 
			btnFindNext.Enabled = btnFindList.Enabled = btnFindView.Enabled = enabled; // активация кнопок поиска
			split.Panel1Collapsed = !enabled; // отображение боковой панели
		}

		void InformNotFound(string what, string col)
		{
			var msg = "Could not find:  '" + what + "'";
			msg += col != null ? "  in column:  " + col : ".\nSelect a column to search";
			new FormMsg(msg, 3000);
		}

		#region Find Next
		void Find()
		{
			try
			{
				var text = MyKit.Trim(comboFind.Text);	// Искомыйтекст
				if (findWhat != text)
					findWhat = text;
				if (bFindNext)
					searchRowID++;
				if (FindNext())
				{
					if (!comboFind.Items.Contains(comboFind.Text))
						comboFind.Items.Add(comboFind.Text);
					bFindNext = true;
				}
				else
					InformNotFound(findWhat, ds.Tables[searchTableID].Columns[searchColumnID].ColumnName);// Определите имя колонки (используйте индексы: searchTableID и searchColumnID);
			}
			catch (Exception ex) { MyKit.SetErrorMsg(ex); }	
		}

		bool FindNext()
		{
			var bFound = false;
			var grid = grids[searchTableID];
			for (int i = searchRowID; findWhat.Length > 0 && !bFound; )
			{
				object o = grid[searchColumnID, i].Value;// Значение ячейки grid[столбец searchColumnID, строка i]
				if (o != null)
				{
					var val = o.ToString();
					if (val.StartsWith(comboFind.Text, StringComparison.InvariantCultureIgnoreCase)) //(Если val начинается с искомой строки (без учета регистра))
					{
						grid.CurrentCell = grid[searchColumnID, i];// Включает Databinding
						// Выделитевсюстрокуgrid
						searchRowID = i;// Запомнитеиндекс строки в переменной searchRowID
						bFound = true;// Не забудьте про флаг
						break;
					}
				}
				if (i == grid.RowCount - 1)
					i = -1;
				i++;
				if (i == searchRowID)
					break;
			}
			return bFound;
		}
		#endregion


		#region Find List
		// Поиск в памяти (DataTable). Создание новой таблицы-формы.
		void CreateFormFound(Control control, string filter)
		{
			var msg = new Label
			{
				Location = new Point(3, 3),
				AutoSize = true,
				Font = new Font("Comic Sans MS", 9),
				ForeColor = Color.DarkGreen,
				BorderStyle = BorderStyle.FixedSingle,
				Text = "'" + filter + "' in " + listTable.Text
			};

			// Ваш код. . .  Если форма существует убейте ее.
			if (formFound != null && formFound.Enabled) formFound.Close();

			formFound = new Form
			{
				Text = "Search results",
				// Ваш код. . .Настройте другие свойства формы
				Height = Height,
				BackColor = Color.LightGreen,
				Owner = this,
				Visible = true
			};
			formFound.Controls.Add(msg); //label
			formFound.Controls.Add(control); //contrpl
			formFound.Location = new Point(Location.X + Width, Location.Y); //location
		}

		string GetSearchCriteria()
		{
			string criteria = null;
			try
			{
				findWhat = MyKit.Trim(comboFind.Text);

				var type = ds.Tables[searchTableID].Columns[searchColumnID].DataType;
				switch (type.Name)
				{
					case "String": criteria = " LIKE '" + findWhat + "*'"; break;

					// Вашкод. ..Добавьтеветвидлявещественныхчисловыхтиповданных
					case "Double": ; break;
					// Вашкод. ..Критерий не должен иметь вид LIKE. Он должен иметь вид:" > {0} AND {1} < {2}"
					case "Single": criteria = string.Format(" > {0} AND {2} < {1}", findWhat + "-0.005", findWhat + "+0.005", listColumn.Text); 
						break;

					case "Byte":
					case "SByte":
					case "Int16":
					case "UInt16":
					case "Int64":
					case "UInt64":
					case "UInt32":
					case "Boolean":
					case "Int32": criteria = "=" + findWhat; break;
					case "DateTime":
						DateTime dt;
						bool ok = DateTime.TryParse(findWhat, out dt);
						if (ok)
							criteria = "='" + dt.ToShortDateString() + "'";
						else
							throw new Exception("Could not parse date string");
						break;
				}
				return criteria;
			}
			catch (Exception ex) { MyKit.SetErrorMsg(ex); }
			return null;
		}

		void ShowList()
		{
			var criteria = GetSearchCriteria();
			DataRow[] rows = null;
			try
			{
				var filter = listColumn.Text + criteria; //Критерий фильтрации, подаваемый на вход метода Select, зависит от активной колонки активной таблицы
				rows = ds.Tables[searchTableID].Select(filter); // Отфильтруйте строки нужной таблицы с помощью метода Select
				if (rows.Length != 0)
				{
					// Вашкод. . .  Запомнитеискомый текст в выпадающем списке comboFind
					if (!comboFind.Items.Contains(comboFind.Text))
						comboFind.Items.Add(comboFind.Text);
					CreateFormFound(CreateList(rows), filter);
				}
				else
					InformNotFound(criteria, listColumn.Text);
			}
			catch (Exception ex) { MyKit.SetErrorMsg(ex); }
		}

		Control CreateList(DataRow[] rows) // помещаем результат поиска в ListView (нет необходимости редактировать данные)
		{
			var list = new ListView
			{
				View = View.Details,
				FullRowSelect = true,
				GridLines = true,
				Location = new Point(3,30),
				Width = 315,
				Height = Height - 74
			};
			var cols = rows[0].Table.Columns;
			list.Columns.Add(cols[0].ColumnName, 30);
			list.Columns.Add(cols[1].ColumnName, 100);
			list.Columns.Add(cols[2].ColumnName, 100);

			foreach (var r in rows)
				list.Items.Add(new ListViewItem(new string[] { r[0].ToString(), r[1].ToString(), r[2].ToString() }));

			list.SelectedIndexChanged += listFound_SelectedIndexChanged;
			return list;	
		}

		void listFound_SelectedIndexChanged(object sender, EventArgs e)
		{
			var lv = (ListView)sender;
			var items = lv.SelectedItems;
			if (items.Count == 0)
				return;
			int pk = int.Parse(items[0].Text);

			if (searchTableID == 1)
			{
				int fk = int.Parse(items[0].SubItems[1].Text);
				SelectRow(grids[0], fk);
			}
			SelectRow(grids[searchTableID], pk);
		}

		void SelectRow(DataGridView grid, int id)
		{
			for (int i = 0; i < grid.RowCount - 1; i++)
			{
				if ((int)grid[0, i].Value == id)
				{
					grid.CurrentCell = grid[0, i];
					grid.Rows[i].Selected = true;
				}
			}
		}

		#endregion

		#region Find View

		void ShowGrid(string filter)
		{
			if (!comboFind.Items.Contains(comboFind.Text))
				comboFind.Items.Add(comboFind.Text);
			// Создаем образ таблицы студентов           
			try
			{ // создаем view
				view = new DataView(ds.Tables[searchTableID])
				{
					RowFilter = filter, // Настраиваем фильтр отбора строк               
					//Sort = "ID DESC", // Настраиваем режим сортировки               
					AllowDelete = false,
					AllowEdit = false,
					AllowNew = false,
				};
				// создаем grid
				var grid = new DataGridView
				{
					AutoGenerateColumns = false,
					Width = 220
				};
				// заполняем grid
				grid.Columns.Add(new DataGridViewTextBoxColumn
				{
					DataPropertyName = searchTableID == 0 ? "Name" : "Course",
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});
				grid.DataSource = view;
				grid.AutoSize = true;
				grid.Location = new Point(3, 30);
				grid.CellEnter += Grid_CellEnter;

				CreateFormFound(grid, filter);

			}
			catch (Exception ex) { new FormMsg(ex.ToString(), 3000); }

		}

		void Grid_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			int pk = (int)view[e.RowIndex][0];
			if (searchTableID == 1)
			{
				int fk = (int)view[e.RowIndex][1];
				SelectRow(grids[0], fk);
			}
			SelectRow(grids[searchTableID], pk);
		}

		#endregion

		#region Average

		void ShowAverage()
		{
			listTable.SelectedIndex = 0;
			listColumn.SelectedIndex = 5;
			ShowGrid("Average >= " + txtAvLo.Text + " AND Average <= " + txtAvgHi.Text);
		}
		#endregion

		#endregion


		void Save()
		{
			var dlg = new SaveFileDialog
			{
				// Настройте свойства InitialDirectory, Filter, и FileName
				InitialDirectory = dirName, // текущая директория
				Filter = "Xml-files (*.xml)| *.xml",
				FileName = fileName
			};
			if (dlg.ShowDialog() == DialogResult.OK && dlg.FileName != null && ds.Tables[0].Rows.Count != 0)
				DoSave(dlg.FileName);
		}

		void DoSave(string file)
		{
			ds.WriteXml(file);
			SetPath(file);
		}

		void Open()
		{
			var dlg = new OpenFileDialog
			{
				// Настройте свойства InitialDirectory, Filter, и FileName
				InitialDirectory = dirName, // текущую директорию
				Filter = "Xml-files (*.xml)| *.xml",
				FileName = fileName
			};

			if (dlg.ShowDialog() == DialogResult.OK && dlg.FileName != null)
				DoOpen(dlg.FileName);
		}

		void DoOpen(string file)
		{
			ds.Tables[0].RowChanged -= OnStudsRowChanged; // снимаем обработчик чтобы не добавлять уже существующие
			ds.Clear(); // при чтении данных из файла DataSet рождается заново, поэтому старые данные необходимо уничтожить 
			ds.ReadXml(file);
			ds.AcceptChanges(); // чтобы строки были фиксорованны
			SetPath(file);
			ds.Tables[0].RowChanged += OnStudsRowChanged;
		}

		void SetPath(string fn)
		{
			Text = fn;
			dirName = Path.GetDirectoryName(fn) + "\\";
			fileName = Path.GetFileName(fn);
		}



		#endregion
	}
}
