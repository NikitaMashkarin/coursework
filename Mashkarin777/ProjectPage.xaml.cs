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
    /// <summary>
    /// Логика взаимодействия для ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        private int id;
        public ProjectPage(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void BtnGetAllProjects_Click(object sender, RoutedEventArgs e)
        {
            //using (var context = new mashkarin777Entities())
            //{
            //    var projects = context.Project.ToList();

            //    string output = "Список проектов:\n\n";
            //    foreach (var project in projects)
            //    {
            //        output += $"ID: {project.Id}, Название: {project.Name}, Дата начала: {project.Date_start?.ToShortDateString()}, Дата окончания: {project.Date_end?.ToShortDateString()}\n";
            //    }

            //    MessageBox.Show(output);
            //}
            Manager.MainFrame.Navigate(new AllProjectsTablePage());
        }

        private void BtnGetTaskProkectById_Click(object sender, RoutedEventArgs e)
        {
            //if (!int.TryParse(TaskIdTextBox.Text.Trim(), out int projectId))
            //{
            //    MessageBox.Show("Введите корректный ID проекта.");
            //    return;
            //}

            //using (var context = new mashkarin777Entities())
            //{
            //    var tasks = context.Project_objectives
            //        .Where(po => po.Id_project == projectId)
            //        .Select(po => po.Task)
            //        .ToList();

            //    if (!tasks.Any())
            //    {
            //        MessageBox.Show("Задачи для проекта не найдены.");
            //        return;
            //    }

            //    string output = $"Задачи проекта ID {projectId}:\n\n";
            //    foreach (var task in tasks)
            //    {
            //        output += $"ID: {task.Id}, Название: {task.Name}, Выполнена: {(task.Made == true ? "Да" : "Нет")}\n";

            //    }

            //    MessageBox.Show(output);
            //}
            Manager.MainFrame.Navigate(new ProjectTasksTablePage());
        }

        private void BtnGetUnfinishedTasks_Click(object sender, RoutedEventArgs e)
        {
            //using (var context = new mashkarin777Entities())
            //{
            //    var unfinishedProjects = context.Project
            //        .Where(p => p.Project_objectives
            //            .Any(po => po.Task != null && po.Task.Made == false))
            //        .ToList();

            //    if (!unfinishedProjects.Any())
            //    {
            //        MessageBox.Show("Все проекты завершены.");
            //        return;
            //    }

            //    string output = "Незавершённые проекты:\n\n";
            //    foreach (var project in unfinishedProjects)
            //    {
            //        output += $"ID: {project.Id}, Название: {project.Name}\n";
            //    }

            //    MessageBox.Show(output);
            //}
            Manager.MainFrame.Navigate(new UnfinishedProjectsTablePage());
        }

        private void BtnCreateReport_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new CreateReportPage(id));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new CreateProjectPage());
        }
    }
}
