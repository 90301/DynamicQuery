using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicQuery
{
    /// <summary>
    /// Holds metadata for a column / Variable
    /// </summary>
    public class ColVariable
    {
        public QueryMetadata Qmd;

        /// <summary>
        /// The name of the table's column name
        /// </summary>
        public string ColName { get; set; }

        public Boolean Selected { get; set; } = false;


        public ColVariable(string colName, QueryMetadata qmd)
        {
            this.ColName = colName;
            this.Qmd = qmd;

            this.Qmd.addCol(this);
        }

    }
}
