using System;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Urfu_Shedule_Parser.Saving_Data
{
    internal class Data_Base_Class
    {
        public static MySqlConnectionStringBuilder _connectionString_Builder = new MySqlConnectionStringBuilder
        {
            Server = Properties.Resources.MySQL_Beget_Server,
            Database = Properties.Resources.MySQL_DB_Name,
            UserID = Properties.Resources.MySQL_DB_Name,
            Password = Properties.Resources.MySQL_pass, 
            //CharacterSet = Properties.Resources.MySQL_Charset
        };
        //string _connectionString = Properties.Resources.MySQL_ConnectionString; /*_connectionString_Builder.ConnectionString;*/ //Properties.Resources.ConnectionString;
        MySqlConnection sql_Connection = new MySqlConnection(_connectionString_Builder.ConnectionString);
        //SqlConnection sql_Connection = new SqlConnection(Properties.Resources.ConnectionString);

        //public void Sql_Connection_Method()
        //{
        //    Task.Run(() =>
        //    {
        //        try
        //        {
        //            sql_Connection.Open();
        //        }
        //        catch (Exception)
        //        {
        //            return;
        //        }
        //        finally
        //        { }
        //    });
        //}
        public void Sql_Connection_Method()
        {
            //_connectionString_Builder.Password = Properties.Resources.MySQL_pass;
            //_connectionString_Builder.Pooling = true;
            //_connectionString_Builder.Server = Properties.Resources.MySQL_IP;
            //_connectionString_Builder.UserID = Properties.Resources.MySQL_UserID;
            //_connectionString_Builder.Database = Properties.Resources.TableName;
            //_connectionString_Builder.


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
                { }
            });
        }

        //public SqlConnection sql_connection_return ()
        //{
        //    return sql_Connection;
        //}
        public MySqlConnection sql_connection_return()
        {
            return sql_Connection;
        }

        public void Sql_Table_Fill (string data)
        {

        }
    }
}
