using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Urfu_Shedule_Parser.Display_Data_From_DB
{
    internal class DB_Display
    {
        Saving_Data.Data_Base_Class DB_Connect = new Saving_Data.Data_Base_Class();

        ObservableCollection<string[]> data = new ObservableCollection<string[]>();

        public DataTable DB_Table(string sql_string)
        {
            DataTable dt = new DataTable();
            DataSet _dataSet = new DataSet();
            try
            {
                var _connection = DB_Connect.sql_connection_return();
                _connection.Open();
                
                SqlCommand command = new SqlCommand(sql_string, _connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command/*"SELECT * FROM Shedule"*/);

                command.ExecuteNonQuery();
                
                adapter.Fill(_dataSet, "_shedule");
                dt = _dataSet.Tables["_shedule"];
                _connection.Close();
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
