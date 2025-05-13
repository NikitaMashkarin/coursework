using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class CurrentTasksTablePage : Page
    {
        public CurrentTasksTablePage()
        {
            InitializeComponent();
            LoadCurrentTasks();
        }

        private void LoadCurrentTasks()
        {
            try
            {
                using (var context = new mashkarin777Entities())
                {
                    var currentTasks = context.Task
                        .Where(t => t.Made == false && t.Date_end >= DateTime.Today)
                        .Select(t => new
                        {
                            t.Name,
                            t.Description,
                            t.Date_end,
                            Worker = t.Social_worker.Full_name
                        })
                        .ToList();

                    if (!currentTasks.Any())
                    {
                        MessageBox.Show("Нет текущих задач.", "Текущие задачи", MessageBoxButton.OK, MessageBoxImage.Information);
                        TasksDataGrid.ItemsSource = null;
                    }
                    else
                    {
                        TasksDataGrid.ItemsSource = currentTasks;
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
