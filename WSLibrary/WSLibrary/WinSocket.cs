using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace WSLibrary
{
    public class WinSocket
    {
        public struct Sockaddr
        {
            public short sin_family;
            public short sin_port;
            public int sin_addr;
            public long sin_zero;
        }

        [DllImport("ws2_32.dll", SetLastError = true)]
        private static extern IntPtr Socket(Int32 af, Int32 type, Int32 protocol);

        [DllImport("ws2_32.dll")]
        private static extern int Bind(IntPtr socket, ref Sockaddr addr, int namelen);

        [DllImport("ws2_32.dll", SetLastError = true)]
        private static extern int Recvfrom(IntPtr socketHandle, byte[] pinnedBuffer, int len, SocketFlags socketFlags, Sockaddr SockAdd, int Size);

        [DllImport("ws2_32.dll", SetLastError = true)]
        private static extern int Sendto(IntPtr socketHandle, byte[] pinnedBuffer, int len, SocketFlags socketFlags, ref Sockaddr SockAdd, int Size);

        [DllImport("ws2_32.dll")]
        public static extern short Htons(int hostshort);

        [DllImport("wsock32.dll")]
        public static extern int Inet_addr(string cp);

        [DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 WSAGetLastError();

        public static int SendTo(IntPtr socketHandle, byte[] pinnedBuffer, int len, SocketFlags socketFlags, ref Sockaddr SockAdd, int Size)
        {
            return Sendto(socketHandle, pinnedBuffer, len, socketFlags, ref SockAdd, Size);
        }

        public static int RecvFrom(IntPtr socketHandle, byte[] pinnedBuffer, int len, SocketFlags socketFlags, Sockaddr SockAdd, int Size)
        {
            return Recvfrom(socketHandle, pinnedBuffer, len, socketFlags, SockAdd, Size);
        }

        public static IntPtr Socket(int sock_stream, Int32 protocol)
        {
            return Socket(AF_INET, sock_stream, protocol);
        }


        public static int AdvBind(string ipAddress, int port, IntPtr socketHandle)
        {
            Sockaddr remoteAddress;
            int resultCode = -1;
            int errorCode = 0;

            if (socketHandle != IntPtr.Zero)
            {
                remoteAddress = new Sockaddr();
                remoteAddress.sin_family = AF_INET;
                remoteAddress.sin_port = Htons((short)port);
                remoteAddress.sin_addr = Inet_addr(ipAddress);
                remoteAddress.sin_zero = 0;

                if (remoteAddress.sin_addr != 0)
                {
                    resultCode = Bind(socketHandle, ref remoteAddress, Marshal.SizeOf(remoteAddress));
                    errorCode = WSAGetLastError();
                }
            }
            return resultCode;
        }
    }  
}
