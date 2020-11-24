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
    public sealed partial class zhu_chitiao : Page
    {
        public zhu_chitiao()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            Loaded += Zhu_chitiao_Loaded;
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;
        }

        private void Zhu_chitiao_Loaded(object sender, RoutedEventArgs e)
        {
            //捕获焦点
            textbox1.Focus(FocusState.Keyboard);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox1.Text) != true)
            {
                daima.huancun.sousuo.danqian_lianjie = textbox1.Text;
                Frame.Navigate(typeof(shousuo_jieguo));
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

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:萌属性";
            Frame.Navigate(typeof(fenlei));
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:作品";
            Frame.Navigate(typeof(fenlei));
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:人物";
            Frame.Navigate(typeof(fenlei));
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:音乐";
            Frame.Navigate(typeof(fenlei));
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:文化";
            Frame.Navigate(typeof(fenlei));
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:软件";
            Frame.Navigate(typeof(fenlei));
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:活动";
            Frame.Navigate(typeof(fenlei));
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:原创漫画";
            Frame.Navigate(typeof(fenlei));
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            daima.huancun.fenlei.danqian_lianjie = daima.huancun.Wangzhi + "Category:设定";
            Frame.Navigate(typeof(fenlei));
        }
    }
}
