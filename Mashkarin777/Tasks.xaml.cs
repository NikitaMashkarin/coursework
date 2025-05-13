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
    /// Логика взаимодействия для Tasks.xaml
    /// </summary>
    public partial class Tasks : Page
    {
        private int id;
        public Tasks(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        private void BtnGetAllTasks_Click(object sender, RoutedEventArgs e)
        {
            //using (var context = new mashkarin777Entities())
            //{
            //    var tasks = context.Task
            //                       .Where(t =>t.Social_worker.Id == id && t.Made == false)
            //                       .ToList();

            //    if (tasks.Count == 0)
            //    {
            //        MessageBox.Show("Невыполненные задачи не найдены.");
            //        return;
            //    }

            //    string taskList = string.Join("\n", tasks.Select(t =>
            //        $"Номер задачи: {t.Id}, Название: {t.Name}, Описание: {t.Description}"));

            //    MessageBox.Show(taskList, "Невыполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new AllTaskTablePage(id));
        }

        private void BtnGetTaskById_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskIdTextBox.Text, out int taskId))
            {
                MessageBox.Show("Введите корректный номер задачи.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var task = context.Task.FirstOrDefault(t => t.Id == taskId);

                if (task == null)
                {
                    MessageBox.Show("Задача с таким номером не найдена.");
                    return;
                }

                MessageBox.Show($"ID: {task.Id}\nНазвание: {task.Name}\nОписание: {task.Description}\nСрок Выполнения: {task.Date_end}",
                                "Информация о задаче",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();;
        }

    }
}
