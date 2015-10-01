using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    public class Matrix
    {
        private int[,] A;
        private int size;

        public int this[int i, int j] { get { return A[i, j]; } set { A[i, j] = value; } }

        public Matrix(graph g)
        {
            size = g.N;
            A = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    A[i, j] = 0;
            for (int k = 0; k < size; k++)
                foreach (int node in g.Adj(k))
                    A[k, node] = 1;
        }

        public int S
        {
            get { return size; }
        }
    }
}
