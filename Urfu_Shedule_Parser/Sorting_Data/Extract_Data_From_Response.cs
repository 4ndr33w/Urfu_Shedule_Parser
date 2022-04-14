using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using Urfu_Shedule_Parser.Shedule_Pattern;

namespace Urfu_Shedule_Parser.Request
{
    public class Extract_Data_From_Response
    {
        MainWindow Form1 = new MainWindow();
        string _response_string = ""; // get_Data.Return_Request_String();
        public ObservableCollection<Weekly_Shedule_Pattern> Weekly_Shedule = new ObservableCollection<Weekly_Shedule_Pattern>();


        public Dictionary<string, int> shedule_collection = new Dictionary<string, int>();
        public List<string> day_shedule = new List<string>();

        ObservableCollection<One_Day_Pattern> daily_shedule = new ObservableCollection<One_Day_Pattern>();
        string abc = "";
        int index = 10;
        string _group_name = "";


        public /*Dictionary<string, int>*/  void Split_request_To_days(string data)
        {
            if (data != String.Empty || data != null)
            {
                string _response = data;
            }
        }

        public List<string> Get_Splitted_Data()
        {
            for (int i = 0; i < day_shedule.Count; i++)
            {
                abc += day_shedule[i].ToString();
                abc += "\n------------------------------------\n";
            }
            //MessageBox.Show(day_shedule.Count.ToString());
            //MessageBox.Show(_response_string.Substring(_response_string.IndexOf("<div class=\"shedule-group-title\">" + 33, 17)));
            //File.WriteAllText(@"D:\00001.txt", abc);
            return day_shedule;
            
        }
    }
}
