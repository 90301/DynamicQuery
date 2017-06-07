using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicQuery;


namespace DynamicQueryTesting
{
    class TestObj
    {

        public static QueryMetadata qmd = new QueryMetadata("TestObj","TestTable");

        private string _realName;
        public ColVariable NameColVariable = new ColVariable("name", qmd);


        public string Name { get { return qmd.getCol(_realName, NameColVariable); } set => _realName = value;
        }

        static TestObj()
        {
           QueryHolder.howDoISelect = DynamicQueryMain.consoleSelect; 
        }
    }


}
