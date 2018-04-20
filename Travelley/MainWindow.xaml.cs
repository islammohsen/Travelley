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
using Travelley.Back_End;

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
        

            int today = DateTime.Today.Day;
            if (DataBase.Trips.Count != 0)
                TripOfTheDay = DataBase.Trips[today % DataBase.Trips.Count]; //generate trip based on today's date
            if (DataBase.TourGuides.Count != 0)
                TourGuideOfTheMonth = TourGuide.GetBestTourGuide(DateTime.Today.Month - 1); //returns tour guide with maximum salary in the past month

            if (TripOfTheDay == null) ;
            //TODO: add message
            //there is no exiting trips
            if (TourGuideOfTheMonth == null) ;
            //Todo: add message
            //There is no tour guides or max existing haas 0 salary in the past month
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DataBase.ShutDown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			CurrentCanvas.Visibility = Visibility.Hidden;
			CurrentCanvas = Main_Canvas;
			CurrentCanvas.Visibility = Visibility.Visible;
			CurrentPanelName_Label.Content = "Home Page";
		}


        private void Customer_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
			CurrentCanvas.Visibility = Visibility.Hidden;
			CurrentCanvas = Customers_Canvas;
			CurrentCanvas.Visibility = Visibility.Visible;
			CurrentPanelName_Label.Content = "Customers";
        }

       private void Ticket_Button_Click(object sender, RoutedEventArgs e)
       {
			CurrentCanvas.Visibility = Visibility.Hidden;
			CurrentCanvas = Tickets_Canvas;
			CurrentCanvas.Visibility = Visibility.Visible;
			CurrentPanelName_Label.Content = "Tickets";
       }

       private void TourGuide_Button_Click(object sender, RoutedEventArgs e)
		{
			CurrentCanvas.Visibility = Visibility.Hidden;
			CurrentCanvas = TourGuides_Canvas;
			CurrentCanvas.Visibility = Visibility.Visible;
			CurrentPanelName_Label.Content = "Tour Guides";
       }

       private void Transactions_Button_Click(object sender, RoutedEventArgs e)
       {
           CurrentPanelName_Label.Content = "Transactions";
       }
       private void Trips_Button_Click(object sender, RoutedEventArgs e)
       {
			CurrentPanelName_Label.Content = "Trips";
			CurrentCanvas.Visibility = Visibility.Hidden;
			CurrentCanvas = Trips_Canvas;
			CurrentCanvas.Visibility = Visibility.Visible;
       }

       private void Button_Mouse_Enter(object sender, MouseEventArgs e)
       {
           Button b = sender as Button;
           b.Background = new SolidColorBrush(Color.FromRgb(21, 31, 40));
           b.Foreground = new SolidColorBrush(Color.FromRgb(232, 126, 49));
       }

       private void Button_Mouse_Leave(object sender, MouseEventArgs e)
       {
           Button b = sender as Button;
           b.Background = new SolidColorBrush(Color.FromRgb(41, 53, 65));
           b.Foreground = Brushes.White;
         }
    }
}
