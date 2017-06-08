namespace StudExam
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.split = new System.Windows.Forms.SplitContainer();
			this.grFind = new System.Windows.Forms.GroupBox();
			this.lblColumn = new System.Windows.Forms.Label();
			this.listColumn = new System.Windows.Forms.ListBox();
			this.lblTable = new System.Windows.Forms.Label();
			this.listTable = new System.Windows.Forms.ListBox();
			this.splitRight = new System.Windows.Forms.SplitContainer();
			this.gridStud = new System.Windows.Forms.DataGridView();
			this.gridExam = new System.Windows.Forms.DataGridView();
			this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
			this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
			this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
			this.bn = new System.Windows.Forms.BindingNavigator(this.components);
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.btnOpen = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.sFind = new System.Windows.Forms.ToolStripLabel();
			this.comboFind = new System.Windows.Forms.ToolStripComboBox();
			this.btnFindNext = new System.Windows.Forms.ToolStripButton();
			this.btnFindList = new System.Windows.Forms.ToolStripButton();
			this.btnFindView = new System.Windows.Forms.ToolStripButton();
			this.sAverage = new System.Windows.Forms.ToolStripLabel();
			this.txtAvLo = new System.Windows.Forms.ToolStripTextBox();
			this.sHi = new System.Windows.Forms.ToolStripLabel();
			this.txtAvgHi = new System.Windows.Forms.ToolStripTextBox();
			this.btnAverage = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
			this.split.Panel1.SuspendLayout();
			this.split.Panel2.SuspendLayout();
			this.split.SuspendLayout();
			this.grFind.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
			this.splitRight.Panel1.SuspendLayout();
			this.splitRight.Panel2.SuspendLayout();
			this.splitRight.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridStud)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridExam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bn)).BeginInit();
			this.bn.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveLastItem.Text = "Move last";
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveNextItem.Text = "Move next";
			// 
			// bindingNavigatorSeparator2
			// 
			this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
			this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorSeparator
			// 
			this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// bindingNavigatorSeparator1
			// 
			this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
			this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// split
			// 
			this.split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.split.Location = new System.Drawing.Point(0, 25);
			this.split.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.split.Name = "split";
			// 
			// split.Panel1
			// 
			this.split.Panel1.Controls.Add(this.grFind);
			// 
			// split.Panel2
			// 
			this.split.Panel2.Controls.Add(this.splitRight);
			this.split.Size = new System.Drawing.Size(730, 411);
			this.split.SplitterDistance = 148;
			this.split.SplitterWidth = 3;
			this.split.TabIndex = 9;
			// 
			// grFind
			// 
			this.grFind.BackColor = System.Drawing.Color.Linen;
			this.grFind.Controls.Add(this.lblColumn);
			this.grFind.Controls.Add(this.listColumn);
			this.grFind.Controls.Add(this.lblTable);
			this.grFind.Controls.Add(this.listTable);
			this.grFind.Location = new System.Drawing.Point(4, 7);
			this.grFind.Name = "grFind";
			this.grFind.Size = new System.Drawing.Size(104, 187);
			this.grFind.TabIndex = 0;
			this.grFind.TabStop = false;
			this.grFind.Text = "Search in :";
			// 
			// lblColumn
			// 
			this.lblColumn.AutoSize = true;
			this.lblColumn.Location = new System.Drawing.Point(12, 77);
			this.lblColumn.Name = "lblColumn";
			this.lblColumn.Size = new System.Drawing.Size(45, 13);
			this.lblColumn.TabIndex = 3;
			this.lblColumn.Text = "Column:";
			// 
			// listColumn
			// 
			this.listColumn.FormattingEnabled = true;
			this.listColumn.Location = new System.Drawing.Point(10, 94);
			this.listColumn.Name = "listColumn";
			this.listColumn.Size = new System.Drawing.Size(84, 82);
			this.listColumn.TabIndex = 2;
			// 
			// lblTable
			// 
			this.lblTable.AutoSize = true;
			this.lblTable.Location = new System.Drawing.Point(11, 20);
			this.lblTable.Name = "lblTable";
			this.lblTable.Size = new System.Drawing.Size(37, 13);
			this.lblTable.TabIndex = 1;
			this.lblTable.Text = "Table:";
			// 
			// listTable
			// 
			this.listTable.FormattingEnabled = true;
			this.listTable.Items.AddRange(new object[] {
            "Studs",
            "Exams"});
			this.listTable.Location = new System.Drawing.Point(9, 37);
			this.listTable.Name = "listTable";
			this.listTable.Size = new System.Drawing.Size(84, 30);
			this.listTable.TabIndex = 0;
			// 
			// splitRight
			// 
			this.splitRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitRight.Location = new System.Drawing.Point(0, 0);
			this.splitRight.Name = "splitRight";
			this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitRight.Panel1
			// 
			this.splitRight.Panel1.Controls.Add(this.gridStud);
			// 
			// splitRight.Panel2
			// 
			this.splitRight.Panel2.Controls.Add(this.gridExam);
			this.splitRight.Size = new System.Drawing.Size(579, 411);
			this.splitRight.SplitterDistance = 190;
			this.splitRight.TabIndex = 0;
			// 
			// gridStud
			// 
			this.gridStud.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridStud.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridStud.Location = new System.Drawing.Point(0, 0);
			this.gridStud.Name = "gridStud";
			this.gridStud.RowTemplate.Height = 24;
			this.gridStud.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridStud.Size = new System.Drawing.Size(579, 190);
			this.gridStud.TabIndex = 8;
			// 
			// gridExam
			// 
			this.gridExam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridExam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridExam.Location = new System.Drawing.Point(0, 0);
			this.gridExam.Name = "gridExam";
			this.gridExam.RowTemplate.Height = 24;
			this.gridExam.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridExam.Size = new System.Drawing.Size(579, 217);
			this.gridExam.TabIndex = 9;
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "Position";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = "Current position";
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMovePreviousItem.Text = "Move previous";
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorMoveFirstItem.Text = "Move first";
			// 
			// bindingNavigatorCountItem
			// 
			this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
			this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 22);
			this.bindingNavigatorCountItem.Text = "для {0}";
			this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorAddNewItem.Text = "Add new";
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
			this.bindingNavigatorDeleteItem.Text = "Delete";
			// 
			// bn
			// 
			this.bn.AddNewItem = this.bindingNavigatorAddNewItem;
			this.bn.CountItem = this.bindingNavigatorCountItem;
			this.bn.DeleteItem = this.bindingNavigatorDeleteItem;
			this.bn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
			this.bn.Location = new System.Drawing.Point(0, 436);
			this.bn.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
			this.bn.MoveLastItem = this.bindingNavigatorMoveLastItem;
			this.bn.MoveNextItem = this.bindingNavigatorMoveNextItem;
			this.bn.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
			this.bn.Name = "bn";
			this.bn.PositionItem = this.bindingNavigatorPositionItem;
			this.bn.Size = new System.Drawing.Size(730, 25);
			this.bn.TabIndex = 8;
			this.bn.Text = "bn";
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnSave,
            this.sFind,
            this.comboFind,
            this.btnFindNext,
            this.btnFindList,
            this.btnFindView,
            this.sAverage,
            this.txtAvLo,
            this.sHi,
            this.txtAvgHi,
            this.btnAverage});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(730, 25);
			this.toolStrip.TabIndex = 10;
			// 
			// btnOpen
			// 
			this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
			this.btnOpen.ImageTransparentColor = System.Drawing.Color.Black;
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(23, 22);
			this.btnOpen.Text = "Open";
			// 
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Black;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "Save";
			// 
			// sFind
			// 
			this.sFind.Name = "sFind";
			this.sFind.Size = new System.Drawing.Size(33, 22);
			this.sFind.Text = "Find:";
			// 
			// comboFind
			// 
			this.comboFind.AutoSize = false;
			this.comboFind.Name = "comboFind";
			this.comboFind.Size = new System.Drawing.Size(68, 23);
			// 
			// btnFindNext
			// 
			this.btnFindNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnFindNext.Image = ((System.Drawing.Image)(resources.GetObject("btnFindNext.Image")));
			this.btnFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnFindNext.Name = "btnFindNext";
			this.btnFindNext.Size = new System.Drawing.Size(23, 22);
			this.btnFindNext.Text = "Find Next";
			// 
			// btnFindList
			// 
			this.btnFindList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnFindList.Image = ((System.Drawing.Image)(resources.GetObject("btnFindList.Image")));
			this.btnFindList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnFindList.Name = "btnFindList";
			this.btnFindList.Size = new System.Drawing.Size(23, 22);
			this.btnFindList.Text = "Find List";
			// 
			// btnFindView
			// 
			this.btnFindView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnFindView.Image = ((System.Drawing.Image)(resources.GetObject("btnFindView.Image")));
			this.btnFindView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnFindView.Name = "btnFindView";
			this.btnFindView.Size = new System.Drawing.Size(23, 22);
			this.btnFindView.Text = "Find View";
			// 
			// sAverage
			// 
			this.sAverage.Name = "sAverage";
			this.sAverage.Size = new System.Drawing.Size(75, 22);
			this.sAverage.Text = "Average low:";
			// 
			// txtAvLo
			// 
			this.txtAvLo.Name = "txtAvLo";
			this.txtAvLo.Size = new System.Drawing.Size(25, 25);
			this.txtAvLo.Text = "3";
			// 
			// sHi
			// 
			this.sHi.Name = "sHi";
			this.sHi.Size = new System.Drawing.Size(20, 22);
			this.sHi.Text = "hi:";
			// 
			// txtAvgHi
			// 
			this.txtAvgHi.Name = "txtAvgHi";
			this.txtAvgHi.Size = new System.Drawing.Size(16, 25);
			this.txtAvgHi.Text = "5";
			// 
			// btnAverage
			// 
			this.btnAverage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAverage.Image = ((System.Drawing.Image)(resources.GetObject("btnAverage.Image")));
			this.btnAverage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAverage.Name = "btnAverage";
			this.btnAverage.Size = new System.Drawing.Size(23, 22);
			this.btnAverage.Text = "Average";
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(730, 461);
			this.Controls.Add(this.split);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.bn);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "FormMain";
			this.Text = "Students DataSet";
			this.split.Panel1.ResumeLayout(false);
			this.split.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
			this.split.ResumeLayout(false);
			this.grFind.ResumeLayout(false);
			this.grFind.PerformLayout();
			this.splitRight.Panel1.ResumeLayout(false);
			this.splitRight.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
			this.splitRight.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridStud)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridExam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bn)).EndInit();
			this.bn.ResumeLayout(false);
			this.bn.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
		private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
		private System.Windows.Forms.SplitContainer split;
		private System.Windows.Forms.GroupBox grFind;
		private System.Windows.Forms.Label lblColumn;
		private System.Windows.Forms.ListBox listColumn;
		private System.Windows.Forms.Label lblTable;
		private System.Windows.Forms.ListBox listTable;
		private System.Windows.Forms.SplitContainer splitRight;
		private System.Windows.Forms.DataGridView gridStud;
		private System.Windows.Forms.DataGridView gridExam;
		private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
		private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
		private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
		private System.Windows.Forms.BindingNavigator bn;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton btnOpen;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripLabel sFind;
		private System.Windows.Forms.ToolStripComboBox comboFind;
		private System.Windows.Forms.ToolStripButton btnFindNext;
		private System.Windows.Forms.ToolStripButton btnFindList;
		private System.Windows.Forms.ToolStripButton btnFindView;
		private System.Windows.Forms.ToolStripLabel sAverage;
		private System.Windows.Forms.ToolStripTextBox txtAvLo;
		private System.Windows.Forms.ToolStripLabel sHi;
		private System.Windows.Forms.ToolStripTextBox txtAvgHi;
		private System.Windows.Forms.ToolStripButton btnAverage;

	}
}

