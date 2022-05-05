using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Urfu_Shedule_Parser.Shedule_Pattern;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Globalization;

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

        Display_Data_From_DB.DB_Display _display = new Display_Data_From_DB.DB_Display();

        string connection_String = Properties.Resources.ConnectionString; //ConfigurationManager.ConnectionStrings[0].ConnectionString.ToString();
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
                //DateTime EndTime = DateTime.Now;
                //TimeSpan span = EndTime - StartTime;
                
            });
        }

        private void show_result_Button_Click(object sender, RoutedEventArgs e)
        {
            Grid_Data.ItemsSource = _display.DB_Table("SELECT * FROM Shedule").DefaultView;
        }

        private void clear_table_Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (Grid_Data.ItemsSource != null && Grid_Data != null && _display.DB_Table("DELETE FROM [Shedule]") != null)
                {
                    Grid_Data.ItemsSource = _display.DB_Table("DELETE FROM [Shedule]").DefaultView;
                }
            }
            catch (Exception)
            {
                return;
            }
           
        }

        private void Today_Btn_Click(object sender, RoutedEventArgs e)
        {
            string _today_str = DateTime.Today.ToString("dd MMMM", _culture);
            //Grid_Data.ItemsSource = _display.DB_Table($"SELECT * FROM Shedule WHERE Shedule.Date LIKE ('%{_today_str}%')").DefaultView;
            Grid_Data.ItemsSource = _display.DB_Table($"SELECT * FROM Shedule WHERE Shedule.Date = N'{_today_str}'").DefaultView;
        }

        private void Tomorrow_Btn_Click(object sender, RoutedEventArgs e)
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            string tomorrow_str = tomorrow.ToString("dd MMMM", _culture);

            Grid_Data.ItemsSource = _display.DB_Table($"SELECT * FROM Shedule WHERE Shedule.Date = N'{tomorrow_str}'").DefaultView;
        }

        private void Current_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void This_Week_Btn_Click(object sender, RoutedEventArgs e)
        {
            DateTime _sunday_date = DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);
            TimeSpan _span_days = _sunday_date - DateTime.Today;
            string _sunday_str = _sunday_date.ToString("dd MMMM", _culture);
            string _today_str = DateTime.Today.ToString("dd MMMM", _culture);
             DateTime tomorrow = DateTime.Today.AddDays(1);
            string tomorrow_str = tomorrow.ToString("dd MMMM", _culture);
            string _sql_str = $"SELECT * FROM Shedule WHERE Shedule.Date BETWEEN'{_today_str}' AND '{_sunday_str}'";
            //for (int i = 1; i < (int)_span_days.TotalDays; i++)
            //{
            //    _sql_str += $" AND Shedule.Date = '{DateTime.Today.AddDays(i).ToString("dd MMMM", _culture)}'";
            //}

            Grid_Data.ItemsSource = _display.DB_Table(_sql_str).DefaultView;

        }
    }
}
