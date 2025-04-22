using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void BtnGetAllTasks_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new mashkarin777Entities())
            {
                var reports = context.Report.Include("Social_worker").ToList();

                if (reports.Count == 0)
                {
                    MessageBox.Show("Отчеты не найдены.");
                    return;
                }

                string reportList = string.Join("\n\n", reports.Select(r =>
                    $"ID: {r.Id}\nДата: {r.Date_create:dd.MM.yyyy}\nСотрудник: {r.Social_worker.Full_name.Trim()}\nОписание: {r.Description}"));

                MessageBox.Show(reportList, "Список отчетов", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnGetTaskById_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskIdTextBox.Text, out int reportId))
            {
                MessageBox.Show("Введите корректный ID отчета.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var report = context.Report.Include("Social_worker")
                                           .FirstOrDefault(r => r.Id == reportId);

                if (report == null)
                {
                    MessageBox.Show("Отчет с таким ID не найден.");
                    return;
                }

                string reportInfo = $"ID: {report.Id}\nДата: {report.Date_create:dd.MM.yyyy}\n" +
                                    $"Сотрудник: {report.Social_worker.Full_name.Trim()}\nОписание: {report.Description}";

                MessageBox.Show(reportInfo, "Информация об отчете", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
