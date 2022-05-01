using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Urfu_Shedule_Parser.Shedule_Pattern;
using System.Data.SqlClient;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace Urfu_Shedule_Parser.Sorting_Data
{
    public class Sort_Data
    {
        string _group_name = "";
        string _response;
        ObservableCollection<One_Day_Pattern> _one_day_shedule = new ObservableCollection<One_Day_Pattern>();
        ObservableCollection<Lesson_Pattern> _lessons_list_of_day = new ObservableCollection<Lesson_Pattern>();

        public Weekly_Shedule_Pattern Week_Shedule_List = new Weekly_Shedule_Pattern();
        Lesson_Pattern _lesson = new Lesson_Pattern();
        List<string> _raw_shedule_strings__splittet_by_days = new List<string>();
        One_Day_Pattern _one_day = new One_Day_Pattern();
        string test = "";
        int counter = 0;

        Saving_Data.Data_Base_Class DB_Fills = new Saving_Data.Data_Base_Class();

        public Weekly_Shedule_Pattern Weekly_Shedule_Sort(string data)
        {
            DB_Fills.Sql_Connection_Method();
            
            SqlConnection connection = DB_Fills.sql_connection_return();
            _response = data;
            string[] group_splitted = /*new string[] { " ", " " };*/_response.Substring(_response.IndexOf("Группа "), 25).Split(' ');
            _group_name = /*group_splitted[0] + ' ' + */group_splitted[1];

            SqlCommand sql_command = null;

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

                    int StartIndex = _one_day_string.IndexOf("<b>");
                    int EndIndex = _one_day_string.IndexOf("</b>");
                    string _date_string = _one_day_string.Substring(StartIndex + 3, EndIndex - StartIndex - 3);
                }
            }
            foreach (var item in _raw_shedule_strings__splittet_by_days)
            {

                _one_day_shedule.Add(new One_Day_Pattern(Dayly_Shedule_Sort(item)));

            }
            int id = 0;

            Thread.Sleep(2000); // ждём пока откроется соединение с БД. джём именно таким методом так - потому что по-другому пока не умею
            Task.WaitAll();
            sql_command = new SqlCommand("DELETE FROM Shedule", connection);
            sql_command.ExecuteNonQuery();

            foreach (var item in _one_day_shedule[0].Get_Lessons)
                {
                        id++;

                        sql_command = new SqlCommand(
                            $"INSERT INTO [Shedule] (Id, Date, Duration, LessonNumber, LessonName, Chamber, LessonType, Teacher, GroupName) VALUES ('{id}', N'{item.DateString}', N'{item.Duration}', N'{item.Discipline[0]}', N'{item.Discipline.Substring(4)}', N'{item.Chamber}', N'{item.Lesson_Type}', N'{item.Teacher}', N'{_group_name}')", connection);
                        sql_command.ExecuteNonQuery();
            }
                Week_Shedule_List = new Weekly_Shedule_Pattern(_group_name, _one_day_shedule);

            connection.Close();
            MessageBox.Show("Парсинг выполнен. DB connection state: " + connection.State.ToString());

            return Week_Shedule_List;

        }

        private One_Day_Pattern Dayly_Shedule_Sort(string data)
        {
            string _day = data;
            int StartIndex = _day.IndexOf("<b>");
            int EndIndex = _day.IndexOf("</b>");
            string _date_string = _day.Substring(StartIndex + 3, EndIndex - StartIndex - 3);

            if (EndIndex - StartIndex > 0)
            {
                _lesson.DateString = _day.Substring(StartIndex + 3, EndIndex - StartIndex - 3);
                string _date_string_ = _lesson.DateString;
                StartIndex = _day.IndexOf("</b></td>");

                    _day = data.Substring(StartIndex);

                int _duration_StartIndex = _day.IndexOf("time\">", _day.IndexOf("</td>"));
                while (_duration_StartIndex > 0)
                {
                    _duration_StartIndex = _day.IndexOf("time\">", _day.IndexOf("</td>"));

                    if (_duration_StartIndex > -1 && _day.IndexOf("</td>", _duration_StartIndex) > _duration_StartIndex)
                    {
                        int _duration_EndIndex = _day.IndexOf("</td>", _duration_StartIndex);
                        _lesson.Duration = _day.Substring(_duration_StartIndex + 6, _duration_EndIndex - _duration_StartIndex - 6);

                        int _disciple_StartIndex = _day.IndexOf("<dd>", _duration_EndIndex);
                        int _disciple_EndIndex = _day.IndexOf("</dd>", _disciple_StartIndex);
                        if (_day.IndexOf("</dd>") > _day.IndexOf("<dd>"))
                        {
                            string _discipline = _day.Substring(_disciple_StartIndex + 4, _disciple_EndIndex - _disciple_StartIndex - 4);
                            string[] _discipline_splitted = _discipline.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();
                            _discipline = "";

                            for (int i = 0; i < _discipline_splitted.Length; i++)
                            {
                                _discipline += _discipline_splitted[i] + " "; 
                            }
                            char[] Array_of_discipline_symbols = new char[_discipline.Length];
                            string _temporary_discipline_string = "";

                            _lesson.Discipline = _discipline;

                            _day = _day.Substring(_day.IndexOf("</dd>"));

                            int _lesson_type_StartIndex = _day.IndexOf("<span class=\"teacher\">");
                            if (_lesson_type_StartIndex < 0) { _lesson_type_StartIndex = 0; }
                            int _lesson_type_EndIndex = _day.IndexOf("</span>");
                            _lesson.Lesson_Type = _day.Substring(_lesson_type_StartIndex + 22, _lesson_type_EndIndex - _lesson_type_StartIndex - 22);

                            _day = _day.Substring(_day.IndexOf("</span>") + 7);

                            int _cabinet_StartIndex = _day.IndexOf("cabinet\">");
                            if (_cabinet_StartIndex < 0 || _day.IndexOf("</span>") - _cabinet_StartIndex < 0)
                            {
                                _lesson.Chamber = " ";
                            }
                            else
                            {
                                int _cabinet_EndIndex = _day.IndexOf("</span>");

                                string _cabinet = _day.Substring(_cabinet_StartIndex + 22, _cabinet_EndIndex - _cabinet_StartIndex - 22);
                                string[] _cabinet_splitted = _cabinet.Split(' ').Where(c => !String.IsNullOrWhiteSpace(c)).ToArray();
                                _cabinet = "";

                                for (int i = 0; i < _cabinet_splitted.Length; i++)
                                {
                                    _cabinet += _cabinet_splitted[i] + ' ';
                                }
                                char[] Array_of_cabinet_symbols = new char[_cabinet.Length];
                                string _temporary_cabinet_string = "";

                                _lesson.Chamber = _cabinet;
                                _day = _day.Substring(_day.IndexOf("</span>", _cabinet_StartIndex));
                            }


                            int _teacher_StartIndex = _day.IndexOf("<span class=\"teacher\">");
                                if (_teacher_StartIndex < 0) 
                            {
                                _lesson.Teacher = "";
                            }
                                else
                            {
                                int _teacher_EndIndex = _day.IndexOf("</span>", _teacher_StartIndex);
                                _lesson.Teacher = _day.Substring(_teacher_StartIndex + 22, _teacher_EndIndex - _teacher_StartIndex - 22);
                                StartIndex = _day.IndexOf("</span>");
                                _day = _day.Substring(_day.IndexOf("</span>", _teacher_StartIndex));
                            }

                            _lessons_list_of_day.Add(new Lesson_Pattern(_lesson));
                        }
                    }
                }
            }
            return _one_day = new One_Day_Pattern(_date_string, _lessons_list_of_day);
        }
    }
}
