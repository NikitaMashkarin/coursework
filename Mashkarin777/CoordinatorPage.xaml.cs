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
    /// Логика взаимодействия для CoordinatorPage.xaml
    /// </summary>
    public partial class CoordinatorPage : Page
    {
        private int id;
        public CoordinatorPage(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна с отчетами
        }

        private void BtnSocialWorkers_Click(object sender, RoutedEventArgs e)
        {
            // Переход к социальным работникам
        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            // Переход к задачам
        }

        //private void BtnBack_Click(object sender, RoutedEventArgs e)
        //{
        //    this.NavigationService.GoBack();
        //}

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }

    }
}
