using System;
using System.Collections.Generic;

namespace MatrixConception
{
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(T[,] values) : this(values, null, values.GetLength(0)) { }

        public DiagonalMatrix(int dimension) : this(null, null, dimension) { }

        public DiagonalMatrix(T[,] values, IComparer<T> comparer, int dimension) : base(values, comparer, dimension)
        { }

        private bool IsDiagonal(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (comparer.Compare(matrix[i, j], default(T)) != 0 && i != j)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected override void CheckValues(T[,] values)
        {
            base.CheckValues(values);
            if (!IsDiagonal(values))
            {
                throw new ArgumentException("Invalid input values.");
            }
        }
    }
}
