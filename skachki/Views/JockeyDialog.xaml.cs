using System.Windows;
using skachki.Models;

namespace skachki.Views
{
    public partial class JockeyDialog : Window
    {
        public Jockey Jockey { get; private set; }

        public JockeyDialog()
        {
            InitializeComponent();
            Jockey = new Jockey();
        }

        public JockeyDialog(Jockey jockey)
        {
            InitializeComponent();
            Jockey = jockey;
            txtFirst.Text   = jockey.FirstName;
            txtLast.Text    = jockey.LastName;
            txtAge.Text     = jockey.Age.ToString();
            txtCountry.Text = jockey.Country;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirst.Text)   ||
                string.IsNullOrWhiteSpace(txtLast.Text)    ||
                string.IsNullOrWhiteSpace(txtAge.Text)     ||
                string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Возраст должен быть положительным числом.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Jockey.FirstName = txtFirst.Text.Trim();
            Jockey.LastName  = txtLast.Text.Trim();
            Jockey.Age       = age;
            Jockey.Country   = txtCountry.Text.Trim();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
