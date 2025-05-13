using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class PositionsPage : Page
    {
        public PositionsPage()
        {
            InitializeComponent();
            LoadPositionsData();
        }

        private void LoadPositionsData()
        {
            using (var context = new mashkarin777Entities())
            {
                var positions = context.Social_worker
                    .GroupBy(sw => sw.Post)
                    .Select(g => new
                    {
                        Post = g.Key,
                        Count = g.Count()
                    })
                    .AsEnumerable()
                    .Select(g => new
                    {
                        PositionName = GetPositionName(g.Post),
                        g.Count
                    })
                    .ToList();

                if (positions.Any())
                {
                    PositionsDataGrid.ItemsSource = positions;
                }
                else
                {
                    MessageBox.Show("Нет данных о должностях сотрудников.", "Должности сотрудников", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private string GetPositionName(int? postId)
        {
            if (postId == null)
                return "Неизвестная должность";

            switch (postId.Value)
            {
                case 1:
                    return "Администратор";
                case 2:
                    return "Социальный работник";
                case 3:
                    return "Координатор";
                default:
                    return "Неизвестная должность";
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
