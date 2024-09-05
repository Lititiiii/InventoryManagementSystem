using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace InventoryManagementSystem.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "WPF UI - InventoryManagementSystem";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            
            new NavigationViewItem()
            {
                Content = "设备信息管理",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                
                MenuItemsSource = new object[]{
                    new NavigationViewItem()
                    {
                        Content = "设备信息",
                        Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                        TargetPageType = typeof(Views.Pages.设备管理页面.设备信息管理),
                    },
                }
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
