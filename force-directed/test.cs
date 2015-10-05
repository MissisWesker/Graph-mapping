using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    class test
    {
        static void Main()
        {
            graph g = generate_graph();
            ForceDirected FD = new ForceDirected(g);

            for (int i = 0; i < 1500; i++)
            {
                FD.Iterate();
            }
            Console.ReadKey();
        }

        private static graph generate_graph()
        {
            graph.creator gc = new graph.creator(5);
            gc.AddEdge(0, 1);
            gc.AddEdge(0, 2);
            gc.AddEdge(1, 3);
            gc.AddEdge(2, 3);
            gc.AddEdge(2, 4);
            gc.AddEdge(3, 4);
            graph g = gc.create();
            return g;
        }

        private static graph generate_grid(int n, int m)
        {
            graph.creator gc = new graph.creator(n * m);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    int num = i * m + j;
                    if (j < m - 1) gc.AddEdge(num, num + 1);
                    if (i < n - 1) gc.AddEdge(num, num + m);
                }
            return gc.create();
        }
    }
}