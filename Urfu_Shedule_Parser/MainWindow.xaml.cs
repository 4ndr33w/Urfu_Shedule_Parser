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

namespace Urfu_Shedule_Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime DateFrom = DateTime.Now;//DateTime.MinValue;
        DateTime DateTo = DateTime.Now;
        int next_week;
        public List<string> Shedule_list = new List<string>();

        Shedule_Scheme shedule = new Shedule_Scheme();
        public ObservableCollection<Shedule_Scheme> shedule_collection = new ObservableCollection<Shedule_Scheme>();
        public MainWindow()
        {
            InitializeComponent();
            List_Box.Items.Clear();
        }

        private void Start_Parse_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTo.AddDays(7);
            int days_to_sunday = DayOfWeek.Sunday - DateTime.Now.DayOfWeek;
            TimeSpan shedule_span = DateFrom.Date - DateTo.Date;
            int two_weeks_days_span = shedule_span.Days;



            if (DateTo.DayOfWeek != DayOfWeek.Sunday)
            {
                DateTo.AddDays(days_to_sunday);
            }


            Group_Number_TextBox.Text = DateTime.Now.ToString();
            Request.Get_Data run_test = new Request.Get_Data();
            //Request.Extract_Data_From_Shedule(run_test);
            //Sorting_Data.ExtraExtract_Data_From_Shedule extract_datas = new Request.Extract_Data_From_Shedule();
            Request.Extract_Data_From_Shedule extract_datas = new Request.Extract_Data_From_Shedule();

            //File.WriteAllText(@"D:\555.txt" , run_test.get_data());
            extract_datas.Split_request_To_days(run_test.get_data());
            Thread.Sleep(500);
            Task.Run(() =>
            {
                Shedule_list = extract_datas.Get_Splitted_Data();
            });


            //List_Box.ItemsSource = shedule_collection;

        }
    }
}
