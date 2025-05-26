using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class ProjectParticipationPage : Page
    {
        public ProjectParticipationPage()
        {
            InitializeComponent();
            LoadParticipationData();
        }

        private void LoadParticipationData()
        {
            try
            {
                using (var context = new mashkarin777Entities())
                {
                    var projectParticipation = context.Project_objectives
                        .GroupBy(po => po.Id_task)
                        .Select(g => new
                        {
                            TaskId = g.Key,
                            ProjectCount = g.Select(po => po.Id_project).Distinct().Count()
                        })
                        .ToList();

                    if (!projectParticipation.Any())
                    {
                        MessageBox.Show("Нет участия в проектах.", "Участие в проектах", MessageBoxButton.OK, MessageBoxImage.Information);
                        ParticipationDataGrid.ItemsSource = null;
                    }
                    else
                    {
                        ParticipationDataGrid.ItemsSource = projectParticipation;
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

        private void ExportToTxtButton_Click(object sender, RoutedEventArgs e)
        {
            if (ParticipationDataGrid.ItemsSource == null)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = "УчастиеЗадачВПроектах.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Участие задач в проектах:");
                        writer.WriteLine();

                        foreach (var item in ParticipationDataGrid.ItemsSource)
                        {
                            dynamic participation = item;
                            writer.WriteLine($"ID Задачи: {participation.TaskId}");
                            writer.WriteLine($"Количество проектов: {participation.ProjectCount}");
                            writer.WriteLine(new string('-', 40));
                        }
                    }

                    MessageBox.Show("Отчёт успешно сохранён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении отчёта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
