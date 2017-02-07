using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class EnumTypeOfWord
    {
        public enum TypeOfWord { None=-1, Noun, Adjective, Adverb, Verb, Other };

			public static string valueEnum(TypeOfWord t)
				{
					string s;
				switch(t)
				{
					case TypeOfWord.None:
						s = "";
						break;
					case TypeOfWord.Noun:
						s = "Существительное"; break;
					case TypeOfWord.Adjective:
						s = "Прилагательное"; break;
					case TypeOfWord.Adverb:
						s = "Наречие"; break;
					case TypeOfWord.Verb:
						s = "Глагол"; break;
					case TypeOfWord.Other:
						s = ""; break;
					default: s = ""; break;
				}
				return s;
				}
    }
}
