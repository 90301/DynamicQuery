using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicQuery
{
    public class QueryHolder
    {
        /// <summary>
        /// Holds information to create a sql query
        /// Key is object name or collection name [Usually statically defined once per object]
        /// 
        /// Attempt to make "Type"
        /// </summary>
        public static Dictionary<String,QueryMetadata> QueryMetadata = new Dictionary<string, QueryMetadata>();
    }


    public class QueryMetadata
    {
        /// <summary>
        /// A list of all columns.
        /// Key is the actual variable name such as SSN
        /// </summary>
        public Dictionary<String,ColVariable> colInfo = new Dictionary<string, ColVariable>();
        /// <summary>
        /// Holds Join Conditions for larger selects across multiple
        /// </summary>
        public Dictionary<String, JoinTable> JoinTables { get; set; }

        /// <summary>
        /// First Table
        /// </summary>
        public string BaseTable { get; set; }

    }

    public class JoinTable
    {
        public QueryMetadata Metadata { get; set; }
        /// <summary>
        /// The database table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// [OPTIONAL]
        /// The table's nickname, used to reference this table
        /// Default is Accending Letters (Max default joins limited to 26 tables)
        /// </summary>
        public string TableNickname { get; set; }

        public string JoinType { get; set; }

        public List<string> JoinConditions { get; set; }

        //LIST OF JOIN CONSTANTS
        public const string LEFT_JOIN = "Left Join";
        public const string RIGHT_JOIN = "Right Join";
        public const string INNER_JOIN = "Inner Join";

        //Join Conditions

        //Basic Join Conditions
        /// <summary>
        /// The simplist way of adding a join condition
        /// This ONLY adds the text. if you need a non-supported join condition
        /// ONLY add the condition,do not include keywords like ON.
        /// Default functionality is "AND" for multiple conditions
        /// You must take into account table / join table nicknames (EX:)
        /// [JoinTableNickname].[JoinVariableName] = [BaseTableNickname].[BaseVariableName]
        /// </summary>
        /// <param name="joinCondition"></param>
        public  void addPlainJoinCondition(String joinCondition)
        {
            if (!this.JoinConditions.Contains(joinCondition))
            {
                this.JoinConditions.Add(joinCondition);
            }
        }

        /// <summary>
        /// Joins based on the base query having the same col value as this
        /// table. Similar to NATRUAL JOIN column names must be the same to use this
        /// </summary>
        /// <param name="colName"></param>
        public void addSameColJoin(String colName)
        {
            String joinCondition = "";

            joinCondition +=  Metadata.BaseTable + "." + colName + " = " + this.TableName + "." + colName;

            JoinConditions.Add(joinCondition);
        }
    }
}

