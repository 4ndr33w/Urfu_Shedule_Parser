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

namespace Urfu_Shedule_Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //DateTime DateFrom = DateTime.Now;//DateTime.MinValue;
        //DateTime DateTo = DateTime.Now;
        //int next_week;
        //public List<string> Shedule_list = new List<string>();

        //One_Day_Pattern shedule = new One_Day_Pattern();
        //public ObservableCollection<One_Day_Pattern> shedule_collection = new ObservableCollection<One_Day_Pattern>();
        public ObservableCollection<Weekly_Shedule_Pattern> Shedule = new ObservableCollection<Weekly_Shedule_Pattern>();
        Weekly_Shedule_Pattern One_Group_Shedule = new Weekly_Shedule_Pattern();
        public MainWindow()
        {
            InitializeComponent();
            List_Box.Items.Clear();
        }

        private void Start_Parse_Button_Click(object sender, RoutedEventArgs e)
        {

            Group_Number_TextBox.Text = DateTime.Now.ToString();
            Request.Get_Data run_test = new Request.Get_Data();

            Sorting_Data.Sort_Data sort_shedule = new Sorting_Data.Sort_Data();

            //File.WriteAllText(@"D:\555.txt" , run_test.get_data());
            
            //Thread.Sleep(500);
            
            Task.Run(() =>
            {
                for (int i = 0; i < run_test.get_data().Count; i++)
                {
                    string test = "";
                    One_Group_Shedule = sort_shedule.Weekly_Shedule_Sort(run_test.get_data()[i]);

                    for (int k = 0; k < One_Group_Shedule.DayPattern_List.Count; k++)
                    {
                        test += One_Group_Shedule.Group + "\n";
                        test += One_Group_Shedule.DayPattern_List[k].DateString + "\n";

                        for (int m = 0; m < One_Group_Shedule.DayPattern_List[k].Get_Lessons.Count; m++)
                        {
                            test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Duration + "  ||  ";
                            test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Discipline + "\n";
                            test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Chamber + "  ||  ";
                            test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Teacher + "\n";
                            test += One_Group_Shedule.DayPattern_List[k].Get_Lessons[m].Lesson_Type + "\n";
                            test += "-------------------------------------------------------------------";
                            test += "-------------------------------------------------------------------";
                        }
                    }
                    File.WriteAllText(@"D:\123\555.txt", test);
                    //sort_shedule
                    //extract_datas.Split_request_To_days(run_test.get_data()[i]);
                    //Thread.Sleep(500);
                    //Shedule_list = extract_datas.Get_Splitted_Data();
                    //File.WriteAllText("D:\\555555.txt", Shedule_list[Shedule_list.Count - 1]);
                }
               
            });


            //List_Box.ItemsSource = shedule_collection;

        }
    }
}
