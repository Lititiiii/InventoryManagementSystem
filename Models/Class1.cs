using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace InventoryManagementSystem.Models
{
    partial class DatatableViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = "test";
        [ObservableProperty]
        private DeviceInformation deviceinformation;
        [ObservableProperty]
        private IEnumerable<DeviceInformation> _sqlresult;

        public void SomeMethod()
        {
            // 设置 Title
            Title = "New Title";
        }
        string connectstring = "server=150.158.101.170; port=3306; database=ims; user=IMS; password=Cy2Ni5r6GH8aE4B6;";

        public bool Connect_SQL()
        {
            try
            {
                using (MySqlConnection connection = new(connectstring))
                {
                    connection.Open();
                    Console.WriteLine("连接成功！");

                    // 执行查询 
                    Sqlresult = connection.Query<DeviceInformation>(
                        "SELECT \r\n    TABLE_NAME AS       'Table_Name',\r\n    COLUMN_NAME AS      'Field_Name',\r\n    COLUMN_TYPE AS      'Data_Type',\r\n    IS_NULLABLE AS      'Is_Nullable',\r\n    COLUMN_DEFAULT AS   'Default_Value',\r\n    COLUMN_COMMENT AS   'Comment'\r\nFROM \r\n    INFORMATION_SCHEMA.COLUMNS\r\nWHERE \r\n    TABLE_SCHEMA = 'ims'  -- 替换为您的数据库名\r\nORDER BY \r\n    TABLE_NAME, \r\n    ORDINAL_POSITION;  -- 按表名和字段顺序排序");
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"数据库连接错误: {ex.Message}");
                return false; // 或者根据需要处理错误
            }
            catch (Exception ex)
            {
                Console.WriteLine($"其他错误: {ex.Message}");
                return false; // 或者根据需要处理错误
            }
        }
    }
    
}
