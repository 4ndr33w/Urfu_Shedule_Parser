using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows;
//using System.Threading.Tasks;

namespace Urfu_Shedule_Parser.Saving_Data
{
    internal class Data_Base_Class
    {
        //string connection_String = Properties.Resources.ConnectionString;
        SqlConnection sql_Connection = new SqlConnection(Properties.Resources.ConnectionString);
        //MySqlConnection connection = new MySqlConnection(Properties.Resources.ConnectionString);

        public void Sql_Connection_Method()
        {
            

            Task.Run(() =>
            {
                try
                {
                    sql_Connection.Open();
                }
                catch (Exception)
                {
                    return;
                }
                finally
                {
                    //MessageBox.Show(sql_Connection.State.ToString());
                    //return sql_Connection;
                }
            });
            //return sql_Connection;
        }

        public SqlConnection sql_connection_return ()
        {
            return sql_Connection;
        }

        public void Sql_Table_Fill (string data)
        {

        }
    }
}
