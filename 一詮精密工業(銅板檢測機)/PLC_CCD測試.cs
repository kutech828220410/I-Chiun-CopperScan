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
            rJ_Button_比例尺計算.MouseDownEvent += RJ_Button_比例尺計算_MouseDownEvent;
            plC_UI_Init1.Add_Method(sub_PLC_CCD測試);
        }

        private void RJ_Button_比例尺計算_MouseDownEvent(MouseEventArgs mevent)
        {
            double mm = plC_NumBox_比例尺計算_mm.Value / 100D;
            double pixcels = plC_NumBox_比例尺計算_pixcels.Value;

            plC_NumBox_比例尺_pixcel_mm.Value = (int)(pixcels / mm * 1000000);
            plC_NumBox_比例尺_mm_pixcel.Value = (int)(mm / pixcels * 1000000);

            MyMessageBox.ShowDialog("計算完成");
        }

        private void sub_PLC_CCD測試()
        {
            sub_Program_CCD測試一次();
            sub_Program_CCD軸到指定位置();
        }
        #region PLC_CCD測試一次

        List<MetalMarkAIPost> CCD_metalMarkAIPosts = new List<MetalMarkAIPost>();
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
            if (cnt_Program_CCD測試一次 == 12)
            {
                PLC_Device_CCD測試一次_OK.Bool = true;

                string text = comboBox_主畫面_型號選擇.GetComboBoxText();
                List<double> CCD_stage_position = new List<double>();
                CCD_stage_position.Add(0);
                CCD_stage_position.Add((plC_NumBox_CCD位置2.Value - plC_NumBox_CCD位置1.Value) / 100D);
                CCD_stage_position.Add((plC_NumBox_CCD位置3.Value - plC_NumBox_CCD位置2.Value) / 100D);
                CCD_stage_position.Add((plC_NumBox_CCD位置4.Value - plC_NumBox_CCD位置3.Value) / 100D);
                CCD_stage_position.Add((plC_NumBox_CCD位置5.Value - plC_NumBox_CCD位置4.Value) / 100D);
                double pixcel_to_mm = plC_NumBox_比例尺_mm_pixcel.Value / 1000000D;
                List<object[]> list_value = new List<object[]>();
                double org_ccd_poX = plC_NumBox_原點中心X_pixcel.Value;
                double org_ccd_poY = plC_NumBox_原點中心Y_pixcel.Value;
                for (int stage = 0; stage < CCD_metalMarkAIPosts.Count; stage++)
                {
                    for (int i = 0; i < CCD_metalMarkAIPosts[stage].Fails.Count; i++)
                    {
                        int po_X = CCD_metalMarkAIPosts[stage].Fails[i].Split(',')[0].StringToInt32();
                        int po_Y = CCD_metalMarkAIPosts[stage].Fails[i].Split(',')[1].StringToInt32();
                        if (text == enum_AI_test_Type.DoorPoint.GetEnumDescription())
                        {
                            //"DFP071-SGP"
                            if (stage == 2)
                            {
                                if (po_Y == 0) continue;
                                po_Y = po_Y - 1;
                            }
                            po_Y = po_Y + stage * 2;

                        }
                        if (text == enum_AI_test_Type.GoldSize.GetEnumDescription())
                        {
                            //"DFP030-SGP"
                            po_Y = po_Y + stage * 5;
                        }
                        if (CCD_metalMarkAIPosts[stage].Centers.ContainsKey(CCD_metalMarkAIPosts[stage].Fails[i]))
                        {
                            string str_center = CCD_metalMarkAIPosts[stage].Centers[CCD_metalMarkAIPosts[0].Fails[i]];
                            double center_x = (str_center.Split(',')[0].StringToDouble() - org_ccd_poX) * pixcel_to_mm;
                            double center_y = (str_center.Split(',')[1].StringToDouble() - org_ccd_poY) * pixcel_to_mm + CCD_stage_position[stage];


                            object[] value = new object[new enum_CCD_Fail_Result().GetLength()];
                            value[(int)enum_CCD_Fail_Result.GUID] = Guid.NewGuid().ToString();

                            value[(int)enum_CCD_Fail_Result.中心] = $"{center_x},{center_y}";
                            value[(int)enum_CCD_Fail_Result.已吸取] = "N";



                            list_value.Add(value);
                        }
                        else
                        {
                            Logger.Log("error", $"[{text == enum_AI_test_Type.DoorPoint.GetEnumDescription()}]({po_X},{po_Y}) fail point nonpair to center");
                        }
                    }
                }

                sqL_DataGridView_CCD_Fail_檢測結果.AddRows(list_value, true);


                cnt_Program_CCD測試一次 = 65500;
            }
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
            PLC_Device_CCD測試一次_OK.Bool = false;
            sqL_DataGridView_CCD_Fail_檢測結果.ClearGrid();
            CCD_metalMarkAIPosts.Clear();
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
            CCD_metalMarkAIPosts.Add(metalMarkAIPost);
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
            MetalMarkAIPost metalMarkAIPost = Function_測試一次(rJ_TextBox_主畫面_檢測別名.Text, 2);
            if (metalMarkAIPost.ResultImagePath == null)
            {
                MyMessageBox.ShowDialog("檢測失敗");
                cnt = 65500;
                return;
            }
            CCD_metalMarkAIPosts.Add(metalMarkAIPost);
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
            MetalMarkAIPost metalMarkAIPost = Function_測試一次(rJ_TextBox_主畫面_檢測別名.Text, 3);
            if (metalMarkAIPost.ResultImagePath == null)
            {
                MyMessageBox.ShowDialog("檢測失敗");
                cnt = 65500;
                return;
            }
            CCD_metalMarkAIPosts.Add(metalMarkAIPost);
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
