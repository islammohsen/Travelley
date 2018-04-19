using System;
using System.Collections.Generic;
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

namespace Travelley
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas CurrentCanvas;
        Trip TripOfTheDay;
        TourGuide TourGuideOfTheMonth;
        public MainWindow()
        {
            InitializeComponent();
            DataBase.Intialize();
            CurrentCanvas = Main_Canvas;

            int today=DateTime.Today.Day;
            TripOfTheDay = DataBase.Trips[today%DataBase.Trips.Count]; //generate trip based on today's date
            TourGuideOfTheMonth = TourGuide.GetBestTourGuide(DateTime.Today.Month - 1); //returns tour guide with maximum salary in the past month
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DataBase.ShutDown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Customer_Button_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
