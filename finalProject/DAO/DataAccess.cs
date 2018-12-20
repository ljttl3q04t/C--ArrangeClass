using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace finalProject.DAO
{
    class DataAccess
    {
        public static SqlConnection getConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["FinalProjectPRNConnectString"].ToString();
            SqlConnection myConnection = new SqlConnection(conString);
            return myConnection;
        }

        public static DataTable getData(string sql) // select
        {
            SqlCommand myCommand = new SqlCommand(sql, getConnection());
            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = myCommand;
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            return ds.Tables[0];
        }

        public static int executeSQL(string sql) // update, insert, delete
        {
            SqlCommand myCommand = new SqlCommand(sql, getConnection());
            myCommand.Connection.Open();
            int count = myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();
            return count;
        }
    }
}
