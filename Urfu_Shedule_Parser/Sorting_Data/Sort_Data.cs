using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urfu_Shedule_Parser.Shedule_Pattern;
using System.IO;

namespace Urfu_Shedule_Parser.Sorting_Data
{
    public class Sort_Data
    {
        string _group_name = "";
        ObservableCollection<One_Day_Pattern> _one_day_shedule = new ObservableCollection<One_Day_Pattern>(); // = new List<Shedule_Scheme>();
        ObservableCollection<Lesson_Pattern> _lessons_list_of_day = new ObservableCollection<Lesson_Pattern>();
        public ObservableCollection<Weekly_Shedule_Pattern> Weekly_Shedule = new ObservableCollection<Weekly_Shedule_Pattern>();
        Lesson_Pattern _lesson = new Lesson_Pattern();
        public void Sorting_one_day(List<string> one_group_data, int number_of_lessons_in_day, string group)
        {

            for (int m = 0; m < one_group_data.Count; m++)
            {
                //string data = one_group_data;
                string data = one_group_data[m];
                //File.WriteAllText($"D:\\123\\{index++}.txt", data.Substring(data.IndexOf("Группа "), 25));

                //int Day_Sheldue_StartIndex = data.IndexOf("colspan=\"3\"><b>");
                //int Day_Sheldue_EndIndex = data.IndexOf("<td colspan=\"3\"> </td>");

                string day_sheldue_string; // = day_shedule_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex - 15);
                                           //----------------------------------------------------------------------------------------------
                                           //string[] group_name_split = data.Substring(data.IndexOf("Группа "), 25).Split(' ');
                string _group_name = group; // group_name_split[0] + ' ' + group_name_split[1];


                //----------------------------------------------------------------------------------------------
                if (number_of_lessons_in_day > 0)
                {
                    int DateString_StartIndex = data.IndexOf("<b>");//, data.IndexOf("<tr class=\"divide\">");/* */// = 0;
                    int DateString_EndIndex = data.IndexOf("<td colspan=\"3\">");
                    int Duration_StartIndex = 0;
                    int Duration_EndIndex = 0;
                    int Discipline_EndIndex = 0;
                    Duration_EndIndex = data.IndexOf("<b>"); // data.IndexOf("<b>", DateString_EndIndex);/* */
                    while (DateString_StartIndex > -1 && DateString_EndIndex > -1)
                    //do
                    {
                        for (int i = 0; i < number_of_lessons_in_day; i++)
                        {
                            //----------------------------------------------------------------------------------------------
                            //DateString_StartIndex = data.IndexOf("<b>", DateString_EndIndex);/* */

                            DateString_StartIndex = data.IndexOf("<b>", DateString_EndIndex);
                            if (DateString_StartIndex > -1 && DateString_EndIndex > -1)
                            {
                                DateString_EndIndex = data.IndexOf("</b></td>", DateString_StartIndex);/*\">\n<td colspan=\"3\"> </td>\n</tr>*/
                                //Duration_EndIndex = DateString_EndIndex;

                                _lesson.DateString = data.Substring(DateString_StartIndex + 3, DateString_EndIndex - DateString_StartIndex - 3);
                                //----------------------------------------------------------------------------------------------
                                Duration_StartIndex = data.IndexOf("<td class=\"shedule-weekday-time\">", Duration_EndIndex);
                                if (Duration_StartIndex > -1 && Duration_EndIndex > -1)
                                {
                                    Duration_EndIndex = data.IndexOf("</td>", Duration_StartIndex);
                                    _lesson.Duration = data.Substring(Duration_StartIndex + 33, Duration_EndIndex - Duration_StartIndex - 33);
                                    //---------------------------------------------------------------------------------------------
                                    int Discipline_StartIndex = data.IndexOf("<dd>", Duration_EndIndex);
                                    if (Discipline_StartIndex > -1 && Discipline_EndIndex > -1)
                                    {
                                        Discipline_EndIndex = data.IndexOf("</dd>", Discipline_StartIndex);

                                        _lesson.Discipline = data.Substring(Discipline_StartIndex + 4, Discipline_EndIndex - Discipline_StartIndex - 4);
                                        //---------------------------------------------------------------------------------------------
                                        int LessonType_StartIndex = data.IndexOf("<span class=\"teacher\">", Discipline_EndIndex);
                                        if (LessonType_StartIndex > -1)
                                        {
                                            int LessonType_EndIndex = data.IndexOf("</span>", LessonType_StartIndex);

                                            _lesson.Lesson_Type = data.Substring(LessonType_StartIndex + 22, LessonType_EndIndex - LessonType_StartIndex - 22);
                                            //---------------------------------------------------------------------------------------------
                                            int Chamber_StartIndex = data.IndexOf("<span class=\"cabinet\">", LessonType_EndIndex);
                                            if (Chamber_StartIndex > -1)
                                            {
                                                int Chamber_EndIndex = data.IndexOf("</span>", Chamber_StartIndex);

                                                _lesson.Chamber = data.Substring(Chamber_StartIndex + 22, Chamber_EndIndex - Chamber_StartIndex - 22);
                                                //---------------------------------------------------------------------------------------------
                                                int Teacher_StartIndex = data.IndexOf("<span class=\"teacher\">", Chamber_EndIndex);
                                                int Teacher_EndIndex = data.IndexOf("</span>", Teacher_StartIndex);

                                                _lesson.Teacher = data.Substring(Teacher_StartIndex + 22, Teacher_EndIndex - Teacher_StartIndex - 22);
                                                //---------------------------------------------------------------------------------------------
                                                //---------------------------------------------------------------------------------------------
                                                _lessons_list_of_day.Add(_lesson);

                                                _one_day_shedule.Add(new One_Day_Pattern(_group_name, _lesson.DateString, _lessons_list_of_day));
                                                DateString_StartIndex = data.IndexOf("<b>", Teacher_EndIndex);
                                            }
                                            // splitted_discipline_string = splitted_discipline_string.Where(x => !string.IsNullOrEmpty(x)).ToArray();
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
                                        }

                                    }

                                }
                            }

                        }
                    }


                    Weekly_Shedule.Add(new Weekly_Shedule_Pattern(_group_name, _one_day_shedule));

                    string test_string = "";
                    for (int i = 0; i < Weekly_Shedule.Count; i++)
                    {
                        test_string += Weekly_Shedule[i].Group + "\n";
                        for (int j = 0; j < _one_day_shedule.Count; j++)
                        {
                            test_string += Weekly_Shedule[i].DayPattern_List[j].DateString + "\n";
                            for (int k = 0; k < number_of_lessons_in_day; k++)
                            {
                                test_string += Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Duration + " || " +
                                    Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Chamber + " || " +
                                    Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Chamber + "\n" +
                                    Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Discipline + " || " +
                                    Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Lesson_Type + " || " +
                                    Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Teacher + "\n" +
                                    "-----------------------------------------------------------------------\n" +
                                    "-----------------------------------------------------------------------\n";
                            }
                        }
                    }
                    File.WriteAllText($"D:\\123\\11{m + 20}.txt", test_string);
                }
            }
            

            //int day_shedule_dateString_StartIndex = day_sheldue_string.IndexOf("colspan=\"3\"><b>"/*, strStart_DateIndex*/);

            //            int day_shedule_dateString_EndIndex = day_sheldue_string.IndexOf("</b></td>"/*, day_shedule_dateString_StartIndex + 1*/);

            //            int day_shedule_durationString_StartIndex = day_sheldue_string.IndexOf("<td class=\"shedule-weekday-time\">", day_shedule_dateString_EndIndex);
            //            if (day_shedule_durationString_StartIndex > -1)
            //            {
            //                int day_shedule_durationString_EndIndex = day_sheldue_string.IndexOf("</td>", day_shedule_durationString_StartIndex);

            //                int day_shedule_Discipline_StartIndex = day_sheldue_string.IndexOf("<dd>", day_shedule_durationString_EndIndex);

            //                int day_shedule_Discipline_EndIndex = day_sheldue_string.IndexOf("</dd>", day_shedule_Discipline_StartIndex);

            //                Scheme1.DateString = day_sheldue_string.Substring(day_shedule_dateString_StartIndex + 15, day_shedule_dateString_EndIndex - day_shedule_da





        }
        int counter = 0;
        public ObservableCollection<Lesson_Pattern> Sort_Day_String(string data)
        {
            ObservableCollection<One_Day_Pattern> day_Pattern = new ObservableCollection<One_Day_Pattern>();
            string sorted_string = "";

            int DateString_StartIndex = data.IndexOf("<b>");//, data.IndexOf("<tr class=\"divide\">");/* */// = 0;
            int DateString_EndIndex = data.IndexOf("</b>");

            int Duration_StartIndex = data.IndexOf("<td class=\"shedule-weekday-time\">", DateString_EndIndex);
            int Duration_EndIndex = data.IndexOf("<td class=\"shedule-weekday-time\">") + 20; //data.IndexOf("</td>", Duration_StartIndex);

            int Discipline_StartIndex = data.IndexOf("<dd>", Duration_EndIndex);
            int Discipline_EndIndex = data.IndexOf("</dd>", Discipline_StartIndex);

            int LessonType_StartIndex = data.IndexOf("<span class=\"teacher\">", Discipline_EndIndex);
            int LessonType_EndIndex = data.IndexOf("</span>", LessonType_StartIndex);

            int Chamber_StartIndex = data.IndexOf("<span class=\"cabinet\">", LessonType_EndIndex);
            int Chamber_EndIndex = data.IndexOf("</span>", Chamber_StartIndex);

            int Teacher_StartIndex = data.IndexOf("<span class=\"teacher\">", Chamber_EndIndex);
            int Teacher_EndIndex = data.IndexOf("</span>", Teacher_StartIndex);

            //Duration_EndIndex = data.IndexOf("<b>"); // data.IndexOf("<b>", DateString_EndIndex);/* */

            //----------------------------------------------------------------------------------------------------------

            string day_sheldue_string; // = day_shedule_string.Substring(Day_Sheldue_StartIndex, Day_Sheldue_EndIndex - Day_Sheldue_StartIndex - 15);
                                       //----------------------------------------------------------------------------------------------
                                       //string[] group_name_split = data.Substring(data.IndexOf("Группа "), 25).Split(' ');
            //string _group_name = group; // group_name_split[0] + ' ' + group_name_split[1];


            //----------------------------------------------------------------------------------------------

                while (data.IndexOf("<td class=\"shedule-weekday-time\">", data.IndexOf("<td class=\"shedule-weekday-time\">") + 20) > -1)
                //do
                {
                counter++;
                        //----------------------------------------------------------------------------------------------
                        //DateString_StartIndex = data.IndexOf("<b>", DateString_EndIndex);/* */

                        DateString_StartIndex = data.IndexOf("<b>", DateString_EndIndex);
                        if (DateString_StartIndex > -1 && DateString_EndIndex > -1)
                        {
                            DateString_EndIndex = data.IndexOf("</b></td>", DateString_StartIndex);/*\">\n<td colspan=\"3\"> </td>\n</tr>*/
                            //Duration_EndIndex = DateString_EndIndex;

                            _lesson.DateString = data.Substring(DateString_StartIndex + 3, DateString_EndIndex - DateString_StartIndex - 3);
                    //----------------------------------------------------------------------------------------------
                    Duration_StartIndex = data.IndexOf("<td class=\"shedule-weekday-time\">", Duration_EndIndex);
                            if (Duration_StartIndex > -1 && Duration_EndIndex > -1)
                            {
                                Duration_EndIndex = data.IndexOf("</td>", Duration_StartIndex);
                                _lesson.Duration = data.Substring(Duration_StartIndex + 33, Duration_EndIndex - Duration_StartIndex - 33);
                                //---------------------------------------------------------------------------------------------
                                Discipline_StartIndex = data.IndexOf("<dd>", Duration_EndIndex);
                                if (Discipline_StartIndex > -1 && Discipline_EndIndex > -1)
                                {
                                    Discipline_EndIndex = data.IndexOf("</dd>", Discipline_StartIndex);

                                    _lesson.Discipline = data.Substring(Discipline_StartIndex + 4, Discipline_EndIndex - Discipline_StartIndex - 4);
                                    //---------------------------------------------------------------------------------------------
                                    LessonType_StartIndex = data.IndexOf("<span class=\"teacher\">", Discipline_EndIndex);
                                    if (LessonType_StartIndex > -1)
                                    {
                                        LessonType_EndIndex = data.IndexOf("</span>", LessonType_StartIndex);

                                        _lesson.Lesson_Type = data.Substring(LessonType_StartIndex + 22, LessonType_EndIndex - LessonType_StartIndex - 22);
                                        //---------------------------------------------------------------------------------------------
                                        Chamber_StartIndex = data.IndexOf("<span class=\"cabinet\">", LessonType_EndIndex);
                                        if (Chamber_StartIndex > -1)
                                        {
                                            Chamber_EndIndex = data.IndexOf("</span>", Chamber_StartIndex);

                                            _lesson.Chamber = data.Substring(Chamber_StartIndex + 22, Chamber_EndIndex - Chamber_StartIndex - 22);
                                            //---------------------------------------------------------------------------------------------
                                            Teacher_StartIndex = data.IndexOf("<span class=\"teacher\">", Chamber_EndIndex);
                                            Teacher_EndIndex = data.IndexOf("</span>", Teacher_StartIndex);

                                            _lesson.Teacher = data.Substring(Teacher_StartIndex + 22, Teacher_EndIndex - Teacher_StartIndex - 22);
                                            //---------------------------------------------------------------------------------------------
                                            //---------------------------------------------------------------------------------------------
                                            _lessons_list_of_day.Add(_lesson);

                                            _one_day_shedule.Add(new One_Day_Pattern(_group_name, _lesson.DateString, _lessons_list_of_day));
                                            DateString_StartIndex = data.IndexOf("<b>", Teacher_EndIndex);
                                        }
                                        // splitted_discipline_string = splitted_discipline_string.Where(x => !string.IsNullOrEmpty(x)).ToArray();
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
                                    }

                                }

                            }
                        }
                }
            

                //Weekly_Shedule.Add(new Weekly_Shedule_Pattern(_group_name, _one_day_shedule));

                string test_string = "";
                for (int i = 0; i < Weekly_Shedule.Count; i++)
                {
                    test_string += Weekly_Shedule[i].Group + "\n";
                    for (int j = 0; j < _one_day_shedule.Count; j++)
                    {
                        test_string += Weekly_Shedule[i].DayPattern_List[j].DateString + "\n";
                        for (int k = 0; k < counter; k++)
                        {
                            test_string += Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Duration + " || " +
                                Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Chamber + " || " +
                                Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Chamber + "\n" +
                                Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Discipline + " || " +
                                Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Lesson_Type + " || " +
                                Weekly_Shedule[i].DayPattern_List[j].Get_Lessons[k].Teacher + "\n" +
                                "-----------------------------------------------------------------------\n" +
                                "-----------------------------------------------------------------------\n";
                        }
                    }
                }
                File.WriteAllText($"D:\\123\\22{counter + 20}.txt", test_string);

            //------------------------------------------------------------------------------------------------------------


            return _lessons_list_of_day;//_one_day_shedule;
        }

        public void Sorting_Weekly()
        {

        }




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




        ///////-----------------------------------------------------------------------------------------------------------------------------------------------
        ///  //int Day_Sheldue_StartIndex = test_string.IndexOf("colspan=\"3\"><b>");
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

        //int Discipline_StartIndex = _response_string.IndexOf("<dd>" + 10, strEnd_DurationIndex - 10);
        //int Discipline_EndIndex = _response_string.IndexOf("</dd>", strStart_DurationIndex);


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
}
