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

            this.Refresh();
        }

        private void Main_Form_LoadFinishedEvent(EventArgs e)
        {
            
        }

   
    }
}
