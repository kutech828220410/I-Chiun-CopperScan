using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using MyUI;
namespace 一詮精密工業_銅板檢測機_
{
    [EnumDescription("engineering_model")]
    public enum enum_engineering_model
    {
        [Description("GUID,VARCHAR,50,PRIMARY")]
        GUID,
        [Description("型號代碼,VARCHAR,50,INDEX")]
        型號代碼,
        [Description("型號名稱,VARCHAR,200,NONE")]
        型號名稱,
        [Description("檢測別名,VARCHAR,200,NONE")]
        檢測別名,
        [Description("分類,VARCHAR,200,INDEX")]
        分類,
        [Description("規格,VARCHAR,200,NONE")]
        規格,
        [Description("單位,VARCHAR,10,NONE")]
        單位,
        [Description("製造商,VARCHAR,500,NONE")]
        製造商,
        [Description("材質,VARCHAR,50,NONE")]
        材質,
        [Description("執行標準,VARCHAR,50,NONE")]
        執行標準,
        [Description("狀態,VARCHAR,20,NONE")]
        狀態,
        [Description("備註,VARCHAR,500,NONE")]
        備註,
        [Description("設定Base64,LONGTEXT,50,NONE")]
        設定Base64,
        [Description("建表人員,VARCHAR,50,INDEX")]
        建表人員,
        [Description("建表時間,DATETIME,50,INDEX")]
        建表時間,

    }
    public class engineering_model
    {
        /// <summary>
        /// 唯一識別碼
        /// </summary>
        [JsonPropertyName("GUID")]
        public string GUID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 型號代碼
        /// </summary>
        [JsonPropertyName("ModelCode")]
        public string 型號代碼 { get; set; }

        /// <summary>
        /// 型號名稱
        /// </summary>
        [JsonPropertyName("ModelName")]
        public string 型號名稱 { get; set; }

        /// <summary>
        /// 檢測別名
        /// </summary>
        [JsonPropertyName("Alias")]
        public string 檢測別名 { get; set; }

        /// <summary>
        /// 分類
        /// </summary>
        [JsonPropertyName("Category")]
        public string 分類 { get; set; }

        /// <summary>
        /// 規格
        /// </summary>
        [JsonPropertyName("Spec")]
        public string 規格 { get; set; }

        /// <summary>
        /// 單位
        /// </summary>
        [JsonPropertyName("Unit")]
        public string 單位 { get; set; }

        /// <summary>
        /// 製造商
        /// </summary>
        [JsonPropertyName("Manufacturer")]
        public string 製造商 { get; set; }

        /// <summary>
        /// 材質
        /// </summary>
        [JsonPropertyName("Material")]
        public string 材質 { get; set; }

        /// <summary>
        /// 執行標準
        /// </summary>
        [JsonPropertyName("Standard")]
        public string 執行標準 { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("Status")]
        public string 狀態 { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [JsonPropertyName("Remark")]
        public string 備註 { get; set; }

        /// <summary>
        /// 設定Base64
        /// </summary>
        [JsonPropertyName("SettingBase64")]
        public string 設定Base64 { get; set; }

        /// <summary>
        /// 建表人員
        /// </summary>
        [JsonPropertyName("CT")]
        public string 建表人員 { get; set; }

        /// <summary>
        /// 建表時間
        /// </summary>
        [JsonPropertyName("CT_TIME")]
        public string 建表時間 { get; set; }

    }

    public partial class Main_Form : MyDialog
    {
        private void Program_engineering_model_Init()
        {
            this.sqL_DataGridView_EngineeringModel.Init(new SQLUI.Table(new enum_engineering_model()));
            if (this.sqL_DataGridView_EngineeringModel.SQL_IsTableCreat() == false) this.sqL_DataGridView_EngineeringModel.SQL_CreateTable();
            else this.sqL_DataGridView_EngineeringModel.SQL_CheckAllColumnName(true);

            this.sqL_DataGridView_EngineeringModel.Set_ColumnVisible(false, new enum_engineering_model().GetEnumNames());

            this.sqL_DataGridView_EngineeringModel.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_engineering_model.型號名稱);
            this.sqL_DataGridView_EngineeringModel.Set_ColumnWidth(350, DataGridViewContentAlignment.MiddleLeft, enum_engineering_model.檢測別名);
            this.sqL_DataGridView_EngineeringModel.Set_ColumnWidth(200, DataGridViewContentAlignment.MiddleLeft, enum_engineering_model.建表時間);

            this.sqL_DataGridView_EngineeringModel.RowDoubleClickEvent += SqL_DataGridView_EngineeringModel_RowDoubleClickEvent;

            this.comboBox_EngineeringModel_檢測別名.DataSource = new enum_AI_test_Type().GetEnumNames();
            this.comboBox_EngineeringModel_檢測別名.SelectedIndex = 0;

            rJ_Button_EngineeringModel_新增.MouseDownEvent += RJ_Button_EngineeringModel_新增_MouseDownEvent;
            rJ_Button_EngineeringModel_修改.MouseDownEvent += RJ_Button_EngineeringModel_修改_MouseDownEvent;
            rJ_Button_EngineeringModel_刪除.MouseDownEvent += RJ_Button_EngineeringModel_刪除_MouseDownEvent;


        }
  
    
        private void RJ_Button_EngineeringModel_刪除_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_EngineeringModel.Get_All_Select_RowsValues();

            if(list_value.Count == 0)
            {
                MyMessageBox.ShowDialog("未選取資料");
                return;
            }
            engineering_model engineering_Model = list_value[0].SQLToClass<engineering_model, enum_engineering_model>();
            if (MyMessageBox.ShowDialog($"確認刪除[{engineering_Model.型號名稱}]?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel) != DialogResult.Yes) return;

            sqL_DataGridView_EngineeringModel.SQL_DeleteExtra(list_value, true);

        }
        private void SqL_DataGridView_EngineeringModel_RowDoubleClickEvent(object[] RowValue)
        {
            engineering_model engineering_Model = RowValue.SQLToClass<engineering_model, enum_engineering_model>();
            rJ_TextBox_EngineeringModel_型號名稱.Text = engineering_Model.型號名稱;
            this.comboBox_EngineeringModel_檢測別名.SelectedItem = engineering_Model.檢測別名;
            richTextBox_EngineeringModel_設定Base64.Text = engineering_Model.設定Base64;
        }
        private void RJ_Button_EngineeringModel_新增_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_EngineeringModel_型號名稱.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("型號名稱空白");
                return;
            }
            object[] value = null;

            List<object[]> list_value = sqL_DataGridView_EngineeringModel.SQL_GetRows((int)enum_engineering_model.型號名稱, rJ_TextBox_EngineeringModel_型號名稱.Text, false);
            if(list_value.Count >= 1)
            {
                MyMessageBox.ShowDialog($"型號名稱[{rJ_TextBox_EngineeringModel_型號名稱.Text}],已經存在");
                return;
            }

            engineering_model engineering_Model = new engineering_model();
            engineering_Model.型號名稱 = rJ_TextBox_EngineeringModel_型號名稱.Text;
            engineering_Model.檢測別名 = this.comboBox_EngineeringModel_檢測別名.GetComboBoxText();
            engineering_Model.設定Base64 = lowerMachine_Panel1.GetDeviceBase64();
            engineering_Model.建表時間 = DateTime.Now.ToDateTimeString_6();
            value = engineering_Model.ClassToSQL<engineering_model, enum_engineering_model>();
            list_value.Add(value);
            this.sqL_DataGridView_EngineeringModel.SQL_AddRows(list_value, true);

            MyMessageBox.ShowDialog("新增成功");

        }
        private void RJ_Button_EngineeringModel_修改_MouseDownEvent(MouseEventArgs mevent)
        {
            if (rJ_TextBox_EngineeringModel_型號名稱.Text.StringIsEmpty())
            {
                MyMessageBox.ShowDialog("請選取型號");
                return;
            }
            object[] value = null;

            List<object[]> list_value = sqL_DataGridView_EngineeringModel.SQL_GetRows((int)enum_engineering_model.型號名稱, rJ_TextBox_EngineeringModel_型號名稱.Text, false);
            if (list_value.Count == 0)
            {
                MyMessageBox.ShowDialog($"型號名稱[{rJ_TextBox_EngineeringModel_型號名稱.Text}],不存在");
                return;
            }

            engineering_model engineering_Model = list_value[0].SQLToClass<engineering_model, enum_engineering_model>();

            engineering_Model.型號名稱 = rJ_TextBox_EngineeringModel_型號名稱.Text;
            engineering_Model.檢測別名 = this.comboBox_EngineeringModel_檢測別名.GetComboBoxText();
            if(MyMessageBox.ShowDialog("是否覆蓋存檔設定?", MyMessageBox.enum_BoxType.Warning, MyMessageBox.enum_Button.Confirm_Cancel)== DialogResult.Yes)
            {
                engineering_Model.設定Base64 = lowerMachine_Panel1.GetDeviceBase64();
            }      
            engineering_Model.建表時間 = DateTime.Now.ToDateTimeString_6();
            this.sqL_DataGridView_EngineeringModel.SQL_ReplaceExtra(list_value, true);
            MyMessageBox.ShowDialog("修改成功");
        }


    }

}
