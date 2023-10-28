using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace ModernUI
{
    /// <summary>
    /// This utility class holds auxiliary functions.
    /// </summary>
    internal static partial class Utils
    {
        /// <summary>
        /// Returns a darker version of the specified color.
        /// </summary>
        /// <param name="color">The color to be darkened.</param>
        /// <returns>A darker version of the specified color.</returns>
        public static Color DarkenColor(Color color) => Color.FromArgb(color.A >> 1, color.R >> 1, color.G >> 1, color.B >> 1);

        /// <summary>
        /// Returns a lighter version of the specified color.
        /// </summary>
        /// <param name="color">The color to be lightened.</param>
        /// <returns>A lighter version of the specified color.</returns>
        public static Color LightenColor(Color color) => Color.FromArgb(LightenValue(color.A, 1.5), LightenValue(color.R, 1.5), LightenValue(color.G, 1.5), LightenValue(color.B, 1.5));

        private static byte LightenValue(byte value, double multiplier)
        {
            var x = (int)(value == 0 ? byte.MaxValue / multiplier : value * multiplier);
            return x > byte.MaxValue ? byte.MaxValue : (byte)x;
        }

        /// <summary>
        /// Displays the system menu of the specified <see cref="Form"/>, at the specified coordinates.
        /// </summary>
        /// <param name="frm">The form to display the system menu.</param>
        /// <param name="X">The X coordinate where to show the system menu.</param>
        /// <param name="Y">The Y coordinate where to show the system menu.</param>
        public static void ShowSystemMenu(this FormBase frm, int X, int Y)
        {
            var cmd = Native.TrackPopupMenuEx(frm.SystemMenuHanle, Native.TPM_LEFTALIGN | Native.TPM_RETURNCMD, X, Y, frm.Handle, IntPtr.Zero);
            if (cmd != 0)
                Native.SendMessage(frm.Handle, Native.WM_SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
        }

        /// <summary>
        /// Displays the system menu of the specified <see cref="Form"/>, at the specified coordinates.
        /// </summary>
        /// <param name="frm">The form to display the system menu.</param>
        /// <param name="p">A <see cref="Point"/> that indicates where to show the system menu.</param>
        public static void ShowSystemMenu(this FormBase frm, Point p) => ShowSystemMenu(frm, p.X, p.Y);

        internal static partial class Native
        {
            [LibraryImport("user32.dll")]
            internal static partial IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

            [LibraryImport("user32.dll")]
            internal static partial int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

            [LibraryImport("user32.dll", EntryPoint = "SendMessageA")]
            internal static partial IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            internal const uint TPM_LEFTALIGN = 0x0000;
            internal const uint TPM_RETURNCMD = 0x0100;
            internal const uint WM_SYSCOMMAND = 0x0112;
        }
    }
}
