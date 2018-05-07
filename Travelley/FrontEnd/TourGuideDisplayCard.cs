using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Travelley.FrontEnd
{
    public class TourGuideDisplayCard
    {
        Rectangle BackGround;
        Label TourGuideName;
        Label TourGuideEmail;
        Label TourGuideSalary;
        Image TourGuideImage;
        Button View_More;
        TourGuide CurrentTourGuide;
        MainWindow CurrentWindow;

        public TourGuideDisplayCard(int index, Canvas CurrentCanvas, TourGuide CurrentTourGuide, MainWindow CurrentWindow)
        {
            this.CurrentTourGuide = CurrentTourGuide;
            this.CurrentWindow = CurrentWindow;

            double BaseLoc = (index * 230) + 140;

            //creating BackGroung
            BackGround = new Rectangle
            {
                Width = 800,
                Height = 220,
                Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                StrokeThickness = 3
            };
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            CurrentCanvas.Children.Add(BackGround);

            TourGuideImage = CurrentTourGuide.UserImage.GetImage();
            TourGuideImage.MaxHeight = 200;
            TourGuideImage.MaxWidth = 300;
            Canvas.SetLeft(TourGuideImage, 140);
            Canvas.SetTop(TourGuideImage, BaseLoc + 20);
            CurrentCanvas.Children.Add(TourGuideImage);

            TourGuideName = new Label
            {
                Content = "Name: " + CurrentTourGuide.Name,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(TourGuideName, 450);
            Canvas.SetTop(TourGuideName, BaseLoc + 20);
            CurrentCanvas.Children.Add(TourGuideName);

            TourGuideEmail = new Label
            {
                Content = "Email: " + CurrentTourGuide.Email,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(TourGuideEmail, 450);
            Canvas.SetTop(TourGuideEmail, BaseLoc + 50);
            CurrentCanvas.Children.Add(TourGuideEmail);


            TourGuideSalary = new Label
            {
                Content = "Salary: " + MainWindow.CurrentCurrency.GetValue(CurrentTourGuide.GetSalary(DateTime.Today.Month, DateTime.Today.Year)),
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(TourGuideSalary, 450);
            Canvas.SetTop(TourGuideSalary, BaseLoc + 80);
            CurrentCanvas.Children.Add(TourGuideSalary);

           View_More = new Button
            {
                Content = "View More",
                Width = 152,
                Height = 48,
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            View_More.Click += View_More_Click;
            Canvas.SetLeft(this.View_More, 750);
            Canvas.SetTop(this.View_More, BaseLoc + 150);
            CurrentCanvas.Children.Add(this.View_More);

            CurrentCanvas.Height = BaseLoc + 250;
        }

        private void View_More_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.ShowTourGuideFullData(CurrentTourGuide);
        }
    }
}
