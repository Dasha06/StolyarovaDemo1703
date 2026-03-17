using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using StolyarovaDemo1703.Models;
using System.Linq;

namespace StolyarovaDemo1703;

public partial class UserWindow : Window
{
    User _user;
    public UserWindow()
    {
        InitializeComponent();
        GetInfo();
    }
    public UserWindow(User user)
    {
        InitializeComponent();
        _user = user;
        UserFioBlock.Text = user.UserFio;
        GetInfo();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow main = new MainWindow();
        main.Show();
        Close();
    }

    private void GetInfo()
    {
        StolyarovaContext context = new StolyarovaContext();
        var listProducts = context.Products.Include(x => x.Manufacturer)
            .Include(x => x.ProdName)
            .Include(x => x.Provider)
            .Include(x => x.Unit)
            .Include(x => x.Category).ToList();

        ListProductsBox.ItemsSource = listProducts;
    }
}