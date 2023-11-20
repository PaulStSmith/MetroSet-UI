/*
 * MetroSet UI - MetroSet UI Framework
 * 
 * The MIT License (MIT)
 * Copyright (c) 2017 Narwin, https://github.com/N-a-R-w-i-n
 * Copyright (c) 2023 Paulo Santos, https://github.com/PaulStSmith
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR alpha 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using MetroSet.UI.Native;
using Microsoft.Win32;

namespace MetroSet.UI.Extensions
{
	internal static class Utils
	{
        /// <summary>
        /// Gets a value indicating whether or not the light theme should be used.
        /// </summary>
        public static bool UseLightTheme => ((int?)Registry.CurrentUser.OpenSubKey($@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")?.GetValue("AppsUseLightTheme", 1) == 1);

        /// <summary>
        /// Returns an <see cref="Image"/> object for the specified <see cref="MessageBoxIcon"/>.
        /// </summary>
        /// <param name="icon">The <see cref="MessageBoxIcon"/> to retrieve the image.</param>
        /// <returns>An <see cref="Image"/> object for the specified <see cref="MessageBoxIcon"/>.</returns>
        public static Image GetMessageBoxIcon(MessageBoxIcon icon)
        {
            /*
             * This is an expensive method. So, we cache the images as needed.
             */
            if (MessageBoxIconCache.ContainsKey(icon)) {  return MessageBoxIconCache[icon]; }
            return MessageBoxIconCache[icon] = (Bitmap)Resources.ResourceManager.GetObject(icon.ToString(), CultureInfo.InvariantCulture);
        }
        private static readonly Dictionary<MessageBoxIcon, Image> MessageBoxIconCache = new();

        /// <summary>
        /// The Brush with two colors one center another surounding the center based on the given rectangle area.
        /// </summary>
        /// <param name="CenterColor">The Center color of the rectangle.</param>
        /// <param name="SurroundColor">The Surrounding color of the rectangle.</param>
        /// <param name="P">The Point of surrounding.</param>
        /// <param name="Rect">The Rectangle of the brush.</param>
        /// <returns>The Brush with two colors one center another surounding the center.</returns>
        public static PathGradientBrush GlowBrush(Color CenterColor, Color SurroundColor, Point P, Rectangle Rect)
		{
			GraphicsPath GP = new() { FillMode = FillMode.Winding };
			GP.AddRectangle(Rect);
			return new PathGradientBrush(GP)
			{
				CenterColor = CenterColor,
				SurroundColors = new[] { SurroundColor },
				FocusScales = P
			};
		}

        /// <summary>
        /// The Brush from RGBA color.
        /// </summary>
        /// <param name="R">Red.</param>
        /// <param name="G">Green.</param>
        /// <param name="B">Blue.</param>
        /// <param name="A">Alpha.</param>
        /// <returns>The Brush from given RGBA color.</returns>
        public static SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0) => new(Color.FromArgb(A, R, G, B));

        /// <summary>
        /// The Pen from RGBA color.
        /// </summary>
        /// <param name="red">Red.</param>
        /// <param name="green">Green.</param>
        /// <param name="blue">Blue.</param>
        /// <param name="alpha">Alpha.</param>
        /// <param name="size"></param>
        /// <returns>The Pen from given RGBA color.</returns>
        public static Pen PenRGBColor(int red, int green, int blue, int alpha, float size) => new (Color.FromArgb(alpha, red, green, blue), size);

        /// <summary>
        /// The Pen from HEX color.
        /// </summary>
        /// <param name="colorWithoutHash">HEX Color without hash.</param>
        /// <param name="size">The size of the pen.</param>
        /// <returns></returns>
        public static Pen PenHTMlColor(string colorWithoutHash, float size = 1) => new (HexColor(colorWithoutHash), size);

        /// <summary>
        /// Gets Color based on given hex color string.
        /// </summary>
        /// <param name="hexColor">Hex Color</param>
        /// <returns>The Color based on given hex color string</returns>
        public static Color HexColor(string hexColor) => ColorTranslator.FromHtml(hexColor);

        /// <summary>
        /// Gets Color based on given hex color string.
        /// </summary>
        /// <param name="hexColor">Hex Color</param>
        /// <returns>The Color based on given hex color string</returns>
        public static Color HexColor(object hexString) => HexColor((string)hexString);

        /// <summary>
        /// The Color from HEX by alpha property.
        /// </summary>
        /// <param name="alpha">Alpha.</param>
        /// <param name="hexColor">HEX Color with hash.</param>
        /// <returns>The Color from HEX with given ammount of transparency</returns>
        public static Color GetAlphaHexColor(int alpha, string hexColor) => Color.FromArgb(alpha, ColorTranslator.FromHtml(hexColor));

        // Check and create handle of control
        // Credits :
        //     control invalidate does not trigger the paint event of hidden or invisible control
        //     see https://stackoverflow.com/questions/38137654
        //     force create handle
        //     see https://stackoverflow.com/questions/1807921/
        /// <summary>
        /// Initialize the Handle of Control and child controls if their handle were not created
        /// </summary>
        public static void InitControlHandle(Control ctrl)
		{
			if (ctrl.IsHandleCreated)
				return;
            _ = ctrl.Handle;
            foreach (Control child in ctrl.Controls)
				InitControlHandle(child);
		}

		/// <summary>
		/// Setting smoothness for hand type cursor especially while hovering controls.
		/// </summary>
		/// <param name="message">Windows message api.</param>
		public static void SmoothCursor(ref Message message)
		{
			if (message.Msg != User32.WM_SETCURSOR)
				return;
			User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
			message.Result = IntPtr.Zero;
		}

		/// <summary>
		/// Get a constrasting color based on the specified color.
		/// </summary>
		/// <param name="color">The colore to calculate a contrasting color.</param>
		/// <returns>A <see cref="Color"/> structure containing a contrasting color.</returns>
        public static Color GetContrastingColor(this Color color)
        {
            double luminance = 0.2126 * color.R / 255F + 0.7152 * color.G / 255F + 0.0722 * color.B / 255F;
            return luminance > 0.5 ? Color.Black : Color.White;
        }

        /// <summary>
        /// Returns a <see cref="Color"/> structure with the luminance adjustment specified.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to be ‘modified’.</param>
        /// <param name="percent">The percentage of luminance to apply.</param>
        /// <returns>A <see cref="Color"/> structure with the luminance adjustment specified.</returns>
        public static Color AdjustLuminance(this Color color, int percent)
        {
            color.ToHSL(out float h, out float s, out float l);
            l += (float)percent / 100;
            l = Math.Min(Math.Max(l, 0), 1);
            return ColorFromHSLA(h, s, l, color.A);
        }

		/// <summary>
		/// Convert the hue, saturation, and luminance into a <see cref="Color"/>, with the specified amount of alpha.
		/// </summary>
		/// <param name="h">The Hue of the color.</param>
		/// <param name="s">The Saturation of the color.</param>
		/// <param name="l">The Luminance of the color.</param>
		/// <param name="a">The transparency of the color.</param>
		/// <returns>A <see cref="Color"/> structure that is equivalent of the HSL value, with the specified amound of alpha.</returns>
        public static Color ColorFromHSLA(float h, float s, float l, byte a)
        {
            float c = (1 - Math.Abs(2 * l - 1)) * s;
            float x = c * (1 - Math.Abs(h / 60 % 2 - 1));
            float m = l - c / 2;
            float r, g, b;
            if (h < 60) { r = c; g = x; b = 0; }
            else if (h < 120) { r = x; g = c; b = 0; }
            else if (h < 180) { r = 0; g = c; b = x; }
            else if (h < 240) { r = 0; g = x; b = c; }
            else if (h < 300) { r = x; g = 0; b = c; }
            else { r = c; g = 0; b = x; }
            return Color.FromArgb(a, (int)((r + m) * 255), (int)((g + m) * 255), (int)((b + m) * 255));
        }

        /// <summary>
        /// Convert a specifed color int Hue, Saturation, and Luminance.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> structure to convert.</param>
        /// <param name="h">OUT. The Hue of the color.</param>
        /// <param name="s">OUT. The Saturation of the color.</param>
        /// <param name="l">OUT. The Luminance of the color.</param>
        public static void ToHSL(this Color color, out float h, out float s, out float l)
        {
            float rf = color.R / 255f;
            float gf = color.G / 255f;
            float bf = color.B / 255f;

            float max = Math.Max(Math.Max(rf, gf), bf);
            float min = Math.Min(Math.Min(rf, gf), bf);

            h = 0;
            l = (max + min) / 2;

            if (max == min)
            {
                h = 0;
                s = 0;
            }
            else
            {
                float d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min);

                if (max == rf)
                    h = (gf - bf) / d + (gf < bf ? 6 : 0);
                else if (max == gf)
                    h = (bf - rf) / d + 2;
                else if (max == bf)
                    h = (rf - gf) / d + 4;

                h /= 6;
            }
        }
    }
}