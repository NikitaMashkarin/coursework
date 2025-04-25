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
                    Adress = AddressTextBox.Text,
                    Passport_series = PassportSeriesTextBox.Text,
                    Passport_number = PassportNumberTextBox.Text,
                    SNILS = SNILSTextBox.Text
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
