using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Urfu_Shedule_Parser.Display_Data_From_DB
{
    internal class DB_Display
    {
        Saving_Data.Data_Base_Class DB_Connect = new Saving_Data.Data_Base_Class();

        ObservableCollection<string[]> data = new ObservableCollection<string[]>();
        public bool SqlConnectionState_Check(MySqlConnection connection)
        {
            //TimeEvent();

            if (connection.State == System.Data.ConnectionState.Open) return true;
            else return false;
        }

        public DataTable DB_Table(string sql_string)
        {
            DataTable dt = new DataTable();
            DataSet _dataSet = new DataSet();
            try
            {
                Saving_Data.Data_Base_Class _DB_connection = new Saving_Data.Data_Base_Class();
                Sorting_Data.Sort_Data _sort = new Sorting_Data.Sort_Data();
                MySqlConnection _connection = DB_Connect.sql_connection_return();
                //var _connection = DB_Connect.sql_connection_return();
                
               // Task.Run(() =>
               //{
                   _connection.Open();
                   while (SqlConnectionState_Check(_connection) == false)
                   {
                       //_connection.Open();
                       SqlConnectionState_Check(_connection);
                       Thread.Sleep(100);
                   }
                   //MessageBox.Show(_connection.State.ToString());
                   MySqlCommand _command = new MySqlCommand(sql_string, DB_Connect.sql_connection_return());
                   MySqlDataAdapter _adapter = new MySqlDataAdapter(_command);

                   //SqlCommand command = new SqlCommand(sql_string, _connection);
                   //SqlDataAdapter adapter = new SqlDataAdapter(command/*"SELECT * FROM Shedule"*/);

                   _command.ExecuteNonQuery();

                   _adapter.Fill(_dataSet, "shedule");
                   dt = _dataSet.Tables["shedule"];
                   _connection.Close();

               //});
                 

              
            }
            catch (System.Exception)
            {
                MessageBox.Show("проблемы с подключением");
                return dt;
            }
           
            return dt;
        }
    }
}
