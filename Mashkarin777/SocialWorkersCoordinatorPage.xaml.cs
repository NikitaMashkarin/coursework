using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    /// <summary>
    /// Логика взаимодействия для SocialWorkersCoordinatorPage.xaml
    /// </summary>
    public partial class SocialWorkersCoordinatorPage : Page
    {
        public SocialWorkersCoordinatorPage()
        {
            InitializeComponent();
        }

        private void BtnGetAllTasks_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new mashkarin777Entities())
            {
                var workers = context.Social_worker.ToList();

                if (workers.Count == 0)
                {
                    MessageBox.Show("Социальные работники не найдены.");
                    return;
                }

                string workerList = string.Join("\n", workers.Select(w =>
                    $"ID: {w.Id}, ФИО: {w.Full_name.Trim()}, Телефон: {w.Phone.Trim()}, Email: {w.Email.Trim()}"));

                MessageBox.Show(workerList, "Список социальных работников",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void BtnGetTaskById_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskIdTextBox.Text, out int workerId))
            {
                MessageBox.Show("Введите корректный ID работника.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var worker = context.Social_worker.FirstOrDefault(w => w.Id == workerId);

                if (worker == null)
                {
                    MessageBox.Show("Работник с таким ID не найден.");
                    return;
                }

                string workerInfo = $"ID: {worker.Id}\nФИО: {worker.Full_name.Trim()}\n" +
                                    $"Телефон: {worker.Phone.Trim()}\nEmail: {worker.Email.Trim()}";

                MessageBox.Show(workerInfo, "Информация о работнике",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
}
