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
        private void PLC_出料盤系統_Init()
        {
            plC_UI_Init1.Add_Method(sub_PLC_出料盤系統);
        }


        private void sub_PLC_出料盤系統()
        {
            sub_Program_出料前升降復位();
            sub_Program_出料後升降復位();
        }


        #region PLC_出料前升降復位
        PLC_Device PLC_Device_出料前升降復位 = new PLC_Device("M5400");
        PLC_Device PLC_Device_出料前升降復位_馬達上升 = new PLC_Device("M54");
        PLC_Device PLC_Device_出料前升降復位_馬達下降 = new PLC_Device("M55");
        PLC_Device PLC_Device_出料前升降復位_原點訊號 = new PLC_Device("X24");
        PLC_Device PLC_Device_出料前升降復位_OK = new PLC_Device("S3020");
        MyTimer MyTimer_出料前升降復位_結束延遲 = new MyTimer();
        MyTimer MyTimer_出料前升降復位_開始延遲 = new MyTimer();
        int cnt_Program_出料前升降復位 = 65534;
        void sub_Program_出料前升降復位()
        {
            if (cnt_Program_出料前升降復位 == 65534)
            {
                this.MyTimer_出料前升降復位_結束延遲.StartTickTime(10000);
                this.MyTimer_出料前升降復位_開始延遲.StartTickTime(10000);
                PLC_Device_出料前升降復位.SetComment("PLC_出料前升降復位");
                PLC_Device_出料前升降復位_OK.SetComment("PLC_出料前升降復位_OK");
                PLC_Device_出料前升降復位.Bool = false;
                cnt_Program_出料前升降復位 = 65535;
            }
            if (cnt_Program_出料前升降復位 == 65535) cnt_Program_出料前升降復位 = 1;
            if (cnt_Program_出料前升降復位 == 1) cnt_Program_出料前升降復位_檢查按下(ref cnt_Program_出料前升降復位);
            if (cnt_Program_出料前升降復位 == 2) cnt_Program_出料前升降復位_初始化(ref cnt_Program_出料前升降復位);
            if (cnt_Program_出料前升降復位 == 3) cnt_Program_出料前升降復位_到達原點(ref cnt_Program_出料前升降復位);
            if (cnt_Program_出料前升降復位 == 4) cnt_Program_出料前升降復位_離開原點(ref cnt_Program_出料前升降復位);
            if (cnt_Program_出料前升降復位 == 5) cnt_Program_出料前升降復位 = 65500;
            if (cnt_Program_出料前升降復位 > 1) cnt_Program_出料前升降復位_檢查放開(ref cnt_Program_出料前升降復位);

            if (cnt_Program_出料前升降復位 == 65500)
            {
                this.MyTimer_出料前升降復位_結束延遲.TickStop();
                this.MyTimer_出料前升降復位_結束延遲.StartTickTime(10000);
                PLC_Device_出料前升降復位.Bool = false;
                PLC_Device_出料前升降復位_馬達下降.Bool = false;
                PLC_Device_出料前升降復位_馬達上升.Bool = false;
                cnt_Program_出料前升降復位 = 65535;
            }
        }
        void cnt_Program_出料前升降復位_檢查按下(ref int cnt)
        {
            if (PLC_Device_出料前升降復位.Bool) cnt++;
        }
        void cnt_Program_出料前升降復位_檢查放開(ref int cnt)
        {
            if (!PLC_Device_出料前升降復位.Bool) cnt = 65500;
        }
        void cnt_Program_出料前升降復位_初始化(ref int cnt)
        {
            PLC_Device_出料前升降復位_OK.Bool = false;

            cnt++;
        }
        void cnt_Program_出料前升降復位_到達原點(ref int cnt)
        {
            PLC_Device_出料前升降復位_馬達上升.Bool = true;
            PLC_Device_出料前升降復位_馬達下降.Bool = false;
            if (this.PLC_Device_出料前升降復位_原點訊號.Bool == true)
            {
                cnt++;
            }
        }
        void cnt_Program_出料前升降復位_離開原點(ref int cnt)
        {
            PLC_Device_出料前升降復位_馬達上升.Bool = false;
            PLC_Device_出料前升降復位_馬達下降.Bool = true;
            if (this.PLC_Device_出料前升降復位_原點訊號.Bool == false)
            {
                PLC_Device_出料前升降復位_OK.Bool = true;

                cnt++;
            }
        }












        #endregion
        #region PLC_出料後升降復位
        PLC_Device PLC_Device_出料後升降復位 = new PLC_Device("M5401");
        PLC_Device PLC_Device_出料後升降復位_馬達上升 = new PLC_Device("M56");
        PLC_Device PLC_Device_出料後升降復位_馬達下降 = new PLC_Device("M57");
        PLC_Device PLC_Device_出料後升降復位_原點訊號 = new PLC_Device("X25");
        PLC_Device PLC_Device_出料後升降復位_OK = new PLC_Device("S3021");
        MyTimer MyTimer_出料後升降復位_結束延遲 = new MyTimer();
        MyTimer MyTimer_出料後升降復位_開始延遲 = new MyTimer();
        int cnt_Program_出料後升降復位 = 65534;
        void sub_Program_出料後升降復位()
        {
            if (cnt_Program_出料後升降復位 == 65534)
            {
                this.MyTimer_出料後升降復位_結束延遲.StartTickTime(10000);
                this.MyTimer_出料後升降復位_開始延遲.StartTickTime(10000);
                PLC_Device_出料後升降復位.SetComment("PLC_出料後升降復位");
                PLC_Device_出料後升降復位_OK.SetComment("PLC_出料後升降復位_OK");
                PLC_Device_出料後升降復位.Bool = false;
                cnt_Program_出料後升降復位 = 65535;
            }
            if (cnt_Program_出料後升降復位 == 65535) cnt_Program_出料後升降復位 = 1;
            if (cnt_Program_出料後升降復位 == 1) cnt_Program_出料後升降復位_檢查按下(ref cnt_Program_出料後升降復位);
            if (cnt_Program_出料後升降復位 == 2) cnt_Program_出料後升降復位_初始化(ref cnt_Program_出料後升降復位);
            if (cnt_Program_出料後升降復位 == 3) cnt_Program_出料後升降復位_到達原點(ref cnt_Program_出料後升降復位);
            if (cnt_Program_出料後升降復位 == 4) cnt_Program_出料後升降復位_離開原點(ref cnt_Program_出料後升降復位);
            if (cnt_Program_出料後升降復位 == 5) cnt_Program_出料後升降復位 = 65500;
            if (cnt_Program_出料後升降復位 > 1) cnt_Program_出料後升降復位_檢查放開(ref cnt_Program_出料後升降復位);

            if (cnt_Program_出料後升降復位 == 65500)
            {
                this.MyTimer_出料後升降復位_結束延遲.TickStop();
                this.MyTimer_出料後升降復位_結束延遲.StartTickTime(10000);
                PLC_Device_出料後升降復位.Bool = false;
                PLC_Device_出料後升降復位_馬達下降.Bool = false;
                PLC_Device_出料後升降復位_馬達上升.Bool = false;
                cnt_Program_出料後升降復位 = 65535;
            }
        }
        void cnt_Program_出料後升降復位_檢查按下(ref int cnt)
        {
            if (PLC_Device_出料後升降復位.Bool) cnt++;
        }
        void cnt_Program_出料後升降復位_檢查放開(ref int cnt)
        {
            if (!PLC_Device_出料後升降復位.Bool) cnt = 65500;
        }
        void cnt_Program_出料後升降復位_初始化(ref int cnt)
        {
            PLC_Device_出料後升降復位_OK.Bool = false;

            cnt++;
        }
        void cnt_Program_出料後升降復位_到達原點(ref int cnt)
        {
            PLC_Device_出料後升降復位_馬達上升.Bool = true;
            PLC_Device_出料後升降復位_馬達下降.Bool = false;
            if (this.PLC_Device_出料後升降復位_原點訊號.Bool == true)
            {
                cnt++;
            }
        }
        void cnt_Program_出料後升降復位_離開原點(ref int cnt)
        {
            PLC_Device_出料後升降復位_馬達上升.Bool = false;
            PLC_Device_出料後升降復位_馬達下降.Bool = true;
            if (this.PLC_Device_出料後升降復位_原點訊號.Bool == false)
            {
                PLC_Device_出料後升降復位_OK.Bool = true;

                cnt++;
            }
        }












        #endregion
    }
}
