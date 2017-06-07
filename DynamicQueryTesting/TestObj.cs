using System;
using System.Data;
using DynamicQuery;

namespace DynamicQueryTesting
{
    public class TestObj
    {
        private const string NAME_CONST = "name";

        public static QueryMetadata qmd = new QueryMetadata("TestObj", "TestTable");

        public static JoinTable jt1 = new JoinTable("TestTable2", JoinTable.LEFT_JOIN,qmd);
        public static ColVariable NameColVariable = new ColVariable(NAME_CONST, qmd);


        private string _realName;

        static TestObj()
        {
            QueryHolder.howDoISelect = consoleSelect;

            #region Join Conditions

            jt1.addSameColJoin(NAME_CONST);
            qmd.addJoinTable(jt1);

            #endregion Join Conditions
        }


        public string Name
        {
            get => qmd.getCol(_realName, NameColVariable);
            set => _realName = value;
        }


        public TestObj()
        {
            Console.WriteLine("Construction started");
        }

        /// <summary>
        ///     TESTING METHOD ONLY, DO NOT USE THIS IN PRODUCTION
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable consoleSelect(string query)
        {
            Console.WriteLine(query);

            return null;
        }
    }
}