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
    /// Логика взаимодействия для SocialWorkerPage.xaml
    /// </summary>
    public partial class SocialWorkerPage : Page
    {
        public SocialWorkerPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage()); // или переход на другую страницу
        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            // Навигация к задачам
        }

        private void BtnRegisterTask_Click(object sender, RoutedEventArgs e)
        {
            // Навигация к регистрации выполнения задачи
        }

    }
}
