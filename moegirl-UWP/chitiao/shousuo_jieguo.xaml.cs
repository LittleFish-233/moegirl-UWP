using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
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
    public sealed partial class shousuo_jieguo : Page
    {
        string danqian_lianjie = "2132135";
        bool shifou_jiazai = true;
        bool shifou_xiugai = true;
        bool shifou_chushihua = false;
        public shousuo_jieguo()
        {
            this.InitializeComponent();
            Loaded += Shousuo_jieguo_Loaded;
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;
        }

        private void Shousuo_jieguo_Loaded(object sender, RoutedEventArgs e)
        {
            daima.huancun.chitiao.danqian_yemian = 1;
            //显示进度条
            shifou_chushihua = false;
            jindutiao(true);
            //捕获UI
            textbox1.Focus(FocusState.Keyboard);
            //更新UI
            textbox1.Text = daima.huancun.sousuo.danqian_lianjie;
            //导航到第一面
            webview1.Navigate(new Uri(daima.huancun.sousuo_wangzhi));
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
               (workItem) =>
               {
                   try
                   {
                       dengdai_wangye();
                       moni_sousuo(daima.huancun.sousuo.danqian_lianjie);
                       shifou_chushihua = true;
                   }
                   catch
                   {

                   }
               });
        }
        /// <summary>
        /// 模拟搜索
        /// </summary>
        private void moni_sousuo(string str)
        {
            //模拟点击
            this.Invoke(async () =>
            {
                try
                {
                    await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('ooui-php-1').value='" + str + "'") });
                    await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('oo-ui-inputWidget-input oo-ui-buttonElement-button')[0].click();") });
                }
                catch
                {

                }
            });
            dengdai_wangye();
            //处理网页
            chuli_wangye();
            //显示成功 不管是否真的成功
            shifou_jiazai = true;
            shifou_xiugai = true;
            //完成
            jindutiao(false);
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
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.margin=0;") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.border = 0; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.padding = '0 0 0 0'; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('content').style.backgroundColor='transparent';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.body.style.background='transparent'; ") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('footer').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('search').style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-search-exists')[0].style.display='none';") });
                            await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-indicators mw-body-content')[0].style.display='none';") });

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
        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
            
        }

        private void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (shifou_jiazai == true && shifou_xiugai == true && shifou_chushihua == true)
            {
                //检查是否为同一个链接
                if (danqian_lianjie.StartsWith(args.Uri.AbsoluteUri) || args.Uri.AbsoluteUri.StartsWith(danqian_lianjie))
                {

                }
                else
                {
                    danqian_lianjie = args.Uri.AbsoluteUri;
                    //清空
                    daima.huancun.chitiao.daohan_duifa.Clear();
                    //获取链接
                    daima.huancun.chitiao.daohan_duifa.Add(args.Uri.AbsoluteUri);
                    //导航到
                    Frame.Navigate(typeof(chitiao_dan));

                }


            }
        }
        private void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {

            if (args.EventType.ToString().Contains("KeyUp"))
            {
                VirtualKey virtualKey = args.VirtualKey;

                switch (virtualKey)
                {
                    case VirtualKey.Enter:
                        button1_Click(null, null);
                        break;
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            moni_sousuo(daima.huancun.sousuo.danqian_lianjie);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
