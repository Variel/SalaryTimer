using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HourlySalary.Annotations;

namespace HourlySalary.ViewModels
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _salary;
        public string Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        private string _clockTime;
        public string ClockTime
        {
            get => _clockTime;
            set
            {
                _clockTime = value;
                OnPropertyChanged(nameof(ClockTime));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
