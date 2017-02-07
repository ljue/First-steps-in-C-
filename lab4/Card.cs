using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;


namespace lab4
{
	[Serializable]
    public class Card
    {			
			
        public String _id { get; set; }
			
        public EnumTypeOfWord.TypeOfWord _typeW { get; set; }
			
				public List<String> _translate { get; set; }

        public Card()
        {
            _id = "None";
            _typeW = EnumTypeOfWord.TypeOfWord.None;
            _translate = new List<String>();

        }

        public Card(String id, EnumTypeOfWord.TypeOfWord typ, List<String> tr)
        {
            _id = id;
            _typeW = typ;
            _translate = tr;
        }

        public Card(String id, EnumTypeOfWord.TypeOfWord typ, String tr)
        {
            _id = id;
            _typeW = typ;
            _translate = new List<String>();
            _translate.Add(tr);

        }


    }
}
