using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace force_directed
{
    public class DistanceMatrix : Matrix
    {
        // matrix initialization based on adjacency matrix
        public DistanceMatrix(graph gh): base(gh)
        {
            int s = this.S;
            int M = s * s;

            for (int i = 0; i < s; i++)
                for (int j = 0; j < s; j++)
                {
                    if (this[i, j] != 1)
                        this[i, j] = M;
                    this[i, i] = 0;
                }
            floid();
        }

        // compute distances
        private void floid()
        {
            for (int i = 0; i < this.S; i++)
                for (int j = 0; j < this.S; j++)
                    for (int k = 0; k < this.S; k++)
                        if (this[j, k] > this[j, i] + this[i, k])
                            this[j, k] = this[j, i] + this[i, k];
        }


    }
}
