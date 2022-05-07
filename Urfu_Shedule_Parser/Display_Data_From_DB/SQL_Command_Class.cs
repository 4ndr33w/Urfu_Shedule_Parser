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
        private static CultureInfo _cultureInfo()
        {
            CultureInfo _culture = CultureInfo.CreateSpecificCulture("ru-RU");
            return _culture;
        }

        private static string _today_date ()
        {
            //CultureInfo _culture = CultureInfo.CreateSpecificCulture("ru-RU");//reateSpecificCulture("ru-RU");
            DateTime today_date = DateTime.Today; //.ToString("d, MMMM", CultureInfo.CreateSpecificCulture("ru-RU")).Dump();
            return today_date.ToString("dd MMMM", _cultureInfo());
        }
        private static string _tomorrow_date()
        {
            //CultureInfo _culture = CultureInfo.CreateSpecificCulture("ru-RU");//reateSpecificCulture("ru-RU");
            DateTime tomorrow = DateTime.Today.AddDays(1);
            string tomorrow_date_str = tomorrow.ToString("dd MMMM", _cultureInfo());
            return tomorrow_date_str;
        }
        private static string _this_week()
        {
            DateTime _sunday_date = DateTime.Today.AddDays(8 - (int)DateTime.Today.DayOfWeek);
            string _sunday_str = _sunday_date.ToString("dd MMMM", _cultureInfo());
            string _today_str = DateTime.Today.ToString("dd MMMM", _cultureInfo());
            string _this_week_sql_string = $"SELECT * FROM {_tableName} WHERE {_tableName}.Date BETWEEN'{_today_str}' AND '{_sunday_str}'";
            return _this_week_sql_string;
        }
        private static string _next_week()
        {
            DateTime _sunday_date = DateTime.Today.AddDays(8 - (int)DateTime.Today.DayOfWeek);
            DateTime _next_sunday = DateTime.Today.AddDays(15 - (int)DateTime.Today.DayOfWeek);
            string _sunday_str = _sunday_date.ToString("dd MMMM", _cultureInfo());
            string _next_sunday_str = _next_sunday.ToString("dd MMMM", _cultureInfo());
            string _next_week_sql_string = $"SELECT * FROM {_tableName} WHERE {_tableName}.Date BETWEEN'{_sunday_str}' AND '{_next_sunday_str}'";
            return _next_week_sql_string;
        }
        public string NextWeek_Lessons { get { return _next_week(); } }
        public string ThisWeek_Lessons { get { return _this_week(); } }

        public string Today_Lessons { get { return _sql_today_lessons; } }
        public string Tomorrow_Lessons { get { return _sql_tomorrow_lessons; } }
        public string Clear_Table { get { return _sql_delete_from_table; } }
        public string TableName { get { return _tableName; } }
        public string SelectAll_From_Table { get { return _sql_select_all_from_table; } }
        public string Insert_IntoTable_WithoutValues { get { return _sql_insert_into_table; } }
        public string CurrentLesson_ID { get { return _current_lesson_ID; } }
        public string CurrentLesson { get { return _sql_current_lesson; } }
        //public int Current_ID_Int ()
        //{
        //    //_current_lesson_ID
        //}
        //public string Today_Lessons_Sql_String { get { return _sql_today_lessons; } }

        private string _sql_current_lesson = $"SELECT * FROM [{_tableName}] where {_tableName}.Date = '{_today_date()}' AND {DateTime.Now.ToShortTimeString()} BETWEEN {_tableName}.StartTime AND {_tableName}.EndTime";
        private string _sql_next_lesson = "";
        private string _sql_today_lessons = $"SELECT * FROM {_tableName} WHERE {_tableName}.Date = '{_today_date()}'";
        private string _sql_tomorrow_lessons = $"SELECT * FROM {_tableName} WHERE {_tableName}.Date = '{_tomorrow_date()}'";
        private string _sql_this_week_lessons = "";
        private string _sql_delete_from_table = $"DELETE FROM {_tableName}";
        private string _sql_select_all_from_table = $"SELECT * FROM {_tableName}";
        //private string _sql_insert_into_table = $"INSERT INTO [{_tableName}] (Id, Date, Duration, LessonNumber, LessonName, Chamber, LessonType, Teacher, GroupName)";
        private string _sql_insert_into_table = $"INSERT INTO {_tableName} (Id, Date, Duration, LessonNumber, LessonName, Chamber, LessonType, Teacher, GroupName, StartTime, EndTime)";
        private string _current_lesson_ID = $"SELECT Id FROM {_tableName} WHERE {_tableName}.Date = '{_today_date()}' AND {DateTime.Now.ToShortTimeString()} BETWEEN {_tableName}.StartTime AND {_tableName}.EndTime";


        private static string _tableName = Properties.Resources.TableName;
    }
}
