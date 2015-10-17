using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    class ForceDirected
    {
        private double[] dx, dy;
        private double h = 0.01;
        private graph g;
        private solution sln;
        private point[] pos;

        public static Random rand = new Random(0);

        public ForceDirected(graph g, point[] p)
        {
            this.g = g;
            pos = p;
            dx = new double[g.N];
            dy = new double[g.N];
            for (int i = 0; i < g.N; i++)
            {
                dx[i] = 0;
                dy[i] = 0;
                pos[i].x = p[i].x;
                pos[i].y = p[i].y;
            }
        }

        public ForceDirected(solution sln)
        {
            this.sln = sln;
            int n = sln.source.N;
            pos = new point[n];
            dx = new double[n];
            dy = new double[n];
            for (int i = 0; i < n; i++)
            {
                dx[i] = 0;
                dy[i] = 0;
            }
        }

        //private void GetInitialSolution()
        //{
        //    for (int i = 0; i < x.Length; i++)
        //    {
        //        x[i] = rand.NextDouble();
        //        y[i] = rand.NextDouble();
        //    }
        //}

        public point[] Iterate(int iterations)
        {
            for (int k = 0; k < iterations; k++)
            {
                for (int j = 0; j < g.N; j++)
                {
                    dx[j] = 0;
                    dy[j] = 0;
                }

                for (int i = 0; i < g.N; i++)
                {
                    foreach (int j in g.Adj(i))
                    {
                        double dist = Math.Sqrt((pos[i].x - pos[j].x) * (pos[i].x - pos[j].x) + (pos[i].y - pos[j].y) * (pos[i].y - pos[j].y));
                        dx[i] += (pos[j].x - pos[i].x) * dist * h;
                        dy[i] += (pos[j].y - pos[i].y) * dist * h;
                    }
                }

                for (int i = 0; i < g.N; i++)
                {
                    for (int j = 0; j < g.N; j++)
                    {
                        double dist = (pos[i].x - pos[j].x) * (pos[i].x - pos[j].x) + (pos[i].y - pos[j].y) * (pos[i].y - pos[j].y);
                        double force = 1.0 / (dist + 0.01);
                        dx[i] -= (pos[j].x - pos[i].x) * force * h;
                        dy[i] -= (pos[j].y - pos[i].y) * force * h;
                    }
                }

                for (int i = 0; i < g.N; i++)
                {
                    pos[i].x = pos[i].x + dx[i];
                    pos[i].y = pos[i].y + dy[i];
                }
                Console.Write("\ngforce:{0:f3}", GlobalForce(g));
            }
            return pos;
        }

        public point[] GetSolution(point[] p)
        {
            List<int> cluster_node = new List<int>(2);

            for (int k = 0; k < sln.result.N; k++)
            {
                cluster_node.Clear();
                foreach (var match in sln.map.Where(s => s.Value == k))
                {
                    cluster_node.Add(match.Key);
                    pos[match.Key].x = p[k].x;
                    pos[match.Key].y = p[k].y;
                }
            }

            for (int k = 0; k < sln.result.N; k++)
            {
                cluster_node.Clear();
                foreach (var match in sln.map.Where(s => s.Value == k))
                    cluster_node.Add(match.Key);

                for (int s = 0; s < 100; s++)
                {
                    for (int t = 0; t < cluster_node.Count; t++)
                    {
                        int i = cluster_node[t];
                        dx[i] = 0;
                        dy[i] = 0;
                        foreach (int j in sln.source.Adj(i))
                        {
                            double dist = Math.Sqrt((pos[i].x - pos[j].x) * (pos[i].x - pos[j].x) + (pos[i].y - pos[j].y) * (pos[i].y - pos[j].y));
                            dx[i] += (pos[j].x - pos[i].x) * dist * h;
                            dy[i] += (pos[j].y - pos[i].y) * dist * h;
                        }

                        for (int j = 0; j < sln.source.N; j++)
                        {
                            double dist = (pos[i].x - pos[j].x) * (pos[i].x - pos[j].x) + (pos[i].y - pos[j].y) * (pos[i].y - pos[j].y);
                            double force = 1.0 / (dist + 0.01);
                            dx[i] -= (pos[j].x - pos[i].x) * force * h;
                            dy[i] -= (pos[j].y - pos[i].y) * force * h;
                        }

                        pos[i].x = pos[i].x + dx[i];
                        pos[i].y = pos[i].y + dy[i];
                    }

                    Console.Write("\ngforce:{0:f3}", GlobalForce(sln.source));

                    if (s % 50 == 0)
                    {
                        double[] x = new double[sln.source.N];
                        double[] y = new double[sln.source.N];

                        for (int j = 0; j < sln.source.N; j++)
                        {
                            x[j] = pos[j].x;
                            y[j] = pos[j].y;
                        }
                        drawer.Paint(sln.source, x, y, 500, 500, string.Format("test_{1}_{0}.png", s / 50, k));
                    }
                }
            }
            return pos;
        }

        public double GlobalForce(graph g)
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
