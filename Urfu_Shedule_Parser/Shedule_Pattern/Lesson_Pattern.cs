using System;

namespace Urfu_Shedule_Parser.Shedule_Pattern
{
    public class Lesson_Pattern
    {
        private DateTime _date;
        private string _date_string;
        private TimeSpan _start_time;
        private TimeSpan _End_time;
        private string _duration;
        private string _teacher_name;
        private string _chamber;
        private string _discipline_name;
        private string _lesson_type;

        public DateTime Date { get { return _date; } set { _date = value; } }
        public string DateString { get { return _date_string; } set { _date_string = value; } }
        public TimeSpan StartTime { get { return _start_time; } set { _start_time = value; } }
        public TimeSpan EndTime { get { return _End_time; } set { _End_time = value; } }
        public string Duration { get { return _duration; } set { _duration = value; } }
        public string Teacher { get { return _teacher_name; } set { _teacher_name = value; } }
        public string Chamber { get { return _chamber; } set { _chamber = value; } }
        public string Discipline { get { return _discipline_name; } set { _discipline_name = value; } }
        public string Lesson_Type { get { return _lesson_type; } set { _lesson_type = value; } }

        public Lesson_Pattern(Lesson_Pattern data)
        {
            string[] date_convert_from_string = data.DateString.Split(' ');
            string[] duration_split = data.Duration.Split(' ');
            _date = Convert.ToDateTime(date_convert_from_string[0] + "." + date_convert_from_string[1] + '.' + DateTime.Now.Year);
            _date_string = data.DateString;
            _start_time = Convert.ToDateTime(duration_split[0]).TimeOfDay;
            _End_time = Convert.ToDateTime(duration_split[2]).TimeOfDay;
            _duration = data.Duration;
            _lesson_type = data.Lesson_Type;

            _chamber = data.Chamber;
            _discipline_name = data.Discipline;
            _teacher_name = data.Teacher;
        }
        public Lesson_Pattern()
        {
            _date = default;// Convert.ToDateTime(date_convert_from_string[0] + "." + date_convert_from_string[1] + '.' + DateTime.Now.Year);
            _date_string = default; // data.DateString;
            _start_time = default;// = data.StartTime;
            _End_time = default;// = data.EndTime;
            _duration = default;// data.Duration;
            _lesson_type = default;// data.Lesson_Type;

            _chamber = default;// data.Chamber;
            _discipline_name = default;// data.Discipline;
            _teacher_name = default;// data.Teacher;
        }
    }
}
