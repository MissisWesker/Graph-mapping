using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    class ForceDirected
    {
        public double[] x, y;
        public double[] dx, dy;
        private double h = 0.01;
        private graph g;

        public static Random rand = new Random();

        public ForceDirected(graph g)
        {
            x = new double[g.N];
            y = new double[g.N];
            dx = new double[g.N];
            dy = new double[g.N];
            this.g = g;
            GetInitialSolution();
        }

        private void GetInitialSolution()
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = rand.NextDouble();
                y[i] = rand.NextDouble();
            }
        }

        public void Iterate()
        {
            for (int j = 0; j < g.N; j++)
                dx[j] = dy[j] = 0;

            for (int i = 0; i < g.N; i++)
            {
                foreach (int j in g.Adj(i))
                {
                    double dist = Math.Sqrt((x[i] - x[j]) * (x[i] - x[j]) + (y[i] - y[j]) * (y[i] - y[j]));
                    dx[i] += (x[j] - x[i]) * dist * h;
                    dy[i] += (y[j] - y[i]) * dist * h;
                }
            }

            for (int i = 0; i < g.N; i++)
            {
                for (int j = 0; j < g.N; j++)
                {
                    double dist = (x[i] - x[j]) * (x[i] - x[j]) + (y[i] - y[j]) * (y[i] - y[j]);
                    double force = 1.0 / (dist + 0.01);
                    dx[i] -= (x[j] - x[i]) * force * h;
                    dy[i] -= (y[j] - y[i]) * force * h;
                }
            }

            for (int i = 0; i < g.N; i++)
            {
                x[i] = x[i] + dx[i];
                y[i] = y[i] + dy[i];
            }
        }

        public double GlobalForce()
        {
            double res = 0.0;
            for (int i = 0; i < g.N; i++)
            {
                for (int j = 0; j < g.N; j++)
                {
                    double dist = Math.Sqrt(dx[i] * dx[i] + dy[i] * dy[i]);
                    res += dist;
                }
            }
            return res;
        }
    }
}
