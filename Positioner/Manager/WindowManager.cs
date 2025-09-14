using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Positioner.Manager
{
    public static class WindowManager
    {
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9;

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public static IEnumerable<IntPtr> GetAllWindowHandles()
        {
            var handles = new List<IntPtr>();
            EnumWindows((hWnd, lParam) =>
            {
                handles.Add(hWnd);
                return true;
            }, IntPtr.Zero);
            return handles;
        }

        public static IEnumerable<IntPtr> GetWindowHandlesForProcess(int processId)
        {
            var handles = new List<IntPtr>();
            EnumWindows((hWnd, lParam) =>
            {
                GetWindowThreadProcessId(hWnd, out uint windowProcId);
                if (windowProcId == processId)
                    handles.Add(hWnd);
                return true;
            }, IntPtr.Zero);
            return handles;
        }

        public static void MoveWindowHandle(IntPtr hWnd, double x, double y, double width, double height)
        {
            ShowWindow(hWnd, SW_RESTORE);
            MoveWindow(hWnd, (int)x, (int)y, (int)width, (int)height, true);
        }
    }
}