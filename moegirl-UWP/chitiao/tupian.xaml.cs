using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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

namespace moegirl_UWP.chitiao
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class tupian : Page
    {
        /// <summary>
        /// 是否加载完成
        /// </summary>
        bool shifou_jiazai = true;
        /// <summary>
        /// 是否获取到图片地址
        /// </summary>
        bool shifou_huoqu_tupian = true;
        string dangqian_tupian_lianjie = "";
        string danqian_lianjie = "";
        public ObservableCollection<Tupian_xinxi> tupian_liebiao = new ObservableCollection<Tupian_xinxi>();
        public tupian()
        {
            this.InitializeComponent();
            daima.huancun.chitiao.Xianshitishi += Chitiao_Xianshitishi;
            NavigationCacheMode = NavigationCacheMode.Enabled;


            //列表绑定集合
            gridview1.ItemsSource = tupian_liebiao;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)//重写
        {
            //判断当前显示的内容是否和缓存一致
            if (danqian_lianjie != daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1])
            {
                //判断是否为空
                if (daima.huancun.chitiao.tupian_liebiao != null)
                {
                    //清空
                    tupian_liebiao.Clear();
                    IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                        (workItem) =>
                        {
                            //遍历列表
                            foreach (daima.tupian_xinxi a in daima.huancun.chitiao.tupian_liebiao)
                            {/*
                                //处理链接
                                string linshi = "https://commons.moegirl.org.cn/File:" + a;
                                //跳转网页
                                this.Invoke(() =>
                                {
                                    webview1.Navigate(new Uri(linshi));
                                });
                                //等待
                                dengdai_wangye();
                                //点击图片
                                this.Invoke(async () =>
                                {
                                    await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByTagName('img')[0].click()") });
                                });
                                //等待
                                dengdai_tupian();

                                this.Invoke(() =>
                                {
                                    try
                                    {
                                        //解析
                                        BitmapImage bitmapImage = new BitmapImage();
                                        bitmapImage.UriSource = new Uri(dangqian_tupian_lianjie);

                                        //添加
                                        Tupian_xinxi tupian_Xinxi = new Tupian_xinxi();
                                        tupian_Xinxi.Biaoti = a;
                                        tupian_Xinxi.Tupianlianjie = bitmapImage;
                                        tupian_liebiao.Add(tupian_Xinxi);

                                    }
                                    catch
                                    {

                                    }
                                });
                                */
                                //解析
                                this.Invoke(() =>
                                {

                                    BitmapImage bitmapImage = new BitmapImage();
                                    bitmapImage.UriSource = new Uri(a.suoluetu);

                                    //添加
                                    Tupian_xinxi tupian_Xinxi = new Tupian_xinxi();
                                    tupian_Xinxi.Biaoti = a.biaoti;
                                    tupian_Xinxi.suoluetu = a.suoluetu;
                                    tupian_Xinxi.Tupianlianjie = bitmapImage;
                                    tupian_liebiao.Add(tupian_Xinxi);
                                });
                            }

                        });

                }
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
        /// 等待网页加载完毕
        /// </summary>
        private void dengdai_tupian()
        {
            //等待加载
            shifou_huoqu_tupian = false;
            int jishu = 0;
            while (shifou_huoqu_tupian == false && jishu < 30)
            {
                System.Threading.Thread.Sleep(100);
                jishu++;
            }
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
        }

        private async void Chitiao_Xianshitishi(string a, int xuhao)
        {
            switch (xuhao)
            {
                case 5:
                    if (a == "2")
                    {
                        //判断是否在显示图片详情
                        if (re1.Visibility == Visibility.Visible)
                        {
                            re1.Visibility = Visibility.Collapsed;
                            gridview1.Visibility = Visibility.Visible;
                            image2.Source = null;
                        }
                        else
                        {
                            if (daima.huancun.chitiao.daohan_duifa.Count > 1)
                            {
                                //复制导航记录
                                string linshi = daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1];
                                //删除导航记录
                                daima.huancun.chitiao.daohan_duifa.RemoveAt(daima.huancun.chitiao.daohan_duifa.Count - 1);
                                //触发事件
                                daima.huancun.chitiao.Kaishitishi("跳转", 2);
                            }
                            else
                            {
                                daima.huancun.chitiao.Kaishitishi("整体向后导航", 8);
                            }
                        }
                    }
                    break;
                case 6:
                    //复制链接
                    if (a == "2")
                    {
                        //判断是否在图片详情中
                        if (re1.Visibility == Visibility.Visible)
                        {
                            DataPackage dp = new DataPackage();
                            Uri uri = new Uri(dangqian_tupian_lianjie);
                            StorageFile file = await daima.Tupian_huancun.xiazai_tupianAsync(uri, tupian_liebiao[gridview1.SelectedIndex].Biaoti);
                            dp.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
                            //检查是否需要保存图片
                            if (daima.huancun.shezhi_Quanju.shifou_fenxiang_baocun == true)
                            {
                                baocun_tupian(file);
                            }
                            ContentDialog noWifiDialog = new ContentDialog
                            {
                                Title = "分享图片成功",
                                Content = "图片已经保存到本地，并且复制到剪切板中了，快去分享给朋友吧。",
                                CloseButtonText = "好的"

                            };
                            await noWifiDialog.ShowAsync();
                            Clipboard.SetContent(dp);
                        }
                        else
                        {
                            DataPackage dp = new DataPackage();
                            dp.SetText(daima.huancun.chitiao.daohan_duifa[daima.huancun.chitiao.daohan_duifa.Count - 1]);
                            Clipboard.SetContent(dp);
                            daima.huancun.chitiao.Kaishitishi("分享成功", 7);
                        }
                    }
                    break;
            }
        }
        private async void baocun_tupian(StorageFile file)
        {
            //获取文件夹
            var myPictures = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            //获取保存到的文件夹
            IObservableVector<StorageFolder> myPictureFolders = myPictures.Folders;
            StorageFolder wenjianjia = null;
            foreach(var item in myPictureFolders)
            {
                if(item.Name==daima.huancun.shezhi_Quanju.baocun_wenjianjia)
                {
                    wenjianjia = item;
                    break;
                }
            }
            if(wenjianjia==null)
            {
                daima.huancun.shezhi_Quanju.baocun_wenjianjia = "Pictures";
                await daima.huancun.shezhi_Quanju.BaocunAsync();
                return;
            }
            //复制文件
            await file.CopyAsync(wenjianjia, tupian_liebiao[gridview1.SelectedIndex].Biaoti, NameCollisionOption.ReplaceExisting);
        }
        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
            dangqian_tupian_lianjie = args.Uri.AbsoluteUri;
            shifou_huoqu_tupian = true;
        }
        private void gridview1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //判断是否为空
            if (tupian_liebiao.Count == 0)
            {
                return;
            }
            //启动进度条
            re1.Visibility = Visibility.Visible;
            gridview1.Visibility = Visibility.Collapsed;
            progress1.IsActive = true;
            //获取图片标题
            string biaoti_linshi = tupian_liebiao[gridview1.SelectedIndex].Biaoti;
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                 (workItem) =>
                 {

                     //处理链接
                     string linshi = "https://commons.moegirl.org.cn/File:" + biaoti_linshi;
                     //跳转网页
                     this.Invoke(() =>
                     {
                         webview1.Navigate(new Uri(linshi));
                     });
                     //等待
                     dengdai_wangye();
                     //点击图片
                     this.Invoke(async () =>
                     {
                         try
                         {
                             await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('internal')[0].click();") });
                         }
                         catch
                         {
                             webview1.Navigate(new Uri(tupian_liebiao[gridview1.SelectedIndex].suoluetu));
                         }
                     });
                     //等待
                     dengdai_tupian();

                     this.Invoke(async () =>
                     {
                         try
                         {
                             //解析
                             StorageFile file = await daima.Tupian_huancun.xiazai_tupianAsync(new Uri(dangqian_tupian_lianjie), biaoti_linshi);
                             using (var stream = await file.OpenAsync(FileAccessMode.Read))
                             {
                                 BitmapImage img = new BitmapImage();
                                 await img.SetSourceAsync(stream);
                                 //添加
                                 image2.Source = img;

                             }
                             //关闭进度条
                             progress1.IsActive = false;
                         }
                         catch
                         {

                         }
                     });
                 });

        }
    }
    /// <summary>
    /// 图片列表信息
    /// </summary>
    public class Tupian_xinxi
    {
        /// <summary>
        /// 图片内容
        /// </summary>
        public BitmapImage Tupianlianjie { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Biaoti { get; set; }
        /// <summary>
        /// 缩略图链接
        /// </summary>
        public string suoluetu = "52";
    }
}
