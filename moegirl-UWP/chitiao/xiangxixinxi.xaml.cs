﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
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

        string danqian_lianjie = "";
        bool shifou_jiazai = true;//是否加载完毕
        bool shifou_xiugai = true;//是否修改完毕
        /// <summary>
        /// 进行修改元素的线程开始时 会添加一个数字 结束时会删除这个数字 如果列表为空 元素修改完成
        /// </summary>
        List<int> xiugai_liebiao = new List<int>();
        /// <summary>
        /// 计时器
        /// </summary>
        private Timer timer;
        public xiangxixinxi()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            daima.huancun.chitiao.Xianshitishi += Chitiao_Xianshitishi;
            Loaded += Xiangxixinxi_Loaded;
        }

        private void Xiangxixinxi_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new Timer(timerCallback, null, (int)TimeSpan.FromMilliseconds(100).TotalMilliseconds, Timeout.Infinite);
        }

        private void Chitiao_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 5:
                    if (daima.huancun.chitiao.daohan_duifa.Count > 1)
                    {
                        //复制导航记录
                        string linshi = daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1];
                        //删除导航记录
                        daima.huancun.chitiao.daohan_duifa.RemoveAt(daima.huancun.chitiao.daohan_duifa.Count - 1);
                        //导航
                        webview1.Navigate(new Uri(linshi));
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
                    webview1.Navigate(new Uri(daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1]));
                }
            }
        }

        private void timerCallback(object state)
        {
            try
            {
                if (xiugai_liebiao.Count == 0)
                {
                    jindutiao(false);
                }
                else
                {
                    jindutiao(true);
                }
            }
            catch
            {
                
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
                        string biaoti = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('firstHeading').innerHTML;") });
                        daima.huancun.chitiao.Kaishitishi(biaoti, 1);

                        await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.margin=0;") });
                        await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.border = 0; ") });
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
                        await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.backgroundColor='transparent';") });
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
            xiugai_liebiao.RemoveAt(0);
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
            danqian_lianjie = args.Uri.AbsoluteUri;
            //检查是否与上一个导航一致
            if (daima.huancun.chitiao.daohan_duifa.Count != 0)
            {
                if (daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1] != args.Uri.AbsoluteUri)
                {
                    daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);
                }
            }
            else
            {
                daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);
            }
            
        }

        private async void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //启动进度条
            jindutiao(true);
            xiugai_liebiao.Add(xiugai_liebiao.Count);
            //检查是否为词条
            if (daima.huancun.panduan_leixing(args.Uri.AbsoluteUri) == 2)
            {
                //检查是否为内链 不用修改元素
                string pattern = @"(#cite_note-)";
                Regex rx = new Regex(pattern);
                if (rx.IsMatch(args.Uri.AbsoluteUri) == false)
                {
                    //因为已经开始导航 只要修改元素
                    IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                        (workItem) =>
                        {
                            xiugai_wangye();
                        });
                }
            }
            else//用浏览器打开
            {
                //取消导航
                args.Cancel = true;
                await Launcher.LaunchUriAsync(new Uri(args.Uri.AbsoluteUri));
                //向后导航
                webview1.Navigate(new Uri(danqian_lianjie));
                //完成
                xiugai_liebiao.RemoveAt(0);
            }
        }
    }
}
