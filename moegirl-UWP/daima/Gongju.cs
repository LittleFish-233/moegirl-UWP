using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace moegirl_UWP.daima
{
    public static class Gongju
    {
        /// <summary>
        /// 相当于暂停时间
        /// </summary>
        /// <param name="ms">要暂停多少毫秒</param>
        public static void Sleep(int ms)
        {
            new System.Threading.ManualResetEvent(false).WaitOne(ms);
        }
        /// <summary>
        /// 检查字符串是否以其结尾
        /// </summary>
        /// <param name="mubiao">要检查的字符串</param>
        /// <param name="jiewei">匹配的字符串</param>
        /// <returns>是否匹配</returns>
        public static bool shifou_jiewei(string mubiao, string jiewei)
        {
            if (mubiao.Length < jiewei.Length)
            {
                return false;
            }
            else
            {
                for (int i = 1; i <= jiewei.Length; i++)
                {
                    if (mubiao[mubiao.Length - i] != jiewei[jiewei.Length - i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns>扩展名</returns>
        public static string huoqu_kuozhangming(string str)
        {
            string linshi = "";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                linshi += str[i];
            }
            string jieguo = "";
            for (int a = 0; a < linshi.Length; a++)
            {
                jieguo += linshi[linshi.Length - a - 1];
            }
            return jieguo;
        }
        /// <summary>
        /// 将字节数，转化成带单位的大小   样式 11GB,500MB  2  1.48GB
        /// </summary>
        /// <param name="gb"></param>
        /// <param name="mb"></param>
        /// <param name="kb"></param>
        /// <param name="zijie"></param>
        /// <param name="yangshi"></param>
        /// <returns></returns>
        public static string zhanyongkongjian(ulong gb, ulong mb, ulong kb, ulong zijie, int yangshi = 2)
        {
            switch (yangshi)
            {
                case 1:
                    return zhanyonglongjian_1(gb, mb, kb, zijie);
                case 2:
                    return zhanyongkongjian_2(gb, mb, kb, zijie);
                default:
                    break;
            }
            return "0 B";
        }
        /// <summary>
        /// 将字节数，转化成带单位的大小   样式 11GB,500MB  2  1.48GB
        /// </summary>
        /// <param name="zijie"></param>
        /// <param name="yangshi"></param>
        /// <returns></returns>
        public static string zhanyongkongjian(ulong zijie, int yangshi = 2)
        {
            if (zijie < 0)
            {
                return "无";
            }
            if (zijie == 0)
            {
                return "无";
            }
            ulong kb = 0;
            ulong mb = 0;
            ulong gb = 0;
            kb = zijie / 1024;
            zijie = zijie % 1024;
            mb = kb / 1024;
            kb = kb % 1024;
            gb = mb / 1024;
            mb = mb % 1024;
            return zhanyongkongjian(gb, mb, kb, zijie, yangshi);
        }
        /// <summary>
        /// 将字节数，转化成带单位的大小 样式一
        /// </summary>
        /// <param name="gb"></param>
        /// <param name="mb"></param>
        /// <param name="kb"></param>
        /// <param name="zijie"></param>
        /// <returns></returns>
        private static string zhanyonglongjian_1(ulong gb, ulong mb, ulong kb, ulong zijie)
        {
            if (gb < 0 || mb < 0 || kb < 0 || zijie < 0)
            {
                return "无";
            }
            if (gb == 0 && mb == 0 && kb == 0 && zijie == 0)
            {
                return "无";
            }
            string gb_;
            string mb_;
            string kb_;
            string zijie_;
            if (gb == 0)
            {
                gb_ = "";
            }
            else
            {
                gb_ = gb.ToString() + " GB, ";

            }
            if (mb == 0)
            {
                mb_ = "";
            }
            else
            {
                mb_ = mb.ToString() + " MB, ";

            }
            if (kb == 0)
            {
                kb_ = "";
            }
            else
            {
                kb_ = kb.ToString() + " KB, ";
            }
            if (zijie == 0)
            {
                zijie_ = "";
            }
            else
            {
                zijie_ = zijie.ToString() + " B, ";
            }
            return gb_ + mb_ + kb_ + zijie_;
        }
        /// <summary>
        /// 将字节数，转化成带单位的大小 样式二
        /// </summary>
        /// <param name="gb_"></param>
        /// <param name="mb_"></param>
        /// <param name="kb_"></param>
        /// <param name="zijie_"></param>
        /// <returns></returns>
        private static string zhanyongkongjian_2(ulong gb_, ulong mb_, ulong kb_, ulong zijie_)
        {
            double gb = gb_;
            double mb = mb_;
            double kb = kb_;
            double zijie = zijie_;
            if (gb < 0 || mb < 0 || kb < 0 || zijie < 0)
            {
                return "0 B";
            }
            if (gb == 0 && mb == 0 && kb == 0 && zijie == 0)
            {
                return "0 B";
            }
            double xiaoshu = 0;
            if (gb == 0 && mb == 0 && kb == 0)
            {
                return zijie.ToString() + " B";
            }
            else
            {
                if (gb == 0 && mb == 0)
                {
                    xiaoshu = 0;
                    xiaoshu = zijie / 1024;
                    xiaoshu = kb + xiaoshu;
                    xiaoshu = Math.Round(xiaoshu, 2, MidpointRounding.AwayFromZero);
                    return xiaoshu.ToString() + " KB";
                }
                else
                {
                    if (gb == 0)
                    {
                        xiaoshu = 0;
                        xiaoshu = zijie / 1024;
                        xiaoshu = (kb + xiaoshu) / 1024;
                        xiaoshu = mb + xiaoshu;
                        xiaoshu = Math.Round(xiaoshu, 2, MidpointRounding.AwayFromZero);
                        return xiaoshu.ToString() + " MB";
                    }
                    else
                    {
                        xiaoshu = 0;
                        xiaoshu = zijie / 1024;
                        xiaoshu = (kb + xiaoshu) / 1024;
                        xiaoshu = (mb + xiaoshu) / 1024;
                        xiaoshu = gb + xiaoshu;
                        xiaoshu = Math.Round(xiaoshu, 2, MidpointRounding.AwayFromZero);
                        return xiaoshu.ToString() + " GB";
                    }
                }
            }
        }
        /// <summary>
        /// 计算文件夹的大小
        /// </summary>
        /// <param name="wenjianjia">目标文件夹</param>
        /// <returns>大小</returns>
        public static async Task<ulong> jisuankongjian_wenjianjia(StorageFolder wenjianjia)
        {
            IReadOnlyList<StorageFile> wenjianliebiao = null;
            IReadOnlyList<StorageFolder> wenjianjialiebiao = null;
            ulong changdu = 0;
            //第一步，获取文件夹中的所有文件夹和文件
            try
            {
                wenjianliebiao = await wenjianjia.GetFilesAsync();
                if (wenjianliebiao.Count != 0)
                {
                    int jishu = 0;
                    while (jishu < wenjianliebiao.Count)
                    {
                        StorageFile linshi = await wenjianjia.GetFileAsync(wenjianliebiao[jishu].Name);
                        var li = await linshi.GetBasicPropertiesAsync();
                        changdu = li.Size + changdu;
                        jishu++;
                    }
                }
                wenjianjialiebiao = await wenjianjia.GetFoldersAsync();
                if (wenjianjialiebiao.Count != 0)
                {
                    int jishu = 0;
                    while (jishu < wenjianjialiebiao.Count)
                    {
                        StorageFolder linshi = await wenjianjia.GetFolderAsync(wenjianjialiebiao[jishu].Name);
                        ulong size = await jisuankongjian_wenjianjia(linshi);
                        changdu = size + changdu;
                        jishu++;
                    }
                }
                return changdu;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除一个文件夹里的所有文件 不包括子文件夹
        /// </summary>
        /// <param name="wenjianjia">目标文件夹</param>
        /// <returns></returns>
        public static async Task<string> SanchuwenjianAsync(StorageFolder wenjianjia, bool shifou_ziwenjianjia)
        {
            try
            {
                //获取文件夹
                if (shifou_ziwenjianjia == true)
                {
                    IReadOnlyList<StorageFolder> wenjianjialiebiao = null;
                    wenjianjialiebiao = await wenjianjia.GetFoldersAsync();

                    foreach (var item in wenjianjialiebiao)
                    {
                        string jieguo = await SanchuwenjianAsync(item, true);
                        if (jieguo != null)
                        {
                            return jieguo;
                        }
                    }
                }
                //获取文件列表
                IReadOnlyList<StorageFile> wenjianliebiao = null;
                wenjianliebiao = await wenjianjia.GetFilesAsync();
                //删除
                int jishu = 0;
                while (wenjianliebiao.Count > jishu)
                {
                    await wenjianliebiao[jishu].DeleteAsync();
                    jishu++;
                }
                return null;
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
        }
        /// <summary>
        /// [颜色：16进制转成RGB]
        /// </summary>
        /// <param name="strColor">设置16进制颜色 [返回RGB]</param>
        /// <returns></returns>
        public static System.Drawing.Color colorHx16toRGB(string strHxColor)
        {
            try
            {
                if (strHxColor.Length == 0)
                {//如果为空
                    return System.Drawing.Color.FromArgb(0, 0, 0);//设为黑色
                }
                else
                {//转换颜色
                    return System.Drawing.Color.FromArgb(System.Int32.Parse(strHxColor.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                }
            }
            catch
            {//设为黑色
                return System.Drawing.Color.FromArgb(0, 0, 0);
            }
        }
    }
}
