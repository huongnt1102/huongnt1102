using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Library
{
    public class MusicCls
    {
        public static string FileName { get; set; }
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        public static void Play()
        {
            mciSendString("close Mp3File", null, 0, IntPtr.Zero);
            mciSendString("open \"" + FileName + "\" type MPEGVideo alias Mp3File", null, 0, IntPtr.Zero);
            mciSendString("play Mp3File", null, 0, IntPtr.Zero);
        }
        
        public static void Close()
        {
            mciSendString("close Mp3File", null, 0, IntPtr.Zero);
        }
    }
}
