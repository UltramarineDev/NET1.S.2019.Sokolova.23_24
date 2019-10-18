using System;
using System.Collections.Generic;

namespace MatrixConception
{
    public class SquareMatrix<T>
    {
        protected T[,] matrix;
        private int dimension;

        protected readonly IComparer<T> comparer;

        public SquareMatrix(T[,] values) : this(values, null, values.GetLength(0)) { }

        public SquareMatrix(int dimension) : this(null, null, dimension) { }

        public SquareMatrix(T[,] values, IComparer<T> comparer, int dimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentException("Dimension can ot be less or equals zero.", nameof(dimension));
            }

            this.dimension = dimension;

            if (comparer == null)
            {
                if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                {
                    this.comparer = Comparer<T>.Default;
                }

                if (this.comparer == null)
                {
                    throw new InvalidOperationException("Default comparer is not found.");
                }
            }
            
            CreateMatrix();

            if (values != null)
            {
                CheckValues(values);
                SetValues(values);
            }
        }

        public T this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckEntries(rowIndex, columnIndex);
                return GetValue(rowIndex, columnIndex);
            }
            set
            {
                CheckEntries(rowIndex, columnIndex);
                SetValue(rowIndex, columnIndex, value);
            }
        }

        protected virtual void CheckValues(T[,] values)
        {
            if (values.GetLength(0) != values.GetLength(1))
            {
                throw new ArgumentException("Invalid input values.");
            }
        }

        protected virtual void SetValues(T[,] values)
        {
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    matrix[i, j] = values[i, j];
                }
            }
        }

        public virtual T GetValue(int rowIndex, int columnIndex)
        => matrix[rowIndex, columnIndex];

        protected virtual void SetValue(int rowIndex, int columnIndex, T value)
        {
            CheckEntries(rowIndex, columnIndex);
            matrix[rowIndex, columnIndex] = value;
        }

        protected void CheckEntries(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= dimension)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.", nameof(rowIndex));
            }

            if (columnIndex < 0 || columnIndex >= dimension)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.", nameof(columnIndex));
            }
        }

        protected bool IsMatrixExists()
        {
            return matrix == null ? false : true;
        }

        protected void CreateMatrix()
        {
            matrix = new T[dimension, dimension];
        }

        public T[,] GetMatrix()
        {
            return IsMatrixExists() ? matrix : throw new ArgumentNullException("Matrix is not exists.");
        }
    }
}
