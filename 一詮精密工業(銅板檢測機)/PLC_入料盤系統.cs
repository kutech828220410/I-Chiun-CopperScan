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
        static public double 入料區升降馬達_換算比 = 0.013;
        private void PLC_入料盤系統_Init()
        {                

            plC_UI_Init1.Add_Method(sub_PLC_入料盤系統);
        }
        private void sub_PLC_入料盤系統()
        {
            if (plC_NumBox_入料區升降馬達設定盤數.Value < 1) plC_NumBox_入料區升降馬達設定盤數.Value = 1;
            if (plC_NumBox_入料區升降馬達現在盤數.Value < 1) plC_NumBox_入料區升降馬達現在盤數.Value = 1;
            //if (plC_NumBox_入料區升降馬達現在盤數.Value > plC_NumBox_入料區升降馬達設定盤數.Value) plC_NumBox_入料區升降馬達現在盤數.Value = plC_NumBox_入料區升降馬達設定盤數.Value;
            if (plC_NumBox_入料區升降馬達總盤高.Value > 0)
            {
                plC_NumBox_入料區升降馬達_單盤高.Value = plC_NumBox_入料區升降馬達總盤高.Value / plC_NumBox_入料區升降馬達設定盤數.Value;
            }
            sub_Program_入料升降回原點();
            sub_Program_入料升降到初始高度();
            sub_Program_入料升降到盤吸取位();
        }
        #region PLC_入料升降回原點
        PLC_Device PLC_Device_入料升降回原點 = new PLC_Device("M5000");
        PLC_Device PLC_Device_入料升降回原點_馬達上升 = new PLC_Device("M52");
        PLC_Device PLC_Device_入料升降回原點_馬達下降 = new PLC_Device("M53");
        PLC_Device PLC_Device_入料升降回原點_原點訊號 = new PLC_Device("T100");
        PLC_Device PLC_Device_入料升降回原點_OK = new PLC_Device("");
        MyTimer MyTimer_入料升降回原點_結束延遲 = new MyTimer();
        MyTimer MyTimer_入料升降回原點_開始延遲 = new MyTimer();
        int cnt_Program_入料升降回原點 = 65534;
        void sub_Program_入料升降回原點()
        {
            if (cnt_Program_入料升降回原點 == 65534)
            {
                this.MyTimer_入料升降回原點_結束延遲.StartTickTime(10000);
                this.MyTimer_入料升降回原點_開始延遲.StartTickTime(10000);
                PLC_Device_入料升降回原點.SetComment("PLC_入料升降回原點");
                PLC_Device_入料升降回原點_OK.SetComment("PLC_入料升降回原點_OK");
                PLC_Device_入料升降回原點.Bool = false;
                cnt_Program_入料升降回原點 = 65535;
            }
            if (cnt_Program_入料升降回原點 == 65535) cnt_Program_入料升降回原點 = 1;
            if (cnt_Program_入料升降回原點 == 1) cnt_Program_入料升降回原點_檢查按下(ref cnt_Program_入料升降回原點);
            if (cnt_Program_入料升降回原點 == 2) cnt_Program_入料升降回原點_初始化(ref cnt_Program_入料升降回原點);
            if (cnt_Program_入料升降回原點 == 3) cnt_Program_入料升降回原點_離開原點(ref cnt_Program_入料升降回原點);
            if (cnt_Program_入料升降回原點 == 4) cnt_Program_入料升降回原點_靠近原點(ref cnt_Program_入料升降回原點);
            if (cnt_Program_入料升降回原點 == 5) cnt_Program_入料升降回原點 = 65500;
            if (cnt_Program_入料升降回原點 > 1) cnt_Program_入料升降回原點_檢查放開(ref cnt_Program_入料升降回原點);

            if (cnt_Program_入料升降回原點 == 65500)
            {
                this.MyTimer_入料升降回原點_結束延遲.TickStop();
                this.MyTimer_入料升降回原點_結束延遲.StartTickTime(10000);
                PLC_Device_入料升降回原點.Bool = false;
                PLC_Device_入料升降回原點_OK.Bool = false;
                PLC_Device_入料升降回原點_馬達下降.Bool = false;
                PLC_Device_入料升降回原點_馬達上升.Bool = false;
                cnt_Program_入料升降回原點 = 65535;
            }
        }
        void cnt_Program_入料升降回原點_檢查按下(ref int cnt)
        {
            if (PLC_Device_入料升降回原點.Bool) cnt++;
        }
        void cnt_Program_入料升降回原點_檢查放開(ref int cnt)
        {
            if (!PLC_Device_入料升降回原點.Bool) cnt = 65500;
        }
        void cnt_Program_入料升降回原點_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_入料升降回原點_離開原點(ref int cnt)
        {
            PLC_Device_入料升降回原點_馬達上升.Bool = true;
            PLC_Device_入料升降回原點_馬達下降.Bool = false;
            if (this.PLC_Device_入料升降回原點_原點訊號.Bool == false)
            {
                cnt++;
            }
        }
        void cnt_Program_入料升降回原點_靠近原點(ref int cnt)
        {
            PLC_Device_入料升降回原點_馬達上升.Bool = false;
            PLC_Device_入料升降回原點_馬達下降.Bool = true;
            if (this.PLC_Device_入料升降回原點_原點訊號.Bool)
            {
                cnt++;
            }
        }












        #endregion
        #region PLC_入料升降到初始高度
        PLC_Device PLC_Device_入料升降到初始高度_馬達上升 = new PLC_Device("M52");
        PLC_Device PLC_Device_入料升降到初始高度_馬達下降 = new PLC_Device("M53");
        PLC_Device PLC_Device_入料升降到初始高度 = new PLC_Device("M5001");
        PLC_Device PLC_Device_入料升降到初始高度_OK = new PLC_Device("");
        MyTimer MyTimer_入料升降到初始高度_初始高度計時 = new MyTimer();
        MyTimer MyTimer_入料升降到初始高度_結束延遲 = new MyTimer();
        MyTimer MyTimer_入料升降到初始高度_開始延遲 = new MyTimer();
        int cnt_Program_入料升降到初始高度 = 65534;
        void sub_Program_入料升降到初始高度()
        {
            if (cnt_Program_入料升降到初始高度 == 65534)
            {
                this.MyTimer_入料升降到初始高度_結束延遲.StartTickTime(10000);
                this.MyTimer_入料升降到初始高度_開始延遲.StartTickTime(10000);
                PLC_Device_入料升降到初始高度.SetComment("PLC_入料升降到初始高度");
                PLC_Device_入料升降到初始高度_OK.SetComment("PLC_入料升降到初始高度_OK");
                PLC_Device_入料升降到初始高度.Bool = false;
                cnt_Program_入料升降到初始高度 = 65535;
            }
            if (cnt_Program_入料升降到初始高度 == 65535) cnt_Program_入料升降到初始高度 = 1;
            if (cnt_Program_入料升降到初始高度 == 1) cnt_Program_入料升降到初始高度_檢查按下(ref cnt_Program_入料升降到初始高度);
            if (cnt_Program_入料升降到初始高度 == 2) cnt_Program_入料升降到初始高度_初始化(ref cnt_Program_入料升降到初始高度);
            if (cnt_Program_入料升降到初始高度 == 3) cnt_Program_入料升降到初始高度_等待回原點待命(ref cnt_Program_入料升降到初始高度);
            if (cnt_Program_入料升降到初始高度 == 4) cnt_Program_入料升降到初始高度_等待回原點完成(ref cnt_Program_入料升降到初始高度);
            if (cnt_Program_入料升降到初始高度 == 5) cnt_Program_入料升降到初始高度_馬達輸出(ref cnt_Program_入料升降到初始高度);
            if (cnt_Program_入料升降到初始高度 == 6) cnt_Program_入料升降到初始高度_等待輸出馬達完成(ref cnt_Program_入料升降到初始高度);
            if (cnt_Program_入料升降到初始高度 == 7) cnt_Program_入料升降到初始高度 = 65500;
            if (cnt_Program_入料升降到初始高度 > 1) cnt_Program_入料升降到初始高度_檢查放開(ref cnt_Program_入料升降到初始高度);

            if (cnt_Program_入料升降到初始高度 == 65500)
            {
                this.MyTimer_入料升降到初始高度_結束延遲.TickStop();
                this.MyTimer_入料升降到初始高度_結束延遲.StartTickTime(10000);
                PLC_Device_入料升降到初始高度.Bool = false;
                PLC_Device_入料升降到初始高度_OK.Bool = false;
                PLC_Device_入料升降回原點.Bool = false;
                PLC_Device_入料升降到初始高度_馬達上升.Bool = false;
                PLC_Device_入料升降到初始高度_馬達下降.Bool = false;
                cnt_Program_入料升降到初始高度 = 65535;
            }
        }
        void cnt_Program_入料升降到初始高度_檢查按下(ref int cnt)
        {
            if (PLC_Device_入料升降到初始高度.Bool) cnt++;
        }
        void cnt_Program_入料升降到初始高度_檢查放開(ref int cnt)
        {
            if (!PLC_Device_入料升降到初始高度.Bool) cnt = 65500;
        }
        void cnt_Program_入料升降到初始高度_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_入料升降到初始高度_等待回原點待命(ref int cnt)
        {
            if (PLC_Device_入料升降回原點.Bool == false)
            {
                PLC_Device_入料升降回原點.Bool = true;
                cnt++;
            }
        }
        void cnt_Program_入料升降到初始高度_等待回原點完成(ref int cnt)
        {
            if (PLC_Device_入料升降回原點.Bool == false)
            {
                cnt++;
            }
        }
        void cnt_Program_入料升降到初始高度_馬達輸出(ref int cnt)
        {
            int temp = (int)((plC_NumBox_入料升降馬達_初始高度.Value / 100) / 入料區升降馬達_換算比);
            PLC_Device_入料升降到初始高度_馬達上升.Bool = true;
            PLC_Device_入料升降到初始高度_馬達下降.Bool = false;
            MyTimer_入料升降到初始高度_初始高度計時.TickStop();

            MyTimer_入料升降到初始高度_初始高度計時.StartTickTime(temp);
            cnt++;
        }
        void cnt_Program_入料升降到初始高度_等待輸出馬達完成(ref int cnt)
        {
            if(MyTimer_入料升降到初始高度_初始高度計時.IsTimeOut())
            {
                PLC_Device_入料升降到初始高度_馬達上升.Bool = false;
                PLC_Device_入料升降到初始高度_馬達下降.Bool = false;

                cnt++;
            }
           
        }












        #endregion
        #region PLC_入料升降到盤吸取位
        PLC_Device PLC_Device_入料升降到盤吸取位 = new PLC_Device("M5002");
        PLC_Device PLC_Device_入料升降到盤吸取位_OK = new PLC_Device("M5002");
        int cnt_Program_入料升降到盤吸取位 = 65534;
        void sub_Program_入料升降到盤吸取位()
        {
            if (cnt_Program_入料升降到盤吸取位 == 65534)
            {

                PLC_Device_入料升降到盤吸取位.SetComment("PLC_入料升降到盤吸取位");
                PLC_Device_入料升降到盤吸取位_OK.SetComment("PLC_入料升降到盤吸取位_OK");
                PLC_Device_入料升降到盤吸取位.Bool = false;
                cnt_Program_入料升降到盤吸取位 = 65535;
            }
            if (cnt_Program_入料升降到盤吸取位 == 65535) cnt_Program_入料升降到盤吸取位 = 1;
            if (cnt_Program_入料升降到盤吸取位 == 1) cnt_Program_入料升降到盤吸取位_檢查按下(ref cnt_Program_入料升降到盤吸取位);
            if (cnt_Program_入料升降到盤吸取位 == 2) cnt_Program_入料升降到盤吸取位_初始化(ref cnt_Program_入料升降到盤吸取位);
            if (cnt_Program_入料升降到盤吸取位 == 3) cnt_Program_入料升降到盤吸取_等待回原點待命(ref cnt_Program_入料升降到盤吸取位);
            if (cnt_Program_入料升降到盤吸取位 == 4) cnt_Program_入料升降到盤吸取_等待回原點完成(ref cnt_Program_入料升降到盤吸取位);
            if (cnt_Program_入料升降到盤吸取位 == 5) cnt_Program_入料升降到盤吸取_馬達輸出(ref cnt_Program_入料升降到盤吸取位);
            if (cnt_Program_入料升降到盤吸取位 == 6) cnt_Program_入料升降到盤吸取_等待輸出馬達完成(ref cnt_Program_入料升降到盤吸取位);
            if (cnt_Program_入料升降到盤吸取位 == 7) cnt_Program_入料升降到盤吸取位 = 65500;
            if (cnt_Program_入料升降到盤吸取位 > 1) cnt_Program_入料升降到盤吸取位_檢查放開(ref cnt_Program_入料升降到盤吸取位);

            if (cnt_Program_入料升降到盤吸取位 == 65500)
            {
                PLC_Device_入料升降到盤吸取位.Bool = false;
                PLC_Device_入料升降到盤吸取位_OK.Bool = false;
                PLC_Device_入料升降回原點.Bool = false;
                PLC_Device_入料升降到初始高度_馬達上升.Bool = false;
                PLC_Device_入料升降到初始高度_馬達下降.Bool = false;
                cnt_Program_入料升降到盤吸取位 = 65535;
            }
        }
        void cnt_Program_入料升降到盤吸取位_檢查按下(ref int cnt)
        {
            if (PLC_Device_入料升降到盤吸取位.Bool) cnt++;
        }
        void cnt_Program_入料升降到盤吸取位_檢查放開(ref int cnt)
        {
            if (!PLC_Device_入料升降到盤吸取位.Bool) cnt = 65500;
        }
        void cnt_Program_入料升降到盤吸取位_初始化(ref int cnt)
        {
            cnt++;
        }
        void cnt_Program_入料升降到盤吸取_等待回原點待命(ref int cnt)
        {
            if (PLC_Device_入料升降回原點.Bool == false)
            {
                PLC_Device_入料升降回原點.Bool = true;
                cnt++;
            }
        }
        void cnt_Program_入料升降到盤吸取_等待回原點完成(ref int cnt)
        {
            if (PLC_Device_入料升降回原點.Bool == false)
            {
                cnt++;
            }
        }
        void cnt_Program_入料升降到盤吸取_馬達輸出(ref int cnt)
        {
            double 初始高度 = (double)(plC_NumBox_入料升降馬達_初始高度.Value / 100);
            double 盤高偏移 = (5000 - plC_NumBox_入料區升降馬達總盤高.Value) / 100;
            double 盤高度 = (double)(((plC_NumBox_入料區升降馬達現在盤數.Value  - 1) * (plC_NumBox_入料區升降馬達_單盤高.Value)) / 100);
            int temp = (int)((初始高度 + 盤高度 + 盤高偏移) / 入料區升降馬達_換算比);
            PLC_Device_入料升降到初始高度_馬達上升.Bool = true;
            PLC_Device_入料升降到初始高度_馬達下降.Bool = false;
            MyTimer_入料升降到初始高度_初始高度計時.TickStop();

            MyTimer_入料升降到初始高度_初始高度計時.StartTickTime(temp);
            cnt++;
        }
        void cnt_Program_入料升降到盤吸取_等待輸出馬達完成(ref int cnt)
        {
            if (MyTimer_入料升降到初始高度_初始高度計時.IsTimeOut())
            {
                PLC_Device_入料升降到初始高度_馬達上升.Bool = false;
                PLC_Device_入料升降到初始高度_馬達下降.Bool = false;

                cnt++;
            }

        }













        #endregion
    }


}
