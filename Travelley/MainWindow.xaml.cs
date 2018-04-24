
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
using System.IO;
using Microsoft.Win32;
using Travelley.FrontEnd;

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
        TourGuide ActiveTourGuide;
        public Trip ActiveTrip;
        Customer ActiveCustomer;

        string SelectedPath = "";

        TourGuide t;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }
        public MainWindow()
        {

            InitializeComponent();

            DataBase.Intialize();


            CurrentCanvas = Main_Canvas;

            //Trip NewTrip = new Trip("2", DataBase.TourGuides[0], "General", "Alex", "Cairo", 0, new DateTime(2018, 5, 1), new DateTime(2018, 5, 10));
            //NewTrip.TripImage = new CustomImage("D:/test.png");
            //DataBase.InsertTrip(NewTrip);

            int today = DateTime.Today.Day;
            if (DataBase.Trips.Count != 0)
                TripOfTheDay = DataBase.Trips[today % DataBase.Trips.Count]; //generate trip based on today's date
            if (DataBase.TourGuides.Count != 0)
                TourGuideOfTheMonth = TourGuide.GetBestTourGuide(DateTime.Today.Month - 1); //returns tour guide with maximum salary in the past month



            if (TripOfTheDay != null)
            {
                TripOfTheDay_IMG.Source = TripOfTheDay.TripImage.GetImage().Source;
                TripOfTheDay_Label.Content = TripOfTheDay.Departure + " - " + TripOfTheDay.Destination;
                ActiveTrip = TripOfTheDay;
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
            CustomerFullData_Email.Content = c.Email;
            CustomerFullData_Gender.Content = c.Gender;
            CustomerFullData_Language.Content = c.Languages[0].ToString();
            CustomerFullData_IMG.Source = c.UserImage.GetImage().Source;
            CustomerFullData_PhoneNumber.Content = c.PhoneNumber;
        }
        private void ShowTourGuideFullData(TourGuide t)
        {
            CurrentPanelName_Label.Content = "TourGuide Full Data";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = TourGuideFullData_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;

            TourGuideFullData_Name.Content = t.Name;
            TourGuideFullData_Nationality.Content = t.Nationality;
            TourGuideFullData_Id.Content = t.Id;
            TourGuideFullData_Email.Content = t.Email;
            TourGuideFullData_Gender.Content = t.Gender;
            //  TourGuideFullData_Language.Content = t.Languages[0].ToString();
            TourGuideFullData_IMG.Source = t.UserImage.GetImage().Source;
            TourGuideFullData_PhoneNumber.Content = t.PhoneNumber;

        }

        private void EditCustomerDetails(Customer c)
        {
            string name = c.Name, nationality = c.Nationality, phone_number = c.PhoneNumber,
                language = c.Languages[0], gender = c.Gender, email = c.Email;

            if (EditCustomerFullData_Name.Text != "")
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
        private void EditTourGuideData(TourGuide t)
        {
            string name = t.Name, nationality = t.Nationality, phone_number = t.PhoneNumber,
                language = t.Languages[0], gender = t.Gender, email = t.Email;

            if (EditTourGuideFullData_Name.Text != "")
                name = EditTourGuideFullData_Name.Text;
            if (EditTourGuideFullData_Nationality.Text != "")
                nationality = EditTourGuideFullData_Nationality.Text;
            if (EditTourGuideFullData_PhoneNumber.Text != "")
                phone_number = EditTourGuideFullData_PhoneNumber.Text;
            if (EditTourGuideFullData_Language.Text != "")
                language = EditTourGuideFullData_Language.Text;
            if (TourGuideGender_ComboBox.Text != "")
                gender = TourGuideGender_ComboBox.Text;
            if (EditTourGuideFullData_Email.Text != "")
                email = EditTourGuideFullData_Email.Text;
            //  DataBase.UpdateTourGuide(t, t.Id, name, nationality, gender, email, phone_number, t.UserImage);

        }
        private void TripOfTheDay_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowTripFullData(TripOfTheDay);
        }
        private void ShowAddTourGuideCanvas()
        {
            CurrentPanelName_Label.Content = "Add New TourGuide";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = AddNewTourGuide_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;

        }

        private void Customer_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowCustomerFullData(cus);
        }

        private void ShowListOfTrips(List<Trip> list)
        {

            for (int i = 0; i < list.Count; i++)
            {
                TripDisplayCard t = new TripDisplayCard(list[i], i, ref CurrentCanvas, this);
            }

            return;
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void TourGuide_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowTourGuideFullData(t);
        }



        private void AddCustomer_AddCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            bool NationalId = String.IsNullOrEmpty(AddCustomer_National_Id_TextBox.Text);
            bool name = String.IsNullOrEmpty(AddCustomer_Name_TextBox.Text);
            bool phone = String.IsNullOrEmpty(AddCustomer_Phone_TextBox.Text);
            bool email = String.IsNullOrEmpty(AddCustomer_Email_TextBox.Text);
            bool nationality = String.IsNullOrEmpty(AddCustomer_Nationality_TextBox.Text);
            bool gender = String.IsNullOrEmpty(AddCustomer_Gender_ComboBox.Text);

            if (NationalId || name || phone || email || nationality || gender)
            {
                AddCustomer_Error_Label.Content = "Please Fill All Fields!";
                return;
            }

            if (DataBase.CheckUniqueCustomerId(AddCustomer_National_Id_TextBox.Text.ToString()) == false)
            {
                AddCustomer_Error_Label.Content = "Customer Already Registered!";
                return;
            }

            List<String> languages = new List<String>();
            languages.Add("English");

            Customer NewCustomer = new Customer(
                AddCustomer_National_Id_TextBox.Text.ToString(),
                AddCustomer_Name_TextBox.Text.ToString(),
                AddCustomer_Nationality_TextBox.Text.ToString(),
                languages,
                AddCustomer_Gender_ComboBox.ToString(),
                AddCustomer_Email_TextBox.Text.ToString(),
                AddCustomer_Phone_TextBox.ToString()
                );

            DataBase.InsertCustomer(NewCustomer);

            AddCustomer_Error_Label.Content = "Customer Added Successfuly";

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
            EditCustomerFullData_Name.Text = CustomerFullData_Name.Content.ToString();

            EditCustomerFullData_Nationality.Text = CustomerFullData_Nationality.Content.ToString();
            EditCustomerFullData_Email.Text = CustomerFullData_Email.Content.ToString();
            Gender_ComboBox.Text = CustomerFullData_Gender.Content.ToString();
            EditCustomerFullData_Language.Text = CustomerFullData_Language.Content.ToString();
            EditCustomerFullData_PhoneNumber.Text = CustomerFullData_PhoneNumber.Content.ToString();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAddTripCanvas();
        }

        private void ShowEditTrip_Canvas(Trip t)
        {
            CurrentPanelName_Label.Content = "Edit Trip Data";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = EditTrip_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;

            EditTrip_TripIDTextbox.Text = TripFullData_TripId.Content.ToString();
            EditTrip_TripDeptTextbox.Text = TripFullData_DepartureAndDestination.Content.ToString().Split('-')[0].Trim();
            EditTrip_TripDestTextbox.Text = TripFullData_DepartureAndDestination.Content.ToString().Split('-')[1].Trim();
            EditTrip_TripDiscTextbox.Text = TripFullData_Discount.Content.ToString();
            EditTrip_EnTimePicker.Text = TripFullData_EndDate.Content.ToString();
            EditTrip_StTimePicker.Text = TripFullData_StartDate.Content.ToString();
            EditTrip_TourCombo.Text = TripFullData_TourGuideName.Content.ToString();



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

        public void TourGuideFullData_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentPanelName_Label.Content = "Edit TourGuide Data";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = EditTourGuideData_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            EditTourGuideFullData_Name.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            EditTourGuideFullData_Name.Text = TourGuideFullData_Name.Content.ToString();

            EditTourGuideFullData_Nationality.Text = TourGuideFullData_Nationality.Content.ToString();
            EditTourGuideFullData_Email.Text = TourGuideFullData_Email.Content.ToString();
            TourGuideGender_ComboBox.Text = TourGuideFullData_Gender.Content.ToString();
            //EditTourGuideFullData_Language.Text = TourGuideFullData_Language.Content.ToString();
            EditTourGuideFullData_PhoneNumber.Text = TourGuideFullData_PhoneNumber.Content.ToString();

        }
        private void AddTourGuide(TourGuide t)
        {
            string name = t.Name, nationality = t.Nationality, phone_number = t.PhoneNumber,
               language = t.Languages[0], gender = t.Gender, email = t.Email;

            if (AddTourGuideFullData_Name.Text != "")
                name = AddTourGuideFullData_Name.Text;
            if (AddTourGuideFullData_Nationality.Text != "")
                nationality = AddTourGuideFullData_Nationality.Text;
            if (AddTourGuideFullData_PhoneNumber.Text != "")
                phone_number = AddTourGuideFullData_PhoneNumber.Text;
            if (AddTourGuideFullData_language.Text != "")
                language = AddTourGuideFullData_language.Text;
            if (AddTourGuideGender_ComboBox.Text != "")
                gender = AddTourGuideGender_ComboBox.Text;
            if (AddTourGuideFullData_Email.Text != "")
                email = AddTourGuideFullData_Email.Text;



        }

        private void EditTourGuideData_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            EditTourGuideData(t);
        }

        private void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dlg.Title = "Select TourGuide Image";
            dlg.ShowDialog();
            dlg.FileName.ToString();
        }

        private void AddTourGuide_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            bool tourErrorFound = false;
            if (DataBase.CheckUniqueTourGuideId(t.Id) == true)
            {
                AddTourGuide_Error_ID.Content = "This id is already found!";
                tourErrorFound = true;

            }
            else AddTourGuide_Error_ID.Content = "";
            if (AddTourGuideFullData_Id.Text == "")
            {
                AddTourGuide_Error_ID.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
                AddTourGuide_Error_ID.Content = "";


            if (AddTourGuideFullData_Name.Text.Trim() == "")
            {
                AddTourGuide_Error_Name.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else AddTourGuide_Error_Name.Content = "";
            if (AddTourGuideFullData_Email.Text.Trim() == "")
            {
                AddTourGuide_Error_Email.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else AddTourGuide_Error_Email.Content = "";
            if (AddTourGuideFullData_language.Text.Trim() == "")
            {
                AddTourGuide_Error_Language.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else AddTourGuide_Error_Language.Content = "";
            if (AddTourGuideGender_ComboBox.Text == "")
            {
                AddTourGuide_Error_Gender.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else AddTourGuide_Error_Gender.Content = "";
            if (AddTourGuideFullData_Nationality.Text == "")
            {
                AddTourGuide_Error__Natinaity.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else AddTourGuide_Error__Natinaity.Content = "";
            if (AddTourGuideFullData_PhoneNumber.Text == "")
            {
                AddTourGuide_Error_PhoneNumber.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else AddTourGuide_Error_PhoneNumber.Content = "";
            //if(SelectedPath=="")
            //{
            //  AddTourGuide_Error__Image.Content = "This field can't be empty!";
            //tourErrorFound = true;

            //}
            if (tourErrorFound == true)
                return;

            //TourGuide temp = new TourGuide(AddTourGuideFullData_Id.Text, AddTourGuideFullData_Name.Text, AddTourGuideFullData_Nationality.Text
            //     , AddTourGuideGender_ComboBox.Text, AddTourGuideFullData_Email.Text, AddTourGuideFullData_PhoneNumber.Text);
            // temp.UserImage = new CustomImage(SelectedPath);
            AddTourGuide_Error__Natinaity.Content = "";
            AddTourGuide_Error_PhoneNumber.Content = "";
            AddTourGuide_Error_ID.Content = "";
            AddTourGuide_Error_Name.Content = "";
            AddTourGuide_Error_Email.Content = "";
            AddTourGuide_Error_Language.Content = "";
            AddTourGuide_Error_Gender.Content = "";
            AddTourGuideFullData_Email.Text = "";
            AddTourGuideFullData_Name.Text = "";
            AddTourGuideFullData_Id.Text = "";
            AddTourGuideFullData_Nationality.Text = "";
            AddTourGuideFullData_language.Text = "";
            AddTourGuideFullData_PhoneNumber.Text = "";
            AddTourGuideGender_ComboBox.Text = "";

            MessageBox.Show("TourGuide is Succesfully Added");
            //todo Image

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShowAddTourGuideCanvas();
        }

        private void ShowTicketsTypes(Trip CurrentTrip)
        {
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentCanvas = TicketsTypes_Canvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            TicketsTypes_ScrollViewr.Visibility = Visibility.Visible;
            int index = 0;
            
            foreach(KeyValuePair<string, int> x in CurrentTrip.NumberOfSeats)
            {
                TicketsTypesCard T2 = new TicketsTypesCard(index, TicketsTypes_Canvas, CurrentTrip, x.Key, x.Value, CurrentTrip.PriceOfSeat[x.Key]);
                index++;
            }
        }

        private void TicketsTypes_Canvas_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TicketsTypes_Canvas.Visibility == Visibility.Hidden)
                TicketsTypes_Canvas.Visibility = Visibility.Hidden;
        }

        private void EditTrip_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EditTrip_Discount_ErrorLabel.Content = "";
            EditTrip_TourGuide_ErrorLabel.Content = "";
            EditTrip_TripDep_ErrorLabel.Content = "";
            EditTrip_TripDes_ErrorLabel.Content = "";
            EditTrip_TripEnTime_ErrorLabel.Content = "";
            EditTrip_TripID_ErrorLabel.Content = "";
            EditTrip_TripPhoto_ErrorLabel.Content = "";
            EditTrip_TripStTime_ErrorLabel.Content = "";
            



            bool errorfound = false;
            if (EditTrip_TripIDTextbox.Text.Trim() == "")
            {
                EditTrip_TripID_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (DataBase.CheckUniqueTripId(EditTrip_TripIDTextbox.Text) == false)
            {
                EditTrip_TripID_ErrorLabel.Content = "This ID is already used";
                errorfound = true;
            }
            if (EditTrip_TripDeptTextbox.Text.Trim() == "")
            {
                EditTrip_TripDep_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (EditTrip_TripDestTextbox.Text.Trim() == "")
            {
                EditTrip_TripDes_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (EditTrip_TripDiscTextbox.Text.Trim() == "")
            {
                EditTrip_Discount_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (EditTrip_StTimePicker.SelectedDate < DateTime.Today)
            {
                EditTrip_TripStTime_ErrorLabel.Content = "Trip can't start before today!";
                errorfound = true;
            }
            if (EditTrip_EnTimePicker.SelectedDate < DateTime.Today)
            {
                EditTrip_TripEnTime_ErrorLabel.Content = "Trip can't end before today!";
                errorfound = true;
            }
            if (EditTrip_EnTimePicker.SelectedDate < EditTrip_StTimePicker.SelectedDate)
            {
                EditTrip_TripEnTime_ErrorLabel.Content = "Trip can't end before start time!";
                errorfound = true;
            }
            if (SelectedPath == "")
            {
                EditTrip_TripPhoto_ErrorLabel.Content = "You must choose photo!";
                errorfound = true;
            }
            if (EditTrip_EnTimePicker.Text == "")
            {
                EditTrip_TripEnTime_ErrorLabel.Content = "You must choose end time!";
                errorfound = true;
            }
            if (EditTrip_StTimePicker.Text == "")
            {
                EditTrip_TripStTime_ErrorLabel.Content = "You must choose start time!";
                errorfound = true;
            }
            if (errorfound == true)
            {
                return;
            }
            //string type = "Family";
            //  DataBase.UpdateTrip(ActiveTrip, EditTrip_TripIDTextbox.Text, EditTrip_TourCombo.Text, type, EditTrip_TripDeptTextbox.Text, EditTrip_TripDestTextbox.Text, double.Parse(EditTrip_TripDiscTextbox.Text),DateTime.Parse( EditTrip_StTimePicker.Text),DateTime.Parse( EditTrip_EnTimePicker.Text), new CustomImage(SelectedPath));
            //Todo Fe moseba hna fel type
            //Todo check datetime
        }

        private void TripFullData_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowEditTrip_Canvas(ActiveTrip);
        }
    }
}

