using System;
using System.Windows.Forms;

namespace Kit
{
	class CalendarControl : DateTimePicker, IDataGridViewEditingControl
	{
		DataGridView grid;
		bool valueChanged = false;
		int rowID;

		public CalendarControl() { Format = DateTimePickerFormat.Short; }

		#region IDataGridViewEditingControl Implementation

		public DataGridView EditingControlDataGridView
		{
			get { return grid; }
			set { grid = value; }
		}
		public object EditingControlFormattedValue
		{
			get { return Value.ToShortDateString(); }
			set
			{
				if (value is string)
					Value = DateTime.Parse((string)value);
			}
		}
		public int EditingControlRowIndex
		{
			get { return rowID; }
			set { rowID = value; }
		}
		public bool EditingControlValueChanged
		{
			get { return valueChanged; }
			set { valueChanged = value; }
		}
		public Cursor EditingPanelCursor { get { return base.Cursor; } }
		public bool RepositionEditingControlOnValueChange { get { return false; } }
		
		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle cellStyle)
		{
			Font = cellStyle.Font;
			CalendarForeColor = cellStyle.ForeColor;
			CalendarMonthBackground = cellStyle.BackColor;
		}

		public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
		{
			switch (key & Keys.KeyCode) // Let the DateTimePicker handle the keys listed
			{
				case Keys.Left:			case Keys.Up:			case Keys.Down:			case Keys.Right:
				case Keys.Home:			case Keys.End:		case Keys.PageDown:	case Keys.PageUp:
					return true;
				default: return !dataGridViewWantsInputKey;
			}
		}
		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return EditingControlFormattedValue;
		}

		public void PrepareEditingControlForEdit(bool selectAll) { }

		#endregion

		protected override void OnValueChanged(EventArgs e)
		{
			valueChanged = true;
			EditingControlDataGridView.NotifyCurrentCellDirty(true);
			base.OnValueChanged(e);
		}
	}

	public class CalendarCell : DataGridViewTextBoxCell
	{
		public CalendarCell() : base() { Style.Format = "d"; }

		public override Type EditType { get { return typeof(CalendarControl); } }
		public override Type ValueType { get { return typeof(DateTime); } }
		public override object DefaultNewRowValue { get { return DBNull.Value; } }

		public override void InitializeEditingControl(int rowIndex, object
			initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			CalendarControl c = DataGridView.EditingControl as CalendarControl;
			if (Value != null)
				if (Value != DBNull.Value)
					c.Value = (DateTime)Value;
		}
	}

	public class CalendarColumn : DataGridViewColumn
	{
		public CalendarColumn() : base(new CalendarCell()) { }

		public override DataGridViewCell CellTemplate
		{
			get { return base.CellTemplate; }
			set // Ensure that the cell used for the template is a CalendarCell
			{
				if (value != null && !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
					throw new InvalidCastException("Must be a CalendarCell");
				base.CellTemplate = value;
			}
		}
	}
}
