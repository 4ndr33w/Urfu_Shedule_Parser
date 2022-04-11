using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urfu_Shedule_Parser
{
    public class Shedule_Scheme
    {
        private DateTime _date;
        private string _date_string;
        private DateTime _start_time;
        private DateTime _End_time;
        private string _duration;
        private string _teacher_name;
        private string _chamber;
        private string _discipline_name;
        private string _group_name;

        public DateTime Date { get { return _date; } set { _date = value; } }
        public string DateString { get { return _date_string; } set { _date_string = value; } }
        public DateTime StartTime { get { return _start_time; } set { _start_time = value; } }
        public DateTime EndTime { get { return _End_time; } set { _End_time = value; } }
        public string Duration { get { return _duration; } set { _duration = value; } }
        public string Teacher { get { return _teacher_name; } set { _teacher_name = value; } }
        public string Chamber { get { return _chamber; } set { _chamber = value; } }
        public string Discipline { get { return _discipline_name; } set { _discipline_name = value; } }
        public string GroupName { get { return _group_name; } set { _group_name = value; } }

        public Shedule_Scheme()
        {
            _date = DateTime.Now;
            _date_string = _date.ToShortDateString();
            _start_time = DateTime.Now;
            _End_time = DateTime.Now;
            _duration = "";
            _teacher_name = "";
            _chamber = "";
            _discipline_name = "";
            _group_name = "";
        }
        public Shedule_Scheme(Shedule_Scheme data)
        {
            _date = data.Date;
            _date_string = data.DateString;
            _start_time = data.StartTime;
            _End_time = data.EndTime;
            _duration = _start_time.ToShortTimeString() + " - " + _End_time.ToShortTimeString();
            _teacher_name = data.Teacher;
            _chamber = data.Chamber;
            _discipline_name = data.Discipline;
            _group_name = data.GroupName;
        }

        public override string ToString()
        {
            return this.DateString + ' ' + this.Duration + ' ' + this.Discipline + "\n";
        }
    }
}
