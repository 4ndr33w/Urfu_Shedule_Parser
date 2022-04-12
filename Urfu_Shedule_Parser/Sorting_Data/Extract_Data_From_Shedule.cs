using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Threading;

namespace Urfu_Shedule_Parser.Request
{
    public class Extract_Data_From_Shedule
    {
        //Get_Data get_Data = new Get_Data();
        //Shedule_Scheme Scheme1 = new Shedule_Scheme();
        MainWindow Form1 = new MainWindow();
        string _response_string = ""; // get_Data.Return_Request_String();

        public Dictionary<string, int> shedule_collection = new Dictionary<string, int>();
        public List<string> day_shedule = new List<string>();

        List<One_Day_Pattern> shedule_schemes = new List<One_Day_Pattern>();
        string abc = "";
        int index = 10;


        public /*Dictionary<string, int>*/  void Split_request_To_days(string data)
        {
            if (data != String.Empty || data != null)
            {
                //MessageBox.Show(data.Substring(data.IndexOf("<div class=\"shedule-group-title\">" + 33, 17)));
                //File.WriteAllText($"D:\\123\\{index ++}.txt", data);
                //File.WriteAllText($"D:\\123\\{index++}.txt", data.Substring(data.IndexOf("Группа "), 25));
                _response_string = data;
                day_shedule.Add(_response_string);
                string day_shedule_string = day_shedule[day_shedule.Count - 1];
                int Day_Sheldue_StartIndex = 0; // day_shedule_string.IndexOf("<b>");
                int Day_Sheldue_EndIndex = Day_Sheldue_StartIndex + 1;
                string day_sheldue_string = "";
                int counter = 0;

                int strStart_DurationIndex = 0;

                int counter_of_disciplines_by_day = 0;


                //await Task.Run(() =>
                //{
                string _shedule = "";
                do
                {
                    for (int i = day_shedule_string.IndexOf("<b>"); i < day_shedule_string.Length - 100; i++)
                    {
                        counter++;
                        Day_Sheldue_StartIndex = day_shedule_string.IndexOf("<b>", i);
                        i = Day_Sheldue_StartIndex;

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
                        test.Sorting_one_day(_response_string, counter);

                    }
                }
                while (Day_Sheldue_StartIndex > -1 && Day_Sheldue_EndIndex > Day_Sheldue_StartIndex);
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
