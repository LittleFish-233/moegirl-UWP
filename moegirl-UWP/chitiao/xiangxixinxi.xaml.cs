using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace moegirl_UWP.chitiao
{

    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class xiangxixinxi : Page
    {
        int zong = 0;
        string danqian_lianjie = "+----+-//**/-*/+";
        bool shifou_jiazai = true;//是否加载完毕
        bool shifou_xiugai = true;//是否修改完毕
        public List<daima.tupian_xinxi> tupian_liebiao = new List<daima.tupian_xinxi>();
        public xiangxixinxi()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            daima.huancun.chitiao.Xianshitishi += Chitiao_Xianshitishi;          
        }

        private void Chitiao_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 5:
                    if (daima.huancun.chitiao.daohan_duifa.Count > 1&&a=="0")
                    {
                        //复制导航记录
                        int b = daima.huancun.chitiao.daohan_duifa.Count;
                        string linshi = daima.huancun.chitiao.daohan_duifa[b-2];
                        //删除导航记录
                        daima.huancun.chitiao.daohan_duifa.RemoveAt(b-1);
                        //导航
                        webview1.Navigate(new Uri(linshi));
                    }
                    break;
                case 6:
                    //复制链接
                    if (a == "0")
                    {
                        DataPackage dp = new DataPackage();
                        dp.SetText(daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1]);
                        Clipboard.SetContent(dp);
                        daima.huancun.chitiao.Kaishitishi("分享成功", 7);
                    }
                    break;

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)//重写
        {
            if (shifou_jiazai == true && shifou_xiugai == true)
            {
                //判断当前显示的内容是否和缓存一致
                if (danqian_lianjie != daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1])
                {
                    //开始导航
                    zong = 0;
                    webview1.Navigate(new Uri(daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1]));
                }
                else
                {
                    zong --;
                }
            }
        }


        /// <summary>
        /// 显示进度条 隐藏网页
        /// </summary>
        /// <param name="shifou_kaiqi">是否开启进度条</param>
        private void jindutiao(bool shifou_kaiqi)
        {
            if (shifou_kaiqi == true)
            {
                this.Invoke(() =>
                {
                    webview1.Visibility = Visibility.Collapsed;
                    progress1.IsActive = true;
                });
            }
            else
            {
                this.Invoke(() =>
                {
                    progress1.IsActive = false;
                    webview1.Visibility = Visibility.Visible;
                });
            }
        }
        /// <summary>
        /// 等待网页加载完毕
        /// </summary>
        private void dengdai_wangye()
        {
            //等待加载
            shifou_jiazai = false;
            int jishu = 0;
            while (shifou_jiazai == false && jishu < 30)
            {
                System.Threading.Thread.Sleep(100);
                jishu++;
            }

        }
        /// <summary>
        /// 导航成功后格式化页面
        /// </summary>
        private void chuli_wangye()
        {
            int jishu = 0;
            //修改元素
            shifou_xiugai = false;
            jishu = 0;
            while (shifou_xiugai == false && jishu < 30)
            {
                this.Invoke(async () =>
                {
                    try
                    {
                        //update UI code 
                        //获取标题
                        try
                        {
                            string biaoti = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('firstHeading').innerHTML;") });
                            daima.huancun.chitiao.Kaishitishi(biaoti, 1);

                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.margin=0;") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.border = 0; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.backgroundColor='transparent';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.body.style.background='transparent'; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('footer').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-page-base').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('siteNotice').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-head').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-panel').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('firstHeading').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('siteSub').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('contentSub').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('flowthread').style.display='none';") });
                        }
                        catch
                        { }

                        //删除拓展链接
                        var shuliang = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('navbox').length.toString();") });
                        for (int i = 0; i < int.Parse(shuliang); i++)
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('navbox')[" + i + "].style.display='none';") });
                        }
                        //部分无效 忽略
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('tocfloat')[0].style.display='none';") });
                        }
                        catch
                        { }
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-navigation').style.display='none';") });
                        }
                        catch
                        { }

                        //全部执行完毕退出循环
                        shifou_xiugai = true;

                    }
                    catch//某个步骤出错，继续循环
                    {

                    }
                });
                //避免cpu占用过高
                System.Threading.Thread.Sleep(100);
                jishu++;
            }
        }
        /// <summary>
        /// 获取图片列表
        /// </summary>
        private void huoqu_tupian()
        {
            try
            {
                daima.huancun.chitiao.tupian_liebiao.Clear();
                tupian_liebiao.Clear();
                string shuliang = "";
                this.Invoke(async () =>
                {
                    shuliang = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByTagName('img').length.toString();") });
                    for (int i = 0; i < int.Parse(shuliang); i++)
                    {
                        string linshi1= await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByTagName('img')[" + i + "].alt;") });
                        string linshi2 = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByTagName('img')[" + i + "].src;") });
                        if(linshi1==""||daima.huancun.chitiao.paichu(linshi1)==false)
                        {
                            continue;
                        }
                        daima.tupian_xinxi linshi3 = new daima.tupian_xinxi();
                        linshi3.biaoti = linshi1;
                        linshi3.suoluetu = linshi2;
                        tupian_liebiao.Add(linshi3);
                    }
                    daima.huancun.chitiao.tupian_liebiao = tupian_liebiao;
                });

            }
            catch
            {

            }
        }

        /// <summary>
        /// 格式化网页 包括等待网页加载
        /// </summary>
        private void xiugai_wangye()
        {
            //等待加载
            dengdai_wangye();
            //修改元素
            chuli_wangye();
            //显示成功 不管是否真的成功
            shifou_jiazai = true;
            shifou_xiugai = true;
            //完成
            jindutiao(false);
            //继续获取元素
            huoqu_tupian();
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
            //检查列表是否为空
            if (daima.huancun.chitiao.daohan_duifa.Count != 0)
            {
                //检查是否为内链 不用修改元素
                string pattern = @"(#cite_note-)";
                Regex rx = new Regex(pattern);
                if (rx.IsMatch(args.Uri.AbsoluteUri) == false)
                {
                    //检查是否与上一个导航一致
                    if (daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1] != args.Uri.AbsoluteUri)
                    {
                        //检查是否 为末尾#冲突
                        if (daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1].StartsWith(args.Uri.AbsoluteUri) || args.Uri.AbsoluteUri.StartsWith(daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1]))
                        {

                        }
                        else
                        {
                            daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);
                            danqian_lianjie = args.Uri.AbsoluteUri;
                        }
                    }
                }
            }
            else
            {
                daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);
            }

        }

        private async void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //检查是否为词条
            if (daima.huancun.panduan_leixing(args.Uri.AbsoluteUri) == 2)
            {
                //检查是否为内链 不用修改元素
                string pattern = @"(#cite_note-)";
                Regex rx = new Regex(pattern);
                if (rx.IsMatch(args.Uri.AbsoluteUri) == false)
                {
                    //检查是否为同一个链接
                    if (danqian_lianjie.StartsWith(args.Uri.AbsoluteUri) || args.Uri.AbsoluteUri.StartsWith(danqian_lianjie))
                    {

                    }
                    else
                    {
                        danqian_lianjie = args.Uri.AbsoluteUri;
                        //启动进度条
                        jindutiao(true);
                        //因为已经开始导航 只要修改元素
                        IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                            (workItem) =>
                            {
                                xiugai_wangye();
                            });
                    }
                }
            }
            else//用浏览器打开
            {
                //启动进度条
                jindutiao(true);
                //取消导航
                args.Cancel = true;
                await Launcher.LaunchUriAsync(new Uri(args.Uri.AbsoluteUri));
                //向后导航
                webview1.Navigate(new Uri(danqian_lianjie));
                //完成
                jindutiao(false);
            }
        }
    }
}
