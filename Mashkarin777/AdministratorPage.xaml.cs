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
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        public AdministratorPage()
        {
            InitializeComponent();
        }

        private void BtnProjects_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна или логика по проектам
        }

        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
        {
            // Открытие базы сотрудников
        }

        private void BtnGenerateSummaryReport_Click(object sender, RoutedEventArgs e)
        {
            // Генерация сводного отчёта
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            // Генерация сводного отчёта
        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            // Генерация сводного отчёта
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }
    }
}
