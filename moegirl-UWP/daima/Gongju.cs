using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
