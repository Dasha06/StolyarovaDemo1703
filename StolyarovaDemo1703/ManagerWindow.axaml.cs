using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using StolyarovaDemo1703.Models;
using System.Linq;

namespace StolyarovaDemo1703;

public partial class ManagerWindow : Window
{
    User _user;
    public ManagerWindow()
    {
        InitializeComponent();
    }

    public ManagerWindow(User user)
    {
        InitializeComponent();
        _user = user;
        UserFioBlock.Text = user.UserFio;
        GetInfo();
        PullComboBox();
    }

    private void PullComboBox()
    {
        StolyarovaContext context = new StolyarovaContext();
        var providerList = context.Providers.Select(x => x.ProviderName).ToList();
        providerList.Add("Все поставщики");

        FilterBox.ItemsSource = providerList.OrderByDescending(x => x == "Все поставщики");
    }

    private void GetInfo()
    {
        StolyarovaContext context = new StolyarovaContext();
        var listProducts = context.Products.Include(x => x.Manufacturer)
            .Include(x => x.ProdName)
            .Include(x => x.Provider)
            .Include(x => x.Unit)
            .Include(x => x.Category).ToList();

        if (SearchBox.Text != null)
        {
            listProducts = listProducts.Where(x => x.Category.CategoryName.ToLower().Contains(SearchBox.Text.ToLower()) ||
                    x.Provider.ProviderName.ToLower().Contains(SearchBox.Text.ToLower()) ||
                    x.ProductDescription.ToLower().Contains(SearchBox.Text.ToLower()) ||
                    x.Manufacturer.ManufacturerName.ToLower().Contains(SearchBox.Text.ToLower()) ||
                    x.ProdName.ProdName.ToLower().Contains(SearchBox.Text.ToLower())).ToList();
        }

        switch (SortingBox.SelectedIndex)
        {
            case 0:
                listProducts = listProducts.OrderBy(x => x.ProductCount).ToList(); break;
            case 1:
                listProducts = listProducts.OrderByDescending(x => x.ProductCount).ToList(); break;

        }
        if (FilterBox.SelectedIndex != 0 && FilterBox.SelectedIndex != -1)
        {
            listProducts = listProducts.Where(x => x.Provider.ProviderName == FilterBox.SelectedItem!.ToString()).ToList();
        }


        ListProductsBox.ItemsSource = listProducts;
    }
    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow main = new MainWindow();
        main.Show();
        Close();
    }

    private void SearchBox_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        GetInfo();
    }

    private void SortingBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        GetInfo();
    }

    private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OrderWindow order = new OrderWindow(_user);
        order.Show();
        Close();
    }
}