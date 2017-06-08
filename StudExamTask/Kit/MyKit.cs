using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kit
{
    public static class MyKit
    {
			public static string GetName(string text)
			{
				if (text == null)
					return "N/A";
				var reg = new Regex("[А-Я|а-я|A-Z|a-z]+[]*");
				string[] captures = { "", "", "" };
				var mc = reg.Matches(text);

				int count = 0;
				foreach (Match m in mc)
				{
					foreach (Capture c in m.Captures)
					{
						if (count < 3)
							captures[count++] = c.Value.Trim().ToLower();
					}
				}

				var result = "";
				foreach (var s in captures)
				{
					var word = new StringBuilder(s);
					if (word.ToString() == "")
						break;
					word[0] = char.ToUpper(word[0]);
					result += word + " ";
				}

				return result == string.Empty ? "N/A" : result.Trim();
			}

			public static string FindFolder(string name)
			{
				string dir = AppDomain.CurrentDomain.BaseDirectory;
				for (char slash = '\\'; dir != null; dir = Path.GetDirectoryName(dir))
				{
					string res = dir.TrimEnd(slash) + slash + name;
					if (Directory.Exists(res))
						return res + slash;
				}
				return null;
			}

			public static byte[] GetImage(string imgDir)
			{
				try
				{
					var dlg = new OpenFileDialog
					{
						InitialDirectory = imgDir,
						Filter = "Image Files (*.bmp;*.jpg;*.gif)|*.bmp;*.jpg;*.gif"
					};
					if (dlg.ShowDialog() == DialogResult.OK && File.Exists(dlg.FileName))
						return File.ReadAllBytes(dlg.FileName);
				}
				catch (Exception ex) { new FormMsg(SetErrorMsg(ex), 10000); }
				return null;
			}

			public static string FormatMsg(string msg)
			{
				var sb = new StringBuilder();
				for (int i = 0, j = 0; i < msg.Length; i++, j++)
				{
					var c = msg[i];
					sb.Append(c);
					if (j > 120)
					{
						while (++i < msg.Length - 1 && (c = msg[i]) != ' ' && c != '.' && c != ',')
							sb.Append(c);
						sb.Append(c);
						sb.Append(c = '\n');
						for (int k = i + 1; k < msg.Length - 1 && msg[k] == ' '; k++)
							i = k;
					}
					if (c == '\n')
						j = 0;
				}
				return sb.ToString();
			}
			public static string Trim(string s)
			{
				return new Regex(@"\s{2,}").Replace(s.Trim(), " ");
			}

			public static string SetErrorMsg(Exception e)
			{
				var msg = e.TargetSite != null ? e.TargetSite.Name : "Unknown";
				msg = string.Format("Error in method: {0}\n{1}", msg, e.Message);

				Exception ex;
				while ((ex = e.InnerException) != null)
					msg += "\n InnerException: " + ex.Message;
				if (e.Data.Count > 0)
				{
					msg += "\n Exception Data:";
					foreach (DictionaryEntry de in e.Data)
						msg += string.Format("\n  {0,-15} : {1}", de.Key, de.Value);
				}
				return msg;
			}
    }
}
