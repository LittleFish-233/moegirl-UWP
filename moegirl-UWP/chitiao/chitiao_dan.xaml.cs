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
        public chitiao_dan()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            daima.huancun.chitiao.Xianshitishi += Chitiao_Xianshitishi;
        }

        private void Chitiao_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 0:
                    ContentFrame.Navigate(typeof(xiangxixinxi));
                    break;
                case 1:
                    textblock1.Text = a;
                    break;
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(pivot1.SelectedIndex)
            {
                case 0:
                    ContentFrame.Navigate(typeof(xiangxixinxi));
                    break;
            }
        }
    }
}
