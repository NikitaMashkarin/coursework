using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class ProjectTasksTablePage : Page
    {
        public ProjectTasksTablePage()
        {
            InitializeComponent();
        }

        private void LoadTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ProjectIdTextBox.Text.Trim(), out int projectId))
            {
                MessageBox.Show("Введите корректный ID проекта.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var tasks = context.Project_objectives
                                   .Where(po => po.Id_project == projectId)
                                   .Select(po => po.Task)
                                   .ToList();

                if (!tasks.Any())
                {
                    MessageBox.Show("Задачи для проекта не найдены.");
                    TasksDataGrid.ItemsSource = null;
                    return;
                }

                TasksDataGrid.ItemsSource = tasks;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
