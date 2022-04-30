using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Urfu_Shedule_Parser.Display_Data_From_DB
{
    internal class DB_Display
    {
        Saving_Data.Data_Base_Class DB_Connect = new Saving_Data.Data_Base_Class();

        ObservableCollection<string[]> data = new ObservableCollection<string[]>();

        //public ObservableCollection<string[]> Collection_Fill ()
        //{
        //    var _connection = DB_Connect.sql_connection_return();
        //    _connection.Open ();

        //    DataSet _dataSet = new DataSet();
        //    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Shedule", _connection);
        //    adapter.Fill(_dataSet, "_shedule");

        //    string[] _row = new string[_dataSet.Tables["_shedule"].Columns.Count];
        //    for (int i = 0; i < _dataSet.Tables["_shedule"].Rows.Count; i++)
        //    {
        //        for (int j = 0; j < _dataSet.Tables["_shedule"].Columns.Count; j++)
        //        {
        //            _row.Append(_dataSet.Tables["_shedule"].Rows[j].ToString());
        //        }
        //        data.Add(_row);
        //    }
        //    _connection.Close();
        //    return data;
        //}

        public DataTable DB_Table()
        {
            var _connection = DB_Connect.sql_connection_return();
            _connection.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Shedule", _connection);
            DataSet _dataSet = new DataSet();
            adapter.Fill(_dataSet, "_shedule");
            dt = _dataSet.Tables["_shedule"];
            _connection.Close();
            return dt;
        }
    }
}
