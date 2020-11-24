using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace moegirl_UWP.daima
{
    public class Shezhi_quanju
    {
        private bool shifou_baocun = false;



        /// <summary>
        /// 客户端版本
        /// </summary>
        public const int banben = 1;
        /// <summary>
        /// 是否分享图片时一并保存
        /// </summary>
        public bool shifou_fenxiang_baocun = true;
        /// <summary>
        /// 图片保存到的文件夹名称
        /// </summary>
        public string baocun_wenjianjia = "Pictures";
        /// <summary>
        /// 最大缓存空间 单位MB
        /// </summary>
        public ulong zuida_huancun = 3096;
        /// <summary>
        /// 缓存清理方式 0启动时清理 1达到上限时清理 2永远不清理缓存
        /// </summary>
        public int huancun_qingli_fangshi = 2;
        /// <summary>
        /// 选择的主题模式 0原谅绿 1鸭蛋黄 2自定义
        /// </summary>
        public int zhuti_xuanzhe = 0;
        /// <summary>
        /// 背景图片 本地名称
        /// </summary>
        public string beijing_tupian = "";
        /// <summary>
        /// 主题颜色 HEX格式
        /// </summary>
        public byte A =255;
        public byte R = 16;
        public byte G = 137;
        public byte B = 62;

        /// <summary>
        /// 背景图片处理方式 0毛玻璃 1亚克力 2遮罩层半透明
        /// </summary>
        public int tupian_chuli = 2;
        /// <summary>
        /// 不透明度
        /// </summary>
        public double toumingdu = 1;
        /// <summary>
        /// 文字颜色主题 0明亮 1黑暗
        /// </summary>
        public int wenzi_zhuti = 0;
        /// <summary>
        /// 图片保存到的文件夹路径
        /// </summary>
        public string baocun_wenjianjia_lujing = "图片库";

        public async Task<int> ChushihuaAsync()
        {
            bool a= await DuquAsync();
            return 0;
        }
        /// <summary>
        /// 获取背景图片
        /// </summary>
        /// <returns></returns>
        public async Task<BitmapImage> huoqu_beijing_tupianAsync()
        {
            try
            {
                StorageFile file = await (await huoqu_geng_wenjianjiaAsync()).GetFileAsync("background.jpg");
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage img = new BitmapImage();
                    await img.SetSourceAsync(stream);
                    return img;
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 保存背景图片 单参数为null时删除背景图片
        /// </summary>
        /// <param name="file"></param>
        public async Task<int> baocun_beijingtupianAsync(StorageFile file)
        {
            if (file == null)
            {
                StorageFile file1 = await (await huoqu_geng_wenjianjiaAsync()).GetFileAsync("background.jpg");
                if(file1!=null)
                {
                    await file1.DeleteAsync();
                }
            }
            else
            {
                await file.CopyAsync(await huoqu_geng_wenjianjiaAsync(), "background.jpg", NameCollisionOption.ReplaceExisting);
            }
            return 0;
        }
        /// <summary>
        /// 获取主题背景
        /// </summary>
        /// <returns></returns>
        public async Task<BitmapImage> huoqu_zhuti_tupianAsync()
        {
            try
            {
                switch (zhuti_xuanzhe)
                {
                    case 0:
                        return new BitmapImage(new Uri("ms-appx:tupian/1.png"));
                    case 1:
                        return new BitmapImage(new Uri("ms-appx:tupian/2.png"));
                    default:
                        return await huoqu_beijing_tupianAsync();
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取主题颜色
        /// </summary>
        /// <returns></returns>
        public Windows.UI.Color huoqu_zhuti_yanse()
        {
            switch (zhuti_xuanzhe)
            {
                case 0:
                    return Windows.UI.Color.FromArgb(255, 16, 137, 62);
                case 1:
                    return Windows.UI.Color.FromArgb(255, 209, 136, 76);
                default:
                    return Windows.UI.Color.FromArgb(A, R, G, B); ;
            }
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns>是否成功</returns>
        public async Task<bool> BaocunAsync()
        {
            //检查是否正在保存
            if(shifou_baocun==true)
            {
                return false;
            }
            shifou_baocun = true;
            try
            {
                //序列化数组 
                var linshi1 = new JObject();
                string linshi4 = linshi1.ToString();
                //序列化json 将数据保存为json
                string data = new JObject(
                                        new JProperty("banben", banben.ToString()),
                                        new JProperty("shifou_fenxiang_baocun", shifou_fenxiang_baocun.ToString()),
                                        new JProperty("baocun_wenjianjia", baocun_wenjianjia),
                                        new JProperty("zuida_huancun", zuida_huancun),
                                        new JProperty("huancun_qingli_fangshi", huancun_qingli_fangshi),
                                        new JProperty("zhuti_xuanzhe", zhuti_xuanzhe),
                                        new JProperty("beijing_tupian", beijing_tupian),
                                        new JProperty("A", A),
                                        new JProperty("R", R),
                                        new JProperty("G", G),
                                        new JProperty("B", B),
                                        new JProperty("tupian_chuli", tupian_chuli),
                                        new JProperty("toumingdu", toumingdu.ToString()),
                                        new JProperty("wenzi_zhuti", wenzi_zhuti),
                                        new JProperty("baocun_wenjianjia_lujing", baocun_wenjianjia_lujing)).ToString();

                //新建文件
                await FileIO.WriteTextAsync(await huoqu_shezhi_wenjianAsync(true), data);
            }
            catch
            {
                shifou_baocun = false;
                return false;
            }
            shifou_baocun = false;
            return true;
        }
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <returns>是否成功</returns>
        public async Task<bool> DuquAsync()
        {
            try
            {
                //打开文件 读取字符串
                string text = await FileIO.ReadTextAsync(await huoqu_shezhi_wenjianAsync(false));
                if(text=="")
                {
                    
                    return await BaocunAsync();
                }
                string[] result = Peizhi_jiexi(text);//解析Json
                //根据读取的数据初始化相应变量
                try
                {
                    if (result[0] != "")
                    {
                        shifou_fenxiang_baocun = bool.Parse(result[0]);
                    }
                    if (result[1] != "")
                    {
                        baocun_wenjianjia = result[1];
                    }
                    if (result[2] != "")
                    {
                        zuida_huancun = ulong.Parse(result[2]);
                    }
                    if (result[3] != "")
                    {
                        huancun_qingli_fangshi = int.Parse(result[3]);
                    }
                    if (result[4] != "")
                    {
                        zhuti_xuanzhe = int.Parse(result[4]);
                    }
                    if (result[5] != "")
                    {
                        beijing_tupian = result[5];
                    }
                    if (result[6] != "")
                    {
                        A = byte.Parse(result[6]);
                    }
                    if (result[7] != "")
                    {
                        R = byte.Parse(result[7]);
                    }
                    if (result[8] != "")
                    {
                        G = byte.Parse(result[8]);
                    }
                    if (result[9] != "")
                    {
                        B = byte.Parse(result[9]);
                    }
                    if (result[10] != "")
                    {
                        tupian_chuli = int.Parse(result[10]);
                    }
                    if (result[11] != "")
                    {
                        toumingdu = double.Parse(result[11]);
                    }
                    if (result[12] != "")
                    {
                        wenzi_zhuti = int.Parse(result[12]);
                    }
                    if (result[13] != "")
                    {
                        baocun_wenjianjia_lujing = result[13];
                    }
                }
                catch
                {

                }
                //检查版本号是否一致
                if (banben.ToString() != result[7])
                {
                    //保存
                    await BaocunAsync();
                }
                return true;

            }
            catch
            {
                await BaocunAsync();
                return false;
            }
        }

        /// <summary>
        /// 解析读取的配置文件Json 外部 负责匹配版本号
        /// </summary>
        /// <param name="str">配置文件json字符串</param>
        /// <returns>按照最新的变量列表返回字符串数组</returns>
        private static string[] Peizhi_jiexi(string str)
        {
            var json = (JObject)JsonConvert.DeserializeObject(str);
            switch (int.Parse(json["banben"].ToString()))
            {
                case 1:
                    return Peizhi_jiexi_1(str);
                default:
                    return null;
            }
        }
        /// <summary>
        /// 解析读取的配置文件 版本1
        /// </summary>
        /// <param name="str">配置文件json字符串</param>
        /// <returns>按照最新的变量列表返回字符串数组</returns>
        private static string[] Peizhi_jiexi_1(string str)
        {
            var json = (JObject)JsonConvert.DeserializeObject(str);
            var shifou_fenxiang_baocun = json["shifou_fenxiang_baocun"];
            var baocun_wenjianjia = json["baocun_wenjianjia"];
            var zuida_huancun = json["zuida_huancun"];
            var huancun_qingli_fangshi = json["huancun_qingli_fangshi"];
            var zhuti_xuanzhe = json["zhuti_xuanzhe"];
            var beijing_tupian = json["beijing_tupian"];
            var A = json["A"];
            var R = json["R"];
            var G = json["G"];
            var B = json["B"];
            var tupian_chuli = json["tupian_chuli"];
            var toumingdu = json["toumingdu"];
            var wenzi_zhuti = json["wenzi_zhuti"];
            var baocun_wenjianjia_lujing = json["baocun_wenjianjia_lujing"];
            //保存结果
            string[] re = new string[14];
            re[0] = shifou_fenxiang_baocun.ToString();
            re[1] = baocun_wenjianjia.ToString();
            re[2] = zuida_huancun.ToString();
            re[3] = huancun_qingli_fangshi.ToString();
            re[4] = zhuti_xuanzhe.ToString();
            re[5] = beijing_tupian.ToString();
            re[6] = A.ToString();
            re[7] = R.ToString();
            re[8] = G.ToString();
            re[9] = B.ToString();
            re[10] = tupian_chuli.ToString();
            re[11] = toumingdu.ToString();
            re[12] = wenzi_zhuti.ToString();
            re[13] = baocun_wenjianjia_lujing.ToString();

            return re;
        }



        /// <summary>
        /// 获取保存设置的文件夹
        /// </summary>
        /// <returns></returns>
        private async Task<StorageFolder> huoqu_geng_wenjianjiaAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            string lin= storageFolder.Path;
            try
            {
                storageFolder = await storageFolder.GetFolderAsync("setting");

            }
            catch (FileNotFoundException)
            {
                //没找到
                storageFolder = await ApplicationData.Current.LocalFolder.
                    CreateFolderAsync("setting");
            }
            return storageFolder;
        }
        /// <summary>
        /// 获取保存设置文本信息的文件
        /// </summary>
        /// <returns></returns>
        private async Task<StorageFile> huoqu_shezhi_wenjianAsync(bool shifou_xinjian)
        {
            StorageFolder storageFolder = await huoqu_geng_wenjianjiaAsync();
            StorageFile file = null;
            if (shifou_xinjian == true)
            {
                file = await storageFolder.CreateFileAsync("infor.txt", CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                file = await storageFolder.CreateFileAsync("infor.txt", CreationCollisionOption.OpenIfExists);
            }
            return file;
        }
    }
}
