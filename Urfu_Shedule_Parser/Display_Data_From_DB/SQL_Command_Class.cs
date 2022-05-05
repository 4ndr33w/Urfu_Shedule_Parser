using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urfu_Shedule_Parser.Display_Data_From_DB
{
    internal class SQL_Command_Class
    {
        string _answer = "";
        private string Sql_Command(string incoming_data)
        {
            return _answer;
        }
        //CultureInfo _culture = CultureInfo.CreateSpecificCulture("ru-RU");//reateSpecificCulture("ru-RU");
        //DateTime today_date = DateTime.Today; //.ToString("d, MMMM", CultureInfo.CreateSpecificCulture("ru-RU")).Dump();
        /*string _today_date_str =*///today_date.ToString("d MMMM", _culture).Dump();
        private static string _today_date ()
        {
            CultureInfo _culture = CultureInfo.CreateSpecificCulture("ru-RU");//reateSpecificCulture("ru-RU");
            DateTime today_date = DateTime.Today; //.ToString("d, MMMM", CultureInfo.CreateSpecificCulture("ru-RU")).Dump();
            return today_date.ToString("dd MMMM", _culture);
        }
        //string _today_date_str = _today_date();

        public string Today_Lessons { get { return _sql_today_lessons; } }
        public string Today_Date { get { return _today_date(); } }

        private string _sql_current_lesson = @"SELECT * FROM [Shedule] where";
        private string _sql_next_lesson = "";
        private string _sql_today_lessons = $"SELECT * FROM Shedule WHERE Id > 2";
        private string _sql_tomorrow_lessons = "";
        private string _sql_this_week_lessons = "";
    }
}
