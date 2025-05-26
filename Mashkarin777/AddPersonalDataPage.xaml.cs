using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class AddPersonalDataPage : Page
    {
        public AddPersonalDataPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdTextBox.Text, out int workerId))
            {
                MessageBox.Show("Введите корректный ID работника.");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddressTextBox.Text) ||
                string.IsNullOrWhiteSpace(PassportSeriesTextBox.Text) ||
                string.IsNullOrWhiteSpace(PassportNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(SNILSTextBox.Text))
            {
                MessageBox.Show("Заполните все поля персональных данных.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var worker = context.Social_worker.FirstOrDefault(w => w.Id == workerId);
                if (worker == null)
                {
                    MessageBox.Show("Работник с указанным ID не найден.");
                    return;
                }

                var personalData = new Personal_data
                {
                    Adress = AddressTextBox.Text.Trim(),
                    Passport_series = PassportSeriesTextBox.Text.Trim(),
                    Passport_number = PassportNumberTextBox.Text.Trim(),
                    SNILS = SNILSTextBox.Text.Trim()
                };

                context.Personal_data.Add(personalData);
                context.SaveChanges();

                worker.Personal_data = personalData.Id;

                context.SaveChanges();

                MessageBox.Show("Персональные данные успешно сохранены и связаны с работником.");
            }
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
