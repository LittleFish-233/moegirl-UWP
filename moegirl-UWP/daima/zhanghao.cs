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
    public class Zhanghao
    {
        private bool shifou_baocun = false;

        /// <summary>
        /// 客户端版本
        /// </summary>
        public const int banben = 1;
        /// <summary>
        /// 用户名
        /// </summary>
        public string yonghuming = "未命名用户-12138";
        /// <summary>
        /// 个性签名
        /// </summary>
        public string gexingqianming = "这个用户很懒，什么都没写哦";
        /// <summary>
        /// 用户组
        /// </summary>
        public string yonghuzhu = "用户";
        /// <summary>
        /// 编辑数
        /// </summary>
        public int bianjishu = 0;
        /// <summary>
        /// 注册时间
        /// </summary>
        public string zhuceshijian = "";
        /// <summary>
        /// 语言
        /// </summary>
        public string yuyan = "";
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string dianzi_youjian = "";
        /// <summary>
        /// 是否登入
        /// </summary>
        public bool shifou_dengru = false;


        public async Task<int> ChushihuaAsync()
        {
            bool a = await DuquAsync();
            return 0;
        }
        /// <summary>
        /// 获取缓存头像
        /// </summary>
        /// <returns></returns>
        public async Task<BitmapImage> huoqu_beijing_tupianAsync()
        {
            try
            {
                StorageFile file = await (await huoqu_geng_wenjianjiaAsync()).GetFileAsync("touxiang.jpg");
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
        /// 保存缓存头像 单参数为null时删除头像
        /// </summary>
        /// <param name="file"></param>
        public async Task<int> baocun_beijingtupianAsync(StorageFile file)
        {
            try
            {
            if (file == null)
            {
                StorageFile file1 = await (await huoqu_geng_wenjianjiaAsync()).GetFileAsync("touxiang.jpg");
                if (file1 != null)
                {
                    await file1.DeleteAsync();
                }
            }
            else
            {
                await file.CopyAsync(await huoqu_geng_wenjianjiaAsync(), "touxiang.jpg", NameCollisionOption.ReplaceExisting);
            }

            }
            catch
            {

            }  
            return 0;

        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns>是否成功</returns>
        public async Task<bool> BaocunAsync()
        {
            //检查是否正在保存
            if (shifou_baocun == true)
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
                                        new JProperty("yonghuming", yonghuming),
                                        new JProperty("gexingqianming", gexingqianming),
                                        new JProperty("yonghuzhu", yonghuzhu),
                                        new JProperty("bianjishu", bianjishu),
                                        new JProperty("zhuceshijian", zhuceshijian),
                                        new JProperty("yuyan", yuyan),
                                        new JProperty("dianzi_youjian", dianzi_youjian),
                                        new JProperty("shifou_dengru", shifou_dengru)).ToString();

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
                if (text == "")
                {

                    return await BaocunAsync();
                }
                string[] result = Peizhi_jiexi(text);//解析Json
                //根据读取的数据初始化相应变量
                try
                {
                    if (result[0] != "")
                    {
                        yonghuming = result[0];
                    }
                    if (result[1] != "")
                    {
                        gexingqianming = result[1];
                    }
                    if (result[2] != "")
                    {
                        yonghuzhu = result[2];
                    }
                    if (result[3] != "")
                    {
                        bianjishu =int.Parse( result[3]);
                    }
                    if (result[4] != "")
                    {
                        zhuceshijian = result[4];
                    }
                    if (result[5] != "")
                    {
                        yuyan = result[5];
                    }
                    if (result[6] != "")
                    {
                        dianzi_youjian = result[6];
                    }
                    if (result[7] != "")
                    {
                        shifou_dengru = bool.Parse(result[7]);
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
            var yonghuming = json["yonghuming"];
            var gexingqianming = json["gexingqianming"];
            var yonghuzhu = json["yonghuzhu"];
            var bianjishu = json["bianjishu"];
            var zhuceshijian = json["zhuceshijian"];
            var yuyan = json["yuyan"];
            var dianzi_youjian = json["dianzi_youjian"];
            var shifou_dengru = json["shifou_dengru"];
            //保存结果
            string[] re = new string[8];
            re[0] = yonghuming.ToString();
            re[1] = gexingqianming.ToString();
            re[2] = yonghuzhu.ToString();
            re[3] = bianjishu.ToString();
            re[4] = zhuceshijian.ToString();
            re[5] = yuyan.ToString();
            re[6] = dianzi_youjian.ToString();
            re[7] = shifou_dengru.ToString();

            return re;
        }



        /// <summary>
        /// 获取保存设置的文件夹
        /// </summary>
        /// <returns></returns>
        private async Task<StorageFolder> huoqu_geng_wenjianjiaAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            string lin = storageFolder.Path;
            try
            {
                storageFolder = await storageFolder.GetFolderAsync("user");

            }
            catch (FileNotFoundException)
            {
                //没找到
                storageFolder = await ApplicationData.Current.LocalFolder.
                    CreateFolderAsync("user");
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
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="a">信息</param>
        /// <param name="xuhao">序号 用作匹配</param>
        public delegate void delegateRun(string a, int xuhao);
        //定义一个事件
        public event delegateRun Xianshitishi;
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="a">信息</param>
        /// <param name="xuhao">序号 用作匹配 1更改背景图片 2更换图片处理方式 3更换主题</param>
        public void Kaishitishi(string a, int xuhao)
        {
            Xianshitishi?.Invoke(a, xuhao);
        }

    }
}
