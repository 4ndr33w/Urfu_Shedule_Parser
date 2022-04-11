using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urfu_Shedule_Parser.Shedule_Pattern
{
    public class Shedule_For_Group
    {
        private string _group;
        private List<Shedule_Scheme> _group_shedule_list;

        public string Group { get { return _group; } set { _group = value; } }
        public List<Shedule_Scheme> GroupShedule_List { get { return _group_shedule_list; } set { _group_shedule_list = value; } }

        public Shedule_For_Group()
        {
            _group = "";
            _group_shedule_list = new List<Shedule_Scheme>();
        }

        public Shedule_For_Group(string group, List<Shedule_Scheme> list)
        {
            _group = group;
            _group_shedule_list = list;
        }
    }
}
