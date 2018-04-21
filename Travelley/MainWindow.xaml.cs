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

            TourGuide t = new TourGuide("1", "ahmed", "egy", "male", "asa", "011");
            t.UserImage = new CustomImage("C:/Users/Ramy_/Pictures/Camera Roll/WIN_20180406_21_08_56_Pro.jpg");
            DataBase.TourGuides.Add(t);

            Trip trip = new Trip("2", t, "family", "Cairo", "Alex", 0, new DateTime(2017, 5, 4), new DateTime(2017, 6, 4));
            trip.TripImage = new CustomImage("E:/beach.jpeg");  //Put a valid image just to test
            DataBase.Trips.Add(trip);

            int today = DateTime.Today.Day;
            if (DataBase.Trips.Count != 0)
                TripOfTheDay = DataBase.Trips[today % DataBase.Trips.Count]; //generate trip based on today's date
            if (DataBase.TourGuides.Count != 0)
                TourGuideOfTheMonth = TourGuide.GetBestTourGuide(DateTime.Today.Month - 1); //returns tour guide with maximum salary in the past month



            if (TripOfTheDay != null)
            {
                TripOfTheDay_IMG.Source = TripOfTheDay.TripImage.GetImage().Source;
                TripOfTheDay_Label.Content = TripOfTheDay.Departure + " - " + TripOfTheDay.Destination;
            }

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
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = Transactions_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
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

        private void ShowTripFullData(Trip t)
        {
            CurrentPanelName_Label.Content = "Trip Full Data";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = TripFullData_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;

            TripFullData_Discount.Content = t.Discount.ToString() + " %";
            TripFullData_DepartureAndDestination.Content = t.Departure + " - " + t.Destination;
            TripFullData_IMG.Source = t.TripImage.GetImage().Source;
            TripFullData_StartDate.Content = t.Start.ToShortDateString();
            TripFullData_EndDate.Content = t.End.ToShortDateString();
            TripFullData_TourGuideName.Content = t.Tour.Name;
            TripFullData_TripType.Content = t.Type + " Trip";
            TripFullData_TripId.Content = t.TripId;
        }

        private void TripOfTheDay_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowTripFullData(TripOfTheDay);
        }
    }
}
