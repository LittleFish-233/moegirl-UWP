﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace moegirl_UWP.wangye_zhijie
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class wangye : Page
    {
        public wangye()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            Loaded += Wangye_Loaded;
        }

        private async void Wangye_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //导航到缓存地址
                if (daima.huancun.wangye_Zhijie.huancun_wangzhi == "")
                {
                    daima.huancun.wangye_Zhijie.huancun_wangzhi = daima.huancun.Wangzhi;
                }
                webview1.Navigate(new Uri(daima.huancun.wangye_Zhijie.huancun_wangzhi));

            }
            catch (Exception exc)
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "启动萌娘百科网页版失败",
                    Content = "无法导航到 "+daima.huancun.wangye_Zhijie.huancun_wangzhi+"，详细信息："+exc.Message,
                    CloseButtonText = "好的"
                };
                await noWifiDialog.ShowAsync();
            }
        }

        private void webview1_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            webview1.Navigate(args.Uri);
            args.Handled = true;
        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //记录当前链接
            daima.huancun.wangye_Zhijie.huancun_wangzhi = args.Uri.AbsoluteUri;
            //检查是否可以导航
            if(webview1.CanGoBack)
            {
                back.IsEnabled = true;
            }
            else
            {
                back.IsEnabled = false;
            }
            if(webview1.CanGoForward)
            {
                forward.IsEnabled = true;
            }
            else
            {
                forward.IsEnabled = false;
            }
            //隐藏进度条
            progress1.Visibility = Visibility.Collapsed;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            webview1.Navigate(new Uri(daima.huancun.Wangzhi));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if(webview1.CanGoBack)
            {
                webview1.GoBack();
            }
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            if(webview1.CanGoForward)
            {
                webview1.GoForward();
            }
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            webview1.Refresh();
        }

        private async void AppBarButton_Click_4(object sender, RoutedEventArgs e)
        {
            //复制链接
            DataPackage dp = new DataPackage();
            dp.SetText(daima.huancun.wangye_Zhijie.huancun_wangzhi);
            Clipboard.SetContent(dp);

            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "复制链接成功",
                Content = "快去分享给朋友吧。",
                CloseButtonText = "好的"
            };
            await noWifiDialog.ShowAsync();
        }

        private void webview1_FrameNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            progress1.Visibility = Visibility.Visible;
        }
    }
}