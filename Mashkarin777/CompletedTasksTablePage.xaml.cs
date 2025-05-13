using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class CompletedTasksTablePage : Page
    {
        public CompletedTasksTablePage()
        {
            InitializeComponent();
            LoadCompletedTasks();
        }

        private void LoadCompletedTasks()
        {
            try
            {
                using (var context = new mashkarin777Entities())
                {
                    var completedTasks = context.Task.Where(t => t.Made == true).ToList();

                    if (!completedTasks.Any())
                    {
                        MessageBox.Show("Нет выполненных задач.", "Выполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
                        TasksDataGrid.ItemsSource = null;
                    }
                    else
                    {
                        TasksDataGrid.ItemsSource = completedTasks;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
