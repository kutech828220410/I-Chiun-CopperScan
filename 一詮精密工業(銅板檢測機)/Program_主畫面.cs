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
        private void Program_主畫面_Init()
        {
            comboBox_主畫面_型號選擇.SelectedIndexChanged += ComboBox_主畫面_型號選擇_SelectedIndexChanged;
            rJ_Button_主畫面_讀取設定.MouseDownEvent += RJ_Button_主畫面_讀取設定_MouseDownEvent;
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
