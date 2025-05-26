using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class ReportsByWorkersPage : Page
    {
        public ReportsByWorkersPage()
        {
            InitializeComponent();
            LoadReportsData();
        }

        private void LoadReportsData()
        {
            try
            {
                using (var context = new mashkarin777Entities())
                {
                    var reportData = context.Report
                        .GroupBy(r => r.Id_social_worker)
                        .Select(g => new
                        {
                            WorkerId = g.Key,
                            WorkerName = context.Social_worker
                                              .Where(sw => sw.Id == g.Key)
                                              .Select(sw => sw.Full_name)
                                              .FirstOrDefault(),
                            ReportCount = g.Count()
                        })
                        .ToList();

                    if (!reportData.Any())
                    {
                        MessageBox.Show("Нет отчётов сотрудников.", "Отчёты сотрудников", MessageBoxButton.OK, MessageBoxImage.Information);
                        ReportsDataGrid.ItemsSource = null;
                    }
                    else
                    {
                        ReportsDataGrid.ItemsSource = reportData;
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
            if (ReportsDataGrid.ItemsSource == null)
            {
                MessageBox.Show("Нет данных для экспорта.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовый файл (*.txt)|*.txt",
                FileName = "ОтчётыСотрудников.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("Отчёты сотрудников:");
                        writer.WriteLine();

                        foreach (var item in ReportsDataGrid.ItemsSource)
                        {
                            dynamic report = item;
                            writer.WriteLine($"ID работника: {report.WorkerId}");
                            writer.WriteLine($"ФИО: {report.WorkerName}");
                            writer.WriteLine($"Количество отчётов: {report.ReportCount}");
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
