using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class CompletedTasksTablePage : Page
    {
        public CompletedTasksTablePage()
        {
            InitializeComponent();
            LoadCompletedTasks();
        }

        private void LoadCompletedTasks()
        {
            try
            {
                using (var context = new mashkarin777Entities())
                {
                    var completedTasks = context.Task.Where(t => t.Made == true).ToList();

                    if (!completedTasks.Any())
                    {
                        MessageBox.Show("Нет выполненных задач.", "Выполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
                        TasksDataGrid.ItemsSource = null;
                    }
                    else
                    {
                        TasksDataGrid.ItemsSource = completedTasks;
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
                FileName = "ВыполненныеЗадачи.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Выполненные задачи:");
                        writer.WriteLine();

                        foreach (var item in TasksDataGrid.ItemsSource)
                        {
                            if (item is Task task)
                            {
                                writer.WriteLine($"ID: {task.Id}");
                                writer.WriteLine($"Название: {task.Name}");
                                writer.WriteLine($"Описание: {task.Description}");
                                writer.WriteLine("----------------------------------");
                            }
                        }
                    }

                    MessageBox.Show("Отчёт успешно сохранён в файл.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении отчёта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
