using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urfu_Shedule_Parser.Shedule_Pattern
{
    public class Lesson_Pattern
    {
        private DateTime _date;
        private string _date_string;
        private DateTime _start_time;
        private DateTime _End_time;
        private string _duration;
        private string _teacher_name;
        private string _chamber;
        private string _discipline_name;

        public DateTime Date { get { return _date; } set { _date = value; } }
        public string DateString { get { return _date_string; } set { _date_string = value; } }
        public DateTime StartTime { get { return _start_time; } set { _start_time = value; } }
        public DateTime EndTime { get { return _End_time; } set { _End_time = value; } }
        public string Duration { get { return _duration; } set { _duration = value; } }
        public string Teacher { get { return _teacher_name; } set { _teacher_name = value; } }
        public string Chamber { get { return _chamber; } set { _chamber = value; } }
        public string Discipline { get { return _discipline_name; } set { _discipline_name = value; } }

        public Lesson_Pattern(Lesson_Pattern data)
        {
            string[] date_convert_from_string =data.DateString.Split(' ');
            _date =  Convert.ToDateTime(date_convert_from_string[0] + "." + date_convert_from_string[1] + '.' + DateTime.Now.Year);
            //_date_string = date_string;
            _date_string = data.DateString;
            _start_time = data.StartTime;
            _End_time = data.EndTime;
            _duration = data.Duration;

            _chamber = data.Chamber;
            _discipline_name = data.Discipline;
            //_date = Convert.ToDateTime(data.DateString);
            _teacher_name = data.Teacher;
            //_start_time = Convert.ToDateTime(data.StartTime);
        }
    }
}
