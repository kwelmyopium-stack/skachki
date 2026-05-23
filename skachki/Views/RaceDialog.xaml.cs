using System.Collections.Generic;
using System.Windows;
using skachki.Models;

namespace skachki.Views
{
    public partial class RaceDialog : Window
    {
        public Race Race { get; private set; }

        public RaceDialog(List<Horse> horses, List<Jockey> jockeys)
        {
            InitializeComponent();
            Race = new Race();
            cmbHorse.ItemsSource  = horses;
            cmbJockey.ItemsSource = jockeys;
        }

        public RaceDialog(List<Horse> horses, List<Jockey> jockeys, Race race)
        {
            InitializeComponent();
            Race = race;
            cmbHorse.ItemsSource  = horses;
            cmbJockey.ItemsSource = jockeys;

            txtLocation.Text        = race.Location;
            dpDate.SelectedDate     = race.RaceDate;
            cmbHorse.SelectedValue  = race.HorseID;
            cmbJockey.SelectedValue = race.JockeyID;
            txtPlace.Text    = race.Place?.ToString()    ?? "";
            txtDistance.Text = race.Distance?.ToString() ?? "";
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Укажите место проведения.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Укажите дату заезда.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmbHorse.SelectedValue == null)
            {
                MessageBox.Show("Выберите лошадь.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cmbJockey.SelectedValue == null)
            {
                MessageBox.Show("Выберите жокея.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int? place = null;
            if (!string.IsNullOrWhiteSpace(txtPlace.Text))
            {
                if (!int.TryParse(txtPlace.Text, out int p) || p <= 0)
                {
                    MessageBox.Show("Позиция должна быть положительным числом.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                place = p;
            }

            int? distance = null;
            if (!string.IsNullOrWhiteSpace(txtDistance.Text))
            {
                if (!int.TryParse(txtDistance.Text, out int d) || d <= 0)
                {
                    MessageBox.Show("Дистанция должна быть положительным числом.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                distance = d;
            }

            Race.Location = txtLocation.Text.Trim();
            Race.RaceDate = dpDate.SelectedDate.Value;
            Race.HorseID  = (int)cmbHorse.SelectedValue;
            Race.JockeyID = (int)cmbJockey.SelectedValue;
            Race.Place    = place;
            Race.Distance = distance;
            DialogResult  = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
