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
                _response_string = data;
                day_shedule.Add(_response_string);
                string _date_string = data.Substring(data.IndexOf("<b>") + 3, data.IndexOf("</b>") - data.IndexOf("<b>") - 3);
                string day_shedule_string = day_shedule[day_shedule.Count - 1];
                int Day_Sheldue_StartIndex = 0; // day_shedule_string.IndexOf("<b>");
                int Day_Sheldue_EndIndex = Day_Sheldue_StartIndex + 1;

                string[] group_name_split = _response_string.Substring(data.IndexOf("Группа "), 25).Split(' ');
                _group_name = group_name_split[0] + ' ' + group_name_split[1];
                string day_sheldue_string = "";
                int counter = 0;

                int strStart_DurationIndex = 0;

                int counter_of_disciplines_by_day = 0;

                string _shedule = "";
                do
                {
                    for (int i = day_shedule_string.IndexOf("<b>"); i < day_shedule_string.Length - 100; i++)
                    {
                        counter++;
                        
                        Day_Sheldue_StartIndex = day_shedule_string.IndexOf("<b>", i);
                        i = Day_Sheldue_StartIndex + 5;

                        if (Day_Sheldue_StartIndex < 0 && i < 0) return;
                        Day_Sheldue_EndIndex = day_shedule_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex/*"<td colspan=\"3\"> </td>"*/);

                        day_sheldue_string = day_shedule_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex);
                        day_sheldue_string += "\n--------------------------------------------------------------------------------------------------\n" +
                            "\n--------------------------------------------------------------------------------------------------\n";

                        day_shedule.Add(day_sheldue_string);
                        _shedule += day_sheldue_string;
                        Day_Sheldue_StartIndex = day_shedule_string.IndexOf("<b>", Day_Sheldue_EndIndex);
                        //abc += day_shedule[day_shedule.Count - 1].ToString();
                        //abc += "\n------------------------------------\n";

                        Sorting_Data.Sort_Data test = new Sorting_Data.Sort_Data();
                        daily_shedule.Add(new One_Day_Pattern(_group_name, _date_string, test.Sort_Day_String(day_sheldue_string)));// test.Sort_Day_String(day_sheldue_string);

                    }
                }
                while (Day_Sheldue_StartIndex > -1 && Day_Sheldue_EndIndex > Day_Sheldue_StartIndex);
                Weekly_Shedule.Add(new Weekly_Shedule_Pattern(_group_name, daily_shedule));
            }
            else return;
           
            //});
            //MessageBox.Show(_response_string.Substring(_response_string.IndexOf("<div class=\"shedule-group-title\">" + 33, 17)));
            //File.WriteAllText(@"D:\00002.txt", abc);
            //MessageBox.Show(/*day_shedule.Count*/counter.ToString());
            //MessageBox.Show(day_shedule.Count.ToString());
            //return day_shedule;



            //string test = "";

            //foreach (var item in day_sheldue_string)
            //{
            //    test += item;
            //    test += "\n----------------------------------------------------------------------\n";
            //}

            //File.WriteAllText(@"D:\124.txt", data);



            //return shedule_collection;
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
