using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class CreateTaskPage : Page
    {
        public CreateTaskPage()
        {
            InitializeComponent();
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string name = TaskNameTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Название задачи обязательно.");
                return;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Описание обязательно.");
                return;
            }

            if (string.IsNullOrWhiteSpace(SocialWorkerIdTextBox.Text))
            {
                MessageBox.Show("ID соц. работника обязателен.");
                return;
            }

            if (string.IsNullOrWhiteSpace(ProjectIdTextBox.Text))
            {
                MessageBox.Show("ID проекта обязателен.");
                return;
            }

            if (!int.TryParse(SocialWorkerIdTextBox.Text.Trim(), out int workerId))
            {
                MessageBox.Show("ID соц. работника должен быть числом.");
                return;
            }

            if (!int.TryParse(ProjectIdTextBox.Text.Trim(), out int projectId))
            {
                MessageBox.Show("ID проекта должен быть числом.");
                return;
            }

            if (!DueDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Срок выполнения обязателен.");
                return;
            }

            DateTime dueDate = DueDatePicker.SelectedDate.Value;

            using (var context = new mashkarin777Entities())
            {
                var socialWorker = context.Social_worker.FirstOrDefault(sw => sw.Id == workerId);
                if (socialWorker == null)
                {
                    MessageBox.Show("Социальный работник с таким ID не найден.");
                    return;
                }

                var project = context.Project.FirstOrDefault(p => p.Id == projectId);
                if (project == null)
                {
                    MessageBox.Show("Проект с таким ID не найден.");
                    return;
                }

                var newTask = new Task
                {
                    Name = name,
                    Description = description,
                    Date_end = dueDate,
                    Made = false,
                    Social_worker = socialWorker
                };

                context.Task.Add(newTask);
                context.SaveChanges();

                var projectObjective = new Project_objectives
                {
                    Id_project = projectId,
                    Id_task = newTask.Id
                };

                context.Project_objectives.Add(projectObjective);
                context.SaveChanges();
            }

            MessageBox.Show("Задача успешно создана и связана с проектом!");
            NavigationService?.GoBack();
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack(); ;
        }
    }
}
