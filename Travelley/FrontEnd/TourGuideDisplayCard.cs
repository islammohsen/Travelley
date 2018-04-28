using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            BackGround = new Rectangle();
            BackGround.Width = 800;
            BackGround.Height = 220;
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            BackGround.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            BackGround.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            BackGround.StrokeThickness = 3;
            CurrentCanvas.Children.Add(BackGround);

            this.TourGuideImage = CurrentTourGuide.UserImage.GetImage();
            this.TourGuideImage.MaxHeight = 200;
            this.TourGuideImage.MaxWidth = 300;
            Canvas.SetLeft(this.TourGuideImage, 140);
            Canvas.SetTop(this.TourGuideImage, BaseLoc + 20);
            CurrentCanvas.Children.Add(this.TourGuideImage);

            this.TourGuideName = new Label();
            this.TourGuideName.Content = "Name: " + CurrentTourGuide.Name;
            this.TourGuideName.FontSize = 25;
            this.TourGuideName.FontWeight = FontWeights.Bold;
            this.TourGuideName.HorizontalAlignment = HorizontalAlignment.Left;
            this.TourGuideName.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.TourGuideName, 450);
            Canvas.SetTop(this.TourGuideName, BaseLoc + 20);
            CurrentCanvas.Children.Add(this.TourGuideName);

            this.TourGuideEmail = new Label();
            this.TourGuideEmail.Content = "Email: " + CurrentTourGuide.Email;
            this.TourGuideEmail.FontSize = 25;
            this.TourGuideEmail.FontWeight = FontWeights.Bold;
            this.TourGuideEmail.HorizontalAlignment = HorizontalAlignment.Left;
            this.TourGuideEmail.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.TourGuideEmail, 450);
            Canvas.SetTop(this.TourGuideEmail, BaseLoc + 50);
            CurrentCanvas.Children.Add(this.TourGuideEmail);

            this.View_More = new Button();
            this.View_More.Content = "View More";
            Canvas.SetLeft(this.View_More, 750);
            Canvas.SetTop(this.View_More, BaseLoc + 150);
            View_More.Width = 152;
            View_More.Height = 48;
            View_More.Background = new SolidColorBrush(Color.FromRgb(232, 126, 49));
            View_More.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            View_More.FontSize = 20;
            View_More.Click += View_More_Click;
            View_More.Cursor = Cursors.Hand;
            CurrentCanvas.Children.Add(this.View_More);

            CurrentCanvas.Height = BaseLoc + 250;
        }

        private void View_More_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.ShowTourGuideFullData(CurrentTourGuide);
        }
    }
}
