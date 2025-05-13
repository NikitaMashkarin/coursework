using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class AllTaskTablePage : Page
    {
        private int _socialWorkerId;

        public AllTaskTablePage(int socialWorkerId)
        {
            InitializeComponent();
            _socialWorkerId = socialWorkerId;
            LoadTasksForSocialWorker();
        }

        private void LoadTasksForSocialWorker()
        {
            using (var context = mashkarin777Entities.GetContext())
            {
                var tasks = context.Task
                    .Where(t => t.Id_social_worker == _socialWorkerId)
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        t.Description,
                        t.Date_end,
                        t.Made
                    }).ToList();

                TasksDataGrid.ItemsSource = tasks;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
