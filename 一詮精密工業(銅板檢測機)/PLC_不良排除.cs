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

namespace 一詮精密工業_銅板檢測機_
{
    public partial class Main_Form : MyDialog
    {
        private void PLC_不良排除_Init()
        {
            plC_UI_Init1.Add_Method(sub_PLC_不良排除);

            rJ_Button_將CCD不良讀取.MouseDownEvent += RJ_Button_將CCD不良讀取_MouseDownEvent;
        }

        private void RJ_Button_將CCD不良讀取_MouseDownEvent(MouseEventArgs mevent)
        {
            List<object[]> list_value = sqL_DataGridView_CCD_Fail_檢測結果.GetAllRows();
            sqL_DataGridView_CCD_Fail_檢測結果.ClearGrid();
            sqL_DataGridView_不良排除狀態.RefreshGrid(list_value);
        }

        private void sub_PLC_不良排除()
        {
            sub_Program_不良排除_檢查吸料NG();
            sub_Program_不良排除_排除完成註記();

        }

        #region PLC_不良排除_檢查吸料NG
        PLC_Device PLC_Device_不良排除_檢查吸料NG = new PLC_Device("M5600");
        PLC_Device PLC_Device_不良排除_檢查吸料NG_有料件要排除 = new PLC_Device("M5601");

        int cnt_Program_不良排除_檢查吸料NG = 65534;
        void sub_Program_不良排除_檢查吸料NG()
        {
            if (cnt_Program_不良排除_檢查吸料NG == 65534)
            {

                PLC_Device_不良排除_檢查吸料NG.SetComment("PLC_不良排除_檢查吸料NG");
                PLC_Device_不良排除_檢查吸料NG_有料件要排除.SetComment("不良排除_檢查吸料NG_有料件要排除");
                PLC_Device_不良排除_檢查吸料NG.Bool = false;
                cnt_Program_不良排除_檢查吸料NG = 65535;
            }
            if (cnt_Program_不良排除_檢查吸料NG == 65535) cnt_Program_不良排除_檢查吸料NG = 1;
            if (cnt_Program_不良排除_檢查吸料NG == 1) cnt_Program_不良排除_檢查吸料NG_檢查按下(ref cnt_Program_不良排除_檢查吸料NG);
            if (cnt_Program_不良排除_檢查吸料NG == 2) cnt_Program_不良排除_檢查吸料NG_初始化(ref cnt_Program_不良排除_檢查吸料NG);
            if (cnt_Program_不良排除_檢查吸料NG == 3) cnt_Program_不良排除_檢查吸料NG = 65500;
            if (cnt_Program_不良排除_檢查吸料NG > 1) cnt_Program_不良排除_檢查吸料NG_檢查放開(ref cnt_Program_不良排除_檢查吸料NG);

            if (cnt_Program_不良排除_檢查吸料NG == 65500)
            {
                PLC_Device_不良排除_檢查吸料NG.Bool = false;
                cnt_Program_不良排除_檢查吸料NG = 65535;
            }
        }
        void cnt_Program_不良排除_檢查吸料NG_檢查按下(ref int cnt)
        {
            if (PLC_Device_不良排除_檢查吸料NG.Bool) cnt++;
        }
        void cnt_Program_不良排除_檢查吸料NG_檢查放開(ref int cnt)
        {
            if (!PLC_Device_不良排除_檢查吸料NG.Bool) cnt = 65500;
        }
        void cnt_Program_不良排除_檢查吸料NG_初始化(ref int cnt)
        {
            List<object[]> list_value = sqL_DataGridView_不良排除狀態.GetAllRows();
            List<object[]> list_value_buf = (from temp in list_value
                                             where temp[(int)enum_CCD_Fail_Result.已吸取].ObjectToString() == "N"
                                             select temp).ToList();
            if(list_value_buf.Count == 0)
            {
                PLC_Device_不良排除_檢查吸料NG_有料件要排除.Bool = false;
                cnt = 65500;
                return;
            }
            double center_x = list_value_buf[0][(int)enum_CCD_Fail_Result.中心].ObjectToString().Split(',')[0].StringToDouble() * 100;
            double center_y = list_value_buf[0][(int)enum_CCD_Fail_Result.中心].ObjectToString().Split(',')[1].StringToDouble() * 100;

            plC_NumBox_不良排除_吸料位置X.Value = (int)center_x;
            plC_NumBox_不良排除_吸料位置Y.Value = (int)center_y;
            PLC_Device_不良排除_檢查吸料NG_有料件要排除.Bool = true;
            cnt++;
        }














        #endregion
        #region PLC_不良排除_排除完成註記
        PLC_Device PLC_Device_不良排除_排除完成註記 = new PLC_Device("M5610");

        int cnt_Program_不良排除_排除完成註記 = 65534;
        void sub_Program_不良排除_排除完成註記()
        {
            if (cnt_Program_不良排除_排除完成註記 == 65534)
            {

                PLC_Device_不良排除_排除完成註記.SetComment("PLC_不良排除_排除完成註記");
                PLC_Device_不良排除_排除完成註記.Bool = false;
                cnt_Program_不良排除_排除完成註記 = 65535;
            }
            if (cnt_Program_不良排除_排除完成註記 == 65535) cnt_Program_不良排除_排除完成註記 = 1;
            if (cnt_Program_不良排除_排除完成註記 == 1) cnt_Program_不良排除_排除完成註記_檢查按下(ref cnt_Program_不良排除_排除完成註記);
            if (cnt_Program_不良排除_排除完成註記 == 2) cnt_Program_不良排除_排除完成註記_初始化(ref cnt_Program_不良排除_排除完成註記);
            if (cnt_Program_不良排除_排除完成註記 == 3) cnt_Program_不良排除_排除完成註記 = 65500;
            if (cnt_Program_不良排除_排除完成註記 > 1) cnt_Program_不良排除_排除完成註記_檢查放開(ref cnt_Program_不良排除_排除完成註記);

            if (cnt_Program_不良排除_排除完成註記 == 65500)
            {
                PLC_Device_不良排除_排除完成註記.Bool = false;
                cnt_Program_不良排除_排除完成註記 = 65535;
            }
        }
        void cnt_Program_不良排除_排除完成註記_檢查按下(ref int cnt)
        {
            if (PLC_Device_不良排除_排除完成註記.Bool) cnt++;
        }
        void cnt_Program_不良排除_排除完成註記_檢查放開(ref int cnt)
        {
            if (!PLC_Device_不良排除_排除完成註記.Bool) cnt = 65500;
        }
        void cnt_Program_不良排除_排除完成註記_初始化(ref int cnt)
        {
            string center_str = $"{ plC_NumBox_不良排除_吸料位置X.Value / 100}";
            List<object[]> list_value = sqL_DataGridView_不良排除狀態.GetAllRows();
            for (int i = 0; i < list_value.Count; i++)
            {
                double center_x = list_value[i][(int)enum_CCD_Fail_Result.中心].ObjectToString().Split(',')[0].StringToDouble() * 100;
                double center_y = list_value[i][(int)enum_CCD_Fail_Result.中心].ObjectToString().Split(',')[1].StringToDouble() * 100;
                if (plC_NumBox_不良排除_吸料位置X.Value != (int)center_x) continue;
                if (plC_NumBox_不良排除_吸料位置Y.Value != (int)center_y) continue;
                list_value[i][(int)enum_CCD_Fail_Result.已吸取] = "Y";
                sqL_DataGridView_不良排除狀態.ReplaceExtra(list_value[i], true);
            }
         

     

            cnt++;
        }














        #endregion
    }
}
