using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    public class graph
    {
        private int[] adjacency;
        private int[] index;

        public graph(int[] index, int[] adjacency)
        {
            this.adjacency = adjacency;
            this.index = index;
        }

        // node count
        public int N
        {
            get { return index.Length - 1; }
        }

        // edge count
        public int M
        {
            get { return index[index.Length - 1] / 2; }
        }

        // node's degree
        public int Degree(int node)
        {
            return index[node + 1] - index[node];
        }

        // list of adjacency nodes
        public IEnumerable<int> Adj(int node)
        {
            for (int i = index[node]; i < index[node + 1]; i++)
                yield return adjacency[i];
        }

        // graph creator
        public class creator
        {
            private List<int>[] adj;

            public creator(int N)
            {
                adj = new List<int>[N];
                for (int i = 0; i < N; i++)
                    adj[i] = new List<int>();
            }

            public void AddEdge(int i, int j)
            {
                adj[i].Add(j);
                adj[j].Add(i);
            }

            public graph create()
            {
                int N = adj.Length;
                int[] index = new int[N + 1];
                int M = 0;
                foreach (var i in adj)
                    M += i.Count;
                int[] adjacency = new int[M];
                M = 0;
                index[0] = 0;
                for (int node = 0; node < N; node++)
                {
                    foreach (int j in adj[node])
                    {
                        adjacency[M] = j;
                        M++;
                    }
                    index[node + 1] = M;
                }
                return new graph(index, adjacency);
            }
        }
    }
}
