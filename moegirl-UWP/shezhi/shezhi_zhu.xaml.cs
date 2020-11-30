using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace moegirl_UWP.shezhi
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class shezhi_zhu : Page
    {
        bool shifou_jiazai = false;//是否加载完成
        string danqian_yanse = "";
        ulong huancun_daxiao = 0;
        public shezhi_zhu()
        {
            this.InitializeComponent();
            Loaded += Shezhi_zhu_Loaded;
        }

        private async void Shezhi_zhu_Loaded(object sender, RoutedEventArgs e)
        {
            //更新UI
            toggle1.IsOn = daima.huancun.shezhi_Quanju.shifou_fenxiang_baocun;
            if (toggle1.IsOn == false)
            {
                textbox1.IsEnabled = false;
                button1.IsEnabled = false;
            }
            textbox1.Text = daima.huancun.shezhi_Quanju.baocun_wenjianjia_lujing;
            textblock9.Text = daima.Gongju.zhanyongkongjian(daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024);
            combobox1.SelectedIndex = daima.huancun.shezhi_Quanju.huancun_qingli_fangshi;
            slider1.Value = daima.huancun.shezhi_Quanju.zuida_huancun;
            image4.Source = await daima.huancun.shezhi_Quanju.huoqu_beijing_tupianAsync();

            Windows.UI.Color linshi1 = Windows.UI.Color.FromArgb(daima.huancun.shezhi_Quanju.A, daima.huancun.shezhi_Quanju.R, daima.huancun.shezhi_Quanju.G, daima.huancun.shezhi_Quanju.B);
            color1.Color = linshi1;
            danqian_yanse = textbox2.Text = linshi1.ToString();

            if (daima.huancun.shezhi_Quanju.zhuti_xuanzhe != 2)
            {
                textblock15.Visibility = Visibility.Collapsed;
                textblock16.Visibility = Visibility.Collapsed;
                button4.Visibility = Visibility.Collapsed;
                image4.Visibility = Visibility.Collapsed;
                textbox2.Visibility = Visibility.Collapsed;
                textblock19.Visibility = Visibility.Collapsed;
                textblock20.Visibility = Visibility.Collapsed;
                textblock21.Visibility = Visibility.Collapsed;
                textblock22.Visibility = Visibility.Collapsed;
                textblock23.Visibility = Visibility.Collapsed;
                button5.Visibility = Visibility.Collapsed;
                radio2.Visibility = Visibility.Collapsed;
                radio3.Visibility = Visibility.Collapsed;
                slider2.Visibility = Visibility.Collapsed;
            }
            else
            {
                textblock15.Visibility = Visibility.Visible;
                textblock16.Visibility = Visibility.Visible;
                button4.Visibility = Visibility.Visible;
                image4.Visibility = Visibility.Visible;
                textbox2.Visibility = Visibility.Visible;
                textblock19.Visibility = Visibility.Visible;
                textblock20.Visibility = Visibility.Visible;
                textblock21.Visibility = Visibility.Visible;
                textblock22.Visibility = Visibility.Visible;
                textblock23.Visibility = Visibility.Visible;
                button5.Visibility = Visibility.Visible;
                radio2.Visibility = Visibility.Visible;
                radio3.Visibility = Visibility.Visible; 
                slider2.Visibility = Visibility.Visible;
            }

            if (daima.huancun.shezhi_Quanju.tupian_chuli==0|| daima.huancun.shezhi_Quanju.tupian_chuli == 1)
            {
                textblock21.Visibility = Visibility.Collapsed;
                textblock22.Visibility = Visibility.Collapsed;
                slider2.Visibility = Visibility.Collapsed;
            }
            slider2.Value = daima.huancun.shezhi_Quanju.toumingdu * 100;
            textblock22.Text= slider2.Value + "%";

            radio1.SelectedIndex = daima.huancun.shezhi_Quanju.zhuti_xuanzhe;
            radio2.SelectedIndex = daima.huancun.shezhi_Quanju.tupian_chuli;
            radio3.SelectedIndex = daima.huancun.shezhi_Quanju.wenzi_zhuti;
            toggle2.IsOn = daima.huancun.shezhi_Quanju.shifou_liulanqi_touming;

            //计算缓存大小
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
            (workItem) =>
            {
                jisuan_huancun();
            });

            shifou_jiazai = true;
        }
        //计算缓存大小 以及更新UI
        private async void jisuan_huancun()
        {
            huancun_daxiao = await daima.Gongju.jisuankongjian_wenjianjia(ApplicationData.Current.LocalCacheFolder);
            this.Invoke(() =>
            {
                textblock7.Text = "使用的空间：" + daima.Gongju.zhanyongkongjian(huancun_daxiao) + "/" + daima.Gongju.zhanyongkongjian(daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024);
                progress1.Value = ((double)huancun_daxiao / (double)(daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024)) * 100;
            });
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }


        //分享时保存图片
        private async void toggle1_Toggled(object sender, RoutedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            daima.huancun.shezhi_Quanju.shifou_fenxiang_baocun = toggle1.IsOn;
            textbox1.IsEnabled = toggle1.IsOn;
            button1.IsEnabled = toggle1.IsOn;
            await daima.huancun.shezhi_Quanju.BaocunAsync();
        }
        //选择保存图片的文件夹
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }

            //选择文件夹
            var myPictures = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            StorageFolder newFolder = await myPictures.RequestAddFolderAsync();
            //保存信息
            if (newFolder != null)
            {
                daima.huancun.shezhi_Quanju.baocun_wenjianjia_lujing = newFolder.Path;
                daima.huancun.shezhi_Quanju.baocun_wenjianjia = newFolder.Name;
                await daima.huancun.shezhi_Quanju.BaocunAsync();
            }
            //更新UI
            textbox1.Text = daima.huancun.shezhi_Quanju.baocun_wenjianjia_lujing;
        }
        //打开缓存文件夹
        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalCacheFolder);
        }
        //设置缓存上限
        private void slider1_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            //更新数据
            daima.huancun.shezhi_Quanju.zuida_huancun = (ulong)slider1.Value;
            //更新UI
            textblock9.Text = daima.Gongju.zhanyongkongjian(daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024);
            textblock7.Text = "使用的空间：" + daima.Gongju.zhanyongkongjian(huancun_daxiao) + "/" + daima.Gongju.zhanyongkongjian(daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024);
            progress1.Value = ((double)huancun_daxiao / (double)(daima.huancun.shezhi_Quanju.zuida_huancun * 1024 * 1024)) * 100;

        }
        //设置缓存清理方式
        private async void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            daima.huancun.shezhi_Quanju.huancun_qingli_fangshi = combobox1.SelectedIndex;
            await daima.huancun.shezhi_Quanju.BaocunAsync();
        }
        //清理缓存
        private async void button3_Click(object sender, RoutedEventArgs e)
        {
            //清理缓存
            string jieguo = await daima.Gongju.SanchuwenjianAsync(ApplicationData.Current.LocalCacheFolder, true);
            //计算缓存大小
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
            (workItem) =>
            {
                jisuan_huancun();
            });
            if (jieguo != null)
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "清理缓存失败",
                    Content = "请联系开发人员，详细信息：<" + jieguo + ">",
                    CloseButtonText = "好的"

                };
                await noWifiDialog.ShowAsync();
            }
            else
            {
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = "清理缓存成功",
                    CloseButtonText = "好的"

                };
                await noWifiDialog.ShowAsync();
            }
        }
        //选择背景图片
        private async void button4_Click(object sender, RoutedEventArgs e)
        {
            //选择图片
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    //复制文件到储存位置
                    await daima.huancun.shezhi_Quanju.baocun_beijingtupianAsync(file);
                }
                catch (Exception exc)
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "保存背景图片失败",
                        Content = "请联系开发人员，详细信息：<" + exc.Message + ">",
                        CloseButtonText = "好的"
                    };
                    await noWifiDialog.ShowAsync();
                    return;
                }          
                //处理图片预览
                if (daima.huancun.shezhi_Quanju.tupian_chuli == 0)
                {

                }
                else
                {
                    using (var stream = await file.OpenAsync(FileAccessMode.Read))
                    {
                        BitmapImage img = new BitmapImage();
                        await img.SetSourceAsync(stream);
                        image4.Source = img;
                    }
                }
            }
            else
            {
                //删除背景图片
                await daima.huancun.shezhi_Quanju.baocun_beijingtupianAsync(null);
            }
            //触发事件
            daima.huancun.shezhi.Kaishitishi("更换背景图片", 1);
        }
        //重启
        private async void ToggleThemeTeachingTip2_ActionButtonClick(Microsoft.UI.Xaml.Controls.TeachingTip sender, object args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.RequestRestartAsync("退出");
        }
        //选择主题颜色
        private async void TravelFlyout_Closed(object sender, object e)
        {
            if (color1.Color.ToString() != danqian_yanse)
            {
                //获取颜色
                daima.huancun.shezhi_Quanju.A = color1.Color.A;
                daima.huancun.shezhi_Quanju.R = color1.Color.R;
                daima.huancun.shezhi_Quanju.G = color1.Color.G;
                daima.huancun.shezhi_Quanju.B = color1.Color.B;
                await daima.huancun.shezhi_Quanju.BaocunAsync();
                //显示提示
                ToggleThemeTeachingTip2.IsOpen = true;
                danqian_yanse = color1.Color.ToString();
            }
        }
        //设置图片处理方式
        private async void radio2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            switch (radio2.SelectedIndex)
            {
                case 0:
                    textblock21.Visibility = Visibility.Collapsed;
                    textblock22.Visibility = Visibility.Collapsed;
                    slider2.Visibility = Visibility.Collapsed;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "自动毛玻璃特效还在研究中",
                        Content = "已经为你打开了一个在线毛玻璃特效网站，请先用这个网站处理你的图片，之后再用处理过后的图片作为背景，并把不透明度设为100%",
                        CloseButtonText = "好的"
                    };
                    await noWifiDialog.ShowAsync();
                    await Launcher.LaunchUriAsync(new Uri("https://www.asqql.com/tpmbl/"));
                    radio2.SelectedIndex = daima.huancun.shezhi_Quanju.tupian_chuli;
                    break;
                case 1:
                    textblock21.Visibility = Visibility.Collapsed;
                    textblock22.Visibility = Visibility.Collapsed;
                    slider2.Visibility = Visibility.Collapsed;

                    daima.huancun.shezhi_Quanju.tupian_chuli = radio2.SelectedIndex;
                    daima.huancun.shezhi.Kaishitishi(daima.huancun.shezhi_Quanju.tupian_chuli.ToString(), 2);
                    
                    await daima.huancun.shezhi_Quanju.BaocunAsync();
                    break;
                case 2:
                    textblock21.Visibility = Visibility.Visible;
                    textblock22.Visibility = Visibility.Visible;
                    slider2.Visibility = Visibility.Visible;
                    slider2.Value = daima.huancun.shezhi_Quanju.toumingdu * 100;
                    
                    daima.huancun.shezhi_Quanju.tupian_chuli = radio2.SelectedIndex;
                    daima.huancun.shezhi.Kaishitishi(daima.huancun.shezhi_Quanju.tupian_chuli.ToString(), 2);
                    
                    await daima.huancun.shezhi_Quanju.BaocunAsync();
                    break;
            }
        }
        //设置透明度
        private async void slider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            daima.huancun.shezhi_Quanju.toumingdu = slider2.Value/(double)100;
            textblock22.Text = slider2.Value + "%";
            await daima.huancun.shezhi_Quanju.BaocunAsync();
            //触发事件
            daima.huancun.shezhi.Kaishitishi(daima.huancun.shezhi_Quanju.tupian_chuli.ToString(), 2);
        }
        //更改主题
        private async void radio1_SelectionChanged(int a)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            daima.huancun.shezhi_Quanju.zhuti_xuanzhe = a;
            //修改显示
            if(daima.huancun.shezhi_Quanju.zhuti_xuanzhe!=2)
            {
                textblock15.Visibility = Visibility.Collapsed;
                textblock16.Visibility = Visibility.Collapsed;
                button4.Visibility = Visibility.Collapsed;
                image4.Visibility = Visibility.Collapsed;
                textbox2.Visibility = Visibility.Collapsed;
                textblock19.Visibility = Visibility.Collapsed;
                textblock20.Visibility = Visibility.Collapsed;
                textblock21.Visibility = Visibility.Collapsed;
                textblock22.Visibility = Visibility.Collapsed;
                textblock23.Visibility = Visibility.Collapsed;
                button5.Visibility = Visibility.Collapsed;
                radio2.Visibility = Visibility.Collapsed;
                radio3.Visibility = Visibility.Collapsed;
                slider2.Visibility = Visibility.Collapsed;

            }
            else
            {
                textblock15.Visibility = Visibility.Visible;
                textblock16.Visibility = Visibility.Visible;
                button4.Visibility = Visibility.Visible;
                image4.Visibility = Visibility.Visible;
                textbox2.Visibility = Visibility.Visible;
                textblock19.Visibility = Visibility.Visible;
                textblock20.Visibility = Visibility.Visible;
                textblock21.Visibility = Visibility.Visible;
                textblock22.Visibility = Visibility.Visible;
                textblock23.Visibility = Visibility.Visible;
                button5.Visibility = Visibility.Visible;
                radio2.Visibility = Visibility.Visible;
                radio3.Visibility = Visibility.Visible;
                slider2.Visibility = Visibility.Visible;


            }
            await daima.huancun.shezhi_Quanju.BaocunAsync();
            //触发事件
            daima.huancun.shezhi.Kaishitishi("更换主题", 3);
            //显示提示
            ToggleThemeTeachingTip2.IsOpen = true;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            radio1_SelectionChanged(0);
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            radio1_SelectionChanged(1);
        }

        private void RadioButton_Click_2(object sender, RoutedEventArgs e)
        {
            radio1_SelectionChanged(2);
        }
        //浏览器背景透明
        private async void toggle2_Toggled(object sender, RoutedEventArgs e)
        {
            if (shifou_jiazai == false)
            {
                return;
            }
            daima.huancun.shezhi_Quanju.shifou_liulanqi_touming = toggle2.IsOn;
            await daima.huancun.shezhi_Quanju.BaocunAsync();
        }
    }
}
