using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using Urfu_Shedule_Parser.Shedule_Pattern;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data;

namespace Urfu_Shedule_Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private SqlConnection sql_Connection = null;

        public ObservableCollection<Weekly_Shedule_Pattern> Shedule = new ObservableCollection<Weekly_Shedule_Pattern>();
        Weekly_Shedule_Pattern One_Group_Shedule = new Weekly_Shedule_Pattern();
        ObservableCollection<One_Day_Pattern> Weekly_List = new ObservableCollection<One_Day_Pattern>();
        ObservableCollection<Lesson_Pattern> Lessons = new ObservableCollection<Lesson_Pattern>();
        List<string> get_response = new List<string>();

        string connection_String = Properties.Resources.ConnectionString; //ConfigurationManager.ConnectionStrings[0].ConnectionString.ToString();
        public MainWindow()
        {
            
            InitializeComponent();
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Saving_Data.Data_Base_Class DataBase_Connection = new Saving_Data.Data_Base_Class();
            DataBase_Connection.Sql_Connection_Method();
    }

        private void Start_Parse_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime StartTime = DateTime.Now;
            //int counter = 0;
            Group_Number_TextBox.Text = DateTime.Now.ToString();
            Request.Get_Data run_test = new Request.Get_Data();

            Sorting_Data.Sort_Data sort_data = new Sorting_Data.Sort_Data();

            Task.Run(() =>
            {
                //string test = "";
                get_response = run_test.get_data();
                for (int i = 0; i < get_response.Count; i++)
                {
                    One_Group_Shedule = sort_data.Weekly_Shedule_Sort(get_response[i]);

                    //for (int k = 0; k < One_Group_Shedule.DayPattern_List.Count; k++)
                    //{
                        
                    //    for (int m = 0; m < One_Group_Shedule.DayPattern_List[k].Get_Lessons.Count; m++)
                    //    {
                    //        test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].DateString + "\n";
                    //        test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Duration + "\n";
                    //        test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Discipline + "\n";
                    //        test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Lesson_Type + "\n";
                    //        test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Teacher + "\n";
                    //        test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Chamber + "\n";
                    //        test += "\n-------------------\n";
                    //    }
                    //}
                    //File.WriteAllText("d:\\123\\MainWindow.txt", test /*span.TotalSeconds.ToString()*/);
                }
                
                
                string _group_name = "";
                Dispatcher.Invoke(new Action(() =>
                {
                    //Thread.Sleep(20000);
                    //for (int i = 0; i < One_Group_Shedule.DayPattern_List.Count; i++)
                    //{
                        Weekly_List.Add(new One_Day_Pattern(One_Group_Shedule.DayPattern_List[One_Group_Shedule.DayPattern_List.Count - 1]));

                    //    _group_name = One_Group_Shedule.Group;
                    //    Shedule.Add(new Weekly_Shedule_Pattern(_group_name, Weekly_List));
                    //}
                    _group_name = One_Group_Shedule.Group;
                    Shedule.Add(new Weekly_Shedule_Pattern(_group_name, Weekly_List));
                    //List_Box.ItemsSource = Lessons;
                    //Institute_ComboBox.ItemsSource = One_Group_Shedule.DayPattern_List;


                    foreach (var group in Shedule)
                    {
                        //test += group.Group + "\n\n\n";
                        //counter++;
                        foreach (var day_Shedule in group.DayPattern_List)
                        {
                            Lessons = day_Shedule.Get_Lessons;
                            //test += day_Shedule.DateString + "\n\n";
                            foreach (var daily_Lesson in day_Shedule.Get_Lessons.ToArray())
                            {
                                //test += group.Group + "\n\n\n";
                                Lessons.Add(new Lesson_Pattern(daily_Lesson));
                                //test += daily_Lesson.DateString + "\n";
                                //test += daily_Lesson.Duration + "\n";
                                //test += daily_Lesson.Discipline + "\n";
                                //test += daily_Lesson.Chamber + "\n";
                                //test += daily_Lesson.Lesson_Type + "\n";
                                //test += daily_Lesson.Teacher + "\n\n";
                                //test += "\n------------------------------------------\n\n\n";
                            }
                            //counter++;
                            //test += group.Group + "\n\n\n";
                            //test += "\n------------------------------------------\n\n\n";
                            //File.WriteAllText($"d:\\123\\MainWindow _{counter}_{counter}.txt", test /*span.TotalSeconds.ToString()*/);
                            //test = "";
                        }
                        //File.WriteAllText($"d:\\123\\MainWindow _{counter}.txt", test /*span.TotalSeconds.ToString()*/);
                        //test = "";
                    }

                    //List_Box.ItemsSource = Lessons/*.Where(x => (x.DateString == DateTime.Today.ToString("M")))*/;
                    
                }));
                DateTime EndTime = DateTime.Now;
                TimeSpan span = EndTime - StartTime;
                //Saving_Data.Data_Base_Class DataBase_Connection = new Saving_Data.Data_Base_Class();
                MessageBox.Show(span.TotalSeconds.ToString());
                //File.WriteAllText($"d:\\123\\TimeSpan.txt", span.TotalSeconds.ToString());
            });
        }

        private void show_result_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clear_table_Button_Click(object sender, RoutedEventArgs e)
        {
            Saving_Data.Data_Base_Class DataBase_Connection = new Saving_Data.Data_Base_Class();
            var connection = DataBase_Connection.sql_connection_return();
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM DataTable", connection);
            command.ExecuteNonQuery();
            MessageBox.Show(command.ExecuteNonQuery().ToString());
            connection.Close();
            
            //DataBase_Connection.sql_connection_return().
        }
    }
}
