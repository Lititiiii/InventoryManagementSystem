using Devart.Data.Linq;
using ImModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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

namespace InventoryManagementSystem.Views.Pages.设备类型管理页面
{
    /// <summary>
    /// 添加设备类型.xaml 的交互逻辑
    /// </summary>
    public partial class 设备类型列表 : Page
    {
        public ImEntities context ;
        public ObservableCollection<EquipmentType> equipmenttypes ;

        public 设备类型列表()
        {
            InitializeComponent();
            context = new ImEntities();
            equipmenttypes = new ObservableCollection<EquipmentType>();
            foreach (var item in context.EquipmentTypes.ToList())
            {
                equipmenttypes.Add(item);
            }
            设备类型_DataGrid.ItemsSource = equipmenttypes; //绑定数据源
        }

        private void LoadData()
        {
            
        }

        private void 添加设备类型_Button_Click(object sender, RoutedEventArgs e)
        {
            添加设备类型 添加设备类型_window = new 添加设备类型();
            添加设备类型_window.DeviceTypeSubmitted += 添加设备类型_window_DeviceTypeSubmitted;
            添加设备类型_window.ShowDialog();
        }

        private void 添加设备类型_window_DeviceTypeSubmitted(object? sender, EquipmentType e)
        {
         
            e.Id = equipmenttypes.Count == 0 ?  e.Id = 0 : equipmenttypes.Max(e => e.Id) + 1;
            if (equipmenttypes.Any(et => et.EquipmentTypeName == e.EquipmentTypeName))
            {
                System.Windows.MessageBox.Show("存在相同类型，请检查");
                return;
            }
            equipmenttypes.Add(e);
            context.AddToEquipmentTypes(e);
            context.SaveChanges();
        }

        private void 删除设备类型_Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前按钮所在的 DataGrid 行
            var button = sender as Button;
            var dataContext = button?.DataContext as EquipmentType; // 获取当前行的数据上下文
            var department = context.EquipmentTypes.FirstOrDefault(d => d.Id == dataContext.Id); // 使用 FirstOrDefault 查找
            if (department != null)
            {
                context.EquipmentTypes.DeleteObject(department);
                context.SaveChanges();
            }
            // 处理删除逻辑
            if (dataContext != null)
            {
                // 从数据源中移除该项
                equipmenttypes.Remove(dataContext);
            }
        }

        private void 修改类型详情_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private bool IsMaximize = false;
       
    }
   
}
