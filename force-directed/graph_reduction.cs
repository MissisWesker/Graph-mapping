using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    public class Reducer
    {
        private graph g, ng;
        private Dictionary<int,int> new_graph;
        private int[,] weight;
        private static Random rand = new Random(0);
        private List<int> nodes;

        public Reducer(graph g)
        {
            this.g = g;
            new_graph = new Dictionary<int,int>(g.N);
        }

        public graph Reduce()
        {
            int current, next, n = 0, k = 0;
            nodes = new List<int>(g.N);
            for (int i = 0; i < g.N; i++)
                nodes.Add(i);
            
            do
            {
                int r = rand.Next(nodes.Count);
                current = nodes[r];
                new_graph[current] = n;
                k++;
                nodes.RemoveAt(nodes.IndexOf(current));
                next = GetNeighbour(current);
                if (next != -1)
                {
                    new_graph[next] = n;
                    nodes.RemoveAt(nodes.IndexOf(next));
                    k++;
                }
                n++;
            }
            while (k != g.N);

            weight = new int[n, n];
            graph.creator gc = new graph.creator(n);
            for (int i = 0; i < g.N; i++)
            {
                foreach (int j in g.Adj(i))
                {
                    if ((new_graph[j] != new_graph[i]) && (j > i))
                    {
                        if (weight[new_graph[i], new_graph[j]] == 0)
                            gc.AddEdge(new_graph[i], new_graph[j]);
                        weight[new_graph[i], new_graph[j]]++;
                        weight[new_graph[j], new_graph[i]]++;
                    }
                }
            }
            ng = gc.create();
            return ng;
        }

        private int GetNeighbour(int node)
        {
            foreach (int j in g.Adj(node))
                if (nodes.Contains(j))
                    return j;
            return -1;
        }
    }
}
