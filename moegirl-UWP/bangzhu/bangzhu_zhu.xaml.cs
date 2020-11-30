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

namespace moegirl_UWP.bangzhu
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class bangzhu_zhu : Page
    {
        public bangzhu_zhu()
        {
            this.InitializeComponent();
            Loaded += Guanyu_zhu_Loaded;

        }

        private void Guanyu_zhu_Loaded(object sender, RoutedEventArgs e)
        {
            //匹配页面
            yonghushouce.Navigate(typeof(yonghu));
            guanfangwendang.Navigate(typeof(guanfang));

        }

        private void rootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            xianshi_beijing(rootPivot.SelectedIndex);
        }
        /// <summary>
        /// 显示背景
        /// </summary>
        /// <param name="n">从0开始</param>
        private void xianshi_beijing(int n)
        {
            switch (n)
            {
                case 0:
                    rext1.Visibility = Visibility.Visible;
                    rext2.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    rext1.Visibility = Visibility.Collapsed;
                    rext2.Visibility = Visibility.Visible;
                    break;
            }
        }

    }
}
