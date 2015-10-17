using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    public struct solution
    {
        public graph source;
        public graph result;
        public Dictionary<int, int> map;
    }

    public struct point
    {
        public double x;
        public double y;
    }

    public class Reducer
    {
        private solution sln;
        private static Random rand = new Random(0);
        private List<int> nodes;

        public Reducer(graph g)
        {
            sln.source = g;
            sln.map = new Dictionary<int, int>(g.N);
        }

        public solution Reduce()
        {
            int current, next, n = 0, k = 0;
            nodes = new List<int>(sln.source.N);
            for (int i = 0; i < sln.source.N; i++)
                nodes.Add(i);
            
            do
            {
                int r = rand.Next(nodes.Count);
                current = nodes[r];
                sln.map[current] = n;
                k++;
                nodes.RemoveAt(nodes.IndexOf(current));
                next = GetNeighbour(current);
                if (next != -1)
                {
                    sln.map[next] = n;
                    nodes.RemoveAt(nodes.IndexOf(next));
                    k++;
                }
                n++;
            }
            while (k != sln.source.N);

            int[,] weight = new int[n, n];
            graph.creator gc = new graph.creator(n);
            for (int i = 0; i < sln.source.N; i++)
            {
                foreach (int j in sln.source.Adj(i))
                {
                    if ((sln.map[j] != sln.map[i]) && (j > i))
                    {
                        if (weight[sln.map[i], sln.map[j]] == 0)
                            gc.AddEdge(sln.map[i], sln.map[j]);
                        weight[sln.map[i], sln.map[j]]++;
                        weight[sln.map[j], sln.map[i]]++;
                    }
                }
            }
            sln.result = gc.create();
            return sln;
        }

        private int GetNeighbour(int node)
        {
            foreach (int j in sln.source.Adj(node))
                if (nodes.Contains(j))
                    return j;
            return -1;
        }
    }
}
