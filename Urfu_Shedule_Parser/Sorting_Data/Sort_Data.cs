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
        string _response;
        ObservableCollection<One_Day_Pattern> _one_day_shedule = new ObservableCollection<One_Day_Pattern>(); // = new List<Shedule_Scheme>();
        ObservableCollection<Lesson_Pattern> _lessons_list_of_day = new ObservableCollection<Lesson_Pattern>();
        public ObservableCollection<Weekly_Shedule_Pattern> Groups_Shedule_Collection = new ObservableCollection<Weekly_Shedule_Pattern>();
        public Weekly_Shedule_Pattern Week_Shedule_List = new Weekly_Shedule_Pattern();
        Lesson_Pattern _lesson = new Lesson_Pattern();
        List<string> _raw_shedule_strings__splittet_by_days = new List<string>();
        One_Day_Pattern _one_day = new One_Day_Pattern();
        string test = "";
        int _test_counter = 0;

        public /*ObservableCollection<*/Weekly_Shedule_Pattern/*>*/ Weekly_Shedule_Sort(string data)
        {
            _response = data;
            string[] group_splitted = _response.Substring(_response.IndexOf("Группа "), 25).Split(' ');
            _group_name = group_splitted[0] + ' ' + group_splitted[1];
            

            int One_Day_List_StartIndex = data.IndexOf("<b>");
            int One_Day_List_EndIndex = data.IndexOf("<td colspan=\"3\"> </td>", One_Day_List_StartIndex);
            

            while (One_Day_List_StartIndex > -1 && One_Day_List_EndIndex > One_Day_List_StartIndex)
            {
                string _one_day_string = _response.Substring(One_Day_List_StartIndex, One_Day_List_EndIndex - One_Day_List_StartIndex);
                One_Day_List_StartIndex = data.IndexOf("<td colspan=\"3\"><b>", One_Day_List_EndIndex);
                if (One_Day_List_StartIndex > 0)
                {
                    One_Day_List_EndIndex = data.IndexOf("<td colspan=\"3\"> </td>", One_Day_List_StartIndex);

                    _raw_shedule_strings__splittet_by_days.Add(_one_day_string);

                    int StartIndex = _one_day_string.IndexOf("<b>"); // > -1 ? _day.IndexOf("<b>") : -1;
                    int EndIndex = _one_day_string.IndexOf("</b>");
                    string _date_string = _one_day_string.Substring(StartIndex + 3, EndIndex - StartIndex - 3);
                }
                
                //if (Dayly_Shedule_Sort(_one_day) != new ObservableCollection<One_Day_Pattern>())
                //{
                //    Week_Shedule_List = new Weekly_Shedule_Pattern(_group_name, Dayly_Shedule_Sort(_one_day));
                //    //Groups_Shedule_Collection.Add(new Weekly_Shedule_Pattern(_group_name, Dayly_Shedule_Sort(_one_day)));
                //}
                ////One_Day_List_StartIndex = data.IndexOf("<td colspan=\"3\"><b>", One_Day_List_StartIndex);
                //_response = data.Substring(One_Day_List_EndIndex);
                //_one_day = _response.Substring(One_Day_List_StartIndex, One_Day_List_EndIndex - One_Day_List_StartIndex);

                //string test = "";
                //One_Group_Shedule = sort_shedule.Weekly_Shedule_Sort(run_test.get_data()[i]);

                //for (int k = 0; k < Week_Shedule_List.DayPattern_List.Count; k++)
                //{
                //    test += Week_Shedule_List.Group + "\n";
                //    test += Week_Shedule_List.DayPattern_List[k].DateString + "\n";

                //    for (int m = 0; m < Week_Shedule_List.DayPattern_List[k].Get_Lessons.Count; m++)
                //    {
                //        test += Week_Shedule_List.DayPattern_List[k].Get_Lessons[m].Duration + "  ||  ";
                //        test += Week_Shedule_List.DayPattern_List[k].Get_Lessons[m].Discipline + "\n";
                //        test += Week_Shedule_List.DayPattern_List[k].Get_Lessons[m].Chamber + "  ||  ";
                //        test += Week_Shedule_List.DayPattern_List[k].Get_Lessons[m].Teacher + "\n";
                //        test += Week_Shedule_List.DayPattern_List[k].Get_Lessons[m].Lesson_Type + "\n";
                //        test += "-------------------------------------------------------------------";
                //        test += "-------------------------------------------------------------------";
                //    }
                //}

                //File.WriteAllText(@"D:\123\777.txt", test);

            }
            
            foreach (var item in _raw_shedule_strings__splittet_by_days)
            {

                _one_day_shedule.Add(new One_Day_Pattern(Dayly_Shedule_Sort(item)));
            }
            File.WriteAllText(@"D:\123\999.txt", test);
            return Week_Shedule_List = new Weekly_Shedule_Pattern(_group_name, _one_day_shedule); //Groups_Shedule_Collection;
        }

        private /*ObservableCollection<*/One_Day_Pattern/*>*/ Dayly_Shedule_Sort(string data)
        {
            string _day = data;
            int StartIndex = _day.IndexOf("<b>"); // > -1 ? _day.IndexOf("<b>") : -1;
            int EndIndex = _day.IndexOf("</b>");
            string _date_string = _day.Substring(StartIndex + 3, EndIndex - StartIndex - 3);

            if (EndIndex - StartIndex > 0)
            {
                _lesson.DateString = _day.Substring(StartIndex + 3, EndIndex - StartIndex - 3);
                test += _lesson.DateString + "\n";
                string _date_string_ = _lesson.DateString;
                StartIndex = _day.IndexOf("</b></td>");
                //while (StartIndex > -1/* && StartIndex < _day.Length && EndIndex > StartIndex && EndIndex > 0*/)
                //{
                    //EndIndex = data.IndexOf("<td colspan=\"3\"><b>");
                //if (/*EndIndex > 0 */EndIndex - StartIndex > 0)
                //{
                    _day = data.Substring(StartIndex);

                int _duration_StartIndex = _day.IndexOf("time\">", _day.IndexOf("</td>"));
                while (_duration_StartIndex > 0)
                {
                    _duration_StartIndex = _day.IndexOf("time\">", _day.IndexOf("</td>"));

                    if (_duration_StartIndex > -1 && _day.IndexOf("</td>", _duration_StartIndex) > _duration_StartIndex)
                    {
                        int _duration_EndIndex = _day.IndexOf("</td>", _duration_StartIndex);
                        _lesson.Duration = _day.Substring(_duration_StartIndex + 6, _duration_EndIndex - _duration_StartIndex - 6);
                        test += _lesson.Duration;
                        test += "\n-----Duration------\n";

                        int _disciple_StartIndex = _day.IndexOf("<dd>", _duration_EndIndex);
                        int _disciple_EndIndex = _day.IndexOf("</dd>", _disciple_StartIndex);
                        if (_day.IndexOf("</dd>") > _day.IndexOf("<dd>"))
                        {
                            string _discipline = _day.Substring(_disciple_StartIndex + 4, _disciple_EndIndex - _disciple_StartIndex - 4);
                            string[] _discipline_splitted = _discipline.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();
                            _discipline = "";

                            for (int i = 0; i < _discipline_splitted.Length; i++)
                            {
                                _discipline += _discipline_splitted[i];
                            }
                            char[] Array_of_discipline_symbols = new char[_discipline.Length];
                            string _temporary_discipline_string = "";

                            // строка содержит излишнее количество пробелов и разделителей и пустых строк, которые 
                            // не убираются полностью выше применённым методом "_discipline.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); "
                            // по этому оставшиеся пустые места буду убирать вручную
                            //for (int i = 0; i < _discipline.Length - 1; i++)
                            //{
                            //    if (_discipline[i] != ' ' && _discipline[i + 1] != ' ') { _temporary_discipline_string.Append(_discipline[i]); }
                            //}
                            _lesson.Discipline = _discipline; // _temporary_discipline_string;
                            test += _lesson.Discipline;
                            test += "\n-----Discipline-----\n";
                            _day = _day.Substring(_day.IndexOf("</dd>"));
                            string _cabinet = _day.Substring(_day.IndexOf("<span class='\"cabinet\">") + 22, _day.IndexOf("</span>") - _day.IndexOf("<span class='\"cabinet\">") - 22);
                            string[] _cabinet_splitted = _cabinet.Split(' ').Where(c => !String.IsNullOrWhiteSpace(c)).ToArray();
                            _cabinet = "";

                            for (int i = 0; i < _cabinet_splitted.Length; i++)
                            {
                                _cabinet += _cabinet_splitted[i] + ' ';
                            }
                            char[] Array_of_cabinet_symbols = new char[_cabinet.Length];
                            string _temporary_cabinet_string = "";
                            //for (int i = 0; i < _cabinet.Length - 1; i++)
                            //{
                            //    if (_cabinet[i] != ' ' && _cabinet[i + 1] != ' ') { _temporary_cabinet_string.Append(_cabinet[i]); }
                            //}
                            _lesson.Chamber = _cabinet; // _temporary_cabinet_string;
                            test += _lesson.Chamber;
                            test += "\n------Chamber------\n";
                            _day = _day.Substring(_day.IndexOf("</span>"));

                            int _teacher_StartIndex = _day.IndexOf("<span class=\"teacher\">");
                            if (_teacher_StartIndex < 0) { return _one_day = new One_Day_Pattern(_date_string, _lesson); }
                            int _teacher_EndIndex = _day.IndexOf("</span>", _teacher_StartIndex);

                            _lesson.Teacher = _day.Substring(_teacher_StartIndex + 22, _teacher_EndIndex - _teacher_StartIndex - 22);
                            test += _lesson.Teacher;
                            test += "\n-------Teacher-------\n";
                            StartIndex = _day.IndexOf("</span>");
                            _day = _day.Substring(_day.IndexOf("</span>"));
                            _lessons_list_of_day.Add(new Lesson_Pattern(_lesson));
                            test += "\n------------------\n";
                            test += "\n------------------\n";

                            //_one_day_shedule.Add(new One_Day_Pattern(_date_string, _lessons_list_of_day));
                        }
                    }
                }
                //_day = _day.Substring(StartIndex);
                //if (_day.IndexOf("</td>", _day.IndexOf("time\">")) > _day.IndexOf("time\">"))
                //{
                        //}
                    //}
                   
                //}
                //string test = "";
                //for (int k = 0; k < _one_day_shedule.Count; k++)
                //{
                //    for (int m = 0; m < _one_day_shedule[k].Get_Lessons.Count; m++)
                //    {
                //        test += _one_day_shedule[k].Get_Lessons[m].Duration + "  ||  ";
                //        test += _one_day_shedule[k].Get_Lessons[m].Discipline + "\n";
                //        test += _one_day_shedule[k].Get_Lessons[m].Chamber + "  ||  ";
                //        test += _one_day_shedule[k].Get_Lessons[m].Teacher + "\n";
                //        test += _one_day_shedule[k].Get_Lessons[m].Lesson_Type + "\n";
                //        test += "-------------------------------------------------------------------";
                //        test += "-------------------------------------------------------------------";
                //    }
                //}
                _test_counter++;
                File.WriteAllText($"D:\\123\\999{_test_counter}.txt", test);
            }

            //if (_lessons_list_of_day != null)
            //{

            //}


            //}
            //else _one_day_shedule = new ObservableCollection<One_Day_Pattern>();// default;



            //for (int k = 0; k < Week_Shedule_List.DayPattern_List.Count; k++)
            //{
            //    test += Week_Shedule_List.Group + "\n";
            //    test += Week_Shedule_List.DayPattern_List[k].DateString + "\n";



            return _one_day = new One_Day_Pattern(_date_string, _lesson); //_lessons_list_of_day; // _one_day_shedule; // == null ? null : _one_day_shedule;
        }

    }
}
