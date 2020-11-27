using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace moegirl_UWP.chitiao
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class chitiao_dan : Page
    {
        int danqian_xuanze = 0;
        string biaoyi = "";
        public chitiao_dan()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            daima.huancun.chitiao.Xianshitishi += Chitiao_Xianshitishi;
            Loaded += Chitiao_dan_Loaded;
        }

        private void Chitiao_dan_Loaded(object sender, RoutedEventArgs e)
        {
            textblock1.Text = daima.huancun.sousuo.danqian_lianjie;
            //修改选项卡
            if (danqian_xuanze == 0)
            {
                daima.huancun.chitiao.Kaishitishi("导航到", 9);
            }
            else
            {
                pivot1.SelectedIndex = 0;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)//重写
        {
            daima.huancun.chitiao.danqian_yemian = 3;
            textblock1.Text = biaoyi;
        }
        private async void Chitiao_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 0:
                    ContentFrame.Navigate(typeof(xiangxixinxi));
                    break;
                case 1:
                    biaoyi= textblock1.Text = a;
                    break;
                case 2:
                    pivot1.SelectedIndex = 0;
                    break;
                case 4:
                    daima.huancun.chitiao.Kaishitishi(pivot1.SelectedIndex.ToString(), 5);
                    break;
                case 7:
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "复制链接成功",
                        Content = "快去分享给朋友吧。",
                        CloseButtonText = "好的"

                    };
                    await noWifiDialog.ShowAsync();
                    break;
                case 8:
                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                    }
                    break;
                case 10:
                    Frame.Navigate(typeof(fenlei));
                    break;
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            xianshi_beijing(pivot1.SelectedIndex);
            danqian_xuanze = pivot1.SelectedIndex;
            switch(pivot1.SelectedIndex)
            {
                case 0:
                    ContentFrame.Navigate(typeof(xiangxixinxi));
                    break;
                case 1:
                    ContentFrame.Navigate(typeof(xiangguan));
                    break;
                case 2:
                    ContentFrame.Navigate(typeof(tupian));
                    break;
            }
        }

        //主页
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(zhu_chitiao));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.chitiao.Kaishitishi("后退", 4);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.chitiao.Kaishitishi(pivot1.SelectedIndex.ToString(), 6);
        }
        /// <summary>
        /// 显示背景
        /// </summary>
        /// <param name="n">从0开始</param>
        private void xianshi_beijing(int n)
        {
            switch(n)
            {
                case 0:
                    rext1.Visibility = Visibility.Visible;
                    rext2.Visibility = Visibility.Collapsed;
                    rext3.Visibility = Visibility.Collapsed;
                    rext4.Visibility = Visibility.Collapsed;
                    rext5.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    rext1.Visibility = Visibility.Collapsed;
                    rext2.Visibility = Visibility.Visible;
                    rext3.Visibility = Visibility.Collapsed;
                    rext4.Visibility = Visibility.Collapsed;
                    rext5.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    rext1.Visibility = Visibility.Collapsed;
                    rext2.Visibility = Visibility.Collapsed;
                    rext3.Visibility = Visibility.Visible;
                    rext4.Visibility = Visibility.Collapsed;
                    rext5.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    rext1.Visibility = Visibility.Collapsed;
                    rext2.Visibility = Visibility.Collapsed;
                    rext3.Visibility = Visibility.Collapsed;
                    rext4.Visibility = Visibility.Visible;
                    rext5.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    rext1.Visibility = Visibility.Collapsed;
                    rext2.Visibility = Visibility.Collapsed;
                    rext3.Visibility = Visibility.Collapsed;
                    rext4.Visibility = Visibility.Collapsed;
                    rext5.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
