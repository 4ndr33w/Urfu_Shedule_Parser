using System;
using System.Collections.ObjectModel;
using Urfu_Shedule_Parser.Shedule_Pattern;

namespace Urfu_Shedule_Parser
{
    public class One_Day_Pattern
    {
        private DateTime _date;
        private string _date_string;
        private ObservableCollection<Lesson_Pattern> _lessons = new ObservableCollection<Lesson_Pattern>();

        private string _group_name;

        public DateTime Date { get { return _date; } set { _date = value; } }
        public string DateString { get { return _date_string; } set { _date_string = value; } }
        public ObservableCollection<Lesson_Pattern> Get_Lessons { get { return _lessons; }}
        public Lesson_Pattern Set_Lesson { set { var _one_lesson = new Lesson_Pattern(value); _lessons.Add(_one_lesson);  } }

        public string GroupName { get { return _group_name; } set { _group_name = value; } }

        public One_Day_Pattern()
        {
            _date = DateTime.Now;
            _date_string = _date.ToShortDateString();
            _lessons = new ObservableCollection<Lesson_Pattern>();
            _group_name = "";
        }
        public One_Day_Pattern(One_Day_Pattern data)
        {
            string[] date_convert_from_string = data.DateString.Split(' ');
            //_date = Convert.ToDateTime(date_convert_from_string[0] + "." + date_convert_from_string[1] + '.' + DateTime.Now.Year);
            _date_string = data.DateString;
            _lessons = data.Get_Lessons;
            _group_name = data.GroupName;
        }

        public One_Day_Pattern(string date_string, ObservableCollection<Lesson_Pattern> lesson_collection)
        {
            string[] date_convert_from_string = date_string.Split(' ');
            _date = default; // Convert.ToDateTime(date_convert_from_string[0] + "." + date_convert_from_string[1] + '.' + DateTime.Now.Year);
            _date_string = date_string;
            _lessons = lesson_collection;
            //_group_name = group_name;
        }
        public One_Day_Pattern(string date_string, /*string date_string,*/ Lesson_Pattern lesson)
        {
            //string[] date_convert_from_string = date_string.Split(' ');
            //_date =  Convert.ToDateTime(date_convert_from_string[0] + "." + date_convert_from_string[1] + '.' + DateTime.Now.Year);
            //_date_string = date_string;
            _lessons.Add(lesson);
            _date_string = date_string;
        }
    }
}
