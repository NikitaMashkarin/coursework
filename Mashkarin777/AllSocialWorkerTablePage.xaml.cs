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
    /// Логика взаимодействия для AllSocialWorkerTablePage.xaml
    /// </summary>
    public partial class AllSocialWorkerTablePage : Page
    {
        public AllSocialWorkerTablePage()
        {
            InitializeComponent();
            LoadWorkers();
        }

        private void LoadWorkers()
        {
            using (var context = new mashkarin777Entities())
            {
                var workers = context.Social_worker
                    .Where(w => w.Post == 2)
                    .ToList();

                if (workers.Count == 0)
                {
                    MessageBox.Show("Социальные работники не найдены.");
                    return;
                }

                WorkersDataGrid.ItemsSource = workers;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
