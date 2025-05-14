using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Mashkarin777
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public StringBuilder errStr = new StringBuilder();
        public RegPage()
        {
            InitializeComponent();
        }

        public void BtnSingIn_Click(object sender, RoutedEventArgs e)
        {
            errStr.Clear();

            if (string.IsNullOrWhiteSpace(FNameTxtBox.Text))
                errStr.AppendLine("Поле 'Фамилия' не должно быть пустым");

            if (string.IsNullOrWhiteSpace(LogintxtBox.Text))
                errStr.AppendLine("Поле 'Имя' не должно быть пустым");

            if (string.IsNullOrWhiteSpace(RoleComboBox.Text))
                errStr.AppendLine("Поле 'Должность' не должно быть пустым");

            int post = 0;
            switch (RoleComboBox.Text)
            {
                case "Администратор":
                    post = 1;
                    break;
                case "Социальный работник":
                    post = 2;
                    break;
                case "Координатор":
                    post = 3;
                    break;
                default:
                    errStr.AppendLine("Такой должности не существует");
                    break;
            }

            if (string.IsNullOrWhiteSpace(Extra1txtBox.Text))
            {
                errStr.AppendLine("Поле 'Номер телефона' не должно быть пустым");
            }
            else if (!Extra1txtBox.Text.All(char.IsDigit) || Extra1txtBox.Text.Length < 11)
            {
                errStr.AppendLine("Введите корректный номер телефона");
            }

            if (string.IsNullOrWhiteSpace(LNametxtBox.Text))
            {
                errStr.AppendLine("Поле 'Электронная почта' не должно быть пустым");
            }
            else if (!LNametxtBox.Text.Contains("@") || !LNametxtBox.Text.Contains("."))
            {
                errStr.AppendLine("Введите корректную электронную почту");
            }

            if (string.IsNullOrWhiteSpace(PwdtxtBox.Text))
            {
                errStr.AppendLine("Поле 'Логин' не должно быть пустым");
            }

            if (string.IsNullOrWhiteSpace(Extra2txtBox.Text))
                errStr.AppendLine("Поле 'Пароль' не должно быть пустым");
            else
            {
                string password = Extra2txtBox.Text;

                if (password.Length < 8)
                {
                    errStr.AppendLine("Пароль должен содержать хотя бы 8 символов");
                }
                if (!password.Any(char.IsLower))
                {
                    errStr.AppendLine("Пароль должен содержать хотя бы одну строчную букву");
                }
                if (!password.Any(char.IsUpper))
                {
                    errStr.AppendLine("Пароль должен содержать хотя бы одну прописную букву");
                }
                if (!password.Any(char.IsDigit))
                {
                    errStr.AppendLine("Пароль должен содержать хотя бы одну цифру");
                }
                if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    errStr.AppendLine("Пароль должен содержать хотя бы один специальный символ");
                }
            }

            if (string.IsNullOrWhiteSpace(Extra3txtBox.Text))
            {
                errStr.AppendLine("Поле 'Повторите пароль' не должно быть пустым");
            }
            else if (Extra2txtBox.Text != Extra3txtBox.Text)
            {
                errStr.AppendLine("Пароли не совпадают");
            }

            if (errStr.Length > 0)
            {
                MessageBox.Show(errStr.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new mashkarin777Entities())
            {
                context.Social_worker.Add(new Social_worker
                {
                    Login = LogintxtBox.Text,
                    Password = PwdtxtBox.Text,
                    Full_name = FNameTxtBox.Text + " " + LogintxtBox.Text,
                    Phone = Extra1txtBox.Text,
                    Email = LNametxtBox.Text,
                    Post = post,
                    Personal_data = null
                });

                context.SaveChanges();
            }

            using (var context = new mashkarin777Entities())
            {
                var user = context.Social_worker
                              .FirstOrDefault(u => u.Login.Trim() == LogintxtBox.Text);

                switch (post)
                {
                    case 1:
                        Manager.MainFrame.Navigate(new AdministratorPage(user.Id));
                        break;
                    case 2:
                        Manager.MainFrame.Navigate(new SocialWorkerPage(user.Id));
                        break;
                    case 3:
                        Manager.MainFrame.Navigate(new CoordinatorPage(user.Id));
                        break;
                }
            }
        }



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AuthPage());
        }

    }
}
