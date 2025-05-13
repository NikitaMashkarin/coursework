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
    /// Логика взаимодействия для AllProjectsTablePage.xaml
    /// </summary>
    public partial class AllProjectsTablePage : Page
    {
        public AllProjectsTablePage()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            using (var context = new mashkarin777Entities())
            {
                var projects = context.Project.ToList();

                if (projects.Count == 0)
                {
                    MessageBox.Show("Проекты не найдены.");
                    return;
                }

                ProjectsDataGrid.ItemsSource = projects;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
