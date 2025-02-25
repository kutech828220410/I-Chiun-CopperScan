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
        public Main_Form()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Load += Main_Form_Load;
            this.MaximumSizeChanged += Main_Form_MaximumSizeChanged;
        }

        private void Main_Form_MaximumSizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            MyMessageBox.form = this.FindForm();
            Basic.MyMessageBox.音效 = false;
             
            this.plC_UI_Init1.Run(this, this.lowerMachine_Panel1);
            this.plC_UI_Init1.UI_Finished_Event += PlC_UI_Init1_UI_Finished_Event;
         
        }

        private void PlC_UI_Init1_UI_Finished_Event()
        {
            PLC_UI_Init.Set_PLC_ScreenPage(panel_main, this.plC_ScreenPage_main);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_輸出入, this.plC_ScreenPage_輸出入);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_後台設定, this.plC_ScreenPage_後台設定);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_軸控, this.plC_ScreenPage_軸控);

            PLC_入料盤系統_Init();
            PLC_輸送系統_Init();
            PLC_出料盤系統_Init();
            this.Refresh();
        }
        #region PLC_Method
        PLC_Device PLC_Device_Method = new PLC_Device("");
        PLC_Device PLC_Device_Method_OK = new PLC_Device("");
        int cnt_Program_Method = 65534;
        void sub_Program_Method()
        {
            if (cnt_Program_Method == 65534)
            {
      
                PLC_Device_Method.SetComment("PLC_Method");
                PLC_Device_Method_OK.SetComment("PLC_Method_OK");
                PLC_Device_Method.Bool = false;
                cnt_Program_Method = 65535;
            }
            if (cnt_Program_Method == 65535) cnt_Program_Method = 1;
            if (cnt_Program_Method == 1) cnt_Program_Method_檢查按下(ref cnt_Program_Method);
            if (cnt_Program_Method == 2) cnt_Program_Method_初始化(ref cnt_Program_Method);
            if (cnt_Program_Method == 3) cnt_Program_Method = 65500;
            if (cnt_Program_Method > 1) cnt_Program_Method_檢查放開(ref cnt_Program_Method);

            if (cnt_Program_Method == 65500)
            {
                PLC_Device_Method.Bool = false;
                PLC_Device_Method_OK.Bool = false;
                cnt_Program_Method = 65535;
            }
        }
        void cnt_Program_Method_檢查按下(ref int cnt)
        {
            if (PLC_Device_Method.Bool) cnt++;
        }
        void cnt_Program_Method_檢查放開(ref int cnt)
        {
            if (!PLC_Device_Method.Bool) cnt = 65500;
        }
        void cnt_Program_Method_初始化(ref int cnt)
        {
            cnt++;
        }














        #endregion

        private void plC_ScreenPage_輸出入_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
