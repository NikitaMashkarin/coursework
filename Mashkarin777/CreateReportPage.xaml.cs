using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class CreateReportPage : Page
    {
        private int _workerId;

        public CreateReportPage(int id)
        {
            InitializeComponent();
            _workerId = id;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTxtBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Описание обязательно.");
                return;
            }

            if (!int.TryParse(IdProjectTxtBox.Text.Trim(), out int projectId))
            {
                MessageBox.Show("ID проекта должен быть числом.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var project = context.Project.FirstOrDefault(p => p.Id == projectId);
                var worker = context.Social_worker.FirstOrDefault(w => w.Id == _workerId);

                if (project == null)
                {
                    MessageBox.Show("Проект с таким ID не найден.");
                    return;
                }

                if (worker == null)
                {
                    MessageBox.Show("Социальный работник не найден.");
                    return;
                }

                var newReport = new Report
                {
                    Date_create = DateTime.Now,
                    Description = description,
                    Id_social_worker = _workerId,
                    Social_worker = worker
                };

                newReport.Project.Add(project);

                context.Report.Add(newReport);
                context.SaveChanges();

                project.Id_report = newReport.Id;
                context.SaveChanges();
            }

            MessageBox.Show("Отчет успешно создан и привязан к проекту.");
            NavigationService?.GoBack();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
