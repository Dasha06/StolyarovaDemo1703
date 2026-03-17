using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using StolyarovaDemo1703.Models;
using System.Linq;

namespace StolyarovaDemo1703;

public partial class OrderWindow : Window
{
    User _user;
    public OrderWindow()
    {
        InitializeComponent();
    }

    public OrderWindow(User user)
    {
        InitializeComponent();
        _user = user;
        GetInfoOrder();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_user.RoleId == 1)
        {
            AdminWindow admin = new AdminWindow(_user);
            admin.Show();
            Close();
        }
        else
        {
            ManagerWindow manager = new ManagerWindow(_user);
            manager.Show();
            Close();
        }

    }

    private void GetInfoOrder()
    {
        StolyarovaContext context = new StolyarovaContext();
        var orders = context.Orders.Include(x => x.Address)
            .Include(x => x.Status).ToList();

        OrdersBox.ItemsSource = orders;
    }
}