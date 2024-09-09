using ImModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InventoryManagementSystem.Views.Pages.设备管理页面
{
    /// <summary>
    /// 设备信息管理.xaml 的交互逻辑
    /// </summary>
    public partial class 设备信息管理 : Page
    {
        private ObservableCollection<EquipmentType> equipmenttypes;
        private ObservableCollection<EquipmentInfo> equipmentinfos;
        
        private ImEntities context;
        public 设备信息管理()
        {
            InitializeComponent();
            equipmentinfos = new ObservableCollection<EquipmentInfo>();
            equipmenttypes = new ObservableCollection<EquipmentType>();
            LoadData();
        }
        private void LoadData()
        {
            context = new ImEntities();
            equipmentinfos= new ObservableCollection<EquipmentInfo>();

            foreach (var item in context.EquipmentInfos.ToList())
            {
                equipmentinfos.Add(item);
            }
            设备信息_DataGrid.ItemsSource = equipmentinfos;
            
            //刷新设备类型
            equipmenttypes= new ObservableCollection<EquipmentType>();
            using (var type_context = new ImEntities())
            {
                var types = context.EquipmentTypes.ToList();
                foreach (var item in types)
                {
                    equipmenttypes.Add(item);
                }
            }
        }
        private void 添加设备信息_Button_Click(object sender, RoutedEventArgs e)
        {
            var formWindow = new 设备信息添加页面(equipmenttypes);
            formWindow.EquipmentInfoSubmitted += 添加设备类型_window_EquipmentInfoSubmittedSubmitted;
            formWindow.ShowDialog(); // 以对话框形式打开
            
        }
        private void 删除设备信息_Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前按钮所在的 DataGrid 行
            var button = sender as Button;
            var dataContext = button?.DataContext as EquipmentInfo; // 获取当前行的数据上下文
            var department = context.EquipmentInfos.FirstOrDefault(d => d.Id == dataContext.Id); // 使用 FirstOrDefault 查找
            if (department != null)
            {
                context.EquipmentInfos.DeleteObject(department);
                context.SaveChanges();
            }
            // 处理删除逻辑
            if (dataContext != null)
            {
                // 从数据源中移除该项
                equipmentinfos.Remove(dataContext);
            }
        }
        private void 编辑设备信息_Button_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前按钮所在的 DataGrid 行
            var button = sender as Button;
            var dataContext = button?.DataContext as EquipmentInfo; // 获取当前行的数据上下文
            var department = context.EquipmentInfos.FirstOrDefault(d => d.Id == dataContext.Id); // 使用 FirstOrDefault 查找
            var formWindow = new 设备信息添加页面(department, equipmenttypes, dataContext.Id);
            formWindow.EquipmentInfoSubmitted += 编辑设备类型_window_EquipmentInfoSubmittedSubmitted;
            formWindow.ShowDialog(); // 以对话框形式打开
        }

        void 添加设备类型_window_EquipmentInfoSubmittedSubmitted(object? sender, EquipmentInfo e)
        {
            equipmentinfos.Add(e);
            context.AddToEquipmentInfos(e);
            context.SaveChanges(); // 提交更改
        }
        void 编辑设备类型_window_EquipmentInfoSubmittedSubmitted(object? sender, EquipmentInfo e)
        {

            var obs_entity = equipmentinfos.FirstOrDefault(a=>a.Id==e.Id);
            equipmentinfos[equipmentinfos.IndexOf(obs_entity)]  = e;
            var equipmentToUpdate = context.EquipmentInfos.FirstOrDefault(a => a.Id == e.Id);
            context.EquipmentInfos.ApplyCurrentValues(e);
            context.SaveChanges();
        }

    }
}
