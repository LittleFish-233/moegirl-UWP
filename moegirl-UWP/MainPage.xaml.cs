using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace moegirl_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            daima.huancun.shezhi.Xianshitishi += Shezhi_Xianshitishi;
            daima.huancun.zhanghao.Xianshitishi += Zhanghao_Xianshitishi;
            daima.huancun.chuangzuo.Xianshitishi += Chuangzuo_Xianshitishi;
        }

        private void Chuangzuo_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 1:
                    ContentFrame.Navigate(typeof(zhanghu.dengru));
                    break;
            }
        }

        private void Zhanghao_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 1:
                    ContentFrame.Navigate(typeof(chitiao.chitiao_dan));
                    break;
                case 2:
                    ContentFrame.Navigate(typeof(chitiao.fenlei));
                    break;
            }
        }

        private async void Shezhi_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 1:
                    BitmapImage img = await daima.huancun.shezhi_Quanju.huoqu_beijing_tupianAsync();
                    image1.Source = img;
                    break;
                case 2:
                    switch (a)
                    {
                        case "1":
                            re1.Visibility = Visibility.Collapsed;
                            image1.Opacity = 0;
                            break;
                        case "2":
                            re1.Visibility = Visibility.Visible;
                            re1.Opacity = daima.huancun.shezhi_Quanju.toumingdu;
                            image1.Opacity = 1;
                            break;
                    }
                    break;
                case 3:
                    //设置背景图片
                    img = await daima.huancun.shezhi_Quanju.huoqu_zhuti_tupianAsync();
                    image1.Source = img;
                    break;
            }
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {             
            //设置背景图片
            BitmapImage img =await daima.huancun.shezhi_Quanju.huoqu_zhuti_tupianAsync();
            image1.Source = img;
            //处理图片
           /* if (daima.huancun.shezhi_Quanju.zhuti_xuanzhe == 2)
            {*/
                switch (daima.huancun.shezhi_Quanju.tupian_chuli)
                {
                    case 1:
                        re1.Visibility = Visibility.Collapsed;
                        image1.Opacity = 0;
                        break;
                    case 2:
                        re1.Visibility = Visibility.Visible;
                        re1.Opacity = daima.huancun.shezhi_Quanju.toumingdu;
                        image1.Opacity = 1;
                        break;
                //}
            }
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
            async (workItem) =>
            {
                //清理缓存
                switch (daima.huancun.shezhi_Quanju.huancun_qingli_fangshi)
                {
                    case 0:
                        qingli_huancun();
                        break;
                    case 1:
                        ulong huancun_daxiao = await daima.Gongju.jisuankongjian_wenjianjia(ApplicationData.Current.LocalCacheFolder);
                        if (huancun_daxiao > daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024)
                        {
                            qingli_huancun();
                        }
                        break;
                }

            });
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        private async void qingli_huancun()
        {
            string jieguo = await daima.Gongju.SanchuwenjianAsync(ApplicationData.Current.LocalCacheFolder, true);
            if (jieguo != null)
            {
                this.Invoke(async () =>
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "清理缓存失败",
                        Content = "请联系开发人员，详细信息：<" + jieguo + ">",
                        CloseButtonText = "好的"

                    };
                    await noWifiDialog.ShowAsync();
                });
            }
        }
        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private void nvSample_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //to do
            }
            else
            {
                Microsoft.UI.Xaml.Controls.NavigationViewItem item = args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem;
                switch (item.Tag)
                {
                    case "wangye":
                        ContentFrame.Navigate(typeof(wangye_zhijie.wangye));
                        break;
                    case "chitiao":
                        /*  switch (daima.huancun.chitiao.danqian_yemian)
                          {
                              case 0:*/
                        ContentFrame.Navigate(typeof(chitiao.zhu_chitiao));
                        /* break;
                   case 1:
                       ContentFrame.Navigate(typeof(chitiao.shousuo_jieguo));
                       break;
                   case 2:
                       ContentFrame.Navigate(typeof(chitiao.fenlei));
                       break;
                   case 3:
             ContentFrame.Navigate(typeof(chitiao.chitiao_dan));
                  break;
            }*/
                        break;
                    case "shezhi":
                        ContentFrame.Navigate(typeof(shezhi.shezhi_zhu));
                        break;
                    case "guanyu":
                        ContentFrame.Navigate(typeof(guanyu.guanyu_zhu));
                        break;
                    case "zhanghu":
                        ContentFrame.Navigate(typeof(zhanghu.dengru));
                        break;
                    case "taolunzhu":
                        ContentFrame.Navigate(typeof(taolun.taolun_zhu));
                        break;
                    case "chuangzuo":
                        ContentFrame.Navigate(typeof(chuangzuo.chuangzuo_zhu));
                        break;
                    case "gongju":
                        ContentFrame.Navigate(typeof(gongjuhe.gongju_zhu));
                        break;
                    case "dating":
                        ContentFrame.Navigate(typeof(zhuye.zhuye_zhu));
                        break;
                    case "yonghu":
                        ContentFrame.Navigate(typeof(bangzhu.bangzhu_zhu));
                        break;
                }
            }
        }

        private void nvSample_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            if(ContentFrame.CanGoBack==true)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
