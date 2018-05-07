using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Travelley.Back_End;
using Travelley.FrontEnd;

namespace Travelley
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Canvas CurrentCanvas;
        public ScrollViewer CurrentScrollViewer;
        Trip TripOfTheDay;
        TourGuide TourGuideOfTheMonth;
        TourGuide ActiveTourGuide;
        public Trip ActiveTrip;
        public Customer ActiveCustomer;
        public static Currency CurrentCurrency;
        public static List<Ticket> LastTransactions;
        public static List<TourGuide> LastTourGuides;
        string SelectedPath = "";

        #region window

        public MainWindow()
        {

            InitializeComponent();

            DataBase.Intialize();

            CurrentScrollViewer = Customers_ScrollViewer;
            CurrentCanvas = Main_Canvas;

            Currency_ComboBox.Items.Add(new EGP());
            Currency_ComboBox.Items.Add(new Dollar());
            Currency_ComboBox.Items.Add(new EURO());
            Currency_ComboBox.Items.Add(new RiyalSaudi());
            Currency_ComboBox.SelectedItem = Currency_ComboBox.Items[0];

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DataBase.ShutDown();
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

        #endregion window

        #region Main_Canvas

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateCurrentCanvas(Main_Canvas, "Home Page");
        }

        private void Trips_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfTrips(Trip.Trips);
        }

        private void Customer_Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfCustomers(Customer.Customers);
        }

        private void TourGuide_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfTourGuides(TourGuide.TourGuides);
        }

        public void Transactions_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfTickets(GetAllTickets());
        }

        private void TripOfTheDay_IMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TripOfTheDay == null)
                return;
            ActiveTrip = TripOfTheDay;
            ShowTripFullData(TripOfTheDay);
        }

        private void Best_TourGuide_IMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TourGuideOfTheMonth != null)
                ShowTourGuideFullData(TourGuideOfTheMonth);
        }

        public void UpdateCurrentCanvas(Canvas NewCanvas, string Header, ScrollViewer NewScrollViewer = null, bool dynamic = false)
        {
            SelectedPath = "";
            CurrentCanvas.Visibility = Visibility.Hidden;
            CurrentScrollViewer.Visibility = Visibility.Hidden;
            if (NewScrollViewer != null)
            {
                CurrentScrollViewer = NewScrollViewer;
                if (CurrentCanvas != NewCanvas)
                    CurrentScrollViewer.ScrollToHome();
                CurrentScrollViewer.Visibility = Visibility.Visible;
            }
            CurrentCanvas = NewCanvas;
            CurrentCanvas.Visibility = Visibility.Visible;
            if (dynamic)
            {
                CurrentCanvas.Children.Clear(); CurrentCanvas.Height = 100;
            }
            CurrentPanelName_Label.Content = Header;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentCurrency = Currency_ComboBox.SelectedItem as Currency;
            if (CurrentCanvas == TourGuideFullData_Canvas)
            {
                ShowTourGuideFullData(ActiveTourGuide);
            }
            if (CurrentCanvas == TicketsTypes_Canvas)
            {
                ShowTicketsTypes(ActiveTrip);
            }
            if (CurrentCanvas == ReserveTicket_Canvas)
            {
                int num = int.Parse(ReserveTicket_NumberOfSeats_TextBox.Text);
                ReserveTicket_NumberOfSeats_TextBox.Text = "";
                ReserveTicket_NumberOfSeats_TextBox.Text = num.ToString();
            }
            if (CurrentCanvas == Transactions_Canvas)
            {
                ShowListOfTickets(LastTransactions);
            }
            if (CurrentCanvas == TourGuides_Canvas)
            {
                ShowListOfTourGuides(LastTourGuides);
            }
        }

        private void Main_Canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Main_Canvas.Visibility == Visibility.Visible)
            {
                int today = DateTime.Today.Day;
                if (Trip.Trips.Count != 0)
                {
                    int index = today % Trip.Trips.Count;
                    TripOfTheDay = Trip.Trips[index]; //generate trip based on today's date

                    if (TripOfTheDay.IsClosed) //trip of the day can't be closed
                    {
                        int i = 0;
                        index = (index + 1) % Trip.Trips.Count;
                        while (true)
                        {
                            if (i == Trip.Trips.Count)
                            {
                                TripOfTheDay = null;
                                break;
                            }
                            if (!Trip.Trips[index].IsClosed)
                            {
                                TripOfTheDay = Trip.Trips[index];
                                break;
                            }
                            i++;
                            index = index + 1 % Trip.Trips.Count;
                        }
                    }

                }
                else
                    TripOfTheDay = null;
                if (TourGuide.TourGuides.Count != 0)
                    TourGuideOfTheMonth = TourGuide.GetBestTourGuide(DateTime.Today.Month - 1); //returns tour guide with maximum salary in the past month
                else
                    TourGuideOfTheMonth = null;



                if (TripOfTheDay != null)
                {
                    TripOfTheDay_IMG.Source = TripOfTheDay.TripImage.GetImage().Source;
                    TripOfTheDay_Label.Content = TripOfTheDay.Departure + " - " + TripOfTheDay.Destination;
                }
                else
                {
                    TripOfTheDay_IMG.Source = (new CustomImage("default-Trip-image.png")).GetImage().Source;
                    TripOfTheDay_Label.Content = "No Open Trips Yet !";
                }
                if (TourGuideOfTheMonth != null)
                {
                    Best_TourGuide_IMG.Source = TourGuideOfTheMonth.UserImage.GetImage().Source;
                    Best_TourGuide_Label.Content = TourGuideOfTheMonth.Name;
                }
                else
                {
                    Best_TourGuide_IMG.Source = (new CustomImage("default-user-image.png")).GetImage().Source;
                    Best_TourGuide_Label.Content = "Improve yourself";
                }

            }
        }
        #endregion Main_Canvas

        #region Trips

        private void ShowListOfTrips(List<Trip> list)
        {
            UpdateCurrentCanvas(Trips_Canvas, "Trips", TripsScrollViewer, true);
            Trip.Trips.Sort();

            Button AddTrip_Button = new Button
            {
                Content = "Add Trip",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Width = 238,
                Height = 77,
                FontSize = 36,
                FontWeight = FontWeights.Bold
            };
            AddTrip_Button.Click += Trips_AddTrip_Button_Click;
            Canvas.SetLeft(AddTrip_Button, 771);
            Canvas.SetTop(AddTrip_Button, 10);
            CurrentCanvas.Children.Add(AddTrip_Button);

            Label Trips_NumberOfOpenTrips_Label = new Label
            {
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                Content = "Number of open trips: " + Trip.GetNumberOfOpenTrips(),
            };
            Canvas.SetLeft(Trips_NumberOfOpenTrips_Label, 111);
            Canvas.SetTop(Trips_NumberOfOpenTrips_Label, 20);
            CurrentCanvas.Children.Add(Trips_NumberOfOpenTrips_Label);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].UpdateTripsStatus();
                TripDisplayCard t = new TripDisplayCard(list[i], i, ref CurrentCanvas, this);
            }
            return;
        }

        private void Trips_AddTrip_Button_Click(object sender, RoutedEventArgs e)
        {
            AddTrip_StTimePicker.SelectedDate = DateTime.Today;
            AddTrip_EnTimePicker.SelectedDate = DateTime.Today;
            AddTrip_Canvas_UpdateTourGuide_ComboBox();
            ShowAddTripCanvas();
        }

        private void ShowAddTripCanvas()
        {
            UpdateCurrentCanvas(AddTrip_Canvas, "Add Trip");
        }

        public void ShowTripFullData(Trip t)
        {
            ActiveTrip = t;
            UpdateCurrentCanvas(TripFullData_Canvas, "Trip Full Data");

            TripFullData_Discount.Content = t.Discount.ToString() + " %";
            TripFullData_DepartureAndDestination.Content = t.Departure + " - " + t.Destination;
            TripFullData_IMG.Source = t.TripImage.GetImage().Source;
            TripFullData_StartDate.Content = t.Start.ToShortDateString();
            TripFullData_EndDate.Content = t.End.ToShortDateString();
            TripFullData_TourGuideName.Content = t.Tour.Name;
            TripFullData_TripId.Content = t.TripId;
            TripFullData_Avaialbleseats_Label.Content = "Available seats: " + ActiveTrip.GetNumberOfAvailableSeats();

            if (ActiveTrip.IsClosed)
            {
                TripFullData_TripStatusClose_Label.Visibility = Visibility.Visible;
                TripFullData_TripStatusOpen_Label.Visibility = Visibility.Hidden;
            }
            else
            {
                TripFullData_TripStatusOpen_Label.Visibility = Visibility.Visible;
                TripFullData_TripStatusClose_Label.Visibility = Visibility.Hidden;
            }
        }

        private void ShowTicketsTypes(Trip CurrentTrip)
        {
            ActiveTrip = CurrentTrip;
            UpdateCurrentCanvas(TicketsTypes_Canvas, "Tickets Types", TicketsTypes_ScrollViewr, true);

            Button TicketsTypes_Add_Button = new Button
            {
                Content = "Add Ticket \n      Type",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Height = 100,
                FontSize = 36,
                FontWeight = FontWeights.Bold,
                Width = 200
            };
            TicketsTypes_Add_Button.Click += TicketsTypes_Add_Button_Click;
            Canvas.SetLeft(TicketsTypes_Add_Button, 820);
            Canvas.SetTop(TicketsTypes_Add_Button, 12);
            CurrentCanvas.Children.Add(TicketsTypes_Add_Button);

            int index = 0;
            foreach (KeyValuePair<string, int> x in CurrentTrip.NumberOfSeats)
            {
                TicketsTypesCard T2 = new TicketsTypesCard(index, TicketsTypes_Canvas, CurrentTrip, x.Key, x.Value, CurrentTrip.PriceOfSeat[x.Key]);
                index++;
            }
        }

        private void ShowEditTrip_Canvas(Trip t)
        {
            ActiveTrip = t;

            UpdateCurrentCanvas(EditTrip_Canvas, "Edit Trip Data");

            EditTrip_TripIDTextbox.Text = TripFullData_TripId.Content.ToString();
            EditTrip_TripDeptTextbox.Text = TripFullData_DepartureAndDestination.Content.ToString().Split('-')[0].Trim();
            EditTrip_TripDestTextbox.Text = TripFullData_DepartureAndDestination.Content.ToString().Split('-')[1].Trim();
            EditTrip_TripDiscTextbox.Text = ActiveTrip.Discount.ToString();
            EditTrip_EnTimePicker.Text = TripFullData_EndDate.Content.ToString();
            EditTrip_StTimePicker.Text = TripFullData_StartDate.Content.ToString();

            EditTrip_Discount_ErrorLabel.Content = "";
            EditTrip_TourGuide_ErrorLabel.Content = "";
            EditTrip_TripDep_ErrorLabel.Content = "";
            EditTrip_TripDes_ErrorLabel.Content = "";
            EditTrip_TripEnTime_ErrorLabel.Content = "";
            EditTrip_TripID_ErrorLabel.Content = "";
            EditTrip_TripPhoto_ErrorLabel.Content = "";
            EditTrip_TripStTime_ErrorLabel.Content = "";

        }

        private void AddTrip_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            //todo more error validations
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
            if (AddTrip_StTimePicker.SelectedDate <= DateTime.Today)
            {
                AddTrip_TripStTime_ErrorLabel.Content = "Trip can't start today or before!";
                errorfound = true;
            }
            if (AddTrip_EnTimePicker.SelectedDate <= DateTime.Today)
            {
                AddTrip_TripEnTime_ErrorLabel.Content = "Trip can't end today or before!";
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
            if (AddTrip_TourCombo.SelectedItem == null)
            {
                MessageBox.Show("You must choose a tourguide");
                errorfound = true;
            }
            if (errorfound == true)
            {
                return;
            }
            Trip T = new Trip(AddTrip_TripIDTextbox.Text, (TourGuide)AddTrip_TourCombo.SelectedItem, AddTrip_TripDeptTextbox.Text,
                AddTrip_TripDestTextbox.Text, double.Parse(AddTrip_TripDiscTextbox.Text),
                AddTrip_StTimePicker.SelectedDate.Value.Date, AddTrip_EnTimePicker.SelectedDate.Value, new CustomImage(SelectedPath), false);
            DataBase.InsertTrip(T);
            AddTrip_Clear_Canvas();
            ShowTicketsTypes(T);
        }

        private void AddTrip_Clear_Canvas()
        {
            AddTrip_TripIDTextbox.Clear();
            AddTrip_TripDeptTextbox.Clear();
            AddTrip_TripDestTextbox.Clear();
            AddTrip_TripDiscTextbox.Clear();
        }

        private void Trip_BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png",
                Title = "Select Trip Photo"
            };
            dlg.ShowDialog();
            SelectedPath = dlg.FileName.ToString();
        }

        private void TicketsTypes_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateCurrentCanvas(AddTicketType_Canvas, "Add Ticket", null, false);
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

            //todo more eror validations
            bool errorfound = false;
            if (EditTrip_TripIDTextbox.Text.Trim() == "")
            {
                EditTrip_TripID_ErrorLabel.Content = "This field can't be empty!";
                errorfound = true;
            }
            if (DataBase.CheckUniqueTripId(EditTrip_TripIDTextbox.Text) == false && EditTrip_TripIDTextbox.Text != ActiveTrip.TripId)
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
            if (EditTrip_StTimePicker.SelectedDate <= DateTime.Today)
            {
                EditTrip_TripStTime_ErrorLabel.Content = "Trip can't start today or before!";
                errorfound = true;
            }
            if (EditTrip_EnTimePicker.SelectedDate <= DateTime.Today)
            {
                EditTrip_TripEnTime_ErrorLabel.Content = "Trip can't end today or before!";
                errorfound = true;
            }
            if (EditTrip_EnTimePicker.SelectedDate < EditTrip_StTimePicker.SelectedDate)
            {
                EditTrip_TripEnTime_ErrorLabel.Content = "Trip can't end before start time!";
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
            if (EditTrip_TourCombo.SelectedItem == null)
            {
                MessageBox.Show("you must choose a tourguide");
                errorfound = true;
            }
            if (errorfound == true)
            {
                return;
            }

            CustomImage TripImage = ActiveTrip.TripImage;
            if (SelectedPath != "")
                TripImage = new CustomImage(SelectedPath);

            DataBase.UpdateTrip(ActiveTrip, new Trip(EditTrip_TripIDTextbox.Text, (TourGuide)EditTrip_TourCombo.SelectedItem, EditTrip_TripDeptTextbox.Text,
                EditTrip_TripDestTextbox.Text, double.Parse(EditTrip_TripDiscTextbox.Text), EditTrip_StTimePicker.SelectedDate.Value.Date, EditTrip_EnTimePicker.SelectedDate.Value.Date,
                TripImage, ActiveTrip.IsClosed));

            EditTrip_Discount_ErrorLabel.Content = "";
            EditTrip_TourGuide_ErrorLabel.Content = "";
            EditTrip_TripDep_ErrorLabel.Content = "";
            EditTrip_TripDes_ErrorLabel.Content = "";
            EditTrip_TripEnTime_ErrorLabel.Content = "";
            EditTrip_TripID_ErrorLabel.Content = "";
            EditTrip_TripPhoto_ErrorLabel.Content = "";
            EditTrip_TripStTime_ErrorLabel.Content = "";

            ShowListOfTrips(Trip.Trips);
        }

        private void TripFullData_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveTrip.IsClosed)
            {
                MessageBox.Show("Can't edit a closed trip");
                return;
            }
            ActiveTourGuide = ActiveTrip.Tour;
            ActiveTourGuide.Trips.Remove(ActiveTrip);
            ShowEditTrip_Canvas(ActiveTrip);
        }

        private void TripFullData_ReserveTrip_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveTrip.IsClosed)
            {
                MessageBox.Show("Canot reserve a closed trip");
                return;
            }
            ShowNewOrExistingCustomerCanvas();
        }

        private void AddTrip_StTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTrip_Canvas_UpdateTourGuide_ComboBox();
        }

        private void AddTrip_EnTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTrip_Canvas_UpdateTourGuide_ComboBox();
        }

        private void AddTrip_Canvas_UpdateTourGuide_ComboBox()
        {
            if (AddTrip_StTimePicker.SelectedDate == null || AddTrip_EnTimePicker.SelectedDate == null)
                return;
            AddTrip_TourCombo.Items.Clear();
            DateTime start = AddTrip_StTimePicker.SelectedDate.Value.Date;
            DateTime end = AddTrip_EnTimePicker.SelectedDate.Value.Date;
            if (start > end)
                return;
            foreach (TourGuide T in TourGuide.TourGuides)
            {
                if (T.CheckAvailability(start, end))
                    AddTrip_TourCombo.Items.Add(T);
            }
            if (AddTrip_TourCombo.Items.Count > 0)
                AddTrip_TourCombo.SelectedItem = AddTrip_TourCombo.Items[0];
        }

        private void EditTrip_StTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EditTrip_Canvas_UpdateTourGuide_ComboBox();
        }

        private void EditTrip_EnTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EditTrip_Canvas_UpdateTourGuide_ComboBox();
        }

        private void EditTrip_Canvas_UpdateTourGuide_ComboBox()
        {
            if (EditTrip_StTimePicker.SelectedDate == null || EditTrip_EnTimePicker.SelectedDate == null)
                return;
            EditTrip_TourCombo.Items.Clear();
            DateTime start = EditTrip_StTimePicker.SelectedDate.Value.Date;
            DateTime end = EditTrip_EnTimePicker.SelectedDate.Value.Date;
            if (start > end)
                return;
            foreach (TourGuide T in TourGuide.TourGuides)
            {
                if (T.CheckAvailability(start, end))
                    EditTrip_TourCombo.Items.Add(T);
            }
            if (EditTrip_TourCombo.Items.Count > 0)
                EditTrip_TourCombo.SelectedItem = EditTrip_TourCombo.Items[0];
        }

        private void TripFullData_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            DataBase.DeleteTrip(ActiveTrip);
            ShowListOfTrips(Trip.Trips);
        }

        private void TripFullData_TicketTypes_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowTicketsTypes(ActiveTrip);
        }

        private void AddTicketType_Canvas_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (AddTicketType_Type_TextBox.Text == "")
            {
                MessageBox.Show("Please enter a ticket type");
                return;
            }
            if (!int.TryParse(AddTicketType_NumberOfSeats_TextBox.Text, out int num))
            {
                MessageBox.Show("Please enter a valid number of seats");
                return;
            }
            if (!double.TryParse(AddTicketType_Price_TextBox.Text, out double price))
            {
                MessageBox.Show("Please enter a valid price");
                return;
            }
            string TicketType = AddTicketType_Type_TextBox.Text;
            if (ActiveTrip.NumberOfSeats.ContainsKey(TicketType))
            {
                MessageBox.Show("Ticket Type already exists");
                return;
            }
            price = CurrentCurrency.ToEGP(price);
            DataBase.InsertTripTickets(ActiveTrip.TripId, TicketType, num, price);
            AddTicketType_Type_TextBox.Text = "";
            AddTicketType_NumberOfSeats_TextBox.Text = "";
            AddTicketType_Price_TextBox.Text = "";
            ShowTicketsTypes(ActiveTrip);
        }

        private void ReserveTicket_Reserve_Button_Click(object sender, RoutedEventArgs e)
        {
            TripType tripType = null;
            if (ReserveTicket_TripType_ComboxBox.Text == "Family")
                tripType = new Family();
            else if (ReserveTicket_TripType_ComboxBox.Text == "Couple")
                tripType = new Couple();
            else if (ReserveTicket_TripType_ComboxBox.Text == "General")
                tripType = new General();
            else if (ReserveTicket_TripType_ComboxBox.Text == "Lonely")
                tripType = new Lonely();
            else if (ReserveTicket_TripType_ComboxBox.Text == "Friends")
                tripType = new Friends();

            if (ReserveTicket_TicketType_ComboxBox.SelectedItem == null)
            {
                MessageBox.Show("select a ticket type");
                return;
            }
            string ticketType = (string)ReserveTicket_TicketType_ComboxBox.SelectedItem;

            if (!(int.TryParse(ReserveTicket_NumberOfSeats_TextBox.Text, out int NumberOfSeats) || NumberOfSeats <= 0))
            {
                MessageBox.Show("Invalid Number of seats!!");
                return;
            }
            if (ActiveTrip.NumberOfSeats[ticketType] < NumberOfSeats)
            {
                MessageBox.Show("No enough seats available in this ticket tpye");
                return;
            }
            if (!tripType.InRange(NumberOfSeats))
            {
                MessageBox.Show("Range of " + ReserveTicket_TripType_ComboxBox.Text + " is " + tripType.minNumberOfSeats + " - " + tripType.maxNumberOfSeats);
                return;
            }
            Ticket obj = ActiveCustomer.ReserveTicket(ActiveTrip, tripType, ticketType, NumberOfSeats);
            DataBase.InsertTransactions(obj.SerialNumber, ActiveCustomer.Id, ActiveTrip.TripId, ticketType, ReserveTicket_TripType_ComboxBox.Text, obj.Price, NumberOfSeats);
            MessageBox.Show("Ticket added");
            ShowListOfTrips(Trip.Trips);
        }

        private void ReserveTicket_NumberOfSeats_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ReserveTicket_TicketType_ComboxBox.SelectedItem == null)
                return;
            int.TryParse(ReserveTicket_NumberOfSeats_TextBox.Text, out int num);
            num = Math.Max(num, 0);
            double discount = 1;
            discount -= ActiveTrip.Discount / 100;
            if (ActiveCustomer.Discount)
                discount -= 0.1;
            ReserveTicket_Price_TextBox.Text = CurrentCurrency.GetValue(((ActiveTrip.PriceOfSeat[ReserveTicket_TicketType_ComboxBox.SelectedItem.ToString()]) * num * Math.Max(0, discount))).ToString();
        }

        private void ReserveTicket_TicketType_ComboxBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string number = ReserveTicket_NumberOfSeats_TextBox.Text;
            ReserveTicket_NumberOfSeats_TextBox.Text = "";
            ReserveTicket_NumberOfSeats_TextBox.Text = number;
        }

        private void ReserveTicket_Canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ReserveTicket_Canvas.Visibility == Visibility.Hidden && ActiveCustomer.Tickets.Count == 0)
            {
                DataBase.DeleteCustomer(ActiveCustomer);
                ActiveCustomer = null;
                return;
            }
        }

        private void ShowReserveTicket()
        {
            UpdateCurrentCanvas(ReserveTicket_Canvas, "Reserve Ticket");

            ReserveTicket_CustomerName_Label.Content = ActiveCustomer.ToString();
            ReserveTicket_Trip_Label.Content = ActiveTrip.Departure + " - " + ActiveTrip.Destination;
            ReserveTicket_TripImage_Image.Source = ActiveTrip.TripImage.GetImage().Source;
            ReserveTicket_TicketType_ComboxBox.Items.Clear();
            ReserveTicket_NumberOfSeats_TextBox.Text = "";
            ReserveTicket_Price_TextBox.Text = "0";
            foreach (KeyValuePair<string, int> c in ActiveTrip.NumberOfSeats)
            {
                ReserveTicket_TicketType_ComboxBox.Items.Add(c.Key);
            }
            if (ReserveTicket_TicketType_ComboxBox.Items.Count > 0)
                ReserveTicket_TicketType_ComboxBox.SelectedItem = ReserveTicket_TicketType_ComboxBox.Items[0];
            if (ActiveCustomer.Discount == true)
            {
                MessageBox.Show("Customer have 10% discount");
            }
        }

        private void TripFullData_TourGuideName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowTourGuideFullData(ActiveTrip.Tour);
        }

        private void TripFullData_ShowTickets_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfTickets(ActiveTrip.Tickets);
        }

        private void EditTrip_Canvas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (CurrentCanvas.Visibility == Visibility.Hidden && ActiveTrip.Tour == ActiveTourGuide && !ActiveTourGuide.Trips.Contains(ActiveTrip))
            {
                ActiveTourGuide.Trips.Add(ActiveTrip);
            }
        }

        private void TripFullData_TripStatusOpen_Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActiveTrip.IsClosed = true;
            DataBase.UpdateTrip(ActiveTrip, new Trip(ActiveTrip.TripId, ActiveTrip.Tour, ActiveTrip.Departure, ActiveTrip.Destination, ActiveTrip.Discount, ActiveTrip.Start, ActiveTrip.End, ActiveTrip.TripImage, true));
            TripFullData_TripStatusOpen_Label.Visibility = Visibility.Hidden;
            TripFullData_TripStatusClose_Label.Visibility = Visibility.Visible;
        }

        private void TripFullData_TripStatusClose_Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ActiveTrip.Start > DateTime.Today)
            {
                ActiveTrip.IsClosed = false;
                DataBase.UpdateTrip(ActiveTrip, new Trip(ActiveTrip.TripId, ActiveTrip.Tour, ActiveTrip.Departure, ActiveTrip.Destination,
                    ActiveTrip.Discount, ActiveTrip.Start, ActiveTrip.End, ActiveTrip.TripImage, false));
                TripFullData_TripStatusClose_Label.Visibility = Visibility.Hidden;
                TripFullData_TripStatusOpen_Label.Visibility = Visibility.Visible;
            }
        }

        #endregion Trips

        #region customers

        private void ShowListOfCustomers(List<Customer> Customers)
        {
            UpdateCurrentCanvas(Customers_Canvas, "Customers", Customers_ScrollViewer, true);
            Customer.Customers.Sort();
            for (int i = 0; i < Customers.Count; i++)
                new CustomerDisplayCard(i, CurrentCanvas, Customers[i], this);
        }

        public void ShowCustomerFullData(Customer c)
        {
            ActiveCustomer = c;
            UpdateCurrentCanvas(CustomerFullDetails_Canvas, "Customer Full Data");

            CustomerFullData_Name.Content = c.Name;
            CustomerFullData_Nationality.Content = c.Nationality;
            CustomerFullData_Id.Content = c.Id;
            CustomerFullData_Email.Content = c.Email;
            CustomerFullData_Gender.Content = c.Gender;
            CustomerFullData_Language.Content = c.Language;
            CustomerFullData_IMG.Source = c.UserImage.GetImage().Source;
            CustomerFullData_PhoneNumber.Content = c.PhoneNumber;
            CustomerFullData_NumberOfTrips_Label.Content = "Number Of Trips: " + c.numberOfTrips;
            CustomerFullData_Discount_Label.Content = "Discount: " + c.Discount;
        }

        private void AddCustomer_AddCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            bool NationalId = String.IsNullOrEmpty(AddCustomer_National_Id_TextBox.Text);
            bool name = String.IsNullOrEmpty(AddCustomer_Name_TextBox.Text);
            bool phone = String.IsNullOrEmpty(AddCustomer_Phone_TextBox.Text);
            bool email = String.IsNullOrEmpty(AddCustomer_Email_TextBox.Text);
            bool nationality = String.IsNullOrEmpty(AddCustomer_Nationality_TextBox.Text);
            bool gender = String.IsNullOrEmpty(AddCustomer_Gender_ComboBox.Text);
            bool language = String.IsNullOrEmpty(AddCustomer_Language_TextBox.Text);
            bool image = String.IsNullOrEmpty(SelectedPath);
            if (NationalId || name || phone || email || nationality || gender || image)
            {
                AddCustomer_Error_Label.Content = "Please Fill All Fields!";
                return;
            }

            if (DataBase.CheckUniqueCustomerId(AddCustomer_National_Id_TextBox.Text.ToString()) == false)
            {
                AddCustomer_Error_Label.Content = "Customer Already Registered!";
                return;
            }


            Customer NewCustomer = new Customer(
                AddCustomer_National_Id_TextBox.Text,
                AddCustomer_Name_TextBox.Text,
                AddCustomer_Nationality_TextBox.Text,
                AddCustomer_Language_TextBox.Text,
                AddCustomer_Gender_ComboBox.Text,
                AddCustomer_Email_TextBox.Text,
                AddCustomer_Phone_TextBox.Text,
                new CustomImage(SelectedPath)
                );

            DataBase.InsertCustomer(NewCustomer);
            ActiveCustomer = NewCustomer;

            ShowReserveTicket();
        }

        private void CustomerFullData_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateCurrentCanvas(EditCustomerFullDetails_Canvas, "Edit Customer Data");

            EditCustomerFullData_Name.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            EditCustomerFullData_Name.Text = CustomerFullData_Name.Content.ToString();

            EditCustomerFullData_Nationality.Text = CustomerFullData_Nationality.Content.ToString();
            EditCustomerFullData_Email.Text = CustomerFullData_Email.Content.ToString();
            Gender_ComboBox.Text = CustomerFullData_Gender.Content.ToString();
            EditCustomerFullData_Language.Text = CustomerFullData_Language.Content.ToString();
            EditCustomerFullData_PhoneNumber.Text = CustomerFullData_PhoneNumber.Content.ToString();
        }

        private void EditCustomerDetails(Customer c)
        {
            string name = c.Name, nationality = c.Nationality, phone_number = c.PhoneNumber,
                language = c.Language, gender = c.Gender, email = c.Email;


            bool tourErrorFound = false;


            if (EditCustomerFullData_Name.Text == "")
            {
                EditCustomer_Name_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
                EditCustomer_Name_ErrorLabel.Content = "";


            if (EditCustomerFullData_Nationality.Text.Trim() == "")
            {
                EditCustomer_Nationality_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else EditCustomer_Nationality_ErrorLabel.Content = "";
            if (EditCustomerFullData_PhoneNumber.Text.Trim() == "")
            {
                EditCustomer_PhoneNumber_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else EditCustomer_PhoneNumber_ErrorLabel.Content = "";
            if (EditCustomerFullData_Language.Text.Trim() == "")
            {
                EditCustomer_Language_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else EditCustomer_Language_ErrorLabel.Content = "";
            if (EditCustomerFullData_Email.Text == "")
            {
                EditCustomer_Email_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else EditCustomer_Email_ErrorLabel.Content = "";
            if (Gender_ComboBox.Text == "")
            {
                EditCustomer_Gender_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else EditCustomer_Gender_ErrorLabel.Content = "";


            if (tourErrorFound == true)
                return;

            name = EditCustomerFullData_Name.Text;

            nationality = EditCustomerFullData_Nationality.Text;

            phone_number = EditCustomerFullData_PhoneNumber.Text;

            language = EditCustomerFullData_Language.Text;

            gender = Gender_ComboBox.Text;

            email = EditCustomerFullData_Email.Text;

            EditCustomerFullData_Name.Text = "";
            EditCustomerFullData_Nationality.Text = "";
            EditCustomerFullData_PhoneNumber.Text = "";
            EditCustomerFullData_Language.Text = "";
            Gender_ComboBox.Text = "";
            EditCustomerFullData_Email.Text = "";

            CustomImage UserImage = ActiveCustomer.UserImage;
            if (SelectedPath != "")
                UserImage = new CustomImage(SelectedPath);

            DataBase.UpdateCustomer(ActiveCustomer, new Customer(ActiveCustomer.Id, name, nationality, language, gender, email, phone_number, UserImage));

            MessageBox.Show("Customer Updated");
            ShowListOfCustomers(Customer.Customers);
        }

        private void EditCustomerData_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            EditCustomerDetails(ActiveCustomer);
        }

        private void ShowNewOrExistingCustomerCanvas()
        {
            UpdateCurrentCanvas(NewOrExistingCustomer_Canvas, "Set Customer Status");
        }

        private void GetCustomerById_Done_Button_Click(object sender, RoutedEventArgs e)
        {
            Customer SelectedCustomer = DataBase.SelectCustomer(GetCustomerById_CustomerId_TextBox.Text.ToString());
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Customer id invalid, Please Enter valid one");
                return;
            }
            ActiveCustomer = SelectedCustomer;
            ShowReserveTicket();
        }

        private void NewOrExistingCustomer_New_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAddCustomerCanvas();
        }

        private void NewOrExistingCustomer_Existing_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowGetCustomerById();
        }

        private void ShowGetCustomerById()
        {
            UpdateCurrentCanvas(GetCustomerById_Canvas, "Get Customer By Id");
            GetCustomerById_CustomerId_TextBox.Text = "";
        }

        private void ShowAddCustomerCanvas()
        {
            UpdateCurrentCanvas(AddCustomer_Canvas, "Add Customer");
            AddCustomer_Name_TextBox.Text = "";
            AddCustomer_Email_TextBox.Text = "";
            AddCustomer_Language_TextBox.Text = "";
            AddCustomer_National_Id_TextBox.Text = "";
            AddCustomer_Phone_TextBox.Text = "";
            AddCustomer_Gender_ComboBox.SelectedItem = "Male";
            AddCustomer_Nationality_TextBox.Text = "";
            AddCustomer_Language_TextBox.Text = "";
        }

        private void AddCustomer_Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png",
                Title = "Select Customer Photo"
            };
            dlg.ShowDialog();
            SelectedPath = dlg.FileName.ToString();
        }

        private void Customer_AddCustomer_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAddCustomerCanvas();
        }

        private void CustomerFullData_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            DataBase.DeleteCustomer(ActiveCustomer);
            ShowListOfCustomers(Customer.Customers);
        }

        private void CustomerFullData_ShowTickets_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfTickets(ActiveCustomer.Tickets);
        }

        #endregion customers

        #region TourGuides

        private void ShowListOfTourGuides(List<TourGuide> TourGuides)
        {
            LastTourGuides = TourGuides;
            UpdateCurrentCanvas(TourGuides_Canvas, "Tour Guides", TourGuides_ScrollViewer, true);
            TourGuide.TourGuides.Sort();

            Button TourGuides_AddTourGuide_Button = new Button
            {
                Content = "Add TourGuide",
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Height = 77,
                FontSize = 36,
                FontWeight = FontWeights.Bold,
                Width = 300,
            };
            TourGuides_AddTourGuide_Button.Click += TourGuides_AddTourGuide_Button_Click;
            Canvas.SetLeft(TourGuides_AddTourGuide_Button, 713);
            Canvas.SetTop(TourGuides_AddTourGuide_Button, 12);
            CurrentCanvas.Children.Add(TourGuides_AddTourGuide_Button);

            Label AvailableTourGuides_Label = new Label
            {
                FontSize = 25,
                Content = "Available: " + DataBase.GetNumberOfAvailableTourGuides().ToString()
            };
            Canvas.SetLeft(AvailableTourGuides_Label, 111);
            Canvas.SetTop(AvailableTourGuides_Label, 12);
            CurrentCanvas.Children.Add(AvailableTourGuides_Label);

            for (int i = 0; i < TourGuides.Count; i++)
                new TourGuideDisplayCard(i, CurrentCanvas, TourGuides[i], this);
        }

        public void ShowTourGuideFullData(TourGuide t)
        {
            ActiveTourGuide = t;
            UpdateCurrentCanvas(TourGuideFullData_Canvas, "Tour Guide Full Data");

            TourGuideFullData_Name.Content = t.Name;
            TourGuideFullData_Nationality.Content = t.Nationality;
            TourGuideFullData_Id.Content = t.Id;
            TourGuideFullData_Email.Content = t.Email;
            TourGuideFullData_Gender.Content = t.Gender;
            TourGuideFullData_Language.Content = t.Language;
            TourGuideFullData_IMG.Source = t.UserImage.GetImage().Source;
            TourGuideFullData_PhoneNumber.Content = t.PhoneNumber;
            TourGuideFullData_Salary.Content = CurrentCurrency.GetValue(t.GetSalary(DateTime.Today.Month, DateTime.Today.Year)).ToString();

        }

        private void EditTourGuideData(TourGuide t)
        {
            string name = t.Name, nationality = t.Nationality, phone_number = t.PhoneNumber, language = t.Language, gender = t.Gender, email = t.Email;

            bool tourErrorFound = false;


            if (EditTourGuideFullData_Name.Text == "")
            {
                EditTourGuide_Name_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
            {
                EditTourGuide_Name_ErrorLabel.Content = "";
            }

            if (EditTourGuideFullData_Nationality.Text.Trim() == "")
            {
                EditTourGuide_Nationality_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
            {
                EditTourGuide_Nationality_ErrorLabel.Content = "";
            }
            if (EditTourGuideFullData_Email.Text.Trim() == "")
            {
                EditTourGuide_Email_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
            {
                EditTourGuide_Email_ErrorLabel.Content = "";
            }
            if (EditTourGuideFullData_Language.Text.Trim() == "")
            {
                EditTourGuide_Language_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
            {
                EditTourGuide_Language_ErrorLabel.Content = "";
            }
            if (TourGuideGender_ComboBox.Text == "")
            {
                EditTourGuide_Gender_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
            {
                EditTourGuide_Gender_ErrorLabel.Content = "";
            }
            if (EditTourGuideFullData_PhoneNumber.Text == "")
            {
                EditTourGuide_PhoneNumber_ErrorLabel.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
            {
                EditTourGuide_PhoneNumber_ErrorLabel.Content = "";
            }

            if (tourErrorFound == true)
                return;


            EditTourGuide_Name_ErrorLabel.Content = "";
            EditTourGuide_Email_ErrorLabel.Content = "";
            EditTourGuide_PhoneNumber_ErrorLabel.Content = "";
            EditTourGuide_Language_ErrorLabel.Content = "";
            EditTourGuide_Gender_ErrorLabel.Content = "";
            EditTourGuide_Nationality_ErrorLabel.Content = "";




            name = EditTourGuideFullData_Name.Text;

            nationality = EditTourGuideFullData_Nationality.Text;

            phone_number = EditTourGuideFullData_PhoneNumber.Text;

            language = EditTourGuideFullData_Language.Text;

            gender = TourGuideGender_ComboBox.Text;

            email = EditTourGuideFullData_Email.Text;

            CustomImage TourGuideImage = t.UserImage;
            if (SelectedPath != "")
                TourGuideImage = new CustomImage(SelectedPath);

            DataBase.UpdateTourGuide(t, new TourGuide(ActiveTourGuide.Id, name, nationality, language, gender, email, phone_number, TourGuideImage));
            MessageBox.Show("TourGuide updated");
            ShowListOfTourGuides(TourGuide.TourGuides);
        }

        private void ShowAddTourGuideCanvas()
        {
            UpdateCurrentCanvas(AddNewTourGuide_Canvas, "Add New TourGuide");
        }

        private void AddTourGuide(TourGuide t)
        {
            string name = t.Name, nationality = t.Nationality, phone_number = t.PhoneNumber,
               language = t.Language, gender = t.Gender, email = t.Email;

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

        private void AddTourGuide_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            bool tourErrorFound = false;


            if (AddTourGuideFullData_Id.Text == "")
            {
                AddTourGuide_Error_ID.Content = "This field can't be empty!";
                tourErrorFound = true;
            }
            else
                AddTourGuide_Error_ID.Content = "";
            if (!DataBase.CheckUniqueTourGuideId(AddTourGuideFullData_Id.Text))
            {
                AddTourGuide_Error_ID.Content = "Id not unique";
                tourErrorFound = true;
            }

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
            if (SelectedPath == "")
            {
                AddTourGuide_Error__Image.Content = "This field can't be empty!";
                tourErrorFound = true;

            }
            else AddTourGuide_Error__Image.Content = "";
            if (tourErrorFound == true)
                return;

            TourGuide temp = new TourGuide(AddTourGuideFullData_Id.Text, AddTourGuideFullData_Name.Text, AddTourGuideFullData_Nationality.Text
                 , AddTourGuideFullData_language.Text, AddTourGuideGender_ComboBox.Text, AddTourGuideFullData_Email.Text,
                 AddTourGuideFullData_PhoneNumber.Text, new CustomImage(SelectedPath));
            DataBase.InsertTourGuide(temp);
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
            ShowListOfTourGuides(TourGuide.TourGuides);

        }

        public void TourGuideFullData_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateCurrentCanvas(EditTourGuideData_Canvas, "Edit TourGuide Data");

            EditTourGuideFullData_Name.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            EditTourGuideFullData_Name.Text = TourGuideFullData_Name.Content.ToString();
            EditTourGuide_Name_ErrorLabel.Content = "";
            EditTourGuide_Email_ErrorLabel.Content = "";
            EditTourGuide_PhoneNumber_ErrorLabel.Content = "";
            EditTourGuide_Language_ErrorLabel.Content = "";
            EditTourGuide_Gender_ErrorLabel.Content = "";
            EditTourGuide_Nationality_ErrorLabel.Content = "";

            EditTourGuideFullData_Nationality.Text = TourGuideFullData_Nationality.Content.ToString();
            EditTourGuideFullData_Email.Text = TourGuideFullData_Email.Content.ToString();
            TourGuideGender_ComboBox.Text = TourGuideFullData_Gender.Content.ToString();
            EditTourGuideFullData_Language.Text = TourGuideFullData_Language.Content.ToString();
            EditTourGuideFullData_PhoneNumber.Text = TourGuideFullData_PhoneNumber.Content.ToString();
        }

        private void EditTourGuideData_Save_Button_Click(object sender, RoutedEventArgs e)
        {

            EditTourGuideData(ActiveTourGuide);
        }

        private void TourGuide_BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "JPG Files (*.jpg)|*.jpg|All files (*.*)|*.*",
                Title = "Select TourGuide Image"
            };
            dlg.ShowDialog();
            dlg.FileName.ToString();
        }


        private void TourGuides_AddTourGuide_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAddTourGuideCanvas();
        }

        private void TourGuideFullData_ShowTrips_Button_Click(object sender, RoutedEventArgs e)
        {
            ShowListOfTrips(ActiveTourGuide.Trips);
        }

        private void TourGuideFullData_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveTourGuide.Trips.Count > 0)
            {
                MessageBox.Show("Canot delete a tourguide who hava a trip");
                return;
            }
            DataBase.DeleteTourGuide(ActiveTourGuide);
            ActiveTourGuide = null;
            ShowListOfTourGuides(TourGuide.TourGuides);
        }

        #endregion TourGuides

        #region Transactions

        public List<Ticket> GetAllTickets()
        {
            List<Ticket> Tickets = new List<Ticket>();
            foreach (Trip CurrentTrip in Trip.Trips)
            {
                foreach (Ticket CurrentTicket in CurrentTrip.Tickets)
                {
                    Tickets.Add(CurrentTicket);
                }
            }
            return Tickets;
        }

        public void ShowListOfTickets(List<Ticket> Tickets)
        {
            UpdateCurrentCanvas(Transactions_Canvas, "Transactions", Transactions_ScrollViewer, true);
            LastTransactions = Tickets;
            for (int i = 0; i < Tickets.Count; i++)
            {
                Customer CurrentCustomer = null;
                foreach (Customer cus in Customer.Customers)
                {
                    if (cus.Tickets.Contains(Tickets[i]))
                    {
                        CurrentCustomer = cus;
                        break;
                    }
                }
                new TicketDisplayCard(i, CurrentCanvas, this, CurrentCustomer, Tickets[i].CurrentTrip, Tickets[i]);
            }
        }

        #endregion Transactions
    }
}

