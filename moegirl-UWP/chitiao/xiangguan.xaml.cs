using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class xiangguan : Page
    {
        string danqian_lianjie = "";
        bool shifou_jiazai = false;
        bool shifou_xiugai = false;
        bool shifou_fanghui = false;

        public xiangguan()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            daima.huancun.chitiao.Xianshitishi += Chitiao_Xianshitishi;
        }

        private void Chitiao_Xianshitishi(string a, int xuhao)
        {
            switch(xuhao)
            {
                case 5:
                    if(a=="1")
                    {
                        shifou_fanghui = true;
                        houtui_wangye();
                    }
                    break;
            }
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)//重写
        {
            //判断当前显示的内容是否和缓存一致
            if (danqian_lianjie != daima.huancun.chitiao.danqian_lianjie)
            {

                //初始化
                chushihua(daima.huancun.chitiao.danqian_lianjie);
            }
        }

        /// <summary>
        /// 初始化词条 详细信息
        /// </summary>
        /// <param name="lianjie"></param>
        private void chushihua(string lianjie)
        {
            //启动进度条
            jindutiao(true);
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                (workItem) =>
                {
                    //导航到该页面
                    this.Invoke(() =>
                    {
                        //update UI code
                        webview1.Navigate(new Uri(lianjie));
                    });

                    //等待加载
                    dengdai_wangye();
                    //修改元素
                    chuli_wangye();

                    this.Invoke(() =>
                    {
                        //update UI code
                        //显示页面
                        jindutiao(false);
                    });

                });
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
            while (shifou_xiugai == false && jishu < 50)
            {
                this.Invoke(async () =>
                {
                    try
                    {
                        //update UI code 

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
                        await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('catlinks').style.display='none';") });
                        await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.backgroundColor='transparent';") });

                        //删除拓展链接外的内容
                        var shuliang = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-parser-output')[1].children.length.toString();") });
                        for (int i = 0; i < int.Parse(shuliang); i++)
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-parser-output')[1].children[" + i + "].style.display='none';") });
                        }
                        shuliang = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('navbox').length.toString();") });
                        for (int i = 0; i < int.Parse(shuliang); i++)
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('navbox')[" + i + "].style.display='block';") });
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
        /// 向后导航 包含格式化
        /// </summary>
        private void houtui_wangye()
        {
            //检查是否可以向后导航
            if(webview1.CanGoBack==false)
            {
                return;
            }
            //启动进度条
            jindutiao(true);

            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                (workItem) =>
                {
                    //返回
                    this.Invoke(() =>
                    {
                        //update UI code
                        webview1.GoBack();
                    });

                    //等待加载
                    dengdai_wangye();
                    //修改元素
                    chuli_wangye();

                    this.Invoke(() =>
                    {
                        //update UI code
                        //显示页面
                        jindutiao(true);
                    });

                });
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
            danqian_lianjie = args.Uri.AbsoluteUri;
            //设置返回状态
            shifou_fanghui = false;
        }

        private async void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //检查
            if (daima.huancun.panduan_leixing(args.Uri.AbsoluteUri) == 2)
            {
                if (args.Uri.AbsoluteUri.StartsWith(daima.huancun.chitiao.danqian_lianjie) == false&& args.Uri.AbsoluteUri!=danqian_lianjie)
                {
                    if (shifou_fanghui == true)
                    {
                        //进行返回操作 尝试解析
                        daima.huancun.chitiao.xiayige_lianjie = args.Uri.AbsoluteUri;
                    }
                    else
                    {
                        //是百科内的网址 尝试解析
                        daima.huancun.chitiao.xiayige_lianjie = args.Uri.AbsoluteUri;
                        //取消导航
                        args.Cancel = true;
                        //触发事件 
                        daima.huancun.chitiao.Kaishitishi("跳转", 2);
                        //向后导航
                        chushihua(daima.huancun.chitiao.danqian_lianjie);

                    }
                }
            }
            else//用浏览器打开
            {
                await Launcher.LaunchUriAsync(new Uri(args.Uri.AbsoluteUri));
                //取消导航
                args.Cancel = true;
                //向后导航
                chushihua(danqian_lianjie);
            }
        }
    }
}
