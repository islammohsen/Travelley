using Microsoft.Win32;
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

        public Canvas CurrentCanvas;
        Trip TripOfTheDay;
        TourGuide TourGuideOfTheMonth;
        Customer cus;
        string SelectedPath = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

        }
        public MainWindow()
        {
          
            InitializeComponent();
           
            DataBase.Intialize();
          
            CurrentCanvas = CustomerFullDetails_Canvas;
           
            List<string> l = new List<string>();
            l.Add("Arabic");
            
            cus = new Customer("1", "Ali Ahmed", "Egyption",l, "Male", "Ali@Gmail.com", "0114849551");
            cus.UserImage = new CustomImage("E:/test.jpg");
            DataBase.Customers.Add(cus);

            CurrentCanvas = Main_Canvas;

            TourGuide t = new TourGuide("1", "ahmed", "egy", "male", "asa", "011");
            t.UserImage = new CustomImage("E:/test.jpg");
            DataBase.TourGuides.Add(t);

            Trip trip = new Trip("2", t, "family", "Cairo", "Alex", 0, new DateTime(2017, 5, 4), new DateTime(2017, 6, 4));
            trip.TripImage = new CustomImage("E:/test.jpg");  //Put a valid image just to test
            DataBase.Trips.Add(trip);
            

            Trip trip2 = new Trip("3", t, "test", "Rome", "Paris", 0, new DateTime(2017, 5, 4), new DateTime(2017, 6, 4));
            trip2.TripImage = new CustomImage("E:/test.jpg");  //Put a valid image just to test
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);
            DataBase.Trips.Add(trip2);


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
            TripsScrollViewer.Visibility = Visibility.Hidden;
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = Main_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Home Page";
        }


        private void Customer_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            TripsScrollViewer.Visibility = Visibility.Hidden;
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = Customers_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Customers";
        }

        private void Ticket_Button_Click(object sender, RoutedEventArgs e)
        {
            TripsScrollViewer.Visibility = Visibility.Hidden;
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = Tickets_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Tickets";
        }

        private void TourGuide_Button_Click(object sender, RoutedEventArgs e)
        {
            TripsScrollViewer.Visibility = Visibility.Hidden;
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = TourGuides_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Tour Guides";
        }

        private void Transactions_Button_Click(object sender, RoutedEventArgs e)
        {
            TripsScrollViewer.Visibility = Visibility.Hidden;
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = Transactions_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Transactions";
        }
        private void Trips_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCanvas == Trips_Canvas)
                return;
            TripsScrollViewer.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Trips";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = Trips_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;

            ShowListOfTrips(DataBase.Trips);
            
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

        public void ShowTripFullData(Trip t)
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
        private void ShowCustomerFullData(Customer c)
        {
            CurrentPanelName_Label.Content = "Customer Full Data";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = CustomerFullDetails_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
           
            CustomerFullData_Name.Content = c.Name;
            CustomerFullData_Nationality.Content = c.Nationality;
            CustomerFullData_Id.Content = c.Id;
            CustomerFullData_Email.Content =c.Email;
            CustomerFullData_Gender.Content = c.Gender;
            CustomerFullData_Language.Content = c.Languages[0].ToString();
            CustomerFullData_IMG.Source = c.UserImage.GetImage().Source;
            CustomerFullData_PhoneNumber.Content = c.PhoneNumber;
            


        }
        private void DeleteCustomer(Customer c)
        {

           


        }
        private void EditCustomerDetails(Customer c)
        {
            string name = c.Name,nationality=c.Nationality,phone_number=c.PhoneNumber,
                language=c.Languages[0],gender=c.Gender,email=c.Email;

            if(EditCustomerFullData_Name.Text!="")
                name = EditCustomerFullData_Name.Text;
            if (EditCustomerFullData_Nationality.Text != "")
               nationality = EditCustomerFullData_Nationality.Text;
            if (EditCustomerFullData_PhoneNumber.Text != "")
                phone_number = EditCustomerFullData_PhoneNumber.Text;
            if (EditCustomerFullData_Language.Text != "")
                language = EditCustomerFullData_Language.Text;
            if (Gender_ComboBox.Text != "")
                gender = Gender_ComboBox.Text;
            if (EditCustomerFullData_Email.Text != "")
                email = EditCustomerFullData_Email.Text;

           // DataBase.UpdateCustomer(c, c.Id, name, nationality, language, gender, email, phone_number, c.UserImage);
        }
        private void TripOfTheDay_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowTripFullData(TripOfTheDay);
        }

        private void Customer_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowCustomerFullData(cus);
        }
        private void ShowListOfTrips(List<Trip>list)
        {
            
            for (int i=0; i<list.Count; i++)
            {
                TripDisplayCard t = new TripDisplayCard(list[i], i , ref CurrentCanvas,this);
            }
            
                return;
        }

        private void EditCustomerData_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            EditCustomerDetails(cus);
        }

        private void CustomerFullData_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentPanelName_Label.Content = "Edit Customer Data";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = EditCustomerFullDetails_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            EditCustomerFullData_Name.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            EditCustomerFullData_Name.Text=CustomerFullData_Name.Content.ToString();

            EditCustomerFullData_Nationality.Text = CustomerFullData_Nationality.Content.ToString();
            EditCustomerFullData_Email.Text =CustomerFullData_Email.Content.ToString();
            Gender_ComboBox.Text= CustomerFullData_Gender.Content.ToString();
            EditCustomerFullData_Language.Text= CustomerFullData_Language.Content.ToString();
            EditCustomerFullData_PhoneNumber.Text=  CustomerFullData_PhoneNumber.Content.ToString();
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAddTripCanvas();
        }
        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            bool errorfound = false;
            if (AddTrip_TripIDTextbox.Text.Trim() == "")
            {
                AddTrip_TripID_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (DataBase.CheckUniqueTripId(AddTrip_TripIDTextbox.Text) == false)
            {
                AddTrip_TripID_ErrorLabel.Content = "This ID is already used";
                errorfound = true;
            }
            if (AddTrip_TripDeptTextbox.Text.Trim() == "")
            {
                AddTrip_TripDep_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (AddTrip_TripDestTextbox.Text.Trim() == "")
            {
                AddTrip_TripDes_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (AddTrip_TripDiscTextbox.Text.Trim() == "")
            {
                AddTrip_Discount_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (AddTrip_StTimePicker.SelectedDate < DateTime.Today)
            {
                AddTrip_TripStTime_ErrorLabel.Content = "Trip can't start before today!";
                errorfound = true;
            }
            if (AddTrip_EnTimePicker.SelectedDate < DateTime.Today)
            {
                AddTrip_TripEnTime_ErrorLabel.Content = "Trip can't end before today!";
                errorfound = true;
            }
            if (AddTrip_EnTimePicker.SelectedDate < AddTrip_StTimePicker.SelectedDate)
            {
                AddTrip_TripEnTime_ErrorLabel.Content = "Trip can't end before start time!";
                errorfound = true;
            }
            if (SelectedPath == "")
            {
                AddTrip_TripPhoto_ErrorLabel.Content = "You must choose photo!";
                errorfound = true;
            }
            if (AddTrip_EnTimePicker.Text == "")
            {
                AddTrip_TripEnTime_ErrorLabel.Content = "You must choose end time!";
                errorfound = true;
            }
            if (AddTrip_StTimePicker.Text == "")
            {
                AddTrip_TripStTime_ErrorLabel.Content = "You must choose start time!";
                errorfound = true;
            }
            if (errorfound == true)
            {
                return;
            }
            //TODO Insert Trip in data base
            //TODO Clear all textboxes after saving
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png";
            dlg.Title = "Select Trip Photo";
            dlg.ShowDialog();
            SelectedPath = dlg.FileName.ToString();
        }
        private void ShowAddTripCanvas()
        {
            if (CurrentCanvas == AddTrip_Canvas)
                return;
            TripsScrollViewer.Visibility = Visibility.Visible;
            CurrentPanelName_Label.Content = "Add Trip";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = AddTrip_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
        }
    }
}
