using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicQuery
{
    /// <summary>
    /// Holds metadata for a column / Variable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColVariable
    {
        /// <summary>
        /// The name of the object that holds this variable
        /// </summary>
        public string objectName { get; set; }
        /// <summary>
        /// The name of the table's column name
        /// </summary>
        public string colName { get; set; }

        /// <summary>
        /// the table the variable comes from
        /// Only important when dealing with joins
        /// </summary>
        public string tableName { get; set; }

        //public  T objValue { get; set; }

        public ColVariable(String objName,String tableName,String colName)
        {
            this.objectName = objName;
            this.tableName = tableName;
            this.colName = colName;
        }
    }
}
