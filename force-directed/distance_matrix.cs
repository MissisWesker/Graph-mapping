using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    public class DistanceMatrix
    {
        private int[,] D;
        private int size;

        public int this[int i, int j] { get { return D[i, j]; } set { D[i, j] = value; } }

        // matrix initialization based on adjacency matrix
        public DistanceMatrix(graph g)
        {
            size = g.N;
            int M = size * size;
            D = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    D[i, j] = M;
            for (int i = 0; i < size; i++)
                D[i, i] = 0;
            for (int k = 0; k < size; k++)
                foreach (int node in g.Adj(k))
                    D[k, node] = 1;
            floid();
        }

        // distance computation
        void floid()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                        if (D[j, k] > D[j, i] + D[i, k])
                            D[j, k] = D[j, i] + D[i, k];
        }

        public int S
        {
            get { return size; }
        }
    }
}
