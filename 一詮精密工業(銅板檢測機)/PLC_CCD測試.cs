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
            sub_Program_CCD軸到指定位置();
        }
        #region PLC_CCD測試一次
        PLC_Device PLC_Device_CCD測試一次 = new PLC_Device("M5010");
        PLC_Device PLC_Device_CCD測試一次_位置1 = new PLC_Device("D11000");
        PLC_Device PLC_Device_CCD測試一次_位置2 = new PLC_Device("D11001");
        PLC_Device PLC_Device_CCD測試一次_位置3 = new PLC_Device("D11002");
        PLC_Device PLC_Device_CCD測試一次_位置4 = new PLC_Device("D11003");
        PLC_Device PLC_Device_CCD測試一次_位置5 = new PLC_Device("D11004");

        PLC_Device PLC_Device_CCD測試一次_位置1_使用 = new PLC_Device("S11004");
        PLC_Device PLC_Device_CCD測試一次_位置2_使用 = new PLC_Device("S11024");
        PLC_Device PLC_Device_CCD測試一次_位置3_使用 = new PLC_Device("S11044");


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
            if (cnt_Program_CCD測試一次 == 3) cnt_Program_CCD測試一次_位置1_初始化(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 4) cnt_Program_CCD測試一次_位置1_等待到達(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 5) cnt_Program_CCD測試一次_位置1_開始測試(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 6) cnt_Program_CCD測試一次_位置2_初始化(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 7) cnt_Program_CCD測試一次_位置2_等待到達(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 8) cnt_Program_CCD測試一次_位置2_開始測試(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 9) cnt_Program_CCD測試一次_位置3_初始化(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 10) cnt_Program_CCD測試一次_位置3_等待到達(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 11) cnt_Program_CCD測試一次_位置3_開始測試(ref cnt_Program_CCD測試一次);
            if (cnt_Program_CCD測試一次 == 12) cnt_Program_CCD測試一次 = 65500;
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
     
        void cnt_Program_CCD測試一次_位置1_初始化(ref int cnt)
        {
            if(PLC_Device_CCD測試一次_位置1_使用.Bool == false)
            {
                cnt++;
                return;                   
            }
            if(PLC_Device_CCD軸到指定位置.Bool ==false)
            {
                PLC_Device_CCD軸到指定位置Data.Value = PLC_Device_CCD測試一次_位置1.Value;
                PLC_Device_CCD軸到指定位置.Bool = true;
                cnt++;
            }
                   
        }
        void cnt_Program_CCD測試一次_位置1_等待到達(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置1_使用.Bool == false)
            {
                cnt++;
                return;
            }
            if (PLC_Device_CCD軸到指定位置.Bool == false)
            {
                cnt++;
            }
        }
        void cnt_Program_CCD測試一次_位置1_開始測試(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置1_使用.Bool == false)
            {
                cnt++;
                return;
            }
            MetalMarkAIPost metalMarkAIPost = Function_測試一次(rJ_TextBox_主畫面_檢測別名.Text, 1);
            if (metalMarkAIPost.ResultImagePath == null)
            {
                MyMessageBox.ShowDialog("檢測失敗");
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                pictureBox_MainForm_01.Load(metalMarkAIPost.ResultImagePath);
            }));

            cnt++;
        }
        void cnt_Program_CCD測試一次_位置2_初始化(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置2_使用.Bool == false)
            {
                cnt++;
                return;
            }
            if (PLC_Device_CCD軸到指定位置.Bool == false)
            {
                PLC_Device_CCD軸到指定位置Data.Value = PLC_Device_CCD測試一次_位置2.Value;
                PLC_Device_CCD軸到指定位置.Bool = true;
                cnt++;
            }

        }
        void cnt_Program_CCD測試一次_位置2_等待到達(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置2_使用.Bool == false)
            {
                cnt++;
                return;
            }
            if (PLC_Device_CCD軸到指定位置.Bool == false)
            {
                cnt++;
            }
        }
        void cnt_Program_CCD測試一次_位置2_開始測試(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置2_使用.Bool == false)
            {
                cnt++;
                return;
            }
            MetalMarkAIPost metalMarkAIPost = Function_測試一次(rJ_TextBox_主畫面_檢測別名.Text, 1);
            if (metalMarkAIPost.ResultImagePath == null)
            {
                MyMessageBox.ShowDialog("檢測失敗");
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                pictureBox_MainForm_02.Load(metalMarkAIPost.ResultImagePath);
            }));

            cnt++;
        }
        void cnt_Program_CCD測試一次_位置3_初始化(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置3_使用.Bool == false)
            {
                cnt++;
                return;
            }
            if (PLC_Device_CCD軸到指定位置.Bool == false)
            {
                PLC_Device_CCD軸到指定位置Data.Value = PLC_Device_CCD測試一次_位置3.Value;
                PLC_Device_CCD軸到指定位置.Bool = true;
                cnt++;
            }

        }
        void cnt_Program_CCD測試一次_位置3_等待到達(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置3_使用.Bool == false)
            {
                cnt++;
                return;
            }
            if (PLC_Device_CCD軸到指定位置.Bool == false)
            {
                cnt++;
            }
        }
        void cnt_Program_CCD測試一次_位置3_開始測試(ref int cnt)
        {
            if (PLC_Device_CCD測試一次_位置3_使用.Bool == false)
            {
                cnt++;
                return;
            }
            MetalMarkAIPost metalMarkAIPost = Function_測試一次(rJ_TextBox_主畫面_檢測別名.Text, 1);
            if (metalMarkAIPost.ResultImagePath == null)
            {
                MyMessageBox.ShowDialog("檢測失敗");
                cnt = 65500;
                return;
            }
            this.Invoke(new Action(delegate
            {
                pictureBox_MainForm_03.Load(metalMarkAIPost.ResultImagePath);
            }));

            cnt++;
        }








        #endregion
        #region PLC_CCD軸到指定位置
        PLC_Device PLC_Device_CCD軸到指定位置 = new PLC_Device("S15128");
        PLC_Device PLC_Device_CCD軸到指定位置Data = new PLC_Device("D15122");
        PLC_Device PLC_Device_CCD軸到指定位置_OK = new PLC_Device("");
        int cnt_Program_CCD軸到指定位置 = 65534;
        void sub_Program_CCD軸到指定位置()
        {
            if (cnt_Program_CCD軸到指定位置 == 65534)
            {

                PLC_Device_CCD軸到指定位置.SetComment("PLC_CCD軸到指定位置");
                PLC_Device_CCD軸到指定位置_OK.SetComment("PLC_CCD軸到指定位置_OK");
                PLC_Device_CCD軸到指定位置.Bool = false;
                cnt_Program_CCD軸到指定位置 = 65535;
            }
            if (cnt_Program_CCD軸到指定位置 == 65535) cnt_Program_CCD軸到指定位置 = 1;
            if (cnt_Program_CCD軸到指定位置 == 1) cnt_Program_CCD軸到指定位置_檢查按下(ref cnt_Program_CCD軸到指定位置);
            if (cnt_Program_CCD軸到指定位置 == 2) cnt_Program_CCD軸到指定位置_初始化(ref cnt_Program_CCD軸到指定位置);
            if (cnt_Program_CCD軸到指定位置 == 3) cnt_Program_CCD軸到指定位置_等待到指定位置完成(ref cnt_Program_CCD軸到指定位置);
            if (cnt_Program_CCD軸到指定位置 == 4) cnt_Program_CCD軸到指定位置 = 65500;
            if (cnt_Program_CCD軸到指定位置 > 1) cnt_Program_CCD軸到指定位置_檢查放開(ref cnt_Program_CCD軸到指定位置);

            if (cnt_Program_CCD軸到指定位置 == 65500)
            {
                PLC_Device_CCD軸到指定位置.Bool = false;
                PLC_Device_CCD軸到指定位置_OK.Bool = false;
                cnt_Program_CCD軸到指定位置 = 65535;
            }
        }
        void cnt_Program_CCD軸到指定位置_檢查按下(ref int cnt)
        {
            if (PLC_Device_CCD軸到指定位置.Bool) cnt++;
        }
        void cnt_Program_CCD軸到指定位置_檢查放開(ref int cnt)
        {
            if (!PLC_Device_CCD軸到指定位置.Bool) cnt = 65500;
        }
        void cnt_Program_CCD軸到指定位置_初始化(ref int cnt)
        {
            PLC_Device_CCD軸到指定位置.Bool = true;
            cnt++;
        }
        void cnt_Program_CCD軸到指定位置_等待到指定位置完成(ref int cnt)
        {
            if(PLC_Device_CCD軸到指定位置.Bool == false)
            {
                cnt++;
            }
       
      
        }













        #endregion
    }
}
