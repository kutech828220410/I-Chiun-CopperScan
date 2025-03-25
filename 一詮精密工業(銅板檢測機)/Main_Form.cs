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
        public enum enum_AI_test_Type
        {
            [Description("DFP030-SGP")]
            GoldSize,
            [Description("DFP071-SGP")]
            DoorPoint,
        }
        double CCD軸_ratio = (6500D / 10000D);
        PLC_Device PLC_Device_CCD_軸卡現在位置 = new PLC_Device("D8350");
        PLC_Device PLC_Device_CCD_計算現在位置 = new PLC_Device("D15100");
        PLC_Device PLC_Device_CCD_原始目標位置 = new PLC_Device("D15122");
        PLC_Device PLC_Device_CCD_計算目標位置 = new PLC_Device("D15112");
        PLC_Device PLC_Device_CCD_計算目標位置完成 = new PLC_Device("S15134");

        PLC_Device PLC_Device_不良排除X_軸卡現在位置 = new PLC_Device("D8360");
        PLC_Device PLC_Device_不良排除X_計算現在位置 = new PLC_Device("D15200");
        PLC_Device PLC_Device_不良排除X_原始目標位置 = new PLC_Device("D15222");
        PLC_Device PLC_Device_不良排除X_計算目標位置 = new PLC_Device("D15212");
        PLC_Device PLC_Device_不良排除X_計算目標位置完成 = new PLC_Device("S15234");

        PLC_Device PLC_Device_不良排除Y_軸卡現在位置 = new PLC_Device("D8370");
        PLC_Device PLC_Device_不良排除Y_計算現在位置 = new PLC_Device("D15300");
        PLC_Device PLC_Device_不良排除Y_原始目標位置 = new PLC_Device("D15322");
        PLC_Device PLC_Device_不良排除Y_計算目標位置 = new PLC_Device("D15312");
        PLC_Device PLC_Device_不良排除Y_計算目標位置完成 = new PLC_Device("S15334");
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        #region DBConfigClass
        private static string DBConfigFileName = $@"{currentDirectory}\DBConfig.txt";
        static public DBConfigClass dBConfigClass = new DBConfigClass();
        public class DBConfigClass
        {
            private string metalMarkAI_url = "http://localhost:3005/MetalMarkAI";
            private SQL_DataGridView.ConnentionClass dB_Basic = new SQL_DataGridView.ConnentionClass();

            public SQL_DataGridView.ConnentionClass DB_Basic { get => dB_Basic; set => dB_Basic = value; }
            public string MetalMarkAI_url { get => metalMarkAI_url; set => metalMarkAI_url = value; }
        }
        private void LoadDBConfig()
        {

            string jsonstr = MyFileStream.LoadFileAllText($"{DBConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(new DBConfigClass(), true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }
                MyMessageBox.ShowDialog($"未建立參數文件!請至子目錄設定{DBConfigFileName}");
                Application.Exit();
            }
            else
            {
                dBConfigClass = Basic.Net.JsonDeserializet<DBConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(dBConfigClass, true);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($"{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }

            }
        }
        #endregion
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
            plC_ScreenPage_main.TabChangeEvent += PlC_ScreenPage_main_TabChangeEvent;
            this.plC_UI_Init1.Run(this, this.lowerMachine_Panel1);
            this.plC_UI_Init1.UI_Finished_Event += PlC_UI_Init1_UI_Finished_Event;
            LoadDBConfig();
        }

        private void PlC_UI_Init1_UI_Finished_Event()
        {
            PLC_UI_Init.Set_PLC_ScreenPage(panel_main, this.plC_ScreenPage_main);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_輸出入, this.plC_ScreenPage_輸出入);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_後台設定, this.plC_ScreenPage_後台設定);
            PLC_UI_Init.Set_PLC_ScreenPage(panel_軸控, this.plC_ScreenPage_軸控);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, this.FindForm());

            this.plC_UI_Init1.Add_Method(sub_PLC_program);
            PLC_入料盤系統_Init();
            PLC_輸送系統_Init();
            PLC_出料盤系統_Init();
            PLC_CCD測試_Init();

            Program_資料庫_Init();
            Program_主畫面_Init();


            Program_engineering_model_Init();
            PlC_ScreenPage_main_TabChangeEvent("主畫面");
            this.Refresh();
        }
        private void sub_PLC_program()
        {
            float CCD軸_ratio = (65F / 100F);
            PLC_Device_CCD_計算現在位置.Value = (int)Math.Round((PLC_Device_CCD_軸卡現在位置.Value * CCD軸_ratio));

            PLC_Device_CCD_計算目標位置.Value = (int)Math.Round((PLC_Device_CCD_原始目標位置.Value / CCD軸_ratio));
            PLC_Device_CCD_計算目標位置完成.Bool = true;

            float 不良排除X軸_ratio = (80F / 100F);
            PLC_Device_不良排除X_計算現在位置.Value = (int)Math.Round((PLC_Device_不良排除X_軸卡現在位置.Value * 不良排除X軸_ratio));

            PLC_Device_不良排除X_計算目標位置.Value = (int)Math.Round((PLC_Device_不良排除X_原始目標位置.Value / 不良排除X軸_ratio));
            PLC_Device_不良排除X_計算目標位置完成.Bool = true;

            float 不良排除Y軸_ratio = (625F / 100F);
            PLC_Device_不良排除Y_計算現在位置.Value = (int)Math.Round((PLC_Device_不良排除Y_軸卡現在位置.Value * 不良排除Y軸_ratio));

            PLC_Device_不良排除Y_計算目標位置.Value = (int)Math.Round((PLC_Device_不良排除Y_原始目標位置.Value / 不良排除Y軸_ratio));
            PLC_Device_不良排除Y_計算目標位置完成.Bool = true;
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
        private void PlC_ScreenPage_main_TabChangeEvent(string PageText)
        {
            if (PageText == "參數設定")
            {
                this.sqL_DataGridView_EngineeringModel.SQL_GetAllRows(true);
            }
            if (PageText == "主畫面")
            {
                string text = comboBox_主畫面_型號選擇.Text;
                List<object[]> list_value = this.sqL_DataGridView_EngineeringModel.SQL_GetAllRows(false);
                List<string> strs = (from temp in list_value
                                     select temp[(int)enum_engineering_model.型號名稱].ObjectToString()).Distinct().ToList();
                comboBox_主畫面_型號選擇.DataSource = strs;
                if (comboBox_主畫面_型號選擇.SelectedIndex != -1 && comboBox_主畫面_型號選擇.Items.Count > 0)
                {
                    if(text.StringIsEmpty())
                    {
                        comboBox_主畫面_型號選擇.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox_主畫面_型號選擇.SelectedItem = text;
                    }
                }
            }
        }
    }
}
