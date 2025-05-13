using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace Mashkarin777
{
    public partial class WorkersTablePage : Page
    {
        public WorkersTablePage()
        {
            InitializeComponent();
            LoadWorkers();
        }

        private void LoadWorkers()
        {
            using (var context = new mashkarin777Entities())
            {
                var workers = context.Social_worker.ToList();

                if (workers.Count == 0)
                {
                    MessageBox.Show("Работники не найдены.");
                    return;
                }

                var workerData = workers.Select(w => new
                {
                    Id = w.Id,
                    Full_name = w.Full_name,
                    Phone = w.Phone,
                    Email = w.Email,
                    Post = w.Post == 1 ? "Администратор" :
                           w.Post == 2 ? "Социальный работник" :
                           w.Post == 3 ? "Координатор" : "Не указано"
                }).ToList();

                WorkersDataGrid.ItemsSource = workerData;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
