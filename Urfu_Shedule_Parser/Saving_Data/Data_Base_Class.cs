using System;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Urfu_Shedule_Parser.Saving_Data
{
    internal class Data_Base_Class
    {
        SqlConnection sql_Connection = new SqlConnection(Properties.Resources.ConnectionString);

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
                { }
            });
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
