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
        public class MetalMarkAIPost
        {         /// <summary>
                  /// 座標中心點集合
                  /// </summary>
            [JsonPropertyName("centers")]
            public Dictionary<string, string> Centers { get; set; }
            /// <summary>
            /// 失敗座標集合
            /// </summary>
            [JsonPropertyName("fails")]
            public List<string> Fails { get; set; }
            /// <summary>
            /// 原始圖像路徑
            /// </summary>
            [JsonPropertyName("origin_image_path")]
            public string OriginImagePath { get; set; }

            /// <summary>
            /// 結果圖像路徑
            /// </summary>
            [JsonPropertyName("result_image_path")]
            public string ResultImagePath { get; set; }

            [JsonPropertyName("Data")]
            public List<DataItem> Data { get; set; } = new List<DataItem>();
            public class DataItem
            {

                /// <summary>
                /// 操作時間
                /// </summary>
                [JsonPropertyName("stage")]
                public string Stage { get; set; }
                /// <summary>
                /// 操作時間
                /// </summary>
                [JsonPropertyName("product")]
                public string Product { get; set; }
                /// <summary>
                /// 操作時間
                /// </summary>
                [JsonPropertyName("op_time")]
                public string Op_time { get; set; }

             
            }
        }

        

        static public MetalMarkAIPost Function_測試一次(string product, int stage)
        {

            MetalMarkAIPost metalMarkAIPost = new MetalMarkAIPost();
            MetalMarkAIPost.DataItem dataItem = new MetalMarkAIPost.DataItem();
            dataItem.Stage = stage.ToString();
            dataItem.Product = product;
            dataItem.Op_time = DateTime.Now.ToDateTimeString();
            dataItem.Op_time = dataItem.Op_time.Replace("/", "");
            dataItem.Op_time = dataItem.Op_time.Replace(":", "");
            metalMarkAIPost.Data.Add(dataItem);
            string json_in = metalMarkAIPost.JsonSerializationt(true);
            Logger.LogAddLine();
            Logger.Log("metalMarkAIPost", json_in);
            string json_out = Basic.Net.WEBApiPostJson(dBConfigClass.MetalMarkAI_url, json_in);
            Logger.LogAddLine();
            Logger.Log("metalMarkAIPost", json_out);

            metalMarkAIPost = json_out.JsonDeserializet<MetalMarkAIPost>();
            return metalMarkAIPost;
        }

    }
}
