using ImModel;
using InventoryManagementSystem.Views.Pages.设备管理页面;
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

namespace InventoryManagementSystem.Views.Pages.基本信息设置
{
    /// <summary>
    /// 供应商管理.xaml 的交互逻辑
    /// </summary>
    public partial class 供应商管理 : Page
    {
        public 供应商管理()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            using (var context = new ImEntities())
            {
                var accidentLevels = context.PartnerInfos.ToList();
                供应商_DataGrid.ItemsSource = accidentLevels; //绑定数据源
            }
        }
        private void 添加_Button_Click(object sender, RoutedEventArgs e)
        {
            var formWindow = new 新增供应商();
            formWindow.ShowDialog(); // 以对话框形式打开
            LoadData();
        }
        private void 删除_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
