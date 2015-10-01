using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    class ForceDirected
    {
        public double[] x, y;
        private DistanceMatrix distance;
        private double h = 0.01;

        public static Random rand = new Random();

        public ForceDirected(graph g)
        {
            x = new double[g.N];
            y = new double[g.N];
            distance = new DistanceMatrix(g);
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

        public void ComputeDisplacement(graph g)
        {
            double dx, dy;
            double dist, force;

            for (int i = 0; i < g.N; i++)
            {
                foreach (int j in g.Adj(i))
                {
                    dist = Math.Sqrt((x[i] - x[j]) * (x[i] - x[j]) + (y[i] - y[j]) * (y[i] - y[j]));
                    force = dist - 1;
                    dx = (x[j] - x[i]) * force * h;
                    dy = (y[j] - y[i]) * force * h;
                    x[i] = x[i] + dx;
                    y[i] = y[i] + dy;
                    for (int s = 0; s < g.N; s++)
                        Console.WriteLine("node = " + s + "\t" + x[s] + "\t" + y[s]);
                    Console.WriteLine();
                    //Console.WriteLine("i = " + i + "\t" + "j = " + j + "\t"+ x[i] + "\t" + y[i]);
                }
            }
        }
    }
}
