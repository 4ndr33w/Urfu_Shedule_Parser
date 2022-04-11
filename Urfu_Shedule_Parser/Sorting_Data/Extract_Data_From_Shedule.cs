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
        string abc = "";


        public /*Dictionary<string, int>*/  async void Split_request_To_days(string data)
        {
            _response_string = data;
            string test_string = _response_string;
            int Day_Sheldue_StartIndex = 0; // test_string.IndexOf("<b>");
            int Day_Sheldue_EndIndex = Day_Sheldue_StartIndex + 1;
            string day_sheldue_string = "";
            int counter = 0;

            int strStart_DurationIndex = 0;

            int counter_of_disciplines_by_day = 0;
            

            await Task.Run(() =>
            {

                do
                {
                    for (int i = test_string.IndexOf("<b>"); i < test_string.Length - 150; i++)
                    {
                        counter++;
                        Day_Sheldue_StartIndex = test_string.IndexOf("<b>", i);
                        i = Day_Sheldue_StartIndex;

                        if (Day_Sheldue_StartIndex < 0 && i < 0) return;
                        Day_Sheldue_EndIndex = test_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex/*"<td colspan=\"3\"> </td>"*/);

                        day_sheldue_string = test_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex);

                        day_shedule.Add(day_sheldue_string);
                        Day_Sheldue_StartIndex = test_string.IndexOf("<b>", Day_Sheldue_EndIndex);
                        //abc += day_shedule[day_shedule.Count - 1].ToString();
                        //abc += "\n------------------------------------\n";

                    }
                }
                while (Day_Sheldue_StartIndex > -1 && Day_Sheldue_EndIndex > Day_Sheldue_StartIndex);
            });
            //File.WriteAllText(@"D:\00000.txt", abc);
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



            //int Day_Sheldue_StartIndex = test_string.IndexOf("colspan=\"3\"><b>");
            //int Day_Sheldue_EndIndex = test_string.IndexOf("<td colspan=\"3\"> </td>");

            //string day_sheldue_string; // = test_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex - 15);

            //int strStart_DateIndex = getRequest.Response.IndexOf("</div>") + 2;/* */
            //string[] trimmed_string = getRequest.Response.Trim("</dt>\n</dl>\n</td>\n</tr>");


            //----------------------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------------------

            //int strStart_DateIndex = _response_string.IndexOf("<td colspan=\"3\"><b>"/*, strStart_DateIndex*/);     /*\">\n<td colspan = \"3\"><b>*/
            //int strEnd_DateIndex = _response_string.IndexOf("</b></td>", strStart_DateIndex);/*\">\n<td colspan=\"3\"> </td>\n</tr>*/

            //strStart_DurationIndex = _response_string.IndexOf("<td class=\"shedule-weekday-time\">", strEnd_DateIndex);
            //int strEnd_DurationIndex = _response_string.IndexOf("</td>", strStart_DurationIndex);

            //int strStart_DisciplineIndex = _response_string.IndexOf("<dd>" + 10, strEnd_DurationIndex - 10);
            //int strEnd_DisciplineIndex = _response_string.IndexOf("</dd>", strStart_DurationIndex);


            ////do
            ////{
            //Day_Sheldue_StartIndex = test_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex + 22);
            //Day_Sheldue_EndIndex = test_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex);
            //Day_Sheldue_StartIndex = test_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex + 22);
            //Day_Sheldue_EndIndex = test_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex);
            //Day_Sheldue_StartIndex = test_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex + 22);
            //Day_Sheldue_EndIndex = test_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex);
            //day_sheldue_string = test_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex);

            //if (day_sheldue_string.Length > -1)
            //{
            //    if (day_sheldue_string.IndexOf("<td class=\"shedule-weekday-time\">") > -1)
            //    {
            //        //do
            //        //{
            //        do
            //        {
            //            int day_shedule_dateString_StartIndex = day_sheldue_string.IndexOf("colspan=\"3\"><b>"/*, strStart_DateIndex*/);

            //            int day_shedule_dateString_EndIndex = day_sheldue_string.IndexOf("</b></td>"/*, day_shedule_dateString_StartIndex + 1*/);

            //            int day_shedule_durationString_StartIndex = day_sheldue_string.IndexOf("<td class=\"shedule-weekday-time\">", day_shedule_dateString_EndIndex);
            //            if (day_shedule_durationString_StartIndex > -1)
            //            {
            //                int day_shedule_durationString_EndIndex = day_sheldue_string.IndexOf("</td>", day_shedule_durationString_StartIndex);

            //                int day_shedule_Discipline_StartIndex = day_sheldue_string.IndexOf("<dd>", day_shedule_durationString_EndIndex);

            //                int day_shedule_Discipline_EndIndex = day_sheldue_string.IndexOf("</dd>", day_shedule_Discipline_StartIndex);

            //                Scheme1.DateString = day_sheldue_string.Substring(day_shedule_dateString_StartIndex + 15, day_shedule_dateString_EndIndex - day_shedule_dateString_StartIndex - 15);
            //                Scheme1.Duration = day_sheldue_string.Substring(day_shedule_durationString_StartIndex + 33, day_shedule_durationString_EndIndex - day_shedule_durationString_StartIndex - 33);
            //                string[] splitted_discipline_string = day_sheldue_string.Substring(day_shedule_Discipline_StartIndex + 4, day_shedule_Discipline_EndIndex - day_shedule_Discipline_StartIndex - 4).Split('\n');
            //                splitted_discipline_string = splitted_discipline_string.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //                splitted_discipline_string = splitted_discipline_string.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            //                List<string> Discipline_remove_spaces = new List<string>();
            //                for (int i = 0; i < splitted_discipline_string.Length; i++)
            //                {
            //                    Discipline_remove_spaces.Add(splitted_discipline_string[i]);
            //                }
            //                //for (int i = 1; i < Discipline_remove_spaces.Count; i++)
            //                //{
            //                //    Scheme1.Discipline += Discipline_remove_spaces[i] + ' ';
            //                //}
            //                Scheme1.Discipline = Discipline_remove_spaces[1];
            //                Day_Sheldue_StartIndex = test_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex);
            //                if (Day_Sheldue_StartIndex > -1) { Day_Sheldue_EndIndex = test_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex); }


            //                Form1.shedule_collection.Add(Scheme1);
            //                //MessageBox.Show(Scheme1.DateString + "\n" + Scheme1.Duration + "\n" + Scheme1.Discipline + "\n" + Discipline_remove_spaces.Count);
            //            }
            //            File.WriteAllText(@"D:\test.txt", Scheme1.ToString());
            //        }
            //        while (day_sheldue_string.IndexOf("<td class=\"shedule-weekday-time\">") > -1);
            //        //}
            //        //while (Day_Sheldue_StartIndex > -1);

            //    }
            //}
        }

        public List<string> Get_Splitted_Data()
        {
            for (int i = 0; i < day_shedule.Count; i++)
            {
                abc += day_shedule[i].ToString();
                abc += "\n------------------------------------\n";
            }
            MessageBox.Show(day_shedule.Count.ToString());

            File.WriteAllText(@"D:\00001.txt", abc);
            return day_shedule;
            
        }
    }
}
