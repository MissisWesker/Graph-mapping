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
            graph.creator gc = new graph.creator(5);
            gc.AddEdge(0, 1);
            gc.AddEdge(0, 2);
            gc.AddEdge(1, 3);
            gc.AddEdge(2, 3);
            gc.AddEdge(2, 4);
            gc.AddEdge(3, 4);
            graph g = gc.create();

            ForceDirected FD = new ForceDirected(g);
            for (int i = 0; i < g.N; i++)
            {
                Console.WriteLine("node = " + i + "\t" + FD.x[i] + "\t" + FD.y[i]);
            }
            Console.WriteLine();
            FD.ComputeDisplacement(g);

            Console.ReadKey();
        }
    }
}