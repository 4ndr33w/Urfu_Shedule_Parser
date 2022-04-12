using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urfu_Shedule_Parser.Shedule_Pattern
{
    public class Weekly_Shedule_Pattern
    {
        private string _group;
        private ObservableCollection<One_Day_Pattern> _group_shedule_list;

        public string Group { get { return _group; } set { _group = value; } }
        public ObservableCollection<One_Day_Pattern> DayPattern_List { get { return _group_shedule_list; } set { _group_shedule_list = value; } }

        public Weekly_Shedule_Pattern()
        {
            _group = "";
            _group_shedule_list = new ObservableCollection<One_Day_Pattern>();
        }

        public Weekly_Shedule_Pattern(string group, ObservableCollection<One_Day_Pattern> list)
        {
            _group = group;
            _group_shedule_list = list;
        }

        public Weekly_Shedule_Pattern(string group, One_Day_Pattern list)
        {
            _group = group;
            _group_shedule_list.Add(list);
        }
    }
}
