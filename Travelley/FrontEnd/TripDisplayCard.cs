using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using Travelley;
using Travelley.Back_End;
using System.Windows;
using System.Media;
using System.Windows.Media;
using System.Windows.Input;

namespace Travelley
{
    public class TripDisplayCard
    {
        MainWindow window;
        Trip FullTripData;
        Rectangle BackGround;
        Image TripImage;
        Label DepartureAndDestination;
        Label FromStartToEndDate;
        Button MoreInfo;
        Label Status_Label;

       public  TripDisplayCard(Trip t, int index,ref Canvas c, MainWindow m)
        {
            FullTripData = t;
            window = m;
            double baseLoc = (index * 215)+100;
            BackGround = new Rectangle();
            BackGround.Width = 808;
            BackGround.Height = 210;
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, baseLoc);
            BackGround.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            BackGround.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            BackGround.StrokeThickness = 3;
            c.Children.Add(BackGround);

            TripImage = new Image();
            TripImage.Width = 178;
            TripImage.Height = 188;
            Canvas.SetLeft(TripImage, 154);
            Canvas.SetTop(TripImage, baseLoc + 1);
            TripImage.Source = t.TripImage.GetImage().Source;
            c.Children.Add(TripImage);

            DepartureAndDestination = new Label();
            Canvas.SetLeft(DepartureAndDestination, 343);
            Canvas.SetTop(DepartureAndDestination, baseLoc + 1);
            DepartureAndDestination.FontSize = 30;
            DepartureAndDestination.FontWeight = FontWeights.Bold;
            DepartureAndDestination.HorizontalAlignment = HorizontalAlignment.Left;
            DepartureAndDestination.VerticalAlignment = VerticalAlignment.Center;
            DepartureAndDestination.Content = t.Departure + " - " + t.Destination;
            c.Children.Add(DepartureAndDestination);

            FromStartToEndDate = new Label();
            Canvas.SetLeft(FromStartToEndDate, 343);
            Canvas.SetTop(FromStartToEndDate, baseLoc + 64);
            FromStartToEndDate.FontSize = 30;
            FromStartToEndDate.FontWeight = FontWeights.Bold;
            FromStartToEndDate.HorizontalAlignment = HorizontalAlignment.Left;
            FromStartToEndDate.VerticalAlignment = VerticalAlignment.Center;
            FromStartToEndDate.Content = "From " + t.Start.ToShortDateString() + " to " + t.End.ToShortDateString();
            c.Children.Add(FromStartToEndDate);

            Status_Label = new Label();
            Canvas.SetLeft(Status_Label, 343);
            Canvas.SetTop(Status_Label, baseLoc + 120);
            Status_Label.FontSize = 30;
            Status_Label.FontWeight = FontWeights.Bold;
            if (t.IsClosed)
                Status_Label.Content = "Status: closed";
            else
                Status_Label.Content = "Status: Opened";
            c.Children.Add(Status_Label);

            MoreInfo = new Button();
            MoreInfo.Content = "View More";
            Canvas.SetLeft(MoreInfo, 750);
            Canvas.SetTop(MoreInfo, baseLoc + 153);
            MoreInfo.Width = 152;
            MoreInfo.Height = 48;
            MoreInfo.Background = new SolidColorBrush(Color.FromRgb(232, 126, 49));
            MoreInfo.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            MoreInfo.FontSize = 20;
            MoreInfo.Click += MoreInfo_Click;
            MoreInfo.Cursor = Cursors.Hand;
            c.Children.Add(MoreInfo);

            c.Height = baseLoc + 230;
        }

        private void MoreInfo_Click(object sender, RoutedEventArgs e)
        {
            window.ActiveTrip = FullTripData;
            window.ShowTripFullData(FullTripData);
        }
        
    }
}
