using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace HourlySalary
{
    /// <summary>
    /// SettingsWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            TextSalary.Text = Settings.Default.MonthlySalary.ToString();
            StartTimeHour.SelectedValue = Settings.Default.StartTime.Hours.ToString();
            StartTimeMinute.SelectedValue = Settings.Default.StartTime.Minutes.ToString();
            FinishTimeHour.SelectedValue = Settings.Default.FinishTime.Hours.ToString();
            FinishTimeMinute.SelectedValue = Settings.Default.FinishTime.Minutes.ToString();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.Default.MonthlySalary = Double.Parse(TextSalary.Text);
                Settings.Default.StartTime = TimeSpan.FromMinutes(
                    Int32.Parse(StartTimeHour.SelectedValue.ToString()) * 60 +
                    Int32.Parse(StartTimeMinute.SelectedValue.ToString()));
                Settings.Default.FinishTime = TimeSpan.FromMinutes(
                    Int32.Parse(FinishTimeHour.SelectedValue.ToString()) * 60 +
                    Int32.Parse(FinishTimeMinute.SelectedValue.ToString()));

                Settings.Default.Save();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("올바른 값을 입력 해주세요");
            }
        }
    }
}
