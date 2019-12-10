using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using TestStack.White;

namespace WorkConroller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int periodInSeconds = 300;
        public System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public System.Windows.Threading.DispatcherTimer dispatcherTimerRandomizer = new System.Windows.Threading.DispatcherTimer();

      
        public MainWindow()
        {
            InitializeComponent();
            ScreenCapture screenCapture = new ScreenCapture();
            String filename = @$"{Directory.GetCurrentDirectory()}\Sceenshots'ScreenCapture-{DateTime.Now.ToString("ddMMyyyy-hhmmss")}.png";
            System.Drawing.Image newScreen = (System.Drawing.Image)screenCapture.CaptureScreenShot();
            newScreen.Save(filename, ImageFormat.Png);
            currentPeriodTextBox.Text = $"Current period: {periodInSeconds.ToString()}";
            dispatcherTimer.Tick += new EventHandler(DispatcherTimerTick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, periodInSeconds);
            dispatcherTimer.Start();

            dispatcherTimerRandomizer.Tick += new EventHandler(DispatcherTimerTickRandomly);
            dispatcherTimerRandomizer.Interval = new TimeSpan(0, 0, new Random().Next(1, periodInSeconds));
            dispatcherTimerRandomizer.Start();

        }


        private void Save(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(periodTextBox.Text, out periodInSeconds))
            {
                MessageBox.Show("Введены некоректные данные! Введите период в секундах.");
            }
            else if (periodInSeconds < 60)
            {
                MessageBox.Show("Минимальное значение периода 60!");
            }
            currentPeriodTextBox.Text = $"Current period: {periodInSeconds.ToString()}";
            dispatcherTimer.Stop();
            dispatcherTimer.Interval = new TimeSpan(0, 0, periodInSeconds);
            dispatcherTimer.Start();
        }

        private void CloseToTray(object sender, RoutedEventArgs e)
        {
            mainWindow.Visibility = Visibility.Hidden;
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        { 
            dispatcherTimer.Stop();
            dispatcherTimer.Interval = new TimeSpan(0, 0, periodInSeconds);
            dispatcherTimer.Start();

            dispatcherTimerRandomizer.Interval = new TimeSpan(0, 0, new Random().Next(1, periodInSeconds));
            dispatcherTimerRandomizer.Start();
        }

        private void DispatcherTimerTickRandomly(object sender, EventArgs e)
        {
            ScreenCapture screenCapture = new ScreenCapture();
            String filename = @$"{Directory.GetCurrentDirectory()}\Sceenshots'ScreenCapture-{DateTime.Now.ToString("ddMMyyyy-hhmmss")}.png";
            System.Drawing.Image newScreen = (System.Drawing.Image)screenCapture.CaptureScreenShot();
            newScreen.Save(filename, ImageFormat.Png);
            dispatcherTimerRandomizer.Stop();
        }
        
    }
}
