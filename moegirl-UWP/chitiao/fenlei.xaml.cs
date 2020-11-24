using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class fenlei : Page
    {
        string danqian_lianjie = "2132135";
        bool shifou_jiazai = true;
        bool shifou_xiugai = true;

        public fenlei()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            Loaded += Fenlei_Loaded;
        }

        private void Fenlei_Loaded(object sender, RoutedEventArgs e)
        {
            daima.huancun.chitiao.danqian_yemian = 2;
            if (shifou_jiazai == true && shifou_xiugai == true)
            {

                //判断当前显示的内容是否和缓存一致
                if (danqian_lianjie != daima.huancun.fenlei.danqian_lianjie)
                {
                    //导航到第一面
                    webview1.Navigate(new Uri(daima.huancun.fenlei.danqian_lianjie));
                }
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
            while (shifou_xiugai == false && jishu < 15)
            {
                this.Invoke(async () =>
                {
                    try
                    {
                        //update UI code 
                        //获取标题
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('topicpath').style.background='';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('topicpath').style.border='';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('topicpath').style.padding='0 0 0 0';") });
                        }
                        catch { }
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.margin=0;") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.border = 0; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.padding = '0 0 0 0'; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.backgroundColor='transparent';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.body.style.background='transparent'; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('footer').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('siteSub').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-indicators mw-body-content')[0].style.display='none';") });

                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-subcategories').children[0].style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-subcategories').children[1].style.display='none';") });

                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-indicator-mw-helplink').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-page-base').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('siteNotice').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-head').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-panel').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('firstHeading').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('contentSub').style.display='none';") });
                        }
                        catch
                        { }
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-subcategories').children[0].style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-subcategories').children[1].style.display='none';") });
                        }
                        catch
                        { }
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-pages').children[0].style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-pages').children[1].style.display='none';") });
                        }
                        catch
                        {

                        }
                        try
                        {
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-indicator-mw-helplink').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-page-base').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('siteNotice').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-head').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-panel').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('firstHeading').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('contentSub').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('catlinks').style.display='none';") });
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
                System.Threading.Thread.Sleep(200);
                jishu++;
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
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;

        }

        private async void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            //检查是否为字母链接
            string pattern = @"(title=Category)";
            Regex rx = new Regex(pattern);
            //检查是否为分类
            if (daima.huancun.panduan_leixing(args.Uri.AbsoluteUri) == 3 || rx.IsMatch(args.Uri.AbsoluteUri) == true)
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
            else
            {
                //检查是否为词条
                if (daima.huancun.panduan_leixing(args.Uri.AbsoluteUri) == 2)
                {
                    //启动进度条
                    jindutiao(true);
                    //取消导航
                    args.Cancel = true;
                    //检查是否为新链接
                    if (daima.huancun.chitiao.daohan_duifa.Count != 0)
                    {
                        if ((daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1].StartsWith(args.Uri.AbsoluteUri) || args.Uri.AbsoluteUri.StartsWith(daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1])) == false)
                        {
                            daima.huancun.chitiao.daohan_duifa.Clear();
                            daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);

                        }
                    }
                    else
                    {
                        daima.huancun.chitiao.daohan_duifa.Clear();
                        daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);
                    }
                    //跳转                 
                    Frame.Navigate(typeof(chitiao_dan));
                    //完成
                    jindutiao(false);
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(zhu_chitiao));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void webview1_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            if(daima.huancun.panduan_leixing(args.Uri.AbsoluteUri) != 5)
            {
                args.Handled = true;
                webview1.Navigate(args.Uri);
            }
            else
            {

            }
        }
    }
}
