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
    /// Логика взаимодействия для AllReportsTablePage.xaml
    /// </summary>
    public partial class AllReportsTablePage : Page
    {
        public AllReportsTablePage()
        {
            InitializeComponent();
            LoadReports();
        }

        private void LoadReports()
        {
            using (var context = new mashkarin777Entities())
            {
                var reports = context.Report.Include("Social_worker").ToList();

                if (reports.Count == 0)
                {
                    MessageBox.Show("Отчеты не найдены.");
                    return;
                }

                ReportsDataGrid.ItemsSource = reports;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ReportsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReportsDataGrid.SelectedItem != null)
            {
                var selectedReport = (Report)ReportsDataGrid.SelectedItem;
                MessageBox.Show($"Выбран отчет с ID: {selectedReport.Id}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
