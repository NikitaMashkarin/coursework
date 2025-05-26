using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class OverdueTasksTablePage : Page
    {
        public OverdueTasksTablePage()
        {
            InitializeComponent();
            LoadOverdueTasks();
        }

        private void LoadOverdueTasks()
        {
            try
            {
                using (var db = new mashkarin777Entities())
                {
                    var overdueTasks = db.Task
                        .Where(t => t.Made == false && t.Date_end < DateTime.Today)
                        .Select(t => new
                        {
                            t.Name,
                            t.Description,
                            t.Date_end,
                            Worker = t.Social_worker.Full_name
                        })
                        .ToList();

                    OverdueTasksDataGrid.ItemsSource = overdueTasks;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void ExportToTxtButton_Click(object sender, RoutedEventArgs e)
        {
            if (OverdueTasksDataGrid.ItemsSource == null)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = "ПросроченныеЗадачи.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Просроченные задачи:");
                        writer.WriteLine();

                        foreach (var item in OverdueTasksDataGrid.ItemsSource)
                        {
                            dynamic task = item;
                            writer.WriteLine($"Название: {task.Name}");
                            writer.WriteLine($"Описание: {task.Description}");
                            writer.WriteLine($"Срок: {task.Date_end:dd.MM.yyyy}");
                            writer.WriteLine($"Работник: {task.Worker}");
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
