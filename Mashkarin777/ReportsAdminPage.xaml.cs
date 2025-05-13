using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class ReportsAdminPage : Page
    {
        public ReportsAdminPage()
        {
            InitializeComponent();
        }

        private mashkarin777Entities db = mashkarin777Entities.GetContext();

        private void CompletedTasksReport_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    var completedTasks = db.Task.Where(t => t.Made == true).ToList();
            //    if (completedTasks.Any())
            //    {
            //        string result = "Выполненные задачи:\n";
            //        foreach (var task in completedTasks)
            //        {
            //            result += $"- {task.Name}: {task.Description}\n";
            //        }
            //        MessageBox.Show(result, "Выполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Нет выполненных задач.", "Выполненные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            Manager.MainFrame.Navigate(new CompletedTasksTablePage());
        }

        private void CurrentTasksReport_Click(object sender, RoutedEventArgs e)
        {
            //var currentTasks = db.Task
            //                     .Where(t => t.Made == false && t.Date_end >= DateTime.Today)
            //                     .Select(t => new
            //                     {
            //                         t.Name,
            //                         t.Description,
            //                         t.Date_end,
            //                         Worker = t.Social_worker.Full_name
            //                     })
            //                     .ToList();

            //if (currentTasks.Any())
            //{
            //    string result = "Текущие задачи:\n";
            //    foreach (var task in currentTasks)
            //    {
            //        result += $"- {task.Name}: {task.Description}, Срок: {task.Date_end}, Работник: {task.Worker}\n";
            //    }
            //    MessageBox.Show(result, "Текущие задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Нет текущих задач.", "Текущие задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new CurrentTasksTablePage());
        }

        private void ReportsCountReport_Click(object sender, RoutedEventArgs e)
        {
            //var reportCount = db.Report
            //                    .GroupBy(r => r.Id_social_worker)
            //                    .Select(g => new
            //                    {
            //                        SocialWorker = g.Key,
            //                        ReportCount = g.Count()
            //                    })
            //                    .ToList();

            //if (reportCount.Any())
            //{
            //    string result = "Отчёты сотрудников:\n";
            //    foreach (var report in reportCount)
            //    {
            //        result += $"- Работник {report.SocialWorker}: {report.ReportCount} отчётов\n";
            //    }
            //    MessageBox.Show(result, "Отчёты сотрудников", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Нет отчётов сотрудников.", "Отчёты сотрудников", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new ReportsByWorkersPage());
        }

        private void ProjectsParticipationReport_Click(object sender, RoutedEventArgs e)
        {
            //var projectParticipation = db.Project_objectives
            //                             .GroupBy(po => po.Id_task)
            //                             .Select(g => new
            //                             {
            //                                 Task = g.Key,
            //                                 Projects = g.Select(po => po.Id_project).Distinct().Count()
            //                             })
            //                             .ToList();

            //if (projectParticipation.Any())
            //{
            //    string result = "Участие в проектах:\n";
            //    foreach (var participation in projectParticipation)
            //    {
            //        result += $"- Задача {participation.Task}: {participation.Projects} проектов\n";
            //    }
            //    MessageBox.Show(result, "Участие в проектах", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Нет участия в проектах.", "Участие в проектах", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new ProjectParticipationPage());
        }

        private void PositionsReport_Click(object sender, RoutedEventArgs e)
        {
            //var positions = db.Social_worker
            //                  .GroupBy(sw => sw.Post)
            //                  .Select(g => new
            //                  {
            //                      Post = g.Key,
            //                      Count = g.Count()
            //                  })
            //                  .ToList();

            //if (positions.Any())
            //{
            //    string result = "Должности сотрудников:\n";
            //    foreach (var position in positions)
            //    {
            //        result += $"- Должность {position.Post}: {position.Count} сотрудников\n";
            //    }
            //    MessageBox.Show(result, "Должности сотрудников", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Нет данных о должностях сотрудников.", "Должности сотрудников", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new PositionsPage());
        }

        private void OverdueTasksReport_Click(object sender, RoutedEventArgs e)
        {
            //var overdueTasks = db.Task
            //                     .Where(t => t.Made == false && t.Date_end < DateTime.Today)
            //                     .Select(t => new
            //                     {
            //                         t.Name,
            //                         t.Description,
            //                         t.Date_end,
            //                         Worker = t.Social_worker.Full_name
            //                     })
            //                     .ToList();

            //if (overdueTasks.Any())
            //{
            //    string result = "Просроченные задачи:\n";
            //    foreach (var task in overdueTasks)
            //    {
            //        result += $"- {task.Name}: {task.Description}, Срок: {task.Date_end}, Работник: {task.Worker}\n";
            //    }
            //    MessageBox.Show(result, "Просроченные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("Нет просроченных задач.", "Просроченные задачи", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            Manager.MainFrame.Navigate(new OverdueTasksTablePage());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
