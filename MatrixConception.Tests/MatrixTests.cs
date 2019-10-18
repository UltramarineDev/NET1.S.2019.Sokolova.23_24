using NUnit.Framework;

namespace MatrixConception.Tests
{
    public class MatrixTests
    {
        [Test]
        public void SquareMatrixGetMatrixMethod_CheckIfMatrixIsCreated_Matrix()
        {
            int[,] matrix = new int[5, 5];
            SquareMatrix<int> squareMatrix = new SquareMatrix<int>(5);
            Assert.AreEqual(matrix,squareMatrix.GetMatrix());
        }

        [Test]
        public void SymmentricMatrixGetMatrixMethod_CheckIfMatrixIsCreated_Matrix()
        {
            double[,] matrixDouble = new double[2, 2];
            matrixDouble[0, 0] = 1;
            matrixDouble[0, 1] = 0;
            matrixDouble[1, 0] = 0;
            matrixDouble[1, 1] = 1;

            SymmetricMatrix<double> symmetricMatrix = new SymmetricMatrix<double>(matrixDouble);
            Assert.AreEqual(matrixDouble, symmetricMatrix.GetMatrix());
        }
    }
}
