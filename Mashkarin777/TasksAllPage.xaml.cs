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
    public partial class TasksAllPage : Page
    {
        public TasksAllPage()
        {
            InitializeComponent();
        }

        private void BtnGetAllTasks_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new mashkarin777Entities())
            {
                var tasks = context.Task
                                   .ToList();

                if (tasks.Count == 0)
                {
                    MessageBox.Show("Невыполненные задачи не найдены.");
                    return;
                }

                string taskList = string.Join("\n", tasks.Select(t =>
                    $"ID: {t.Id}\nНазвание: {t.Name}\nОписание: {t.Description}\nСрок Выполнения: {t.Date_end}\nВыполнена: {t.Made}"));

                MessageBox.Show(taskList, "Невыполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnGetTasksModeFalse_Click(object sender, RoutedEventArgs e)
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

                string taskList = string.Join("\n", tasks.Select(t =>
                    $"ID: {t.Id}\nНазвание: {t.Name}\nОписание: {t.Description}\nСрок Выполнения: {t.Date_end}\nВыполнена: {t.Made}"));

                MessageBox.Show(taskList, "Невыполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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

                MessageBox.Show($"ID: {task.Id}\nНазвание: {task.Name}\nОписание: {task.Description}\nСрок Выполнения: {task.Date_end}\nВыполнена: {task.Made}",
                                "Информация о задаче",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack(); ;
        }

        private void BtnAssignTaskToWorker_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskIdAssignTextBox.Text, out int taskId) || !int.TryParse(WorkerIdAssignTextBox.Text, out int workerId))
            {
                MessageBox.Show("Введите корректные числовые значения ID задачи и ID работника.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var task = context.Task.FirstOrDefault(t => t.Id == taskId);
                var worker = context.Social_worker.FirstOrDefault(w => w.Id == workerId);

                if (task == null)
                {
                    MessageBox.Show($"Задача с ID {taskId} не найдена.");
                    return;
                }

                if (worker == null)
                {
                    MessageBox.Show($"Социальный работник с ID {workerId} не найден.");
                    return;
                }

                if (task.Id_social_worker.HasValue && task.Id_social_worker != workerId)
                {
                    var currentWorker = context.Social_worker.FirstOrDefault(w => w.Id == task.Id_social_worker.Value);
                    string currentName = currentWorker != null ? currentWorker.Full_name : $"ID {task.Id_social_worker}";

                    var result = MessageBox.Show(
                        $"Задача уже назначена соц. работнику: {currentName}.\nВы уверены, что хотите переназначить её?",
                        "Подтверждение переназначения",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                        return;
                }

                task.Id_social_worker = workerId;
                context.SaveChanges();

                MessageBox.Show($"Задача ID {taskId} успешно переназначена социальному работнику ID {workerId}.");
            }
        }


        private void BtnCreateTask_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CreateTaskPage());
        }


    }
}
