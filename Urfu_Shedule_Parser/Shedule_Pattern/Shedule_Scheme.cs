using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Urfu_Shedule_Parser.Shedule_Pattern;

namespace Urfu_Shedule_Parser
{
    public class Shedule_Scheme
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

        public Shedule_Scheme()
        {
            _date = DateTime.Now;
            _date_string = _date.ToShortDateString();

            _group_name = "";
        }
        public Shedule_Scheme(Shedule_Scheme data)
        {
            _date = data.Date;
            _date_string = data.DateString;

            _group_name = data.GroupName;
        }

        //public override string ToString()
        //{
        //    return this.DateString + ' ' + this.Duration + ' ' + this.Discipline + "\n";
        //}
    }
}
