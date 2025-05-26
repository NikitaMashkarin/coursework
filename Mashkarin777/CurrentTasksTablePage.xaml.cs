using Microsoft.Win32;
using System;
using System.IO;
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

        private void ExportToTxtButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.ItemsSource == null)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = "ТекущиеЗадачи.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Текущие задачи:");
                        writer.WriteLine();

                        foreach (var item in TasksDataGrid.ItemsSource)
                        {
                            dynamic task = item;
                            writer.WriteLine($"Название: {task.Name}");
                            writer.WriteLine($"Описание: {task.Description}");
                            writer.WriteLine($"Срок выполнения: {task.Date_end:dd.MM.yyyy}");
                            writer.WriteLine($"Социальный работник: {task.Worker}");
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
