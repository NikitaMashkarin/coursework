using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var errStr = new StringBuilder();

            if (string.IsNullOrWhiteSpace(LoginTxtBox.Text))
                errStr.AppendLine("Поле Login не должно быть пустым");

            if (string.IsNullOrWhiteSpace(PswdTxtBox.Text))
                errStr.AppendLine("Поле Password не должно быть пустым");

            using (var context = new mashkarin777Entities())
            {
                var user = context.Social_worker
                           .FirstOrDefault(u => u.Login.Trim() == LoginTxtBox.Text);


                if (user == null || user.Password.Trim() != PswdTxtBox.Text)
                    errStr.AppendLine("Логин или пароль не верен");

                if (errStr.Length > 0)
                {
                    MessageBox.Show(errStr.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                switch (user.Post)
                {
                    case 1:
                        {
                            MessageBox.Show($"{user.Full_name }  Вы зашли с правами Администратора", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MainFrame.Navigate(new AdministratorPage(user.Id));
                            break;
                        }
                    case 2:
                        {
                            MessageBox.Show($"{user.Full_name} Вы зашли с правами Социального работника", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MainFrame.Navigate(new SocialWorkerPage(user.Id));
                            break;
                        }
                    case 3:
                        {
                            MessageBox.Show($"{user.Full_name} Вы зашли с правами Координатора", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager.MainFrame.Navigate(new CoordinatorPage(user.Id));
                            break;
                        }
                    default:
                        return;
                }
            }
        }

        private void BtnSingIn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RegPage());
        }

    }
}
