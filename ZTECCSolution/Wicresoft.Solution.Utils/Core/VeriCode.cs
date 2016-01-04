﻿using System;
using System.Drawing;

namespace Wicresoft.Solution.Utils
{
    /// <summary>
    ///     验证码
    /// </summary>
    public class VeriCode : BaseVeriCode
    {
        private static char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        /// <summary>
        ///     算术运算验证
        /// </summary>
        /// <param name="fontSize">字体长度</param>
        /// <param name="imgWidth">图片长度</param>
        /// <param name="imgHeiht">图片高度</param>
        /// <param name="stainLenght">噪音点长度</param>
        public static Bitmap Arithmetic(out string expression, int fontSize = 11, int imgWidth = 65,
                                      int imgHeiht = 23, int stainLenght = 10)
        {
            expression = string.Empty;
            var rnd = new Random();
            // 生成3个10以内的整数，用来运算 
            var op1 = rnd.Next(0, 20);
            var op2 = rnd.Next(0, 50);
            var result = 0;


            while (op1 > op2)
            {
                op1 = rnd.Next(0, 20);
                op2 = rnd.Next(0, 10);
            }

            // 随机组合运算顺序，只做 + 运算 
            switch (rnd.Next(0, 2))
            {
                case 0:
                    {
                        result = op1 + op2;
                        expression = string.Format("{0} + {1} = ?", op1, op2);
                        break;
                    }
                case 1:
                    {
                        result = op2 - op1;
                        expression = string.Format("{0} + ? = {1}", op1, op2);
                        break;
                    }
                case 2:
                    {
                        result = op2 - op1;
                        expression = string.Format("? + {0} = {1}", op1, op2);
                        break;
                    }
            }

            return ResponseImage(expression, Color.White, fontSize, imgWidth, imgHeiht, stainLenght);
        }

        /// <summary>
        /// 数字
        /// </summary>
        /// <param name="fontSize">字体长度</param>
        /// <param name="imgWidth">图片长度</param>
        /// <param name="imgHeiht">图片高度</param>
        /// <param name="stainLenght">噪音点长度</param>
        /// <param name="codeLenght">随机码长度</param>
        public static Bitmap Alphabet(out string code, int codeLenght = 4, int fontSize = 22,
                                    int imgWidth = 80, int imgHeiht = 40, int stainLenght = 30)
        {
            //code = string.Empty;
            //Random rd = new Random();
            //while (string.IsNullOrWhiteSpace(code) || code.Length < codeLenght)
            //{
            //    code += constant[rd.Next(10)];
            //}
            code = Guid.NewGuid().ToString().Substring(0, 4);
            return ResponseImage(code, Color.White, fontSize, imgWidth, imgHeiht, stainLenght);
        }
    }
    /// <summary>
    ///     验证码
    /// </summary>
    public abstract class BaseVeriCode
    {
        private static readonly string[] FontItems = new[] { "Arial" }; //, "Helvetica", "Geneva", "sans-serif", "Verdana"

        private static readonly Brush[] BrushItems = new[]
                                                         {
                                                             Brushes.LightSlateGray, Brushes.ForestGreen,
                                                             Brushes.OliveDrab
                                                         };

        /// <summary>
        /// 随机类
        /// </summary>
        protected static Random random;
        /// <summary>
        /// 画笔索引
        /// </summary>
        protected static int brushNameIndex;

        /// <summary>
        ///     取一个字体的样式
        /// </summary>
        /// <returns></returns>
        protected static FontStyle GetFontStyle()
        {
            switch (DateTime.Now.Second % 2)
            {
                case 0:
                    {
                        return FontStyle.Regular | FontStyle.Bold;
                    }
                case 1:
                    {
                        return FontStyle.Italic | FontStyle.Bold;
                    }
                default:
                    {
                        return FontStyle.Regular | FontStyle.Bold;
                    }
            }
        }

        /// <summary>
        ///     随机取一个笔刷
        /// </summary>
        /// <returns></returns>
        protected static Brush GetBrush()
        {
            brushNameIndex = random.Next(0, BrushItems.Length);
            return BrushItems[brushNameIndex];
        }

        /// <summary>
        ///     随机取一个字体
        /// </summary>
        /// <returns></returns>
        protected static Font GetFont(int fontSize)
        {
            var fontIndex = random.Next(0, FontItems.Length);
            return new Font(FontItems[fontIndex], fontSize, GetFontStyle());
        }

        /// <summary>
        ///     绘画背景色
        /// </summary>
        /// <param name="g">Graphics对像</param>
        /// <param name="backColor">背景色</param>
        protected static void Paint_Background(Graphics g, Color backColor)
        {
            g.Clear(backColor);
        }

        /// <summary>
        ///     绘画边框
        /// </summary>
        /// <param name="g">Graphics对像</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="imgHeiht">边框高度</param>
        /// <param name="imgWidth">边框长度</param>
        protected static void Paint_Border(Graphics g, int imgWidth, int imgHeiht, Pen borderColor)
        {
            g.DrawRectangle(borderColor, 0, 0, imgWidth - 1, imgHeiht - 1);
        }

        /// <summary>
        ///     绘画文字
        /// </summary>
        /// <param name="g">Graphics对像</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="code">文字</param>
        protected static void Paint_Text(Graphics g, int fontSize, string code)
        {
            g.DrawString(code, GetFont(fontSize), GetBrush(), 1, 1);
        }

        /// <summary>
        ///     绘画噪音点
        /// </summary>
        /// <param name="b">Bitmap对像</param>
        /// <param name="imgHeiht">噪音点高度</param>
        /// <param name="imgWidth">长度</param>
        /// <param name="count">数量</param>
        protected static void Paint_Stain(Bitmap b, int imgWidth, int imgHeiht, int count)
        {
            for (var n = 0; n < count; n++)
            {
                var x = random.Next(imgWidth);
                var y = random.Next(imgHeiht);
                b.SetPixel(x, y, Color.FromName(BrushItems[brushNameIndex].ToString()));
            }
        }

        /// <summary>
        ///     正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        protected static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            var graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;

            for (var i = 0; i < destBmp.Width; i++)
            {
                for (var j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (12 * (double)j) / dBaseAxisLen : (12 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    var dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                        && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }

        /// <summary>
        ///     绘画事件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="code">随机字符</param>
        /// <param name="fontSize">字体长度</param>
        /// <param name="imgWidth">图片长度</param>
        /// <param name="imgHeiht">图片高度</param>
        /// <param name="stainLenght">噪音点长度</param>
        /// <param name="backColor">背景色</param>
        /// <param name="borderColor">边框色</param>
        /// <returns></returns>
        protected static Bitmap ResponseImage(string code, Color backColor, int fontSize = 12,
                                            int imgWidth = 60, int imgHeiht = 20, int stainLenght = 30,
                                            Pen borderColor = null)
        {
            if (borderColor == null)
            {
                borderColor = Pens.DarkGray;
            }

            random = new Random();

            var oBitmap = new Bitmap(imgWidth, imgHeiht);

            var g = Graphics.FromImage(oBitmap);

            Paint_Background(g, backColor);
            Paint_Text(g, fontSize, code);
            Paint_Stain(oBitmap, imgWidth, imgHeiht, stainLenght);
            Paint_Border(g, imgWidth, imgHeiht, borderColor);

            oBitmap = TwistImage(oBitmap, true, 0.5f, 0);

            g.Dispose();
            return oBitmap;
        }
    }
}
