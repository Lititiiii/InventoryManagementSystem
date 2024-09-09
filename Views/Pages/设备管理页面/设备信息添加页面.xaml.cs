using ImModel;
using Mysqlx.Expr;
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
using System.Xml.Linq;
using Wpf.Ui.Controls;
using static System.Net.Mime.MediaTypeNames;
using MessageBox = System.Windows.MessageBox;

namespace InventoryManagementSystem.Views.Pages.设备管理页面
{
    /// <summary>
    /// 设备信息添加页面.xaml 的交互逻辑
    /// </summary>
    public partial class 设备信息添加页面 : Window
    {
        private ObservableCollection<EquipmentType> _equipmenttypes;
        private EquipmentInfo _equipmentInfos;
        public event EventHandler<EquipmentInfo> EquipmentInfoSubmitted; // 定义事件
        public 设备信息添加页面(ObservableCollection<EquipmentType> equipmenttypes)
        {
            InitializeComponent();
            提交设备信息_Button.Click += 提交设备信息_Click;
            foreach (var item  in equipmenttypes.ToList())
            {
                设备类别_ComboBox.Items.Add(item.EquipmentTypeName);
            }
        }
        public 设备信息添加页面(EquipmentInfo equipmentInfos, ObservableCollection<EquipmentType> equipmenttypes ,Int32 equipmentInfos_Id)
        {
            InitializeComponent();
            提交设备信息_Button.Click += (sender, e) =>修改设备信息_Click(sender, e, equipmentInfos_Id);
            _equipmentInfos = equipmentInfos;
            foreach (var item in equipmenttypes.ToList())
            {
                设备类别_ComboBox.Items.Add(item.EquipmentTypeName);
            }
            规格型号_TextBox.Text = equipmentInfos.SpecificationModel;
            设备标识_TextBox.Text = equipmentInfos.DeviceIdentifier;
            设备标识_TextBox.Text = equipmentInfos.DeviceImage;
            设备名称_TextBox.Text = equipmentInfos.DeviceName;
            经销商_TextBox.Text = equipmentInfos.Distributor;
            折旧率_TextBox.Text = equipmentInfos.AccumulatedDepreciationMonths;
            资产负责人_TextBox.Text = equipmentInfos.AssetManager;
            生产厂商_TextBox.Text = equipmentInfos.Manufacturer;
            净残率_TextBox.Text = equipmentInfos.NetValue;
            操作人员_TextBox.Text = equipmentInfos.Operator;
            购置时间_TextBox.Text = equipmentInfos.PurchaseTime;
            备注_TextBox.Text = equipmentInfos.Remarks;
            总功率_TextBox.Text = equipmentInfos.TotalPower;
            购买数量_TextBox.Text = equipmentInfos.DeviceNumber;
            设备类别_ComboBox.Text = Convert.ToString(equipmentInfos.DeviceCategoryId);
            购买数量_TextBox.Text = Convert.ToString(equipmentInfos.Quantity);
            安装地点_TextBox.Text = Convert.ToString(equipmentInfos.InstallationLocation);
            净残率_TextBox.Text = Convert.ToString(equipmentInfos.ResidualValueRate);
            使用部门_TextBox.Text = Convert.ToString(equipmentInfos.UsingDepartmentId);


        }

        private void 提交设备信息_Click(object sender, RoutedEventArgs e )
        {
           
            Int32 _Quantity              =0;
            Int32 _UsingDepartmentId     =0;
            Int32 _AssetOriginalValue = 0;
            Double _ResidualValueRate = 0;

            try
            {
                _Quantity = Convert.ToInt32(购买数量_TextBox.Text);
                _UsingDepartmentId = Convert.ToInt32(使用部门_TextBox.Text);
                _AssetOriginalValue = Convert.ToInt32(净残率_TextBox.Text);
                _ResidualValueRate = Convert.ToDouble(设备价格_TextBox.Text);
            }
            catch (FormatException ex)
            {
                // 检查每个 TextBox 的值，找出哪个有误
                if (!ulong.TryParse(购买数量_TextBox.Text, out _))
                {
                    MessageBox.Show("购买数量 格式错误。");
                }
                else if (!ulong.TryParse(使用部门_TextBox.Text, out _))
                {
                    MessageBox.Show("使用部门 格式错误。");
                }
                else if (!ulong.TryParse(设备价格_TextBox.Text, out _))
                {
                    MessageBox.Show("设备价格 格式错误。");
                }
                else if (!double.TryParse(净残率_TextBox.Text, out _))
                {
                    MessageBox.Show("净残率 格式错误。");
                }
               
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("年龄超出范围。");
            }

            var _InstallationLocation = 安装地点_TextBox.Text;
            var  _DeviceCategoryId= 设备类别_ComboBox.Text;
            var _SpecificationModel = 规格型号_TextBox.Text;
            var _DeviceIdentifier = 设备标识_TextBox.Text;
            var _DeviceImage = 设备标识_TextBox.Text;
            var _DeviceName = 设备名称_TextBox.Text;
            var _Distributor = 经销商_TextBox.Text;
            var _AccumulatedDepreciationMonths = 折旧率_TextBox.Text;
            var _AssetManager = 资产负责人_TextBox.Text;
            var _Manufacturer = 生产厂商_TextBox.Text;
            var _NetValue = 净残率_TextBox.Text;
            var _Operator = 操作人员_TextBox.Text;
            var _PurchaseTime = 购置时间_TextBox.Text;
            var _Remarks = 备注_TextBox.Text;
            var _TotalPower = 总功率_TextBox.Text;
            var _DeviceNumber = 购买数量_TextBox.Text;
            
            using (var context = new ImEntities())
            {
               
                var equipmentInfo = new EquipmentInfo
                {
                    
                    DeviceCategoryId = _DeviceCategoryId,
                    Quantity = _Quantity,
                    InstallationLocation = _InstallationLocation,
                    ResidualValueRate =(float)_ResidualValueRate,
                    UsingDepartmentId = _UsingDepartmentId,
                    DeviceNumber = _DeviceNumber,
                    AssetOriginalValue = _AssetOriginalValue,


                    SpecificationModel = _SpecificationModel,
                    DeviceIdentifier = _DeviceIdentifier,
                    DeviceImage = _DeviceImage,
                    DeviceName = _DeviceName,
                    Distributor = _Distributor,
                    AccumulatedDepreciationMonths = _AccumulatedDepreciationMonths,
                    AssetManager = _AssetManager,
                    
                    Manufacturer = _Manufacturer,
                    NetValue = _NetValue,
                    Operator = _Operator,
                    PurchaseTime = _PurchaseTime,
                    Remarks = _Remarks,
                    TotalPower = _TotalPower,
                   
                    
                };
               EquipmentInfoSubmitted?.Invoke(this, equipmentInfo); // 触发事件
               this.Close();
            }
        }

        private void 修改设备信息_Click(object sender, RoutedEventArgs e, Int32 equipmentInfos_Id)
        {
          

            var _InstallationLocation = 安装地点_TextBox.Text;
            var _DeviceCategoryId = 设备类别_ComboBox.Text;
            var _SpecificationModel = 规格型号_TextBox.Text;
            var _DeviceIdentifier = 设备标识_TextBox.Text;
            var _DeviceImage = 设备标识_TextBox.Text;
            var _DeviceName = 设备名称_TextBox.Text;
            var _Distributor = 经销商_TextBox.Text;
            var _AccumulatedDepreciationMonths = 折旧率_TextBox.Text;
            var _AssetManager = 资产负责人_TextBox.Text;
            var _Manufacturer = 生产厂商_TextBox.Text;
            var _NetValue = 净残率_TextBox.Text;
            var _Operator = 操作人员_TextBox.Text;
            var _PurchaseTime = 购置时间_TextBox.Text;
            var _Remarks = 备注_TextBox.Text;
            var _TotalPower = 总功率_TextBox.Text;
            var _DeviceNumber = 购买数量_TextBox.Text;
            Int32 _Quantity = 0;
            Int32 _UsingDepartmentId = 0;
            Int32 _AssetOriginalValue = 0;
            Double _ResidualValueRate = 0;
            try
            {
                _Quantity = Convert.ToInt32(购买数量_TextBox.Text);
                _UsingDepartmentId = Convert.ToInt32(使用部门_TextBox.Text);
                _AssetOriginalValue = Convert.ToInt32(净残率_TextBox.Text);
                _ResidualValueRate = Convert.ToDouble(设备价格_TextBox.Text);
            }
            catch (FormatException ex)
            {
                // 检查每个 TextBox 的值，找出哪个有误
                if (!ulong.TryParse(购买数量_TextBox.Text, out _))
                {
                    MessageBox.Show("购买数量 格式错误。");
                }
                else if (!ulong.TryParse(使用部门_TextBox.Text, out _))
                {
                    MessageBox.Show("使用部门 格式错误。");
                }
                else if (!ulong.TryParse(设备价格_TextBox.Text, out _))
                {
                    MessageBox.Show("设备价格 格式错误。");
                }
                else if (!double.TryParse(净残率_TextBox.Text, out _))
                {
                    MessageBox.Show("净残率 格式错误。");
                }

                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("年龄超出范围。");
            }



            using (var context = new ImEntities())
            {

                var equipmentInfo = new EquipmentInfo
                {
                    Id = equipmentInfos_Id,
                    DeviceCategoryId = _DeviceCategoryId,
                    Quantity = _Quantity,
                    InstallationLocation = _InstallationLocation,
                    ResidualValueRate = (float)_ResidualValueRate,
                    UsingDepartmentId = _UsingDepartmentId,
                    DeviceNumber = _DeviceNumber,
                    AssetOriginalValue = _AssetOriginalValue,


                    SpecificationModel = _SpecificationModel,
                    DeviceIdentifier = _DeviceIdentifier,
                    DeviceImage = _DeviceImage,
                    DeviceName = _DeviceName,
                    Distributor = _Distributor,
                    AccumulatedDepreciationMonths = _AccumulatedDepreciationMonths,
                    AssetManager = _AssetManager,

                    Manufacturer = _Manufacturer,
                    NetValue = _NetValue,
                    Operator = _Operator,
                    PurchaseTime = _PurchaseTime,
                    Remarks = _Remarks,
                    TotalPower = _TotalPower,


                };
                EquipmentInfoSubmitted?.Invoke(this, equipmentInfo); // 触发事件
                this.Close();
            }
        }

        private void 备注_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void 设备类别_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void 设备名称_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
