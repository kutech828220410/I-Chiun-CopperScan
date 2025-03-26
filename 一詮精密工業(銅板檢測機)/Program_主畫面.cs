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
using MyUI;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
namespace 一詮精密工業_銅板檢測機_
{
    public partial class Main_Form : MyDialog
    {
        public enum enum_CCD_Fail_Result
        {
            [Description("GUID,VARCHAR,50,NONE")]
            GUID,
            [Description("座標,VARCHAR,50,NONE")]
            座標,
            [Description("中心,VARCHAR,50,NONE")]
            中心,
            [Description("已吸取,VARCHAR,50,NONE")]
            已吸取,
        }

        private void Program_主畫面_Init()
        {
            comboBox_主畫面_型號選擇.SelectedIndexChanged += ComboBox_主畫面_型號選擇_SelectedIndexChanged;
            rJ_Button_主畫面_讀取設定.MouseDownEvent += RJ_Button_主畫面_讀取設定_MouseDownEvent;

            sqL_DataGridView_CCD_Fail_檢測結果.Init(new Table(new enum_CCD_Fail_Result()));
            sqL_DataGridView_CCD_Fail_檢測結果.Set_ColumnVisible(false, new enum_CCD_Fail_Result().GetEnumNames());
            sqL_DataGridView_CCD_Fail_檢測結果.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_CCD_Fail_Result.座標);
            sqL_DataGridView_CCD_Fail_檢測結果.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_CCD_Fail_Result.中心);

            sqL_DataGridView_不良排除狀態.Init(new Table(new enum_CCD_Fail_Result()));
            sqL_DataGridView_不良排除狀態.Set_ColumnVisible(false, new enum_CCD_Fail_Result().GetEnumNames());
            sqL_DataGridView_不良排除狀態.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_CCD_Fail_Result.座標);
            sqL_DataGridView_不良排除狀態.Set_ColumnWidth(120, DataGridViewContentAlignment.MiddleCenter, enum_CCD_Fail_Result.中心);
            sqL_DataGridView_不良排除狀態.Set_ColumnWidth(60, DataGridViewContentAlignment.MiddleCenter, enum_CCD_Fail_Result.已吸取);

        }

        private void RJ_Button_主畫面_讀取設定_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = this.sqL_DataGridView_EngineeringModel.SQL_GetRows((int)enum_engineering_model.型號名稱, comboBox_主畫面_型號選擇.GetComboBoxText(), false);
            if (list_value.Count == 0) return;

            string text = list_value[0][(int)enum_engineering_model.設定Base64].ObjectToString();
            lowerMachine_Panel1.LoadDevice(text);
            MyMessageBox.ShowDialog("讀取成功");
        }

        private void ComboBox_主畫面_型號選擇_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<object[]> list_value = this.sqL_DataGridView_EngineeringModel.SQL_GetRows((int)enum_engineering_model.型號名稱, comboBox_主畫面_型號選擇.Text, false);
            if (list_value.Count == 0) return;

            string text = list_value[0][(int)enum_engineering_model.檢測別名].ObjectToString();

            rJ_TextBox_主畫面_檢測別名.Text = text;
        }

        private void sub_Program_主畫面()
        {

        }
    }
}
