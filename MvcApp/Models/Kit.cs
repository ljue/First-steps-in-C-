using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
	public class Kit
	{
		public static string SetErrorMsg(Exception e)
		{
			var msg = e.TargetSite != null ? e.TargetSite.Name : "Unknown";
			msg = string.Format("Error in method: {0}\n{1}", msg, e.Message);
			Exception ex = e;
			while ((ex = ex.InnerException) != null)
				msg += "\n\nInnerException: " + ex.Message;
			if (e.Data.Count > 0)
			{
				msg += "\n\nException Data:";
				foreach (DictionaryEntry de in e.Data)
					msg += string.Format("\n  {0,-15} : {1}", de.Key, de.Value);
			}
			return msg;
		}

	}
}