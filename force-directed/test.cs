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
            Reducer d = new Reducer(g);
            point[] position;
            Random rand = new Random(0);

            Stack<solution> stack = new Stack<solution>();
            do
            {
                solution s = d.Reduce();
                stack.Push(s);
                d = new Reducer(stack.Peek().result);
            }
            while (stack.Peek().result.N > 3);

            position = new point[stack.Peek().result.N];
            for (int i = 0; i < position.Length; i++)
            {
                position[i].x = rand.NextDouble();
                position[i].y = rand.NextDouble();
            }

            ForceDirected FD = new ForceDirected(stack.Peek().result, position);
            position = FD.Iterate(100);

            int size = stack.Count;
            for (int i = 0; i < size; i++)
            {
                FD = new ForceDirected(stack.Peek(), position);
                position = FD.PreciseSolution();
                stack.Pop();
            }

            double[] x = new double[g.N];
            double[] y = new double[g.N];
            for (int i = 0; i < g.N; i++)
            {
                x[i] = position[i].x;
                y[i] = position[i].y;
            }
            drawer.Paint(g, position, 500, 500, "result_graph.png");
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