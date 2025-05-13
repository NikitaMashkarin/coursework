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
    /// Логика взаимодействия для UncompletedTasksTablePage.xaml
    /// </summary>
    public partial class UncompletedTasksTablePage : Page
    {
        public UncompletedTasksTablePage()
        {
            InitializeComponent();
            LoadUncompletedTasks();
        }

        private void LoadUncompletedTasks()
        {
            using (var context = new mashkarin777Entities())
            {
                var tasks = context.Task
                                   .Where(t => t.Made == false)
                                   .ToList();

                if (tasks.Count == 0)
                {
                    MessageBox.Show("Невыполненные задачи не найдены.");
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