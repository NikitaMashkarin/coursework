using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class OverdueTasksTablePage : Page
    {
        public OverdueTasksTablePage()
        {
            InitializeComponent();
            LoadOverdueTasks();
        }

        private void LoadOverdueTasks()
        {
            try
            {
                using (var db = new mashkarin777Entities())
                {
                    var overdueTasks = db.Task
                        .Where(t => t.Made == false && t.Date_end < DateTime.Today)
                        .Select(t => new
                        {
                            t.Name,
                            t.Description,
                            t.Date_end,
                            Worker = t.Social_worker.Full_name
                        })
                        .ToList();

                    OverdueTasksDataGrid.ItemsSource = overdueTasks;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
