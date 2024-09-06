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
using ImModel;
using InventoryManagementSystem.Views.Windows;

namespace InventoryManagementSystem.Views.Pages.基本信息设置
{
    /// <summary>
    /// 新增供应商.xaml 的交互逻辑
    /// </summary>
    public partial class 新增供应商 : Window
    {
        public 新增供应商()
        {
            InitializeComponent();
        }

       
        private void 提交供应商_Button_Click(object sender, RoutedEventArgs e)
        {

            String _UnitName = 单位名称_TextBox.Text;
            String _Address = 地址_TextBox.Text;
            Int32 UnitTypeId = 0;
            try
            {
                UnitTypeId = Convert.ToInt32(单位编号_TextBox.Text);
            }
            catch
            {
                if (!ulong.TryParse(单位编号_TextBox.Text, out _))
                {
                    MessageBox.Show("单位编号输入格式错误。");
                }
                return;
            }
            if (string.IsNullOrWhiteSpace(单位名称_TextBox.Text))
            {
                MessageBox.Show("单位名称不能为空。");
                return;
            }
            if (string.IsNullOrWhiteSpace(地址_TextBox.Text))
            {
                MessageBox.Show("地址不能为空。");
                return;
            }
            else
            {
                using (var context = new ImEntities())
                {

                    var equipmentInfo = new PartnerInfo
                    {
                        Id = 3,
                        Remarks =供应商备注_TextBox.Text,
                        
                        Account =账户_TextBox.Text,
                        Address = _Address,
                        BankName = 开户银行_TextBox.Text,
                        ContactPerson = 联系人_TextBox.Text,
                        ContactPhone = 联系电话_TextBox.Text,
                        Email = 电子信箱_TextBox.Text,
                        Fax = 传真_TextBox.Text,
                        PostalCode = 邮编_TextBox.Text,
                        TaxNumber = 税号_TextBox.Text,
                        UnitName = _UnitName,
                        UnitNumber = 单位编号_TextBox.Text,
                        
                    };
                    context.AddToPartnerInfos(equipmentInfo);
                    context.SaveChanges(); // 提交更改
                }

            }
            
        }
    }
}
