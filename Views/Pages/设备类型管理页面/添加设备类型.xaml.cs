using ImModel;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace InventoryManagementSystem.Views.Pages.设备类型管理页面
{
    /// <summary>
    /// 添加设备类型.xaml 的交互逻辑
    /// </summary>
    public partial class 添加设备类型 : Window
    {
        public event EventHandler<EquipmentType> DeviceTypeSubmitted; // 定义事件
        public 添加设备类型()
        {
            InitializeComponent();
        }

        
        private void 提交_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(设备类型_TextBox.Text))
            {
                System.Windows.MessageBox.Show("设备类型不能为空！");

            }
            else
            {
                EquipmentType equipmenttypes = new EquipmentType()
                {
                    EquipmentTypeName = 设备类型_TextBox.Text,
                    Remarks = 设备类型提交备注_TextBox.Text
                };
                DeviceTypeSubmitted?.Invoke(this, equipmenttypes); // 触发事件
            }
            this.Close();

        }
        private void 取消_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
