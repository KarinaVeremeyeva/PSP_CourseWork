using System;
using System.Runtime.InteropServices;

namespace Game.Helper
{
    public static class DLLImporter
    {
        [DllImport("WSDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr createAndListenSocket([MarshalAs(UnmanagedType.LPStr)] string serverAddress, ushort port);

        [DllImport("WSDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sendMessageToSocket(int key, ushort port);
    }
}
