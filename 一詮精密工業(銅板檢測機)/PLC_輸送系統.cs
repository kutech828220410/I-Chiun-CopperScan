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
        private void PLC_輸送系統_Init()
        {
            plC_UI_Init1.Add_Method(sub_PLC_輸送系統);
        }

      
        private void sub_PLC_輸送系統()
        {
            sub_Program_輸送系統_輸送一格();
        }

        #region PLC_輸送系統_輸送一格
        PLC_Device PLC_Device_輸送系統_輸送一格 = new PLC_Device("M5500");
        PLC_Device PLC_Device_輸送系統_輸送帶正轉 = new PLC_Device("M46");
        PLC_Device PLC_Device_輸送系統_輸送帶解煞車 = new PLC_Device("M45");
        PLC_Device PLC_Device_輸送系統_輸送帶入盤區有盤 = new PLC_Device("S3000");
        PLC_Device PLC_Device_輸送系統_輸送帶CCD區有盤 = new PLC_Device("S3001");
        PLC_Device PLC_Device_輸送系統_輸送帶不良排除區有盤 = new PLC_Device("S3002");
        PLC_Device PLC_Device_輸送系統_輸送帶出盤區有盤 = new PLC_Device("S3003");

        PLC_Device PLC_Device_輸送系統_輸送帶CCD_OK_1 = new PLC_Device("S3011");
        PLC_Device PLC_Device_輸送系統_輸送帶CCD_OK_2 = new PLC_Device("S3012");
        PLC_Device PLC_Device_輸送系統_輸送帶CCD_OK_3 = new PLC_Device("S3013");


        PLC_Device PLC_Device_輸送系統_有無料DATA = new PLC_Device("R3000");
        PLC_Device PLC_Device_輸送系統_CCD_OK_DATA = new PLC_Device("R3001");

        PLC_Device PLC_Device_輸送系統_輸送帶入盤區無盤 = new PLC_Device();
        PLC_Device PLC_Device_輸送系統_輸送帶CCD區無盤 = new PLC_Device();
        PLC_Device PLC_Device_輸送系統_輸送帶不良排除區無盤 = new PLC_Device();
        PLC_Device PLC_Device_輸送系統_輸送帶出盤區無盤 = new PLC_Device();

        PLC_Device PLC_Device_輸送系統_輸送帶有盤延遲 = new PLC_Device("D1501");
        PLC_Device PLC_Device_輸送系統_輸送帶無盤延遲 = new PLC_Device("D1502");
        PLC_Device PLC_Device_輸送系統_輸送帶輸出時間 = new PLC_Device("D1503");
        MyTimer MyTimer_輸送系統_計時 = new MyTimer();

        MyTimer MyTimer_輸送系統_計時輸出 = new MyTimer();

        MyTimer MyTimer_輸送系統_輸送帶入盤區有盤延遲 = new MyTimer();
        MyTimer MyTimer_輸送系統_輸送帶CCD區有盤延遲 = new MyTimer();
        MyTimer MyTimer_輸送系統_輸送帶不良排除區有盤延遲 = new MyTimer();
        MyTimer MyTimer_輸送系統_輸送帶出盤區有盤延遲 = new MyTimer();

        MyTimer MyTimer_輸送系統_輸送帶入盤區無盤延遲 = new MyTimer();
        MyTimer MyTimer_輸送系統_輸送帶CCD區無盤延遲 = new MyTimer();
        MyTimer MyTimer_輸送系統_輸送帶不良排除區無盤延遲 = new MyTimer();
        MyTimer MyTimer_輸送系統_輸送帶出盤區無盤延遲 = new MyTimer();


        bool flag_輸送系統_輸送帶入盤區有盤 = false;
        bool flag_輸送系統_輸送帶CCD區有盤 = false;
        bool flag_輸送系統_輸送帶不良排除區有盤 = false;
        bool flag_輸送系統_輸送帶出盤區有盤 = false;

        bool flag_輸送系統_輸送帶入盤區無盤 = false;
        bool flag_輸送系統_輸送帶CCD區無盤 = false;
        bool flag_輸送系統_輸送帶不良排除區無盤 = false;
        bool flag_輸送系統_輸送帶出盤區無盤 = false;
        PLC_Device PLC_Device_輸送系統_輸送一格_OK = new PLC_Device("");
        int cnt_Program_輸送系統_輸送一格 = 65534;
        void sub_Program_輸送系統_輸送一格()
        {
            PLC_Device_輸送系統_輸送帶入盤區無盤.Bool = !PLC_Device_輸送系統_輸送帶入盤區有盤.Bool;
            PLC_Device_輸送系統_輸送帶CCD區無盤.Bool = !PLC_Device_輸送系統_輸送帶CCD區有盤.Bool;
            PLC_Device_輸送系統_輸送帶不良排除區無盤.Bool = !PLC_Device_輸送系統_輸送帶不良排除區有盤.Bool;
            PLC_Device_輸送系統_輸送帶出盤區無盤.Bool = !PLC_Device_輸送系統_輸送帶出盤區有盤.Bool;


            if (PLC_Device_輸送系統_輸送帶入盤區有盤.Bool)
            {
                if(MyTimer_輸送系統_輸送帶入盤區有盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶入盤區有盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶入盤區有盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶入盤區有盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶有盤延遲.Value);
                flag_輸送系統_輸送帶入盤區有盤 = false;
            }
            if (PLC_Device_輸送系統_輸送帶CCD區有盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶CCD區有盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶CCD區有盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶CCD區有盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶CCD區有盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶有盤延遲.Value);
                flag_輸送系統_輸送帶CCD區有盤 = false;
            }
            if (PLC_Device_輸送系統_輸送帶不良排除區有盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶不良排除區有盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶不良排除區有盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶不良排除區有盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶不良排除區有盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶有盤延遲.Value);
                flag_輸送系統_輸送帶不良排除區有盤 = false;
            }
            if (PLC_Device_輸送系統_輸送帶出盤區有盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶出盤區有盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶出盤區有盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶出盤區有盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶出盤區有盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶有盤延遲.Value);
                flag_輸送系統_輸送帶出盤區有盤 = false;
            }

            if (PLC_Device_輸送系統_輸送帶入盤區無盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶入盤區無盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶入盤區無盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶入盤區無盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶入盤區無盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶無盤延遲.Value);
                flag_輸送系統_輸送帶入盤區無盤 = false;
            }
            if (PLC_Device_輸送系統_輸送帶CCD區無盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶CCD區無盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶CCD區無盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶CCD區無盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶CCD區無盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶無盤延遲.Value);
                flag_輸送系統_輸送帶CCD區無盤 = false;
            }
            if (PLC_Device_輸送系統_輸送帶不良排除區無盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶不良排除區無盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶不良排除區無盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶不良排除區無盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶不良排除區無盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶無盤延遲.Value);
                flag_輸送系統_輸送帶不良排除區無盤 = false;
            }
            if (PLC_Device_輸送系統_輸送帶出盤區無盤.Bool)
            {
                if (MyTimer_輸送系統_輸送帶出盤區無盤延遲.IsTimeOut())
                {
                    flag_輸送系統_輸送帶出盤區無盤 = true;
                }
            }
            else
            {
                MyTimer_輸送系統_輸送帶出盤區無盤延遲.TickStop();
                MyTimer_輸送系統_輸送帶出盤區無盤延遲.StartTickTime(PLC_Device_輸送系統_輸送帶無盤延遲.Value);
                flag_輸送系統_輸送帶出盤區無盤 = false;
            }

            if (cnt_Program_輸送系統_輸送一格 == 65534)
            {

                PLC_Device_輸送系統_輸送一格.SetComment("PLC_輸送系統_輸送一格");
                PLC_Device_輸送系統_輸送一格_OK.SetComment("PLC_輸送系統_輸送一格_OK");
                PLC_Device_輸送系統_輸送一格.Bool = false;
                cnt_Program_輸送系統_輸送一格 = 65535;
            }
            if (cnt_Program_輸送系統_輸送一格 == 65535) cnt_Program_輸送系統_輸送一格 = 1;
            if (cnt_Program_輸送系統_輸送一格 == 1) cnt_Program_輸送系統_輸送一格_檢查按下(ref cnt_Program_輸送系統_輸送一格);
            if (cnt_Program_輸送系統_輸送一格 == 2) cnt_Program_輸送系統_輸送一格_初始化(ref cnt_Program_輸送系統_輸送一格);

            //if (cnt_Program_輸送系統_輸送一格 == 100) cnt_Program_輸送系統_輸送一格_100_等待CCD區盤感應離開(ref cnt_Program_輸送系統_輸送一格);
            //if (cnt_Program_輸送系統_輸送一格 == 101) cnt_Program_輸送系統_輸送一格_100_等待CCD區盤感應到達(ref cnt_Program_輸送系統_輸送一格);
            //if (cnt_Program_輸送系統_輸送一格 == 102) cnt_Program_輸送系統_輸送一格 = 65400;

            //if (cnt_Program_輸送系統_輸送一格 == 200) cnt_Program_輸送系統_輸送一格_200_等待不良排除區盤感應離開(ref cnt_Program_輸送系統_輸送一格);
            //if (cnt_Program_輸送系統_輸送一格 == 201) cnt_Program_輸送系統_輸送一格_200_等待不良排除區盤感應到達(ref cnt_Program_輸送系統_輸送一格);
            //if (cnt_Program_輸送系統_輸送一格 == 202) cnt_Program_輸送系統_輸送一格 = 65400;

            //if (cnt_Program_輸送系統_輸送一格 == 300) cnt_Program_輸送系統_輸送一格_300_等待出盤區盤感應離開(ref cnt_Program_輸送系統_輸送一格);
            //if (cnt_Program_輸送系統_輸送一格 == 301) cnt_Program_輸送系統_輸送一格_300_等待出盤區盤感應到達(ref cnt_Program_輸送系統_輸送一格);
            //if (cnt_Program_輸送系統_輸送一格 == 302) cnt_Program_輸送系統_輸送一格 = 65400;

            if (cnt_Program_輸送系統_輸送一格 == 400) cnt_Program_輸送系統_輸送一格_400_計時輸出(ref cnt_Program_輸送系統_輸送一格);
            if (cnt_Program_輸送系統_輸送一格 == 401) cnt_Program_輸送系統_輸送一格 = 65400;

            if (cnt_Program_輸送系統_輸送一格 > 1) cnt_Program_輸送系統_輸送一格_檢查放開(ref cnt_Program_輸送系統_輸送一格);
            if (cnt_Program_輸送系統_輸送一格 == 65400)
            {
                int temp0 = PLC_Device_輸送系統_有無料DATA.Value * 2;

                PLC_Device_輸送系統_輸送帶入盤區有盤.Bool = ((temp0 >> 0) % 2 == 1);
                PLC_Device_輸送系統_輸送帶CCD區有盤.Bool = ((temp0 >> 1) % 2 == 1);
                PLC_Device_輸送系統_輸送帶不良排除區有盤.Bool = ((temp0 >> 2) % 2 == 1);
                PLC_Device_輸送系統_輸送帶出盤區有盤.Bool = ((temp0 >> 3) % 2 == 1);

                int temp1 = PLC_Device_輸送系統_CCD_OK_DATA.Value * 2;

                PLC_Device_輸送系統_輸送帶CCD_OK_1.Bool = ((temp1 >> 0) % 2 == 1);
                PLC_Device_輸送系統_輸送帶CCD_OK_2.Bool = ((temp1 >> 1) % 2 == 1);
                PLC_Device_輸送系統_輸送帶CCD_OK_3.Bool = ((temp1 >> 2) % 2 == 1);

                cnt_Program_輸送系統_輸送一格 = 65500;
            }
            if (cnt_Program_輸送系統_輸送一格 == 65500)
            {
                PLC_Device_輸送系統_輸送一格.Bool = false;
                PLC_Device_輸送系統_輸送一格_OK.Bool = false;
                PLC_Device_輸送系統_輸送帶解煞車.Bool = false;
                PLC_Device_輸送系統_輸送帶正轉.Bool = false;
                cnt_Program_輸送系統_輸送一格 = 65535;
                Console.WriteLine($"[輸送一格] 計時 : {MyTimer_輸送系統_計時}");
            }
        }
        void cnt_Program_輸送系統_輸送一格_檢查按下(ref int cnt)
        {
            if (PLC_Device_輸送系統_輸送一格.Bool) cnt++;
        }
        void cnt_Program_輸送系統_輸送一格_檢查放開(ref int cnt)
        {
            if (!PLC_Device_輸送系統_輸送一格.Bool) cnt = 65500;
        }
        void cnt_Program_輸送系統_輸送一格_初始化(ref int cnt)
        {
            MyTimer_輸送系統_計時.TickStop();
            MyTimer_輸送系統_計時.StartTickTime(500000);
            MyTimer_輸送系統_計時輸出.TickStop();
            MyTimer_輸送系統_計時輸出.StartTickTime(plC_NumBox_輸送一格時間.Value);
            PLC_Device_輸送系統_輸送帶解煞車.Bool = true;
            PLC_Device_輸送系統_輸送帶正轉.Bool = true;
            cnt = 400;
            return;

            //if (flag_輸送系統_輸送帶入盤區有盤 == true)
            //{
            //    Console.WriteLine($"[輸送一格] 入盤區有盤,等待CCD區盤感應到達");
            //    PLC_Device_輸送系統_輸送帶解煞車.Bool = true;
            //    PLC_Device_輸送系統_輸送帶正轉.Bool = true;
            //    cnt = 100;
            //    return;
            //}
            //else if(flag_輸送系統_輸送帶入盤區有盤 == false)
            //{
            //    if (flag_輸送系統_輸送帶CCD區有盤 == true)
            //    {
            //        Console.WriteLine($"[輸送一格] 入盤區無料,CCD區有盤,等待不良排除區盤感應到達");
            //        PLC_Device_輸送系統_輸送帶解煞車.Bool = true;
            //        PLC_Device_輸送系統_輸送帶正轉.Bool = true;
            //        cnt = 200;
            //        return;
            //    }
            //    else if (flag_輸送系統_輸送帶CCD區有盤 == false)
            //    {
            //        if(flag_輸送系統_輸送帶不良排除區有盤 == true)
            //        {
            //            Console.WriteLine($"[輸送一格] 入盤區無料,CCD區無料,不良排除區有盤,等待出盤區盤感應到達");
            //            PLC_Device_輸送系統_輸送帶解煞車.Bool = true;
            //            PLC_Device_輸送系統_輸送帶正轉.Bool = true;
            //            cnt = 300;
            //            return;
            //        }
            //        else if(flag_輸送系統_輸送帶不良排除區有盤 == false)
            //        {
            //            Console.WriteLine($"[輸送一格] 入盤區無料,CCD區無料,不良排除區無料,等待出盤區盤感應到達");
            //            PLC_Device_輸送系統_輸送帶解煞車.Bool = true;
            //            PLC_Device_輸送系統_輸送帶正轉.Bool = true;
            //            cnt = 300;
            //            return;
            //        }
            //    }              
            //}
            //Console.WriteLine($"[輸送一格] 無效訊號結束");

            cnt = 65500;
        }
        void cnt_Program_輸送系統_輸送一格_100_等待CCD區盤感應離開(ref int cnt)
        {
            if(flag_輸送系統_輸送帶CCD區無盤)
            {
                cnt++;
            }         
        }
        void cnt_Program_輸送系統_輸送一格_100_等待CCD區盤感應到達(ref int cnt)
        {
            if (flag_輸送系統_輸送帶CCD區有盤)
            {
                cnt++;
            }
        }

        void cnt_Program_輸送系統_輸送一格_200_等待不良排除區盤感應離開(ref int cnt)
        {
            if (flag_輸送系統_輸送帶不良排除區無盤)
            {
                cnt++;
            }
        }
        void cnt_Program_輸送系統_輸送一格_200_等待不良排除區盤感應到達(ref int cnt)
        {
            if (flag_輸送系統_輸送帶不良排除區有盤)
            {
                cnt++;
            }
        }

        void cnt_Program_輸送系統_輸送一格_300_等待出盤區盤感應離開(ref int cnt)
        {
            if (flag_輸送系統_輸送帶出盤區無盤 || true)
            {
                cnt++;
            }
        }
        void cnt_Program_輸送系統_輸送一格_300_等待出盤區盤感應到達(ref int cnt)
        {
            if (flag_輸送系統_輸送帶出盤區有盤)
            {
                cnt++;
            }
        }

        void cnt_Program_輸送系統_輸送一格_400_計時輸出(ref int cnt)
        {
            if (MyTimer_輸送系統_計時輸出.IsTimeOut())
            {
                List<object[]> list_value = sqL_DataGridView_CCD_Fail_檢測結果.GetAllRows();
                sqL_DataGridView_CCD_Fail_檢測結果.ClearGrid();
                sqL_DataGridView_不良排除狀態.RefreshGrid(list_value);
                cnt++;
            }
        }

        #endregion
    }

}
