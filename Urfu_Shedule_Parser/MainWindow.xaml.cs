using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Urfu_Shedule_Parser.Shedule_Pattern;
using System.Globalization;
using System.Threading;

namespace Urfu_Shedule_Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Weekly_Shedule_Pattern> Shedule = new ObservableCollection<Weekly_Shedule_Pattern>();
        Weekly_Shedule_Pattern One_Group_Shedule = new Weekly_Shedule_Pattern();
        ObservableCollection<One_Day_Pattern> Weekly_List = new ObservableCollection<One_Day_Pattern>();
        ObservableCollection<Lesson_Pattern> Lessons = new ObservableCollection<Lesson_Pattern>();
        List<string> get_response = new List<string>();
        Request.Get_Data run_test = new Request.Get_Data();
        CultureInfo _culture = CultureInfo.CreateSpecificCulture("ru-RU");
        Saving_Data.Data_Base_Class _sql_check = new Saving_Data.Data_Base_Class();
        

        Display_Data_From_DB.DB_Display _display = new Display_Data_From_DB.DB_Display();
        Display_Data_From_DB.SQL_Command_Class _sql_requests = new Display_Data_From_DB.SQL_Command_Class();

        

        public MainWindow()
        {
            InitializeComponent();
            Request.Static_Group_Prefix.Prefix = Institute_TextBox.Text;
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Institute_TextBox.Text = Properties.Settings.Default.Default_Group_Prefix;
            Saving_Data.Data_Base_Class DataBase_Connection = new Saving_Data.Data_Base_Class();
            DataBase_Connection.Sql_Connection_Method();
    }

        private void Start_Parse_Button_Click(object sender, RoutedEventArgs e)
        {

            Request.Static_Group_Prefix.Prefix = Institute_TextBox.Text;
            Properties.Settings.Default.Default_Group_Prefix = Institute_TextBox.Text;
            Properties.Settings.Default.Save();
            Request.Static_Group_Prefix.Prefix = Properties.Settings.Default.Default_Group_Prefix;
            DateTime StartTime = DateTime.Now;

            Sorting_Data.Sort_Data sort_data = new Sorting_Data.Sort_Data();

            Task.Run(() =>
            {
                if (run_test.get_data()[0] != string.Empty || run_test.get_data()[0] != null)
                {
                    //MessageBox.Show(run_test.get_data()[0]);
                    get_response = run_test.get_data();
                    for (int i = 0; i < get_response.Count; i++)
                    {
                        One_Group_Shedule = sort_data.Weekly_Shedule_Sort(get_response[i]);
                    }

                    string _group_name = "";
                    Dispatcher.Invoke(new Action(() =>
                    {
                        Weekly_List.Add(new One_Day_Pattern(One_Group_Shedule.DayPattern_List[One_Group_Shedule.DayPattern_List.Count - 1]));

                        _group_name = One_Group_Shedule.Group;
                        Shedule.Add(new Weekly_Shedule_Pattern(_group_name, Weekly_List));

                        foreach (var group in Shedule)
                        {
                            foreach (var day_Shedule in group.DayPattern_List)
                            {
                                Lessons = day_Shedule.Get_Lessons;

                                foreach (var daily_Lesson in day_Shedule.Get_Lessons.ToArray())
                                {
                                    Lessons.Add(new Lesson_Pattern(daily_Lesson));
                                }
                            }
                        }
                    }));
                }
                else MessageBox.Show("пусто");
               
                //DateTime EndTime = DateTime.Now;
                //TimeSpan span = EndTime - StartTime;
            });
        }

        private void show_result_Button_Click(object sender, RoutedEventArgs e)
        {
            // Saving_Data.Data_Base_Class _DB_connection = new Saving_Data.Data_Base_Class();
            // Sorting_Data.Sort_Data _sort = new Sorting_Data.Sort_Data();
            // Task.Run(() =>
            //{
            //    while (_sort.SqlConnectionState_Check(_DB_connection.sql_connection_return()) == false)
            //    {
            //        _sort.SqlConnectionState_Check(_DB_connection.sql_connection_return());
            //        Thread.Sleep(100);
            //    }
            //    MessageBox.Show(_DB_connection.sql_connection_return().State.ToString());
            //while (SqlConnectionState_Check(connection) == false)
            //{
            //    SqlConnectionState_Check(connection); // ждём когда откроется sql_connection
            //    Thread.Sleep(100);
            //}
            string _selectAll_from_Table = _sql_requests.SelectAll_From_Table; ;
               Grid_Data.ItemsSource = _display.DB_Table(_selectAll_from_Table).DefaultView;
           //});
            
        }

        private void clear_table_Button_Click(object sender, RoutedEventArgs e)
        {
            string _clearTable = _sql_requests.Clear_Table;
            try
            {
                if (Grid_Data.ItemsSource != null && Grid_Data != null && _display.DB_Table(_clearTable) != null)
                {
                    Grid_Data.ItemsSource = _display.DB_Table(_clearTable).DefaultView;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Today_Btn_Click(object sender, RoutedEventArgs e)
        {
            string _today_lessons = _sql_requests.Today_Lessons;
            Grid_Data.ItemsSource = _display.DB_Table(_today_lessons).DefaultView;
        }

        private void Tomorrow_Btn_Click(object sender, RoutedEventArgs e)
        {
            string _tomorrow_lessons = _sql_requests.Tomorrow_Lessons;
            Grid_Data.ItemsSource = _display.DB_Table(_tomorrow_lessons).DefaultView;
        }

        private void Current_Btn_Click(object sender, RoutedEventArgs e)
        {
            //string _currentLesson = _sql_requests.CurrentLesson;
            //string _id = _sql_requests.CurrentLesson_ID;
            //string _id_str = (_display.DB_Table(_id).DefaultView)[_display.DB_Table(_id).Columns.Count].ToString();
            //int _id_int = Convert.ToInt32(_id);
            //Grid_Data.ItemsSource = _display.DB_Table(_currentLesson).DefaultView;
            //MessageBox.Show($"Col: {_display.DB_Table(_id).Rows[}" + ' ' + $"Row: {_display.DB_Table(_id).Rows.Count}");
        }

        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void This_Week_Btn_Click(object sender, RoutedEventArgs e)
        {
            string _this_week_sql_string = _sql_requests.ThisWeek_Lessons;
            Grid_Data.ItemsSource = _display.DB_Table(_this_week_sql_string).DefaultView;
        }

        private void NextWeek_Btn_Click(object sender, RoutedEventArgs e)
        {
            string _next_week_sql_string = _sql_requests.NextWeek_Lessons;
            Grid_Data.ItemsSource = _display.DB_Table(_next_week_sql_string).DefaultView;
        }
    }
}
