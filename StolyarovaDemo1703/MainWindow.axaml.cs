using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using StolyarovaDemo1703.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StolyarovaDemo1703
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (LoginBox.Text != null && PasswordBox.Text != null)
            {
                StolyarovaContext context = new StolyarovaContext();
                var userFound = context.Users.Include(x => x.Role).FirstOrDefault(x => x.UserLogin ==  LoginBox.Text && x.UserPassword == PasswordBox.Text);

                if (userFound != null)
                {
                    if (userFound.Role.RoleName == "Авторизированный клиент")
                    {
                        UserWindow userWindow = new UserWindow(userFound);
                        userWindow.Show();
                        Close();
                    }
                    else if (userFound.Role.RoleName == "Менеджер")
                    {
                        ManagerWindow manager = new ManagerWindow(userFound);
                        manager.Show();
                        Close();
                    }
                    else
                    {
                        AdminWindow adminWindow = new AdminWindow(userFound);
                        adminWindow.Show();
                        Close();
                    }
                }
                else
                {
                    var message = MessageBoxManager.GetMessageBoxStandard("уведомление", "неправильно внесены логин или пароль",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                    await message.ShowAsync();
                }

            }
            else
            {
                var message = MessageBoxManager.GetMessageBoxStandard("уведомление", "не заполнены логин или пароль", 
                    MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await message.ShowAsync();
            }

        }

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            userWindow.Show();
            Close();
        }
    }
}