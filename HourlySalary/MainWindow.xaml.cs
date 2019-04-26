using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HourlySalary.Properties;
using HourlySalary.ViewModels;

namespace HourlySalary
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum ClockMode
        {
            ClockOnly,
            BigClock,
            BigSalary,
            SalaryOnly
        }

        private bool _windowMoved = false;
        private Timer _timer;

        public DateTime StartTime;
        public DateTime FinishTime;
        public double MonthlySalary = 0;
        public WindowViewModel WindowViewModel;
        public TimerViewModel TimerViewModel;

        public MainWindow()
        {
            InitializeComponent();

            WindowViewModel = new WindowViewModel();
            TimerViewModel = new TimerViewModel();
            DataContext = WindowViewModel;

            ClockOnlyPanel.DataContext = BigClockPanel.DataContext =
                BigSalaryPanel.DataContext = SalaryOnlyPanel.DataContext = TimerViewModel;

            LoadSettings();

            _timer = new Timer(100);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void LoadSettings()
        {
            MonthlySalary = Settings.Default.MonthlySalary;
            WindowViewModel.CurrentClockMode = (ClockMode)Settings.Default.ClockMode;
            WindowViewModel.IsDarkMode = Settings.Default.DarkForeground;
            MenuIsDarkBackground.IsChecked = !WindowViewModel.IsDarkMode;
            StartTime = DateTime.Today.Add(Settings.Default.StartTime);
            FinishTime = DateTime.Today.Add(Settings.Default.FinishTime);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerViewModel.ClockTime = DateTime.Now.ToString("HH:mm:ss.f");

            var elapsedTime = DateTime.Now - StartTime;
            var totalTime = FinishTime - StartTime;
            var ratio = elapsedTime.TotalMilliseconds / totalTime.TotalMilliseconds;

            var dailySalary = MonthlySalary / new GregorianCalendar().GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var currentSalary = Math.Max(0, dailySalary * ratio);

            TimerViewModel.Salary = "W " + currentSalary.ToString("N" + (currentSalary > 1000 ? "0" : currentSalary > 100 ? "1" : currentSalary > 10 ? "2" : "3"));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            _windowMoved = false;
            DragMove();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_windowMoved)
                return;

            var modeInt = (int)WindowViewModel.CurrentClockMode;
            modeInt = (modeInt + 1) % 4;

            WindowViewModel.CurrentClockMode = (ClockMode)modeInt;
            Settings.Default.ClockMode = modeInt;
            Settings.Default.Save();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            _windowMoved = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuIsDarkBackground_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DarkForeground = false;
            WindowViewModel.IsDarkMode = false;
            Settings.Default.Save();
        }

        private void MenuIsDarkBackground_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DarkForeground = true;
            WindowViewModel.IsDarkMode = true;
            Settings.Default.Save();
        }

        private void MenuItemSettings_Click(object sender, EventArgs e)
        {
            new SettingsWindow().ShowDialog();
            LoadSettings();
        }
    }
}
