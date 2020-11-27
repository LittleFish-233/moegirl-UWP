using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace moegirl_UWP.daima
{
    /// <summary>
    /// 用于下载图片 读取缓存图片
    /// </summary>
    public static class Tupian_huancun
    {
        /// <summary>
        /// 获取图片
        /// 如果本地存在，就获取本地
        /// 如果本地不存在，获取网络
        /// </summary>
        /// <param name="lianjie">链接</param>
        /// <returns>文件</returns>
        public static async Task<StorageFile> xiazai_tupianAsync(Uri uri,string wenjianming,bool shifou_wangluo_bixu=false)
        {
            if (shifou_wangluo_bixu == true)
            {
                StorageFile file = await GetHttpImage(uri, wenjianming);
                return file;
            }
            else
            {
                StorageFile file=await GetLoacalFolderImage(uri, wenjianming);
                if(file==null)
                {
                    file = await GetHttpImage(uri, wenjianming);
                }
                return file;
            }
        }

        /// <summary>
        /// 从本地获取图片
        /// </summary>
        /// <param name="uri"></param>
        private static async Task<StorageFile> GetLoacalFolderImage(Uri uri, string wenjianming)
        {
            StorageFolder folder = await GetImageFolder();
            try
            {
                StorageFile file = await folder.GetFileAsync(wenjianming);
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 从网络获取图片
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static async Task<StorageFile> GetHttpImage(Uri uri, string wenjianming)
        {
            try
            {
                Windows.Web.Http.HttpClient http = new Windows.Web.Http.HttpClient();
                IBuffer buffer = await http.GetBufferAsync(uri);
                BitmapImage img = new BitmapImage();
                using (IRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await stream.WriteAsync(buffer);
                    stream.Seek(0);
                    await img.SetSourceAsync(stream);                  
                    return await StorageImageFolderAsync(stream, uri,wenjianming);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 将缓冲区写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static async Task<StorageFile> StorageImageFolderAsync(IRandomAccessStream stream, Uri uri,string wenjianming)
        {
            StorageFolder folder = await GetImageFolder();
            try
            {
                StorageFile file = await folder.CreateFileAsync(wenjianming,CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteBytesAsync(file, await ConvertIRandomAccessStreamByte(stream));
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 转换缓冲区格式
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static async Task<byte[]> ConvertIRandomAccessStreamByte(IRandomAccessStream stream)
        {
            DataReader read = new DataReader(stream.GetInputStreamAt(0));
            await read.LoadAsync((uint)stream.Size);
            byte[] temp = new byte[stream.Size];
            read.ReadBytes(temp);
            return temp;
        }
        /// <summary>
        /// 获取图片文件夹
        /// </summary>
        /// <returns></returns>
        private static async Task<StorageFolder> GetImageFolder()
        {
           //文件夹
            string name = "image";
            StorageFolder folder = null;
            //从本地获取文件夹
            try
            {
                folder = await ApplicationData.Current.LocalCacheFolder.GetFolderAsync(name);
            }
            catch (FileNotFoundException)
            {
               //没找到
                folder = await ApplicationData.Current.LocalCacheFolder.
                    CreateFolderAsync(name);
            }
            return folder;
        }
        /// <summary>
        /// 计算文件是否匹配
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Md5(string str)
        {
            HashAlgorithmProvider hashAlgorithm =
                 HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            CryptographicHash cryptographic = hashAlgorithm.CreateHash();
            IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            cryptographic.Append(buffer);
            return CryptographicBuffer.EncodeToHexString(cryptographic.GetValueAndReset());
        }
    }
}
