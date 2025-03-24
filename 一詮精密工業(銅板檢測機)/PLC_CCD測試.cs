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
        private void PLC_CCD測試_Init()
        {

            plC_UI_Init1.Add_Method(sub_PLC_CCD測試);
        }
        private void sub_PLC_CCD測試()
        {
            sub_Program_CCD測試一次();
        }

        #region PLC_CCD測試一次
        PLC_Device PLC_Device_CCD測試一次 = new PLC_Device("M5010");
        PLC_Device PLC_Device_CCD測試一次_OK = new PLC_Device("");
        int cnt_Program_CCD測試一次 = 65534;
        void sub_Program_CCD測試一次()
        {
            if (cnt_Program_CCD測試一次 == 65534)
            {

                PLC_Device_CCD測試一次.SetComment("PLC_CCD測試一次");
                PLC_Device_CCD測試一次_OK.SetComment("PLC_CCD測試一次_OK");
                PLC_Device_CCD測試一次.Bool = false;
                cnt_Program_CCD測試一次 = 65535;
            }
            if (cnt_Program_CCD測試一次 == 65535) cnt_Program_CCD測試一次 = 1;
            if (cnt_Program_CCD測試一次 == 1) cnt_Program_CCD測試一次_檢查按下(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 2) cnt_Program_CCD測試一次_初始化(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 3) cnt_Program_CCD測試一次_開始測試(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 4) cnt_Program_CCD測試一次 = 65500;
            if (cnt_Program_CCD測試一次 > 1) cnt_Program_CCD測試一次_檢查放開(ref cnt_Program_CCD測試一次);

            if (cnt_Program_CCD測試一次 == 65500)
            {
                PLC_Device_CCD測試一次.Bool = false;
                PLC_Device_CCD測試一次_OK.Bool = false;
                cnt_Program_CCD測試一次 = 65535;
            }
        }
        void cnt_Program_CCD測試一次_檢查按下(ref int cnt)
        {
            if (plC_Button_CCD測試一次.Bool) cnt++;
        }
        void cnt_Program_CCD測試一次_檢查放開(ref int cnt)
        {
            if (!plC_Button_CCD測試一次.Bool) cnt = 65500;
        }
        void cnt_Program_CCD測試一次_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_CCD測試一次_開始測試(ref int cnt)
        {
            MetalMarkAIPost metalMarkAIPost = Function_測試一次(rJ_TextBox_主畫面_檢測別名.Text, 1);
            this.Invoke(new Action(delegate 
            {
                pictureBox_MainForm.Load(metalMarkAIPost.Data[0].ResultImagePath);
            }));
       
            cnt++;
        }













        #endregion
    }
}
