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
    /// Логика взаимодействия для AllTasksTablePage.xaml
    /// </summary>
    public partial class AllTasksTablePage : Page
    {
        public AllTasksTablePage()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void LoadTasks()
        {
            using (var context = new mashkarin777Entities())
            {
                var tasks = context.Task.ToList();

                if (tasks.Count == 0)
                {
                    MessageBox.Show("Задачи не найдены.");
                    return;
                }
                TasksDataGrid.ItemsSource = tasks;
            }
        }

        private void TasksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem is Task selectedTask)
            {
                MessageBox.Show($"Выбрана задача: {selectedTask.Name}\nОписание: {selectedTask.Description}",
                                "Информация о задаче", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
