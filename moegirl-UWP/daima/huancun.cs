using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace moegirl_UWP.daima
{
    public static class huancun
    {
        /// <summary>
        /// 网页版默认导航的地址 也可用作全局默认的根地址
        /// </summary>
        public const string Wangzhi = "https://zh.moegirl.org.cn/";
        /// <summary>
        /// 默认搜索的地址
        /// </summary>
        public const string sousuo_wangzhi = "https://zh.moegirl.org.cn/index.php?title=Special:搜索";

        public static Wangye_zhijie wangye_Zhijie = new Wangye_zhijie();
        public static Chitiao chitiao = new Chitiao();
        public static Shezhi_quanju shezhi_Quanju = new Shezhi_quanju();
        public static Shezhi shezhi = new Shezhi();
        public static Sousuo sousuo = new Sousuo();
        public static Fenlei fenlei = new Fenlei();

        /// <summary>
        /// 判断页面类型 1 主页 2 词条 3 分类 4 工具 5 其他 6讨论 7 帮助
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static int panduan_leixing(string uri)
        {
            if(uri.StartsWith(Wangzhi)==false)
            {
                return 5;
            }
            else
            {
                if(uri==Wangzhi)
                {
                    return 1;
                }
                else
                {
                    if(uri.StartsWith(Wangzhi+ "萌娘百科_talk:讨论版")==true)
                    {
                        return 6;
                    }
                    if (uri.StartsWith(Wangzhi + "Help:") == true)
                    {
                        return 7;
                    }
                    if (uri.StartsWith(Wangzhi + "Category:") == true)
                    {
                        return 3;
                    }
                    if (uri.StartsWith(Wangzhi + "Special:") == true)
                    {
                        return 4;
                    }
                }
            }

            //其他情况 词条
            return 2;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public static async Task<int> ChushihuaAsync()
        {
            await shezhi_Quanju.ChushihuaAsync();
            return 0;
        }
    }

    public class Wangye_zhijie
    {
        /// <summary>
        /// 网页版 缓存的网址
        /// </summary>
        public string huancun_wangzhi = "";
    }

    public class Chitiao
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="a">信息</param>
        /// <param name="xuhao">序号 用作匹配</param>
        public delegate void delegateRun(string a, int xuhao);
        //定义一个事件
        public event delegateRun Xianshitishi;
        /// <summary>
        /// 导航堆伐
        /// </summary>
        public List<string> daohan_duifa = new List<string>();
        /// <summary>
        /// 图片链接列表
        /// </summary>
        public List<tupian_xinxi> tupian_liebiao = new List<tupian_xinxi>();
        /// <summary>
        /// 0 主页 1 搜索 2 分类 3 词条
        /// </summary>
        public int danqian_yemian = 0;

        public string[] paichu_liebiao = {
            "知识共享(Creative Commons) 署名-非商业性使用-相同方式共享 3.0 协议",
            "萌娘百科"
        };

        public Chitiao()
        {
           //daohan_duifa.Add("https://zh.moegirl.org.cn/%E7%8B%90%E5%A6%96%E5%B0%8F%E7%BA%A2%E5%A8%98#");
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="a">信息</param>
        /// <param name="xuhao">序号 用作匹配 1 跳转 2 传递标题 3在其他页面时的跳转 4后退外层 5后退内层 6通知复制链接 7通知链接复制成功 8整个页面向后导航 9由搜索页面跳转到 10跳转分类</param>
        public void Kaishitishi(string a, int xuhao)
        {
            Xianshitishi?.Invoke(a, xuhao);
        }
        /// <summary>
        /// 排除无法加载的图片和不需要加载的图片
        /// </summary>
        /// <param name="str">图片标题</param>
        /// <returns>是否可以加载</returns>
        public bool paichu(string str)
        { 
            //检查是否为svg
            if(Gongju.shifou_jiewei(str,".svg")==true)
            {
                return false;
            }
            //检查是否在排除列表中
            foreach (string item in paichu_liebiao)
            {
                if(str==item)
                {
                    return false;
                }
            }
            return true;
        }

    }
    /// <summary>
    /// 图片列表 传递信息
    /// </summary>
    public class tupian_xinxi
    {
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string suoluetu = "";
        /// <summary>
        /// 图片名称
        /// </summary>
        public string biaoti = "";
    }
    /// <summary>
    /// 设置
    /// </summary>
    public class Shezhi
    {
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
    /// <summary>
    /// 设置
    /// </summary>
    public class Sousuo
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="a">信息</param>
        /// <param name="xuhao">序号 用作匹配</param>
        public delegate void delegateRun(string a, int xuhao);
        //定义一个事件
        public event delegateRun Xianshitishi;
        /// <summary>
        /// 当前图片
        /// </summary>
        public string danqian_lianjie = "";
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
    /// <summary>
    /// 设置
    /// </summary>
    public class Fenlei
    {
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="a">信息</param>
        /// <param name="xuhao">序号 用作匹配</param>
        public delegate void delegateRun(string a, int xuhao);
        //定义一个事件
        public event delegateRun Xianshitishi;
        /// <summary>
        /// 当前图片
        /// </summary>
        public string danqian_lianjie = "";
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
