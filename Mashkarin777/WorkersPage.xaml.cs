using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mashkarin777
{
    /// <summary>
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        public WorkersPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
        private void BtnGetAllWorker_Click(object sender, RoutedEventArgs e)
        {
            //using (var context = new mashkarin777Entities())
            //{
            //    var workers = context.Social_worker.ToList();

            //    if (workers.Count == 0)
            //    {
            //        MessageBox.Show("Работники не найдены.");
            //        return;
            //    }

            //    string output = string.Join("\n\n", workers.Select(w =>
            //        $"ID: {w.Id}\nФИО: {w.Full_name}\nТелефон: {w.Phone}\nEmail: {w.Email}"));

            //    MessageBox.Show(output, "Все работники", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new WorkersTablePage());
        }

        private void BtnAddPersonalData_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddPersonalDataPage());
        }

        private void BtnCreateWorker_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CreateWorkerPage());
        }

        private void BtnGetWorkerById_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PersonalDataIdTextBox.Text, out int Id))
            {
                MessageBox.Show("Введите корректный ID персональных данных.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var workers = context.Social_worker
                    .Where(sw => sw.Id == Id)
                    .ToList();

                if (workers.Count == 0)
                {
                    MessageBox.Show("Сотрудники с такими id не найден.");
                    return;
                }

                string output = string.Join("\n\n", workers.Select(w =>
                    $"ID: {w.Id}\nФИО: {w.Full_name}\nТелефон: {w.Phone}\nEmail: {w.Email}"));

                MessageBox.Show(output, "Сотрудники по ID", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnGetPersanalDataById_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(WorkerIdTextBox.Text, out int workerId))
            {
                MessageBox.Show("Введите корректный ID сотрудника.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var worker = context.Social_worker
                    .Include("Personal_data1")
                    .FirstOrDefault(w => w.Id == workerId);

                if (worker?.Personal_data1 == null)
                {
                    MessageBox.Show("Персональные данные не найдены для указанного сотрудника.");
                    return;
                }

                var data = worker.Personal_data1;
                string output = $"ID: {data.Id}\nАдрес: {data.Adress}\nСерия паспорта: {data.Passport_series}\nНомер паспорта: {data.Passport_number}\nСНИЛС: {data.SNILS}";

                MessageBox.Show(output, "Персональные данные", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
