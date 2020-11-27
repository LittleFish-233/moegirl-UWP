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

namespace moegirl_UWP.zhanghu
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class dengru : Page
    {
        private bool shifou_jiazai=false;
        private bool shifou_anxia = false;
        public dengru()
        {
            this.InitializeComponent();
            Loaded += Dengru_Loaded;
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;

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


        private void Dengru_Loaded(object sender, RoutedEventArgs e)
        {
            if (daima.huancun.zhanghao.shifou_dengru == true)
            {
                Frame.Navigate(typeof(zhanghu_zhu));
            }
            else
            {
                //检查是否在外部登入
                //导航到登入网页
                webview1.Navigate(new Uri(daima.huancun.dengru_wangzhi));
                //等待
                IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
               (workItem) =>
               {
                   dengdai_wangye();
                   //检查是否已经登入  
                   this.Invoke(async () =>
                       {
                           try
                           {
                               await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-createaccount-cta').toString();") });
                               jindutiao(false);
                           }
                           catch
                           {
                               //设置登录状态
                               daima.huancun.zhanghao.shifou_dengru = true;
                               await daima.huancun.zhanghao.BaocunAsync();
                               //已经登录 跳转页面
                               Frame.Navigate(typeof(zhanghu_zhu));
                               return;
                           }
                       });

               });

            }
        }

        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {

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
        /// 显示进度条 隐藏网页
        /// </summary>
        /// <param name="shifou_kaiqi">是否开启进度条</param>
        private void jindutiao(bool shifou_kaiqi)
        {
            if (shifou_kaiqi == true)
            {
                this.Invoke(() =>
                {
                    re1.Visibility = Visibility.Collapsed;
                    progress1.IsActive = true;
                    re1.Visibility = Visibility.Collapsed;
                });
            }
            else
            {
                this.Invoke(() =>
                {
                    progress1.IsActive = false;
                    webview1.Visibility = Visibility.Visible;
                    re1.Visibility = Visibility.Visible;
                });
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //关闭之前的提示
            dengru_xinxi.IsOpen = false;
            dengru_xinxi_3.IsOpen = false;
            button1.IsEnabled = false;

            //检查信息是否正确
            if (textbox1.Text == "")
            {
                dengru_xinxi.Title = "用户名不能为空";
                dengru_xinxi.Subtitle = "";
                dengru_xinxi.IsOpen = true;
                return;
            }
            if (textbox2.Password == "")
            {
                dengru_xinxi.Title = "密码不能为空";
                dengru_xinxi.Subtitle = "";
                dengru_xinxi.IsOpen = true;
                return;
            }
            //导航到登入网页
            webview1.Navigate(new Uri(daima.huancun.dengru_wangzhi));
            //等待
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
           (workItem) =>
             {
                 dengdai_wangye();
                 //检查是否已经登入  
                 this.Invoke(async () =>
                     {
                         try
                         {
                             await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-createaccount-cta').toString();") });
                         }
                         catch
                         {
                             //设置登录状态
                             daima.huancun.zhanghao.shifou_dengru = true;
                             await daima.huancun.zhanghao.BaocunAsync();
                             //已经登录 跳转页面
                             Frame.Navigate(typeof(zhanghu_zhu));
                             
                             return;
                         }
                         //输入账号密码
                         await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('wpName1').value='"+textbox1.Text+"';") });
                         await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('wpPassword1').value='" + textbox2.Password + "';") });
                         await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('wpRemember').click();") });
                         //模拟点击
                         await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('wpLoginAttempt').click();") });
                         shifou_anxia = true;
                     });

             });
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(chuangjian));
        }

        private void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {

        }

        private async void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
            if(shifou_anxia==true)
            {
                shifou_anxia = false;
                try
                {
                    await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-createaccount-cta').toString();") });
                }
                catch
                {
                    //设置登录状态
                    daima.huancun.zhanghao.shifou_dengru = true;
                    await daima.huancun.zhanghao.BaocunAsync();
                    //已经登录 跳转页面
                    Frame.Navigate(typeof(zhanghu_zhu));
                    return;
                }
                //提示密码错误
                dengru_xinxi_3.IsOpen = true;
            }
            else
            {

            }
        }
    }
}
