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
    /// Логика взаимодействия для CreateWorkerPage.xaml
    /// </summary>
    public partial class CreateWorkerPage : Page
    {
        public CreateWorkerPage()
        {
            InitializeComponent();
        }

        private void BtnSingIn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errStr = new StringBuilder();

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
                    {
                        post = 1;
                        break;
                    }
                case "Социальный работник":
                    {
                        post = 2;
                        break;
                    }
                case "Координатор":
                    {
                        post = 3;
                        break;
                    }
                default:
                    errStr.AppendLine("Такой должность не существует");
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
                Manager.MainFrame.GoBack();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }
    }
}
