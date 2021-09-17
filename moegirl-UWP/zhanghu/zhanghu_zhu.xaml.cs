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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace moegirl_UWP.zhanghu
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class zhanghu_zhu : Page
    {
        int danqian_xuanze = 0;
        private bool shifou_jiazai=false;

        public zhanghu_zhu()
        {
            this.InitializeComponent();
            Loaded += Zhanghu_zhu_Loaded;
        }

        private async void Zhanghu_zhu_Loaded(object sender, RoutedEventArgs e)
        {
            //先加载缓存数据
            textblock1.Text = daima.huancun.zhanghao.yonghuming;
            textblock2.Text = daima.huancun.zhanghao.gexingqianming;
            textblock3.Text = "用户组：" + daima.huancun.zhanghao.yonghuzhu;
            textblock4.Text = "编辑数：" + daima.huancun.zhanghao.bianjishu;
            textblock5.Text = "注册时间：" + daima.huancun.zhanghao.zhuceshijian;
            textblock6.Text = "语言：" + daima.huancun.zhanghao.yuyan;
            textblock7.Text = "电子邮件：" + daima.huancun.zhanghao.dianzi_youjian;
            person1.ProfilePicture = await daima.huancun.zhanghao.huoqu_beijing_tupianAsync();
            //异步刷新数据
            IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
            (workItem) =>
            {
                shuaxin_shuju();
                
            });

        }
        /// <summary>
        /// 数据刷新
        /// </summary>
        private void shuaxin_shuju()
        {
            this.Invoke(() =>
            {
                webview1.Navigate(new Uri(daima.huancun.xinxi_wangzhi));
            });
            dengdai_wangye();
            //检查是否已经登入  
            this.Invoke(async () =>
            {
                try
                {
                    await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-createaccount-cta').toString();") });
                    //登入失效
                    daima.huancun.zhanghao.shifou_dengru = false;
                    await daima.huancun.zhanghao.BaocunAsync();
                    // 跳转页面 
                    Frame.Navigate(typeof(dengru));
                }
                catch
                {
                    //成功登入
                    try
                    {
                        daima.huancun.zhanghao.yonghuming = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[0].innerText;") });
                        daima.huancun.zhanghao.yonghuzhu = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[1].innerText;") });
                        daima.huancun.zhanghao.bianjishu = int.Parse(await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[2].innerText;") }));
                        daima.huancun.zhanghao.zhuceshijian = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[3].innerText;") });
                        daima.huancun.zhanghao.yuyan = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('mw-input-wplanguage').value;") });
                        if (daima.huancun.zhanghao.yuyan == "zh-cn")
                        {
                            daima.huancun.zhanghao.yuyan = "zh-CN - 中文（中国大陆）";
                        }
                        daima.huancun.zhanghao.gexingqianming = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[9].innerText;") });
                        string linshi = await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[12].innerText;") });
                        string jieguo = "";
                        bool shifou_dian = false;
                        for (int i = 0; i < linshi.Length; i++)
                        {
                            if (linshi[i] == '.')
                            {
                                shifou_dian = true;
                            }
                            if (linshi[i] == 'm'&&shifou_dian==true)
                            {
                                jieguo += linshi[i];
                                break;
                            }
                            jieguo += linshi[i];
                        }
                        daima.huancun.zhanghao.dianzi_youjian = jieguo;
                        textblock1.Text = daima.huancun.zhanghao.yonghuming;
                        textblock2.Text = daima.huancun.zhanghao.gexingqianming;
                        textblock3.Text = "用户组：" + daima.huancun.zhanghao.yonghuzhu;
                        textblock4.Text = "编辑数：" + daima.huancun.zhanghao.bianjishu;
                        textblock5.Text = "注册时间：" + daima.huancun.zhanghao.zhuceshijian;
                        textblock6.Text = "语言：" + daima.huancun.zhanghao.yuyan;
                        textblock7.Text = "电子邮件：" + daima.huancun.zhanghao.dianzi_youjian;
                        //刷新头像
                        await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('mw-input')[5].children[1].click();") });
                        //导航到用户简介
                        ContentFrame.Navigate(typeof(jianjie));
                        IAsyncAction asyncAction = Windows.System.Threading.ThreadPool.RunAsync(
                        async (workItem) =>
                        {
                            dengdai_wangye();
                            try
                            {
                                //保存
                                await daima.huancun.zhanghao.BaocunAsync();
                                this.Invoke(async () =>
                                {
                                    try
                                    {
                                        StorageFile file = await daima.Tupian_huancun.xiazai_tupianAsync(new Uri(await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementsByClassName('current-avatar')[0].src") })), "touxiang.jpg",true);
                                        await daima.huancun.zhanghao.baocun_beijingtupianAsync(file);
                                        person1.ProfilePicture = await daima.huancun.zhanghao.huoqu_beijing_tupianAsync();
                                    }
                                    catch{ }
                                });
                            }
                            catch { }
                        });
                        return;
                    }
                    catch
                    {

                    }
                }
            });
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

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            xianshi_beijing(pivot1.SelectedIndex);
            danqian_xuanze = pivot1.SelectedIndex;
            switch (pivot1.SelectedIndex)
            {
                case 0:
                    if (daima.huancun.zhanghao.yonghuming != "未命名用户-12138")
                    {
                        ContentFrame.Navigate(typeof(jianjie));
                    }
                  
                    break;
                case 1:
                    ContentFrame.Navigate(typeof(liuyan));
                    break;
                case 2:
                    ContentFrame.Navigate(typeof(lishi_jilu));
                    break;
            }
        }

        public async void Invoke(Action action, Windows.UI.Core.CoreDispatcherPriority Priority = Windows.UI.Core.CoreDispatcherPriority.Normal)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Priority, () => { action(); });
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

        private void button4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(sheding));
        }

        private async void button6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await webview1.InvokeScriptAsync("eval", new string[] { String.Format("document.getElementById('pt-logout').children[0].click();") });
                await WebView.ClearTemporaryWebDataAsync();
                //登入失效
                daima.huancun.zhanghao.shifou_dengru = false;
                await daima.huancun.zhanghao.BaocunAsync();
                // 跳转页面 
                Frame.Navigate(typeof(dengru));
            }
            catch
            {

            }
        }

        private void webview1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {

        }

        private void webview1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            shifou_jiazai = true;
        }
    }
}
