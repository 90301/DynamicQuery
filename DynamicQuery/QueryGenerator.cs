using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicQuery
{
    public class QueryGenerator
    {
        /// <summary>
        /// The simpliest way to generate a String SQL query
        /// </summary>
        /// <param name="dbTable">The table to pull from</param>
        /// <param name="columns">The list of columns to pull</param>
        /// <returns>A string select SQL query</returns>
        public static String genBasicQuery(String dbTable,ICollection<String> columns )
        {
            String rtrnQuery = "";

            rtrnQuery += "Select ";

            bool firstRun = true;
            foreach (String col in columns)
            {
                if (!firstRun)
                {
                    rtrnQuery += " ,";
                } else
                {
                    firstRun = false;
                }
                rtrnQuery += col;
                
            }
            rtrnQuery += " From ";
            rtrnQuery += dbTable;
            return rtrnQuery;
        }

        

    }
}
