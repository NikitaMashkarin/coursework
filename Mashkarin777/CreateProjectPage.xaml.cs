using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class CreateProjectPage : Page
    {
        public CreateProjectPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки "Назад"
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errorBuilder = new StringBuilder();

            string projectName = ProjectNameTextBox.Text.Trim();
            string projectDescription = ProjectDescriptionTextBox.Text.Trim();
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(projectName))
                errorBuilder.AppendLine("Введите название проекта.");

            if (string.IsNullOrWhiteSpace(projectDescription))
                errorBuilder.AppendLine("Введите описание проекта.");

            if (!startDate.HasValue)
                errorBuilder.AppendLine("Укажите дату начала проекта.");

            if (!endDate.HasValue)
                errorBuilder.AppendLine("Укажите дату окончания проекта.");
            else if (startDate.HasValue && endDate < startDate)
                errorBuilder.AppendLine("Дата окончания не может быть раньше даты начала.");

            if (errorBuilder.Length > 0)
            {
                MessageBox.Show(errorBuilder.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                var newProject = new Project
                {
                    Name = projectName,
                    Description = projectDescription,
                    Date_start = startDate,
                    Date_end = endDate,
                    Id_report = null
                };

                context.Project.Add(newProject);
                context.SaveChanges();
            }

            MessageBox.Show("Проект успешно создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            ProjectNameTextBox.Clear();
            ProjectDescriptionTextBox.Clear();
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }
    }
}
