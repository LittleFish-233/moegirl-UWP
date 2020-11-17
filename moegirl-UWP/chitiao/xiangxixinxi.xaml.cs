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
    public sealed partial class xiangxixinxi : Page
    {
        string danqian_lianjie = "";
        bool shifou_jiazai = false;
        bool shifou_xiugai = false;
        public xiangxixinxi()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)//重写
        {
            //判断是否需要显示下一个词条
            if (daima.huancun.chitiao.xiayige_lianjie != "")
            {
                //启动进度条
                webview1.Visibility = Visibility.Collapsed;
                progress1.IsActive = true;
                //初始化
                chushihua(daima.huancun.chitiao.xiayige_lianjie);
            }
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
            webview1.Visibility = Visibility.Collapsed;
            progress1.IsActive = true;
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
                    shifou_jiazai = false;
                    int jishu = 0;
                    while (shifou_jiazai == false&&jishu<30)
                    {
                        System.Threading.Thread.Sleep(100);
                        jishu++;
                    }
                    //修改元素
                    shifou_xiugai = false;
                    jishu = 0;
                    while (shifou_xiugai == false&&jishu<50)
                    {
                        this.Invoke(async () =>
                        {
                            try
                            {
                                //update UI code 
                                //获取标题
                                string biaoti= await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('firstHeading').innerHTML;") });
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
                    this.Invoke(() =>
                    {
                        //update UI code
                        //显示页面
                        progress1.IsActive = false;
                        webview1.Visibility = Visibility.Visible;
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
            daima.huancun.chitiao.danqian_lianjie = args.Uri.AbsoluteUri;
            //清空下一个词条
            daima.huancun.chitiao.xiayige_lianjie = "";
        }

        private async void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            
            //检查
            if (daima.huancun.panduan_leixing(args.Uri.OriginalString) == 2)
            {
                if (args.Uri.OriginalString != daima.huancun.chitiao.xiayige_lianjie)
                {
                    //是百科内的网址 尝试解析
                    daima.huancun.chitiao.xiayige_lianjie = args.Uri.OriginalString;
                    //取消导航
                    args.Cancel = true;
                    //触发事件
                    daima.huancun.chitiao.Kaishitishi("跳转", 0);
                    
                }
            }
            else//用浏览器打开
            {
                await Launcher.LaunchUriAsync(new Uri(args.Uri.OriginalString));
                //取消导航
                args.Cancel = true;
                //向后导航
                chushihua(danqian_lianjie);
            }
        }
    }
}
