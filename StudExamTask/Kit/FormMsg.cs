using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kit
{
	public class FormMsg : Form
	{
		Label lbl;
		Timer timer;

		public FormMsg(string msg, int interval)
		{
			BackColor = Color.Black;
			FormBorderStyle = FormBorderStyle.None;
			AutoSize = true;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			TopMost = true;

			timer = new Timer { Interval = interval };
			timer.Tick += (s, e) => { Dispose(); };

			StringBuilder text = new StringBuilder();
			for (int i = 0; i < msg.Length; i++)
				text.Append(msg[i]);

			lbl = new Label
			{
				Font = new Font("Arial", 9.0F),
				ForeColor = Color.Yellow,
				Padding = new Padding(12, 10, 12, 12),
				Text = text.ToString(),
				AutoSize = true
			};
			lbl.Paint += (s, e) => { e.Graphics.DrawRectangle(Pens.Yellow, 5, 5, lbl.Width - 7, lbl.Height - 10); };
			Controls.Add(lbl);

			Load += (s, e) =>
			{
				Form form = Application.OpenForms[0];
				Left = form.Left + 100;
				Top = form.Top + 100;
				timer.Start();
			};
			Show();
			Update();
		}
	}
}