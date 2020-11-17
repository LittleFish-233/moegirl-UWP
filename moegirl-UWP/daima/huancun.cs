using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moegirl_UWP.daima
{
    public static class huancun
    {
        /// <summary>
        /// 网页版默认导航的地址 也可用作全局默认的根地址
        /// </summary>
        public const string Wangzhi = "https://zh.moegirl.org.cn/";
        public static Wangye_zhijie wangye_Zhijie = new Wangye_zhijie();
        public static Chitiao chitiao = new Chitiao();

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
        /// 当前显示的链接
        /// </summary>
        public string danqian_lianjie = "https://zh.moegirl.org.cn/%E7%8B%90%E5%A6%96%E5%B0%8F%E7%BA%A2%E5%A8%98#";
        /// <summary>
        /// 下一个显示的词条
        /// </summary>
        public string xiayige_lianjie = "";
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
        /// <param name="xuhao">序号 用作匹配 1 跳转 2 传递标题</param>
        public void Kaishitishi(string a, int xuhao)
        {
            Xianshitishi?.Invoke(a, xuhao);
        }
    }
}
