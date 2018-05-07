using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
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

        public TripDisplayCard(Trip t, int index, ref Canvas c, MainWindow m)
        {
            FullTripData = t;
            window = m;
            double baseLoc = (index * 215) + 100;

            BackGround = new Rectangle
            {
                Width = 808,
                Height = 210,
                Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                StrokeThickness = 3
            };
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, baseLoc);
            c.Children.Add(BackGround);

            TripImage = new Image
            {
                Width = 178,
                Height = 188,
                Source = t.TripImage.GetImage().Source
            };
            Canvas.SetLeft(TripImage, 154);
            Canvas.SetTop(TripImage, baseLoc + 1);
            c.Children.Add(TripImage);

            DepartureAndDestination = new Label
            {
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Content = t.Departure + " - " + t.Destination
            };
            Canvas.SetLeft(DepartureAndDestination, 343);
            Canvas.SetTop(DepartureAndDestination, baseLoc + 1);
            c.Children.Add(DepartureAndDestination);

            FromStartToEndDate = new Label
            {
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Content = "From " + t.Start.ToShortDateString() + " to " + t.End.ToShortDateString()
            };
            Canvas.SetLeft(FromStartToEndDate, 343);
            Canvas.SetTop(FromStartToEndDate, baseLoc + 64);
            c.Children.Add(FromStartToEndDate);


            if (t.IsClosed)
            {
                Status_Label = new Label
                {
                    FontSize = 30,
                    FontWeight = FontWeights.Bold,
                    Content = "Status: closed"
                };
            }
            else
            {
                Status_Label = new Label
                {
                    FontSize = 30,
                    FontWeight = FontWeights.Bold,
                    Content = "Status: opened"
                };
            }
            Canvas.SetLeft(Status_Label, 343);
            Canvas.SetTop(Status_Label, baseLoc + 120);
            c.Children.Add(Status_Label);

            MoreInfo = new Button
            {
                Content = "View More",
                Width = 152,
                Height = 48,
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            MoreInfo.Click += MoreInfo_Click;
            Canvas.SetLeft(MoreInfo, 750);
            Canvas.SetTop(MoreInfo, baseLoc + 153);
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
