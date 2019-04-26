using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using HourlySalary.Annotations;

namespace HourlySalary.ViewModels
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        public WindowViewModel()
        {
            BlackColor = Color.FromRgb(20, 20, 20);
            WhiteColor = Color.FromRgb(235, 235, 235);
            BlackBrush = new SolidColorBrush(BlackColor);
            WhiteBrush = new SolidColorBrush(WhiteColor);
        }


        private readonly Color BlackColor;
        private readonly Color WhiteColor;

        private readonly SolidColorBrush BlackBrush;
        private readonly SolidColorBrush WhiteBrush;

        private MainWindow.ClockMode _currentClockMode = MainWindow.ClockMode.ClockOnly;

        private string[] _propertyNames = {
            nameof(ClockOnlyModeVisibility), nameof(BigClockModeVisibility),
            nameof(BigSalaryModeVisibility), nameof(SalaryOnlyModeVisibility)
        };

        public MainWindow.ClockMode CurrentClockMode
        {
            get => _currentClockMode;
            set
            {
                _currentClockMode = value;

                foreach (var prop in _propertyNames)
                    OnPropertyChanged(prop);
            }
        }

        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value; 
                OnPropertyChanged(nameof(IsDarkMode));
                OnPropertyChanged(nameof(ForegroundColor));
                OnPropertyChanged(nameof(ForegroundBrush));
                OnPropertyChanged(nameof(BackgroundColor));
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }

        public Visibility ClockOnlyModeVisibility => _currentClockMode == MainWindow.ClockMode.ClockOnly ? Visibility.Visible : Visibility.Collapsed;
        public Visibility BigClockModeVisibility => _currentClockMode == MainWindow.ClockMode.BigClock ? Visibility.Visible : Visibility.Collapsed;
        public Visibility BigSalaryModeVisibility => _currentClockMode == MainWindow.ClockMode.BigSalary ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SalaryOnlyModeVisibility => _currentClockMode == MainWindow.ClockMode.SalaryOnly ? Visibility.Visible : Visibility.Collapsed;
        public Color ForegroundColor => _isDarkMode ? BlackColor : WhiteColor;
        public Brush ForegroundBrush => _isDarkMode ? BlackBrush : WhiteBrush;
        public Color BackgroundColor => _isDarkMode ? WhiteColor : BlackColor;
        public Brush BackgroundBrush => _isDarkMode ? WhiteBrush : BlackBrush;



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
