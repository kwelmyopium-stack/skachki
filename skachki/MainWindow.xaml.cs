using System.Windows;
using skachki.Models;
using skachki.Views;

namespace skachki
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        public MainWindow()
        {
            InitializeComponent();
            LoadAll();
        }

        private void LoadAll()
        {
            dgHorses.ItemsSource  = _db.GetHorses();
            dgJockeys.ItemsSource = _db.GetJockeys();
            dgRaces.ItemsSource   = _db.GetRaces();
        }

        // ==================== ЛОШАДИ ====================

        private void AddHorse_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new HorseDialog();
            if (dlg.ShowDialog() == true)
            {
                _db.AddHorse(dlg.Horse);
                dgHorses.ItemsSource = _db.GetHorses();
            }
        }

        private void EditHorse_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgHorses.SelectedItem is Horse horse))
            {
                MessageBox.Show("Выберите лошадь.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dlg = new HorseDialog(horse);
            if (dlg.ShowDialog() == true)
            {
                _db.UpdateHorse(dlg.Horse);
                dgHorses.ItemsSource = _db.GetHorses();
            }
        }

        private void DeleteHorse_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgHorses.SelectedItem is Horse horse))
            {
                MessageBox.Show("Выберите лошадь.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Удалить лошадь «{horse.Name}»?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _db.DeleteHorse(horse.HorseID);
                dgHorses.ItemsSource = _db.GetHorses();
            }
        }

        // ==================== ЖОКЕИ ====================

        private void AddJockey_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new JockeyDialog();
            if (dlg.ShowDialog() == true)
            {
                _db.AddJockey(dlg.Jockey);
                dgJockeys.ItemsSource = _db.GetJockeys();
            }
        }

        private void EditJockey_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgJockeys.SelectedItem is Jockey jockey))
            {
                MessageBox.Show("Выберите жокея.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dlg = new JockeyDialog(jockey);
            if (dlg.ShowDialog() == true)
            {
                _db.UpdateJockey(dlg.Jockey);
                dgJockeys.ItemsSource = _db.GetJockeys();
            }
        }

        private void DeleteJockey_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgJockeys.SelectedItem is Jockey jockey))
            {
                MessageBox.Show("Выберите жокея.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Удалить жокея «{jockey.FirstName} {jockey.LastName}»?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _db.DeleteJockey(jockey.JockeyID);
                dgJockeys.ItemsSource = _db.GetJockeys();
            }
        }

        // ==================== ЗАЕЗДЫ ====================

        private void AddRace_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new RaceDialog(_db.GetHorses(), _db.GetJockeys());
            if (dlg.ShowDialog() == true)
            {
                _db.AddRace(dlg.Race);
                dgRaces.ItemsSource = _db.GetRaces();
            }
        }

        private void EditRace_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgRaces.SelectedItem is Race race))
            {
                MessageBox.Show("Выберите заезд.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dlg = new RaceDialog(_db.GetHorses(), _db.GetJockeys(), race);
            if (dlg.ShowDialog() == true)
            {
                _db.UpdateRace(dlg.Race);
                dgRaces.ItemsSource = _db.GetRaces();
            }
        }

        private void DeleteRace_Click(object sender, RoutedEventArgs e)
        {
            if (!(dgRaces.SelectedItem is Race race))
            {
                MessageBox.Show("Выберите заезд.", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Удалить заезд «{race.Location}»?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _db.DeleteRace(race.RaceID);
                dgRaces.ItemsSource = _db.GetRaces();
            }
        }
    }
}
