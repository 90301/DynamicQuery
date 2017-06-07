using System;
using System.Data;

namespace DynamicQuery
{
    public class DynamicQueryMain
    {
        /*
         * Query Cache (Holds Queries for collections / object)
         * Query Executor (Optional, can be over-ridden with a delegate method)
         * Query Generator
         * 
         * Credential Manager
         * Objects(Database Objects)
         * Collections
         * 
         * 
         */

            /// <summary>
            /// TESTING METHOD ONLY, DO NOT USE THIS IN PRODUCTION
            /// </summary>
            /// <param name="query"></param>
            /// <returns></returns>
        public static  DataTable consoleSelect(String query)
        {
            Console.WriteLine(query);

            return null;
        }
    }
}
