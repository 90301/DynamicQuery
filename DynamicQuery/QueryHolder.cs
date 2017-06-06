using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DynamicQuery
{
    public class QueryHolder
    {
        public static QuerySelectDelegate howDoISelect { get; set; }
        /// <summary>
        /// Holds information to create a sql query
        /// Key is object name or collection name [Usually statically defined once per object]
        /// 
        /// Attempt to make "Type"
        /// </summary>
        public static Dictionary<String,QueryMetadata> QueryMetadata = new Dictionary<string, QueryMetadata>();
    }

    public delegate DataTable QuerySelectDelegate(String query);


    public class QueryMetadata
    {
        /// <summary>
        /// A list of all columns.
        /// Key is the actual variable name such as SSN
        /// </summary>
        public Dictionary<String,ColVariable> colInfo = new Dictionary<string, ColVariable>();


        /// <summary>
        /// Holds Join Conditions for larger selects across multiple tables
        /// </summary>
        public Dictionary<String, JoinTable> JoinTables { get; set; }

        /// <summary>
        /// First Table
        /// </summary>
        public string BaseTable { get; set; }

        public  string ObjName { get; set; }


        public QueryMetadata(string objName, string baseTable)
        {
            this.ObjName = objName;
            this.BaseTable = baseTable;
        }

        public void addCol(ColVariable colVariable)
        {
            colInfo[colVariable.ColName] = colVariable;
        }

        public T getCol<T>(T realVariable, ColVariable nameColVariable)
        {
            if (realVariable != null)
            {
                //Value already exists, return the value
                return realVariable;
            }
            else
            {
                //Variable doesn't exist. May be null, or col might not have been selected yet.
                if (nameColVariable.Selected)
                {
                    //Variable has been selected, value is just null
                    return realVariable;
                }
                else
                {
                    //variable hasn't been selected, Do a basic select and add to Query Metadata selected columns
                    String query = SelectSingleCol(nameColVariable);


                }
            }
        }

        public string SelectSingleCol(ColVariable colVariable)
        {
            String queryOutput = "SELECT " + colVariable.ColName;
            queryOutput += " FROM " + getFullTable();

            return queryOutput;
        }

        /// <summary>
        /// Gets the base table with all joins
        /// Another method may be added later that gets the efficient table
        /// with joins only occuring where it is relevant.
        /// </summary>
        /// <returns></returns>
        public string getFullTable()
        {
            String queryOutput = BaseTable;
            foreach (JoinTable joinTable in JoinTables.Values)
            {
                queryOutput += joinTable.TableName + joinTable.getConditions();
            }
            return queryOutput;

        }

        public string getEfficentTable()
        {
            //NOT YET IMPLEMENTED
            return "GET EFFICIENT TABLE NOT YET IMPLEMENTED";
        }
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

        public string getConditions()
        {
            String queryJoinConditions = "ON ";
            foreach (String joinCondition in JoinConditions)
            {
                queryJoinConditions += joinCondition + " ";
            }

            return queryJoinConditions;
        }
    }
}

