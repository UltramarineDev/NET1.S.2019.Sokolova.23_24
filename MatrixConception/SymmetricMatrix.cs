using System;
using System.Collections.Generic;

namespace MatrixConception
{
    public class SymmetricMatrix<T> : SquareMatrix<T>
    {
        public SymmetricMatrix(T[,] values) : this(values, null, values.GetLength(0)) { }

        public SymmetricMatrix(int dimension) : this(null, null, dimension) { }

        public SymmetricMatrix(T[,] values, IComparer<T> comparer, int dimension) : base(values, comparer, dimension)
        { }

        protected override void CheckValues(T[,] values)
        {
            base.CheckValues(values);
            if (!IsSymmetric(values))
            {
                throw new ArgumentException("Invalid input values.");
            }
        }

        private bool IsSymmetric(T[,] entries)
        {
            for (int i = 0; i < entries.GetLength(0); i++)
            {
                for (int j = 0; j < entries.GetLength(1); j++)
                {
                    if (comparer.Compare(entries[i, j], entries[j, i]) != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
