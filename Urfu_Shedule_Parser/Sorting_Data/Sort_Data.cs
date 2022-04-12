using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urfu_Shedule_Parser.Shedule_Pattern;

namespace Urfu_Shedule_Parser.Sorting_Data
{
    public class Sort_Data
    {
        List<Shedule_Scheme> _schemes; // = new List<Shedule_Scheme>();
        Lesson_Pattern _lesson;
        public void Sorting(string one_group_data)
        {
            string data = one_group_data;
            //File.WriteAllText($"D:\\123\\{index++}.txt", data.Substring(data.IndexOf("Группа "), 25));

            //int Day_Sheldue_StartIndex = data.IndexOf("colspan=\"3\"><b>");
            //int Day_Sheldue_EndIndex = data.IndexOf("<td colspan=\"3\"> </td>");

            string day_sheldue_string; // = day_shedule_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex - 15);
            //----------------------------------------------------------------------------------------------
            string[] group_name_split = data.Substring(data.IndexOf("Группа "), 25).Split(' ');
            string _group_name = group_name_split[0] + ' ' + group_name_split[1];
            //----------------------------------------------------------------------------------------------
            //----------------------------------------------------------------------------------------------
            int DateString_StartIndex = data.IndexOf("</div>") + 5;/* */
            int DateString_EndIndex = data.IndexOf("</b></td>", DateString_StartIndex);/*\">\n<td colspan=\"3\"> </td>\n</tr>*/
            //----------------------------------------------------------------------------------------------
            int Duration_StartIndex = data.IndexOf("<td class=\"shedule-weekday-time\">", DateString_EndIndex);
            int Duration_EndIndex = data.IndexOf("</td>", Duration_StartIndex);
            //---------------------------------------------------------------------------------------------
            int strStart_DisciplineIndex = data.IndexOf("<dd>" + 10, Duration_EndIndex - 10);
            int strEnd_DisciplineIndex = data.IndexOf("</dd>", Duration_StartIndex);

            //int DateString_StartIndex = data.IndexOf("<td colspan=\"3\"><b>"/*, DateString_StartIndex*/);     /*\">\n<td colspan = \"3\"><b>*/

        //private string _date_string;
        //private string _duration;
        //private string _teacher_name;
        //private string _chamber;
        //private string _discipline_name;






    }




        ////do
        ////{
        //Day_Sheldue_StartIndex = day_shedule_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex + 22);
        //Day_Sheldue_EndIndex = day_shedule_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex);
        //Day_Sheldue_StartIndex = day_shedule_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex + 22);
        //Day_Sheldue_EndIndex = day_shedule_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex);
        //Day_Sheldue_StartIndex = day_shedule_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex + 22);
        //Day_Sheldue_EndIndex = day_shedule_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex);
        //day_sheldue_string = day_shedule_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex);

        //if (day_sheldue_string.Length > -1)
        //{
        //    if (day_sheldue_string.IndexOf("<td class=\"shedule-weekday-time\">") > -1)
        //    {
        //        //do
        //        //{
        //        do
        //        {
        //            int day_shedule_dateString_StartIndex = day_sheldue_string.IndexOf("colspan=\"3\"><b>"/*, DateString_StartIndex*/);

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
        //                Day_Sheldue_StartIndex = day_shedule_string.IndexOf("colspan=\"3\"><b>", Day_Sheldue_EndIndex);
        //                if (Day_Sheldue_StartIndex > -1) { Day_Sheldue_EndIndex = day_shedule_string.IndexOf("<td colspan=\"3\"> </td>", Day_Sheldue_StartIndex); }


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
}
