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
    }
}