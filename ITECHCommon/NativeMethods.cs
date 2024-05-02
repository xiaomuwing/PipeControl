using System;
using System.Runtime.InteropServices;


namespace PipeControl.Common
{
    public static class NativeMethods
    {
        /// <summary>
        /// 多媒体时钟的触发周期支持范围（毫秒）
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TimerCaps
        {
            /// <summary>
            /// 支持的最小周期
            /// </summary>
            public int periodMin;

            /// <summary>
            /// 支持的最大周期
            /// </summary>
            public int periodMax;
        }
        public delegate void TimeProc(int id, int msg, int user, int param1, int param2);// 计时器事件发生时由WINDOWS调用的代理

        [DllImport("winmm.dll")]
        public static extern int timeGetDevCaps(ref TimerCaps caps, int sizeOfTimerCaps);

        // 创建并开始多媒体时钟
        [DllImport("winmm.dll")]
        public static extern int timeSetEvent(int delay, int resolution, TimeProc proc, int user, int mode);

        // 停止和销毁多媒体时钟
        [DllImport("winmm.dll")]
        public static extern int timeKillEvent(int id);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int iTrack, int dxHotspot, int dyHotspot);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragMove(int x, int y);

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern void ImageList_EndDrag();

        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint uPeriod);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint uPeriod);
    }
}
