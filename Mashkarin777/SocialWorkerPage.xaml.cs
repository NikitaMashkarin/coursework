using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    /// <summary>
    /// Логика взаимодействия для SocialWorkerPage.xaml
    /// </summary>
    public partial class SocialWorkerPage : Page
    {
        private int id;
        public SocialWorkerPage(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Tasks(id));
        }

        private void BtnRegisterTask_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TaskIdTextBox.Text, out int taskId))
            {
                MessageBox.Show("Введите корректный номер задачи.");
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var task = context.Task
                    .FirstOrDefault(t => t.Id == taskId && t.Made == false && t.Social_worker.Id == id);

                if (task == null)
                {
                    MessageBox.Show("Задача не найдена, уже выполнена или не принадлежит вам.");
                    return;
                }

                task.Made = true;
                context.SaveChanges();

                MessageBox.Show($"Задача с ID {task.Id} успешно зарегистрирована как выполненная.",
                                "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
