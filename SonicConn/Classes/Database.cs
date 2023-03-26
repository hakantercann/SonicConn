using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicConn.Classes
{
    public static class Database
    {
        public static SqlConnection GetDatabaseName() 
        {
            string cn_String = Properties.Settings.Default.MonopolyChatConnectionString;

            SqlConnection cn_connection = new SqlConnection(cn_String);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            //</ db oeffnen >


            //< output >

            return cn_connection;
        }

    }
}
