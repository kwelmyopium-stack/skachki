using System.Windows;
using skachki.Models;

namespace skachki.Views
{
    public partial class HorseDialog : Window
    {
        public Horse Horse { get; private set; }

        public HorseDialog()
        {
            InitializeComponent();
            Horse = new Horse();
        }

        public HorseDialog(Horse horse)
        {
            InitializeComponent();
            Horse = horse;
            txtName.Text  = horse.Name;
            txtBreed.Text = horse.Breed;
            txtAge.Text   = horse.Age.ToString();
            txtColor.Text = horse.Color;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)  ||
                string.IsNullOrWhiteSpace(txtBreed.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text)   ||
                string.IsNullOrWhiteSpace(txtColor.Text))
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
            Horse.Name  = txtName.Text.Trim();
            Horse.Breed = txtBreed.Text.Trim();
            Horse.Age   = age;
            Horse.Color = txtColor.Text.Trim();
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
