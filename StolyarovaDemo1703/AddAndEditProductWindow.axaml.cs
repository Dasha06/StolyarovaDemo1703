using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using StolyarovaDemo1703.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StolyarovaDemo1703;

public partial class AddAndEditProductWindow : Window
{
    User _user;
    string namePhoto;
    public AddAndEditProductWindow()
    {
        InitializeComponent();
    }

    public AddAndEditProductWindow(User user)
    {
        InitializeComponent();
        _user = user;
        PullComboBoxes();
    }

    private void PullComboBoxes()
    {
        StolyarovaContext context = new StolyarovaContext();

        var prodName = context.ProductNames.Select(x => x.ProdName).ToList();
        var units = context.Units.Select(x => x.UnitName).ToList();
        var provider = context.Providers.Select(x => x.ProviderName).ToList();
        var manufacturers = context.Manufacturers.Select(x => x.ManufacturerName).ToList();
        var categories = context.Categories.Select(x => x.CategoryName).ToList();

        productNameCombo.ItemsSource = prodName;
        UnitCombo.ItemsSource = units;
        ProviderCombo.ItemsSource = provider;
        ManufacturerCombo.ItemsSource = manufacturers;
        CategoryCombo.ItemsSource = categories;
    }

    private async void Button_ClickSave(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        StolyarovaContext context = new StolyarovaContext();

        StringBuilder errors = new StringBuilder();

        if (productNameCombo.SelectedIndex == -1)
        {
            errors.AppendLine("Не выбрано наименование");
        }
        if (CategoryCombo.SelectedIndex == -1)
        {
            errors.AppendLine("Не выбрана категория");
        }
        if (ManufacturerCombo.SelectedIndex == -1)
        {
            errors.AppendLine("Не выбран производитель");
        }
        if (ProviderCombo.SelectedIndex == -1)
        {
            errors.AppendLine("не выбран поставщик");
        }
        if (UnitCombo.SelectedIndex == -1) 
        {
            errors.AppendLine("Не выбрана единица измерения");
        }
        if (DescriptionBox.Text == null)
        {
            errors.AppendLine("Не указано описание");
        }
        if (PriceBox.Text == null)
        {
            errors.AppendLine("Не указана цена");
        }
        if (CountBox.Text == null)
        {
            errors.AppendLine("Не указано количество на складе");
        }
        if (ActiveDiscountBox.Text == null)
        {
            errors.AppendLine("Не указана дейсвующая скидка");
        }

        if (double.TryParse(PriceBox.Text, out var price)!= true)
        {
            errors.AppendLine("Цена не в числовом значении");
        }
        if (int.TryParse(ActiveDiscountBox.Text, out var discount) != true)
        {
            errors.AppendLine("Действующая скидка не в числовом значении");
        }
        if (int.TryParse(CountBox.Text, out var count) != true)
        {
            errors.AppendLine("Количество на складе не в числовом значении");
        }

        if (errors.Length > 0)
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Уведомление", errors.ToString(), MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await message.ShowAsync();
        }
        else
        {
            var articul = Guid.NewGuid().ToString("N").ToUpper().Substring(26);
            var newProduct = new Product
            {
                ProductDescription = DescriptionBox.Text,
                ProductPrice = double.Parse(PriceBox.Text),
                ProductActiveDiscount = int.Parse(ActiveDiscountBox.Text),
                ProductCount = int.Parse(CountBox.Text),
                CategoryId = context.Categories.Where(x => x.CategoryName == CategoryCombo.Text).Select(x => x.CategoryId).FirstOrDefault(),
                ManufacturerId = context.Manufacturers.Where(x => x.ManufacturerName == ManufacturerCombo.Text).Select(x => x.ManufacturerId).FirstOrDefault(),
                ProdNameId = context.ProductNames.Where(x => x.ProdName == productNameCombo.Text).Select(x => x.ProdNameId).FirstOrDefault(),
                ProviderId = context.Providers.Where(x => x.ProviderName == ProviderCombo.Text).Select(x => x.ProviderId).FirstOrDefault(),
                UnitId = context.Units.Where(x => x.UnitName == UnitCombo.Text).Select(x => x.UnitId).FirstOrDefault(),
                ProductArticul = articul
            };
            if (namePhoto != null)
            {
                newProduct.ProductImage = namePhoto;
            }

            context.Products.Add(newProduct);
            context.SaveChanges();

            AdminWindow admin = new AdminWindow(_user);
            admin.Show();
            Close();
        }

    }

    private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var topLevels = TopLevel.GetTopLevel(this);

        var files = await topLevels.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Image File",
            AllowMultiple = false
        });

        Prewatch.Source = files[0].Path.LocalPath;

        namePhoto = Guid.NewGuid().ToString("N") + ".png";
        File.Copy(files[0].Path.LocalPath, AppDomain.CurrentDomain.BaseDirectory + "/images/" + namePhoto);
        
        try
        {
            File.Copy(files[0].Path.LocalPath, AppDomain.CurrentDomain.BaseDirectory + "../../../images/" + namePhoto);
        }
        catch (Exception ex) 
        {
            var message = MessageBoxManager.GetMessageBoxStandard("Уведомление", "ошибка" + ex, MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await message.ShowAsync();
        }
        
    }
}