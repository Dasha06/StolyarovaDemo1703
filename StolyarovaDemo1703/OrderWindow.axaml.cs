using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StolyarovaDemo1703.Models;

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
    }
}