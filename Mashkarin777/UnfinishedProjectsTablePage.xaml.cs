using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class UnfinishedProjectsTablePage : Page
    {
        public UnfinishedProjectsTablePage()
        {
            InitializeComponent();
            LoadUnfinishedProjects();
        }

        private void LoadUnfinishedProjects()
        {
            using (var context = new mashkarin777Entities())
            {
                var unfinishedProjects = context.Project
                    .Where(p => p.Project_objectives
                        .Any(po => po.Task != null && po.Task.Made == false))
                    .ToList();

                if (!unfinishedProjects.Any())
                {
                    MessageBox.Show("Все проекты завершены.");
                    ProjectsDataGrid.ItemsSource = null;
                    return;
                }

                ProjectsDataGrid.ItemsSource = unfinishedProjects;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
