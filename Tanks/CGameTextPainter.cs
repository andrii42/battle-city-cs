using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CGameTextPainter
    {
        static Dictionary<char, Image> symbols = new Dictionary<char, Image>()
        {
            { '0', Properties.Resources._0},
            { '1', Properties.Resources._1},
            { '2', Properties.Resources._2},
            { '3', Properties.Resources._3},
            { '4', Properties.Resources._4},
            { '5', Properties.Resources._5},
            { '6', Properties.Resources._6},
            { '7', Properties.Resources._7},
            { '8', Properties.Resources._8},
            { '9', Properties.Resources._9},
            { 's', Properties.Resources.s},
            { 't', Properties.Resources.t},
            { 'a', Properties.Resources.a},
            { 'g', Properties.Resources.g},
            { 'e', Properties.Resources.e},
            //{ ' ', Properties.Resources.II}
        };

        public static void WriteCaption(Graphics g, int x, int y, string caption)
        {
            char[] chars = caption.ToCharArray();//= String.Format("{0,0:00}",number).ToCharArray();

            //Array.Reverse(chars);

            Image img;

            for (int i = 0; i < chars.Length; i++)
            {
                symbols.TryGetValue(chars[i], out img);

                if (img != null) g.DrawImage(img, x + 16 * i, y, 16, 16); //else g.DrawImage(Properties.Resources._, x + 16 * i, y, 16, 16);
            }
        }

    }
}
