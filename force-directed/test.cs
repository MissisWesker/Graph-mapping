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
            for (int i = 0; i < 500; i++)
            {
                Console.Write("\ngforce:{0:f3}", FD.GlobalForce());
                FD.Iterate();
            }
            drawer.Paint(g, FD.x, FD.y, 500, 500, "test_first_graph.png");

            Reducer d = new Reducer(g);
            solution s = d.Reduce();
            FD = new ForceDirected(s.result);
            for (int i = 0; i < 500; i++)
            {
                Console.Write("\ngforce:{0:f3}", FD.GlobalForce());
                FD.Iterate();
            }
            drawer.Paint(s.result, FD.x, FD.y, 500, 500, "test_second_graph.png");

            d = new Reducer(s.result);
            solution s2 = d.Reduce();
            FD = new ForceDirected(s2.result);
            for (int i = 0; i < 500; i++)
            {
                Console.Write("\ngforce:{0:f3}", FD.GlobalForce());
                FD.Iterate();
            }
            drawer.Paint(s2.result, FD.x, FD.y, 500, 500, "test_third_graph.png");

            Console.ReadKey();
        }

        private static graph generate_graph()
        {
            graph.creator gc = new graph.creator(9);
            gc.AddEdge(0, 1);
            gc.AddEdge(0, 2);
            gc.AddEdge(1, 3);
            gc.AddEdge(1, 8);
            gc.AddEdge(2, 3);
            gc.AddEdge(2, 4);
            gc.AddEdge(3, 5);
            gc.AddEdge(4, 5);
            gc.AddEdge(4, 6);
            gc.AddEdge(5, 6);
            gc.AddEdge(5, 7);
            gc.AddEdge(5, 8);
            gc.AddEdge(6, 7);
            gc.AddEdge(7, 8);
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