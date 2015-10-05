using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace force_directed
{
    class drawer
    {
        public static void Paint(graph g, double[] x, double[] y, int width, int height, string fname)
        {
            double bx = x.Max();
            double ax = x.Min();
            double by = y.Max();
            double ay = y.Min();
            Bitmap bmp = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(bmp);
            int border = 10;
            
            width -= border * 2;
            height -= border * 2;
            double scale = Math.Min((double)width / (bx - ax), height / (by - ay));
            SolidBrush bnode = new SolidBrush(Color.White);
            Pen pedge = new Pen(Color.Red);
            gr.Clear(Color.Black);
            int node_rad = 2;
            
            for (int i = 0; i < g.N; i++)
            {
                int rxi = border + (int)(scale * (x[i] - ax));
                int ryi = border + (int)(scale * (y[i] - ay));
                foreach (var j in g.Adj(i))
                {
                    int rxj = border + (int)(scale * (x[j] - ax));
                    int ryj = border + (int)(scale * (y[j] - ay));
                    gr.DrawLine(pedge, rxi, ryi, rxj, ryj);
                }
            }
            for (int i = 0; i < g.N; i++)
            {
                int rx = border + (int)(scale * (x[i] - ax));
                int ry = border + (int)(scale * (y[i] - ay));
                gr.FillEllipse(bnode, rx - node_rad, ry - node_rad, node_rad * 2, node_rad * 2);
            }
            bmp.Save(fname);
        }
    }
}
